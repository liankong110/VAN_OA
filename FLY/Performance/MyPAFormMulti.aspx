<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPAFormMulti.aspx.cs"
    Inherits="VAN_OA.Performance.MyPAFormMulti" MasterPageFile="~/DefaultMaster.Master"
    Title="��Ч����ģ�����" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                ��Ч���˽��
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                ����</td>
            <td class="style1">
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
            <td class="style1">
                ����</td>
            <td>
                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
            </td>
            <td>
                �����·�</td>
            <td>
                <asp:Label ID="lblMonth" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                ��������</td>
            <td class="style1">
                <asp:Label ID="lblAttendDays" runat="server"></asp:Label> 
            </td>
            <td class="style1">
                ����</td>
            <td>
                <asp:Label ID="lblLeaveDays" runat="server"></asp:Label> 
            </td>
            <td>
                ȫ�ڽ�</td>
            <td>
                <asp:Label ID="lblFullAttendBonus" runat="server"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="6">
                &nbsp;
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="PAItemId" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvList_RowDataBound" ShowFooter="True">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    ѡ��
                                </td>
                                <td>
                                    ��Ŀ
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    ��ֵ
                                </td>
                                <td>
                                    ���ͽ��
                                </td>
                                <td>
                                    �Ƿ����
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    �Ƿ���
                                </td>
                                <td>
                                    ������
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="loginName" HeaderText="������" />
                        <asp:BoundField DataField="ReviewScore" HeaderText="����ֵ" />
                        <asp:BoundField DataField="Note" HeaderText="ע��" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
        }
    </style>
</asp:Content>
