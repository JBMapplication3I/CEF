<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.ascx.cs" Inherits="Clarity.Ecommerce.DNN.Extensions.CEFComponents.Product" %>
<asp:PlaceHolder id="CEFProductMetaServicePlaceholder" runat="server"></asp:PlaceHolder>
<link rel="stylesheet" ng-href="{{'/js/magiczoomplus.css' | corsLink: 'site'}}" />
<link rel="stylesheet" ng-href="{{'/js/magicscroll.css' | corsLink: 'site'}}" />
<cef-product-details></cef-product-details>
