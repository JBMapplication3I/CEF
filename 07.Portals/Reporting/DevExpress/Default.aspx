<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Default.aspx.cs" Inherits="Clarity.Ecommerce.Service.Reporting._Default" %>
<%--<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>--%>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="padding-top: 6px;">
        <div class="row">
            <div class="col-sm-3">
                <asp:Button runat="server" Text="Create New Report" CssClass="btn btn-primary" ID="CreateNewReport" OnClick="OnCreateNewReportClick" />
<%--            <h4>Pre-Built Reports from Clarity</h4>
                <asp:ListView ID="ReportTypesDLLListView" runat="server" >
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("DisplayName") ?? Eval("Name") %></td>
                            <td><asp:Button data-report-name='<%# Eval("CustomKey") %>' CssClass="btn btn-primary btn-small" runat="server" OnClick="OnLoadReportTypeFromDLL" Text="Load" /></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="tbl2" runat="server" class="table col-xs-12">
                            <tr id="tr2" runat="server">
                                <td runat="server"></td>
                                <td runat="server" style="width: 20%"></td>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <h4>Reports saved to your database</h4>
                <asp:ListView ID="ReportTypesDBListView" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Value").ToString().Replace("Clarity.Ecommerce.Models.Reports.", "") %></td>
                            <td><asp:Button data-report-name='<%# Eval("Key") %>' CssClass="btn btn-primary" runat="server" OnClick="OnLoadReportTypeFromDB" Text="Load" /></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="tbl1" runat="server" class="table col-xs-12">
                            <tr id="tr1" runat="server">
                                <td runat="server"></td>
                                <td runat="server" style="width: 20%"></td>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>--%>
            </div>
            <div class="col-sm-9" style="background-color: #636363">
                <div class="row">
                    <%--<dx:ASPxReportDesigner ID="ASPxReportDesigner1" runat="server" CssClass="row" ColorScheme="dark"
                        SaveReportLayout="ASPxReportDesigner1_OnSaveReportLayout"></dx:ASPxReportDesigner>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
