// <copyright file="CoreController.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the core controller class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    using DotNetNuke.Entities.Modules;

    /// <summary>The Controller class for CEFSetup<br/>
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)<br/>
    /// DotNetNuke will poll this class to find out which Interfaces the class implements.<br/>
    /// The IPortable interface is used to import/export content from a DNN module<br/>
    /// The ISearchable interface is used by DNN to index the content of a module<br/>
    /// The IUpgradeable interface allows module developers to execute code during the upgrade process for a
    /// module.<br/>
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data<br/></summary>
    /// <seealso cref="IUpgradeable"/>
    public class CoreController : IUpgradeable ////: IPortable, ISearchable, IUpgradeable
    {
        /// <summary>UpgradeModule implements the IUpgradeable Interface.</summary>
        /// <param name="version">The current version of the module.</param>
        /// <returns>A string.</returns>
        public string UpgradeModule(string version)
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }
    }
}
