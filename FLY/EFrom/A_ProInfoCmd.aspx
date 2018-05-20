<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="A_ProInfoCmd.aspx.cs"
    Inherits="VAN_OA.EFrom.A_ProInfoCmd" MasterPageFile="~/DefaultMaster.Master"
    Title="审批流程设置" %>

<asp:Content ContentPlaceHolderID="SampleContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                border="1">
                <tr>
                    <td colspan="3" style="height: 20px; background-color: #336699; color: White;">
                        审批流程设置
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 20px; font-size: 15px; color: Red">
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 60%" rowspan="2">
                        <asp:TreeView ID="tvMain" runat="server" OnSelectedNodeChanged="tvMain_SelectedNodeChanged">
                        </asp:TreeView>
                    </td>
                    <td style="vertical-align: top">
                        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" style="display:<%= Display %>;"
                            border="1">
                            <tr>
                                <td>
                                    上级审批角色名称：
                                </td>
                                <td>
                                    <asp:Label ID="lblParent" runat="server" Text="" Style="color: Red"></asp:Label>
                                </td>
                                <asp:Label ID="lblPareId" runat="server" Text="Label"></asp:Label>
                            </tr>
                            <tr>
                                <td>
                                    审批角色：
                                </td>
                                <td>
                                    &nbsp;<asp:DropDownList ID="ddlRoles" runat="server" DataTextField="A_RoleName" DataValueField="A_RoleId"
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAdd" runat="server" Text="添加" Width="65px" BackColor="Yellow"
                                        OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="修改" Width="65px" BackColor="Yellow"
                                        OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="删除" Width="65px" OnClientClick="return confirm( '确定删除吗？')"
                                        OnClick="btnDelete_Click" BackColor="Yellow" />
                                    <asp:Button ID="btnSave" runat="server" Text="保存" Width="65px" BackColor="Yellow"
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="取消" Width="65px" BackColor="Yellow"
                                        OnClick="btnCancel_Click" />
                                    <asp:Label ID="lblMess" runat="server" Text="" Style="color: Red"></asp:Label>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
