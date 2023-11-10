// <copyright file="ComponentViewModel.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the component view model class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.Services.ViewModels
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ComponentViewModel
    {
        ////public ItemViewModel(Item t)
        ////{
        ////    Id = t.ItemId;
        ////    Name = t.ItemName;
        ////    Description = t.ItemDescription;
        ////    AssignedUser = t.AssignedUserId;
        ////}

        ////public ItemViewModel(Item t, string editUrl)
        ////{
        ////    Id = t.ItemId;
        ////    Name = t.ItemName;
        ////    Description = t.ItemDescription;
        ////    EditUrl = editUrl;
        ////}

        ////public ItemViewModel() { }

        ////[JsonProperty("id")]
        ////public int Id { get; set; }

        ////[JsonProperty("name")]
        ////public string Name { get; set; }

        ////[JsonProperty("description")]
        ////public string Description { get; set; }

        ////[JsonProperty("assignedUser")]
        ////public int AssignedUser { get; set; }

        ////[JsonProperty("editUrl")]
        ////public string EditUrl { get; }
    }
}
