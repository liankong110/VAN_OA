<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFGuestProBaseInfo.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFGuestProBaseInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="客户类型列表" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                客户属性
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                客户属性 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlGuestPro" runat="server">
                <asp:ListItem Value="0" Text="公司资源"></asp:ListItem>
                <asp:ListItem Value="1" Text="自我开拓"></asp:ListItem>
                 <asp:ListItem Value="2" Text="原有资源"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                激励系数 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPayXiShu" runat="server" Width="200px"></asp:TextBox>（0---1000 小数2位）
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                系数可变 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlXiShu" runat="server">
                <asp:ListItem Value="1" Text="可变"></asp:ListItem>
                <asp:ListItem Value="0" Text="不变"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
