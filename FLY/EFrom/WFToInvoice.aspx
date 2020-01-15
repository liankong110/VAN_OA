<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFToInvoice.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.WFToInvoice" MasterPageFile="~/DefaultMaster.Master"
    Title="���" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <script type="text/javascript">
        function show() {
            alert("1");
            document.getElementById("btnSub").disabled = false;

        }
    </script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                ���<asp:Label ID="lblDelete" runat="server" Text="ɾ��" ForeColor="Red" Visible="false"/>-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlStyle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="ʵ�ʷ�Ʊ����"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Ԥ����"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                �����ˣ�<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                ��Ŀ���ƣ�
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="btnFp" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioSell_OrderFP.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton2_Click">ѡ��</asp:LinkButton>
                <asp:LinkButton ID="btnYuFu" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioOrderToInvoice.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" Visible="false" OnClick="btnYuFu_Click">ѡ��</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                �������ڣ�
            </td>
            <td>
                <asp:TextBox ID="txtDaoKuanDate" runat="server" Width="200px" OnTextChanged="txtDaoKuanDate_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtDaoKuanDate">
                </cc1:CalendarExtender>
            </td>
            <td>
                ��Ŀ���룺
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="�ܽ�" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                �� <font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Width="200px"></asp:TextBox>
                
            </td>
            <td>
                �ͻ����ƣ�
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ʵ������ϵ����
            </td>
            <td>
                <asp:TextBox ID="txtUpAccount" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                ���ڣ�
            </td>
            <td>
                <asp:TextBox ID="txtZhangQi" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ��Ʊ��:
            </td>
            <td>
                <asp:Label ID="lblFPNo" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblFPId" runat="server" Text="0" Visible="False"></asp:Label>
                
                  <asp:Label ID="Label1" runat="server" Text="��Ʊ���:" style="margin-left:20px;" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblInvoiceTotal" runat="server" Text=""  ForeColor="Red"></asp:Label>
            </td>
             <td>
                <asp:Label ID="LastPayTotal" runat="server" Text="ԭԤ����:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastPayTotal" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                ��ע��
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Height="100px" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
                    <asp:ListItem Selected="True">ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow" OnClick="Button1_Click" OnClientClick="return confirm('ȷ��Ҫ�ύ��')"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            ����
                        </td>
                        <td>
                            ���ݺ�
                        </td>
                        <td>
                            ������
                        </td>
                        <td>
                            ��������
                        </td>
                        <td>
                            ��������
                        </td>
                        <td>
                            ���
                        </td>
                        <td>
                            ��������ϵ��
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            �ͻ�����
                        </td>
                        <td>
                            ��ע
                        </td>
                        <td>
                            ״̬
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
                <asp:BoundField DataField="PONo" HeaderText="��Ŀ���" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PoName" HeaderText="��Ŀ����" SortExpression="PoName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="BusTypeStr" HeaderText="����" SortExpression="BusTypeStr"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DaoKuanDate" HeaderText="��������" SortExpression="DaoKuanDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Total" HeaderText="������" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FPNo" HeaderText="ԭ��Ʊ��" SortExpression="FPNo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-CssClass="item" />
                 <asp:BoundField DataField="NewFPNo" HeaderText="�·�Ʊ��" SortExpression="NewFPNo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle ForeColor="Black" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
