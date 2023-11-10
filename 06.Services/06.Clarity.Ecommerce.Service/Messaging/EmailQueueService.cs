// <copyright file="EmailQueueService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Models;
    using ServiceStack;

    [Authenticate]
    public partial class GetEmailQueues
    {
    }

    [Authenticate]
    public partial class GetEmailQueueByID
    {
    }

    [Authenticate]
    public partial class GetEmailQueueByKey
    {
    }

    [Authenticate]
    [Route("/Messaging/EmailQueue/AddEmailToQueue", "POST", Summary = "Use to create an email queue")]
    public class AddEmailToQueue : EmailQueueModel, IReturn<EmailQueueModel>
    {
    }

    [Authenticate]
    [Route("/Messaging/EmailQueue/DequeueEmail/ID/{ID}", "POST", Summary = "Use to create an email queue")]
    public class DequeueEmail : ImplementsIDBase, IReturn<bool>
    {
    }

    [Authenticate]
    [Route("/Messaging/EmailQueue/DequeueEmail/Key/{Key}", "POST", Summary = "Use to create an email queue")]
    public class DequeueEmailByKey : ImplementsKeyBase, IReturn<bool>
    {
    }

    public partial class EmailQueueService
    {
        public async Task<object?> Post(AddEmailToQueue request)
        {
            return await Workflows.EmailQueues.AddEmailToQueueAsync(request, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(DequeueEmail request)
        {
            return await Workflows.EmailQueues.DequeueEmailAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(DequeueEmailByKey request)
        {
            return await Workflows.EmailQueues.DequeueEmailAsync(request.Key, null).ConfigureAwait(false);
        }
    }
}
