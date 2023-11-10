<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Clarity.Ecommerce.Scheduler.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CEF Scheduler</title>
</head>
<body>
<p>Need to redirect into the dashboard, please wait...</p>
<div id="countdown"></div>
<script>
    var after = 3;
    function countItDown() {
        after--;
        var el = document.getElementById("countdown");
        /*console.log(el);*/
        el.innerHTML = after.toString();
        if (after <= 0) {
            window.location.href = "<%=CefScheduleRoot%>";
            return;
        }
        setTimeout(countItDown, 1000);
    }
    countItDown();
</script>
</body>
</html>
