<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFIn_BankFlow.aspx.cs" Inherits="VAN_OA.Fin.WFIn_BankFlow"
    MasterPageFile="~/DefaultMaster.Master" Title="������ϸ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">������ϸ
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">������ˮ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblReferenceNumber" runat="server" Text="Label"></asp:Label>
                -���������ƣ�
                <asp:Label ID="lblOutPayerName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">�������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlInType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInType_SelectedIndexChanged">
                    <asp:ListItem>��Ʊ�ؿ�</asp:ListItem>
                    <asp:ListItem>�ͻ�Ԥ��</asp:ListItem>
                    <asp:ListItem>����˿�</asp:ListItem>
                    <asp:ListItem>��֤���˿�</asp:ListItem>
                    <asp:ListItem>����</asp:ListItem>
                    <asp:ListItem>������Ϣ</asp:ListItem>
                    <asp:ListItem>����</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">��Ʊ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txtInvoiceTotal" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                <asp:Button ID="btnTotal" runat="server" Text="��ȡ���" BackColor="Yellow" OnClick="btnTotal_Click" /></td>

        </tr>
        <tr>
            <td height="25" width="30%" align="right">���˽�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFPTotal" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">ע�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRemark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" OnClick="btnAdd_Click" OnClientClick="return save();" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" OnClick="btnUpdate_Click" OnClientClick="return save();" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>

    <asp:GridView ID="gv_In" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        ShowFooter="true" AllowPaging="False"
        PageSize="20">
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>������ˮ��
                    </td>
                    <td>��������
                    </td>
                    <td>��Ʊ����
                    </td>
                    <td>��Ʊ���
                    </td>
                    <td>ע��
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center" style="height: 80%">---��������---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>

            <asp:BoundField DataField="Number" HeaderText="���" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ReferenceNumber" HeaderText="������ˮ��" SortExpression="ReferenceNumber" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InType" HeaderText="��������" SortExpression="InType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="FPTotal" HeaderText="���˽��" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remark" HeaderText="ע��" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
    <script type="text/javascript">
        function save() {
            var InType = document.getElementById('<%= ddlInType.ClientID %>').value;
            var GuestName = document.getElementById('<%= txtGuestName.ClientID %>').value;        
            console.log(InType + "-" + GuestName);
            if (InType == "��Ʊ�ؿ�") {
            var outPayerName = document.getElementById('<%= lblOutPayerName.ClientID %>').innerText;
           
            console.log(outPayerName + "---" + GuestName);
            if (outPayerName != GuestName) {
                if (confirm("�����˺ͷ�Ʊ̧ͷ�в�һ��,�Ƿ��������")) {
                    return true;
                }
                else {
                    return false;
                }
            }
             
        }
            return true;
    }
    </script>
</asp:Content>
