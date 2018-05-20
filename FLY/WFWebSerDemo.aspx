<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFWebSerDemo.aspx.cs" Inherits="VAN_OA.WFWebSerDemo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
            ServiceMethod="GetUserName" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
            TargetControlID="TextBox1">
        </cc1:AutoCompleteExtender>
    </div>
    </form>
</body>
</html>
