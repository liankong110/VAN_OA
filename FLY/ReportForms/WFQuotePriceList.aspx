<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFQuotePriceList.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFQuotePriceList" MasterPageFile="~/DefaultMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">

    <script type="text/javascript">
    function state() {
        
    
       
    }
    </script>

    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ���۵�����
            </td>
        </tr>
        <tr>
            <td>
                ��������:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                ������
            </td>
            <td>
                <asp:DropDownList ID="ddlGuestName" runat="server" DataTextField="GuestName" DataValueField="GuestName"
                    Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ���۵�:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtQPNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="���" BackColor="Yellow" OnClick="btnAdd_Click"
                        Width="98px" /></div>
            </td>
        </tr>
    </table>
    <br>
    <asp:Label ID="lblMess" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand">
        <PagerTemplate>
            <br />
            <%--   <asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        ����
                    </td>
                    <td>
                        �༭
                    </td>
                    <td>
                        ɾ��
                    </td>
                    <td>
                        ���۵�
                    </td>
                    <td>
                        ������
                    </td>
                    <td>
                        �򷽱���
                    </td>
                    <td>
                        ���۵�����
                    </td>
                    <td>
                        ���ʽ
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
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:ImageButton ID="btnPDF" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="PDF"
                        AlternateText="����" Visible="false" CommandArgument='<%# Eval("Id") %>' OnClientClick="state()" />
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="PDF" CommandArgument='<%# Eval("Id") %>'>����</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText=" �༭">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="�༭" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ɾ��">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"
                        CommandName="Delete" OnClientClick='return confirm( "ȷ��ɾ����") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="���۵�">
                <ItemTemplate>
                    <asp:Label ID="lblQuoteNo" runat="server" Text='<%# Eval("QuoteNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GuestName" HeaderText="������" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestNo" HeaderText="�򷽱���" SortExpression="GuestNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="QuoteDate" HeaderText="���۵�����" SortExpression="QuoteDate"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="PayStyle" HeaderText="���ʽ" SortExpression="PayStyle"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateUserName" HeaderText="������" SortExpression="CreateUserName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateTime" HeaderText="����ʱ��" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
        PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
        NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
