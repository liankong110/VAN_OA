<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSupplierAdvancePaymentVerify.aspx.cs"
    Inherits="VAN_OA.JXC.WFSupplierAdvancePaymentVerify" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="����������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">��Ӧ��Ԥ���-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>�Ƶ��ˣ�
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            <td>����:
            </td>
            <td>
                <asp:TextBox ID="txtRuTime" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRuTime"
                    Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1">
                </cc1:CalendarExtender>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>��ע��
            </td>
            <td>
                <asp:TextBox ID="txtRemark" Width="100%" runat="server"></asp:TextBox>
            </td>
            <td colspan="2">ԭƱ�ݺţ�             
                  <asp:TextBox ID="txtFristFPNo" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                ��Ʊ�ݺ�:
                  <asp:TextBox ID="txtSecondFPNo" runat="server" Width="150px" Enabled="false"></asp:TextBox>

            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="Panel1" ScrollBars="Horizontal" Width="100%">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Ids" Width="115%" AllowPaging="False" AutoGenerateColumns="False"
            ShowFooter="true" OnRowDataBound="gvList_RowDataBound">
            <PagerTemplate>
                <br />
                <asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="First"></asp:LinkButton>
                <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>�ɹ�����
                        </td>
                        <td>��Ӧ��
                        </td>
                        <td>��Ŀ����
                        </td>
                        <td>��Ŀ����
                        </td>
                        <td>�ͻ�����
                        </td>
                        <td>AE
                        </td>
                        <td>����
                        </td>
                        <td>����
                        </td>
                        <td>С��
                        </td>
                        <td>���
                        </td>
                        <td>��λ
                        </td>
                        <td>�ɹ���
                        </td>
                        <td>ʵ�ɵ���
                        </td>
                        <td>���
                        </td>
                        <td>��Ʊ��
                        </td>
                        <td>��������
                        </td>
                        <td>������
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---��������---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ProNo" HeaderText="�ɹ�����" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="��Ӧ��" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PONo" HeaderText="��Ŀ���" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POGuestName" HeaderText="�ͻ�����" SortExpression="POGuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POAE" HeaderText="AE" SortExpression="POAE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodName" HeaderText="����" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNum" HeaderText="�ɹ���" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceNum" HeaderText="Ԥ������" SortExpression="SupplierInvoiceNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Ԥ������" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSupplierInvoiceNum" runat="server" Text='<%# Eval("SupplierInvoiceNum") %>' 
                            Width="70px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="lastGoodNum" HeaderText="������" SortExpression="lastGoodNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodPrice" HeaderText="ʵ�ɵ���" SortExpression="GoodPrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="���">
                    <ItemTemplate>
                        <asp:Label ID="lblLastTotal" runat="server" Text='<%# Eval("LastTotal") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblLastTotal" runat="server" Text='<%# Eval("LastTotal") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SupplierFPNo" HeaderText="��Ʊ��" SortExpression="SupplierFPNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="��������" SortExpression="SupplierInvoiceDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="Ԥ������" SortExpression="SupplierInvoicePrice"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="��Ʊ��">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSupplierFPNo" runat="server" Text='<%# Eval("SupplierFPNo") %>' 
                            Width="80px"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtSupplierFPNo" ValidationGroup="aa"></asp:RequiredFieldValidator>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��������">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSupplierInvoiceDate" runat="server" Text='<%# Eval("SupplierInvoiceDate","{0:yyyy-MM-dd}") %>'
                            Width="80px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSupplierInvoiceDate"
                            PopupButtonID="txtSupplierInvoiceDate" Format="yyyy-MM-dd">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            ControlToValidate="txtSupplierInvoiceDate" ValidationGroup="aa"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ԥ������">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSupplierInvoicePrice" runat="server" Text='<%# Eval("SupplierInvoicePrice") %>'
                            Width="80px"></asp:TextBox>
                        <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtSupplierInvoicePrice" ValidationGroup="aa"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtSupplierInvoicePrice" ValidationGroup="aa" ValidationExpression="^[0-9]+(.[0-9]{1,3})?$"></asp:RegularExpressionValidator>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--  
                                          <asp:BoundField DataField="SupplierInvoiceTotal" HeaderText="Ԥ�����" SortExpression="SupplierInvoiceTotal" DataFormatString="{0:n2}"
                            ItemStyle-HorizontalAlign="Center" />--%>
                <asp:TemplateField HeaderText="��֧��">
                    <ItemTemplate>
                        <a href="/JXC/WFSupplierInvoiceList.aspx?PayIds=<%# Eval("Ids") %>" target="_blank">
                            <%# Eval("HadSupplierInvoiceTotal")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ԥ�����">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplierInvoiceTotal" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblSupplierInvoiceTotal" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HuiKuanLiLv" HeaderText="�ؿ���" SortExpression="HuiKuanLiLv"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" HeaderStyle-Width="3%" />
                <asp:TemplateField HeaderText="��Ӧ��ȫ��" HeaderStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplierAllName" runat="server" Text='<%# Eval("SupplierAllName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��˰">
                    <ItemTemplate>
                        <asp:CheckBox ID="CBIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="PORemark" HeaderText="��ע" SortExpression="PORemark" HeaderStyle-Width="200" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
    </asp:Panel>


    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
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
            <td colspan="4" align="center">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow" OnClick="Button1_Click"
                    ValidationGroup="aa" Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
