// <copyright file="DataSerializers.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the data serializers class</summary>
namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    /// <summary>A data serializers.</summary>
    public static class DataSerializers
    {
        /// <summary>Initializes static members of the Microsoft.Owin.Security.DataHandler.Serializer.DataSerializers
        /// class.</summary>
        static DataSerializers()
        {
            Properties = new PropertiesSerializer();
            Ticket = new TicketSerializer();
        }

        /// <summary>Gets or sets the properties.</summary>
        /// <value>The properties.</value>
        public static IDataSerializer<AuthenticationProperties> Properties
        {
            get;
            set;
        }

        /// <summary>Gets or sets the ticket.</summary>
        /// <value>The ticket.</value>
        public static IDataSerializer<AuthenticationTicket> Ticket
        {
            get;
            set;
        }
    }
}
