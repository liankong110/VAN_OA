<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_RoleUserCmd.aspx.cs"
    Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.A_RoleUserCmd" MasterPageFile="~/DefaultMaster.Master"
    Title="审批用户管理" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                审批用户信息
            </td>
        </tr>
        <tr>
            <td>
                角色名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlRoles" runat="server" DataTextField="A_RoleName" DataValueField="A_RoleId"
                    Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                用户名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName_ID" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="true" Text="选择全部用户" OnCheckedChanged="cbAll_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
                排序
            </td>
            <td>
                <asp:TextBox ID="txtRole_User_Index" runat="server" Width="50px" Text="0"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlRowState" runat="server" Width="200px">
                    <asp:ListItem Text="有效" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无效" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" 重置 " BackColor="Yellow" OnClick="btnSet_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
