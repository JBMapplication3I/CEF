<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart.ascx.cs" Inherits="Clarity.Ecommerce.DNN.Extensions.CEFComponents.Cart" %>
<cef-cart
    type="<%= CartTypeAttributeValue %>"
    include-quick-order="<%= IncludeQuickOrderAttributeValue %>"
    include-discounts="<%= IncludeDiscountsAttributeValue %>">
</cef-cart>
