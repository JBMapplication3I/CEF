// <copyright file="TicketDataFormat.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ticket data format class</summary>
namespace Microsoft.Owin.Security.DataHandler
{
    using DataProtection;
    using Encoder;
    using Serializer;

    /// <summary>A ticket data format.</summary>
    /// <seealso cref="SecureDataFormat{AuthenticationTicket}"/>
    /// <seealso cref="SecureDataFormat{AuthenticationTicket}"/>
    public class TicketDataFormat : SecureDataFormat<AuthenticationTicket>
    {
        /// <summary>Initializes a new instance of the <see cref="TicketDataFormat" /> class.</summary>
        /// <param name="protector">The protector.</param>
        public TicketDataFormat(IDataProtector protector)
            : base(DataSerializers.Ticket, protector, TextEncodings.Base64Url)
        {
        }
    }
}
