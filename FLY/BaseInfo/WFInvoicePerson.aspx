<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFInvoicePerson.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFInvoicePerson" MasterPageFile="~/DefaultMaster.Master"
    Title="����Ʊ����Ա����" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">����Ʊ����Ա����
            </td>
        </tr>

        <tr>
            <td height="25" width="30%" align="right">��Ա���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">���֤�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCardNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">�ֻ��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">�Ƿ�ͣ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlIsStop" runat="server">
                    <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    <asp:ListItem Text="��" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
