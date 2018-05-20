<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VAN_OA.Login" %>

<%@ Register src="CopyRight.ascx" tagname="CopyRight" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>后台系统登录</title>
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" align="center">
        <tr style="height: 25px;">
            <td colspan="3" align="center">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Home.gif" />
            </td>
        </tr>
        <tr style="height: 23px;">
            <td dir="ltr" align="left">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr style="height: 23px;">
            <td dir="ltr" align="left">
                用户名：
            </td>
            <td align="left">
                <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 23px;">
            <td align="left">
                密&nbsp;&nbsp;&nbsp; 码：
            </td>
            <td align="left">
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="登 录" BackColor="Yellow" OnClick="btnSubmit_Click" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取 消" BackColor="Yellow" />
            </td>
        </tr>
        <tr style="height: 23px;">
            <td align="left" colspan="3">
                <uc1:CopyRight ID="CopyRight1" runat="server" />
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
