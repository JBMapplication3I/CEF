// <copyright file="MessageWithMessageAttachmentsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate message attachments workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;

    public partial class MessageWithMessageAttachmentsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task DeactivateObjectAdditionalPropertiesAsync(
            IMessageAttachment entity,
            DateTime timestamp)
        {
            if (entity.Slave != null)
            {
                entity.Slave.UpdatedDate = timestamp;
                entity.Slave.Active = false;
            }
            return Task.CompletedTask;
        }

        ////protected override IMessageAttachment ModelToNewObject(IMessageAttachmentModel model, DateTime timestamp, string? contextProfileName)
        ////{
        ////    Contract.RequiresNotNull(model);
        ////    //Contract.Requires<InvalidOperationException>((model.AttachmentID > 0) || !string.IsNullOrWhiteSpace(model.AttachmentName), "Must pass either the Attachment ID or Name to match against");
        ////    //
        ////    //var messageAttachment = RegistryLoaderWrapper.GetInstance<IMessageAttachment>(contextProfileName);
        ////    //var library = RegistryLoaderWrapper.GetInstance<ILibrary>(contextProfileName);
        ////    //var document = RegistryLoaderWrapper.GetInstance<IDocument>(contextProfileName);
        ////    //var file = RegistryLoaderWrapper.GetInstance<IFile>(contextProfileName);
        ////    var messageAttachment = model.CreateMessageAttachmentEntity(timestamp, timestamp);
        ////    //// Note: We're mirroring several properties on the MessageAttachment and the Attachment being created for ease of lookup
        ////    //// Assign the File to the Document
        ////    //document.Active = true;
        ////    //document.CustomKey = model.CustomKey ?? model.Name;
        ////    //document.CreatedDate = timestamp;
        ////    //document.File = (File)file;
        ////    //// Assign the Document to the Library
        ////    //library.Active = true;
        ////    //library.CustomKey = model.CustomKey ?? model.Name;
        ////    //library.CreatedDate = timestamp;
        ////    //library.Author = model.Library.Author;
        ////    //library.
        ////    //library.Document = (Document)document;
        ////    //// Assign the Attachment to the MessageAttachment
        ////    //messageAttachment.CustomKey = model.CustomKey = model.CustomKey;
        ////    //messageAttachment.Active = model.Active = true;
        ////    //messageAttachment.CreatedDate = model.CreatedDate = timestamp;
        ////    //messageAttachment.UpdatedDate = model.UpdatedDate = null;
        ////    //messageAttachment.Library = (Library)library;
        ////    // Return the new object
        ////    return messageAttachment;
        ////}
    }
}
