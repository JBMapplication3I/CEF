// <copyright file="OrderEventWebhookReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the order event webhook return class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>An order event webhook return.</summary>
    /*
    {
        "event_id": 180110237510,
        "order_id": 180110219852,
        "account_id": 170908017857,
        "user_id": 111,
        "type": 24,
        "entity_id": "180110237712",
        "description": "Payoneer Escrow received payment ",
        "created": "2018-01-10T23:02:07+00:00",
        "uri": "\/accounts\/170908017857\/orders\/180110219852\/events\/180110237510"
    }
    */
    [DataContract]
    public class OrderEventWebhookReturn
    {
        /// <summary>Gets or sets the identifier of the event.</summary>
        /// <value>The identifier of the event.</value>
        [JsonProperty("event_id")]
        [DataMember(Name = "event_id")]
        [ApiMember(Name = "event_id", DataType = "long", ParameterType = "body", IsRequired = false)]
        public long EventID { get; set; } // 180110237510

        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        [JsonProperty("order_id")]
        [DataMember(Name = "order_id")]
        [ApiMember(Name = "order_id", DataType = "long", ParameterType = "body", IsRequired = false)]
        public long OrderID { get; set; } // 180110219852

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id")]
        [DataMember(Name = "account_id")]
        [ApiMember(Name = "account_id", DataType = "long", ParameterType = "body", IsRequired = false)]
        public long AccountID { get; set; } // 170908017857

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [JsonProperty("user_id")]
        [DataMember(Name = "user_id")]
        [ApiMember(Name = "user_id", DataType = "long", ParameterType = "body", IsRequired = false)]
        public long UserID { get; set; } // 111

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        [DataMember(Name = "type")]
        [ApiMember(Name = "type", DataType = "int", ParameterType = "body", IsRequired = false)]
        public int Type { get; set; } // 24

        /// <summary>Gets or sets the identifier of the entity.</summary>
        /// <value>The identifier of the entity.</value>
        [JsonProperty("entity_id")]
        [DataMember(Name = "entity_id")]
        [ApiMember(Name = "entity_id", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? EntityID { get; set; } // "180110237712"

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
        [DataMember(Name = "description")]
        [ApiMember(Name = "description", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Description { get; set; } // "Payoneer Escrow received payment "

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created")]
        [DataMember(Name = "created")]
        [ApiMember(Name = "created", DataType = "DateTime", ParameterType = "body", IsRequired = false)]
        public DateTime Created { get; set; } // "2018-01-10T23:02:07+00:00"

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri")]
        [DataMember(Name = "uri")]
        [ApiMember(Name = "uri", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? URI { get; set; } // "\/accounts\/170908017857\/orders\/180110219852\/events\/180110237510"
    }
}
