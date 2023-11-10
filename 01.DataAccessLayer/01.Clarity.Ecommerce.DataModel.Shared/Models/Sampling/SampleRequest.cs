// <copyright file="SampleRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the samples request class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISampleRequest
        : ISalesCollectionBase<SampleRequest,
                SampleRequestStatus,
                SampleRequestType,
                SampleRequestItem,
                AppliedSampleRequestDiscount,
                SampleRequestState,
                SampleRequestFile,
                SampleRequestContact,
                SampleRequestEvent,
                SampleRequestEventType>,
            IHaveNotesBase
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales group.</summary>
        /// <value>The identifier of the sales group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the group the sales belongs to.</summary>
        /// <value>The sales group.</value>
        SalesGroup? SalesGroup { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Sampling", "SampleRequest")]
    public class SampleRequest
        : SalesCollectionBase<SampleRequest,
              SampleRequestStatus,
              SampleRequestType,
              SampleRequestItem,
              AppliedSampleRequestDiscount,
              SampleRequestState,
              SampleRequestFile,
              SampleRequestContact,
              SampleRequestEvent,
              SampleRequestEventType>,
          ISampleRequest
    {
        private ICollection<Note>? notes;

        public SampleRequest()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroup { get; set; }
        #endregion
    }
}
