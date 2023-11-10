// <copyright file="ComponentEdit.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the component edit.ascx class</summary>
// ReSharper disable InconsistentNaming, LocalizableElement, StyleCop.SA1300, StyleCop.SA1305
namespace Clarity.Ecommerce.DNN.Extensions
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.UI.UserControls;

    public partial class ComponentEdit : ComponentModuleBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            try
            {
                if (Page.IsPostBack) { return; }
                if (ComponentInitialized)
                {
                    ShowComponentParameters();
                }
                else
                {
                    ShowComponentSelect();
                }
            }
            catch (Exception exc)
            {
                // Module failed to load
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void ShowComponentSelect()
        {
            pnlComponentDefinitionSelect.Visible = true;
            pnlComponentParameters.Visible = false;
            ddlComponentDefinitionSelect.DataSource = ComponentDefinitions/*.Where(c => !c.Name.StartsWith("_"))*/;
            ddlComponentDefinitionSelect.DataTextField = "FriendlyName";
            ddlComponentDefinitionSelect.DataValueField = "Name";
            ddlComponentDefinitionSelect.DataBind();
        }

        protected void ShowComponentParameters()
        {
            pnlComponentDefinitionSelect.Visible = false;
            pnlComponentParameters.Visible = true;
            var hasParameters = ComponentParameters?.Count > 0;
            pnlComponentParametersEditor.Visible = hasParameters;
            pnlComponentParametersNone.Visible = !hasParameters;
            if (!hasParameters) { return; }
            rptParameters.DataSource = ComponentParameters.Select(c => c.Value).OrderBy(p => p.Order);
            rptParameters.DataBind();
        }

        protected void rptParameters_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item
                && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }
            var parameter = (CEFComponentParameter)e.Item.DataItem;
            var hfParameter = (HiddenField)e.Item.FindControl("hfParameter");
            var lblParameter = (LabelControl)e.Item.FindControl("lblParameter");
            var tbParameter = (TextBox)e.Item.FindControl("tbParameter");
            var lblParameterType = (Label)e.Item.FindControl("lblParameterType");
            hfParameter.Value = parameter.Name;
            lblParameter.Text = parameter.FriendlyName;
            lblParameter.HelpText = parameter.Description;
            switch (parameter.Type)
            {
                case { } boolType when boolType == typeof(bool):
                {
                    tbParameter.Text = "true";
                    tbParameter.Attributes.Add("type", parameter.InputType);
                    if ((bool)parameter.Value)
                    {
                        tbParameter.Attributes.Add("checked", "checked");
                    }
                    break;
                }
                case { } intType when intType == typeof(int):
                {
                    tbParameter.Text = parameter.InputValue;
                    tbParameter.Attributes.Add("type", parameter.InputType);
                    tbParameter.Attributes.Add("step", "1");
                    break;
                }
                case { } longType when longType == typeof(long):
                {
                    tbParameter.Text = parameter.InputValue;
                    tbParameter.Attributes.Add("type", parameter.InputType);
                    tbParameter.Attributes.Add("step", "1");
                    break;
                }
                case { } doubleType when doubleType == typeof(double):
                {
                    tbParameter.Text = parameter.InputValue;
                    tbParameter.Attributes.Add("type", parameter.InputType);
                    break;
                }
                case { } decimalType when decimalType == typeof(decimal):
                {
                    tbParameter.Text = parameter.InputValue;
                    tbParameter.Attributes.Add("type", parameter.InputType);
                    break;
                }
                case { } dateType when dateType == typeof(DateTime):
                {
                    tbParameter.Text = parameter.InputValue;
                    tbParameter.Attributes.Add("type", parameter.InputType);
                    break;
                }
                default:
                {
                    tbParameter.Text = parameter.InputValue;
                    break;
                }
            }
            lblParameterType.Text = parameter.Type.Name;
        }

        protected void btnComponentDefinitionSelectSubmit_Click(object sender, EventArgs e)
        {
            var selectedComponent = ddlComponentDefinitionSelect.SelectedValue;
            if (!string.IsNullOrWhiteSpace(selectedComponent))
            {
                var componentDefinition = ComponentController.GetComponentDefinition(selectedComponent);
                ComponentController.SetModuleComponent(ModuleConfiguration, componentDefinition);
            }
            LoadComponent();
            ShowComponentParameters();
        }

        protected void btnComponentDefinitionSelectCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnComponentDefinitionParametersSubmit_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptParameters.Items)
            {
                if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem)
                {
                    continue;
                }
                var hfParameter = (HiddenField)item.FindControl("hfParameter");
                var tbParameter = (TextBox)item.FindControl("tbParameter");
                var key = hfParameter.Value;
                var value = tbParameter.Text;
                if (!ComponentParameters.ContainsKey(key))
                {
                    continue;
                }
                var parameter = ComponentParameters[key];
                if (parameter == null)
                {
                    continue;
                }
                switch (parameter.Type)
                {
                    case { } boolType when boolType == typeof(bool):
                    {
                        parameter.Value = bool.TryParse(value, out var boolValue) && boolValue;
                        break;
                    }
                    case { } intType when intType == typeof(int):
                    {
                        parameter.Value = int.TryParse(value, out var intValue) ? intValue : 0;
                        break;
                    }
                    case { } longType when longType == typeof(long):
                    {
                        parameter.Value = long.TryParse(value, out var longValue) ? longValue : 0;
                        break;
                    }
                    case { } doubleType when doubleType == typeof(double):
                    {
                        parameter.Value = double.TryParse(value, out var doubleValue) ? doubleValue : 0;
                        break;
                    }
                    case { } decimalType when decimalType == typeof(decimal):
                    {
                        parameter.Value = decimal.TryParse(value, out var decimalValue) ? decimalValue : 0;
                        break;
                    }
                    case { } dateType when dateType == typeof(DateTime):
                    {
                        parameter.Value = DateTime.TryParse(value, out var dateValue) ? dateValue : DateTime.MinValue;
                        break;
                    }
                    default:
                    {
                        parameter.Value = value;
                        break;
                    }
                }
            }
            ComponentController.UpdateModuleComponentParameters(ModuleConfiguration, ComponentParameters);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnComponentDefinitionParametersCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }
    }
}
