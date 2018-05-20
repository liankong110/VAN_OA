<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FINProperty.aspx.cs" Inherits="VAN_OA.BaseInfo.FINProperty"
    MasterPageFile="~/DefaultMaster.Master" Title="费用类型" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                费用类型
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                流水号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtProNo" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                类型 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCostType" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                属性 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlPro" runat="server">
                    <asp:ListItem Text="公共" Value="公共"></asp:ListItem>
                    <asp:ListItem Text="个性" Value="个性"></asp:ListItem>
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
