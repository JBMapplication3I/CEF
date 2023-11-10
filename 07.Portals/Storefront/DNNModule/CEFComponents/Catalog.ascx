<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Catalog.ascx.cs" Inherits="Clarity.Ecommerce.DNN.Extensions.CEFComponents.Catalog" %>
<!-- The view will be populated by the angular UI Router engine states -->
<div id="main" role="main" ui-view="main"></div>
<% if (IncludeRecentlyViewedProductsAttributeValue) { %>
<cef-recently-viewed-products
    size="<%= RecentlyViewedProductsSizeAttributeValue %>">
</cef-recently-viewed-products>
<% } %>
<% if (IncludePersonalizationProductsAttributeValue) { %>
<cef-personalization-products
    size="<%= PersonalizationProductsSizeAttributeValue %>">
</cef-personalization-products>
<% } %>
