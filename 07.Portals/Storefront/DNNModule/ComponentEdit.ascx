<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComponentEdit.ascx.cs" Inherits="Clarity.Ecommerce.DNN.Extensions.ComponentEdit" %>
<%-- ReSharper disable Html.PathError --%>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%-- ReSharper restore Html.PathError --%>
<div class="dnnForm dnnEditBasicSettings" id="dnnEditBasicSettings">
    <asp:Panel ID="pnlComponentDefinitionSelect" runat="server">
        <h3 id="dnnSitePanel-BasicSettings" class="dnnClear"><%=LocalizeString("ComponentDefinitionSelectHead")%></h3>
        <fieldset>
            <div class="dnnFormItem">
                <dnn:label ID="lblComponentDefinitionSelect" runat="server" />
                <asp:DropDownList ID="ddlComponentDefinitionSelect" runat="server" />
            </div>
        </fieldset>
        <asp:LinkButton ID="btnComponentDefinitionSelectSubmit" runat="server"
                        OnClick="btnComponentDefinitionSelectSubmit_Click"
                        resourcekey="btnComponentDefinitionSelectSubmit"
                        CssClass="dnnPrimaryAction" />
        <asp:LinkButton ID="btnComponentDefinitionSelectCancel" runat="server"
                        OnClick="btnComponentDefinitionSelectCancel_Click"
                        resourcekey="btnComponentDefinitionSelectCancel"
                        CssClass="dnnSecondaryAction" />
    </asp:Panel>
    <asp:Panel ID="pnlComponentParameters" runat="server">
        <h3><%= ComponentDefinition.FriendlyName %></h3>
        <h4>Parameters:</h4>
        <asp:Panel ID="pnlComponentParametersEditor" runat="server">
            <fieldset>
                <asp:Repeater ID="rptParameters" runat="server" OnItemDataBound="rptParameters_ItemDataBound">
                    <ItemTemplate>
                        <div class="dnnFormItem">
                            <asp:HiddenField ID="hfParameter" runat="server" />
                            <dnn:label ID="lblParameter" runat="server" />
                            <asp:TextBox ID="tbParameter" runat="server" />
                            (<asp:Label ID="lblParameterType" runat="server" />)
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </fieldset>
            <asp:LinkButton ID="btnComponentDefinitionParametersSubmit" runat="server"
                            OnClick="btnComponentDefinitionParametersSubmit_Click"
                            resourcekey="btnComponentDefinitionParametersSubmit"
                            CssClass="dnnPrimaryAction" />
            <asp:LinkButton ID="btnComponentDefinitionParametersCancel" runat="server"
                            OnClick="btnComponentDefinitionParametersCancel_Click"
                            resourcekey="btnComponentDefinitionParametersCancel"
                            CssClass="dnnSecondaryAction" />
        </asp:Panel>
        <asp:Panel ID="pnlComponentParametersNone" runat="server">
            <div class="dnnFormMessage dnnFormInformation">This component does not have any editable parameters.</div>
            <asp:LinkButton ID="btnComponentDefinitionParametersClose" runat="server"
                            OnClick="btnComponentDefinitionParametersCancel_Click"
                            resourcekey="btnComponentDefinitionParametersCancel"
                            CssClass="dnnSecondaryAction" />
        </asp:Panel>
    </asp:Panel>
</div>
