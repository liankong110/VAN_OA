<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSell_OrderOutHouseBack.aspx.cs"
    Inherits="VAN_OA.JXC.WFSell_OrderOutHouseBack" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="���ⵥǩ��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 <script type="text/javascript">
        function ChuKu() {
            var url = "../JXC/DioSellOutOrderBackList.aspx";
            window.showModalDialog(url, null, 'dialogWidth:850px;dialogHeight:450px;help:no;status:no')
        }
</script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ���ⵥǩ��-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                �����ˣ�
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td>
                ����:
            </td>
            <td>
                <asp:TextBox ID="txtRuTime" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRuTime"
                    Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1">
                </cc1:CalendarExtender>  <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ���룺
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                ���ⵥ��:
            </td>
            <td>
                <asp:TextBox ID="txtSellProNo" runat="server" Width="200px"  Enabled="false"></asp:TextBox>
                <asp:LinkButton ID="lbtnAddFiles" runat="server" OnClientClick="ChuKu()" ForeColor="Red"
                    OnClick="LinkButton1_Click1">  <font style="color: Red">*</font>
      ѡ��</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ����:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
            <td>
                ǩ��״̬ ��
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:RadioButton ID="rdoA" runat="server" GroupName="a" Text="�յ�" />
                    <asp:RadioButton ID="rdoB" runat="server" GroupName="a" Text="δ�յ�" />
                    <font style="color: Red">*</font>
                </asp:Panel>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                �ͻ����ƣ�
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
                <asp:Label ID="lblTotal" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                ��ע��
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right" style="width: 100%;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                
                               
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    С��
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    �ͺ�
                                </td>
                                <td>
                                    ��λ
                                </td>
                                <td>
                                    ����
                                </td>
                                
                                <td>
                                    ���۵���
                                </td>
                                <td>
                                    ���۽��
                                </td>
                              
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" />                       
                        <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���۵���">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </ItemTemplate>                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�����ܼ�">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="3">
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
            <td colspan="3">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnPrint" runat="server" Text="��ӡ" BackColor="Yellow" Visible="false"
                    Width="51px" OnClick="btnPrint_Click" />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
