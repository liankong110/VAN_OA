<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="MyWFQuotePriceList.aspx.cs"
    Inherits="VAN_OA.ReportForms.MyWFQuotePriceList" MasterPageFile="~/DefaultMaster.Master"
    Title="���۵��б�" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
<script type="text/javascript">
    function checkAll() {
        if (document.getElementById('<%= cbAll.ClientID %>').checked == false) {
            document.getElementById('<%= ddlCompany.ClientID %>').value = "����������ϵͳ���޹�˾";
        }
        else {
            document.getElementById('<%= ddlCompany.ClientID %>').value = "ȫ��";
        }
          
    }
</script>
    <style>
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
            border-left: 1px solid #888;
            border-top: 1px solid #888;
        }
        th, td
        {
            border-right: 1px solid #888;
            border-bottom: 1px solid #888;
            padding: 1px 1px;
        }
        th
        {
            font-weight: bold;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ���۵��б�
            </td>
        </tr>
        <tr>
            <td>
                ����:
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
                �ͻ�����
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �ͻ���ϵ��:
            </td>
            <td>
                <asp:TextBox ID="txtLinkMan" runat="server"></asp:TextBox>
            </td>
            <td>
                ���۵���:
            </td>
            <td>
                <asp:TextBox ID="txtProno" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �ͻ���ַ:
            </td>
            <td>
                <asp:TextBox ID="txtGuestAddress" runat="server"></asp:TextBox>
            </td>
            <td>
                ��Ʒ����:
            </td>
            <td>
                <asp:TextBox ID="txtGoodName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ��Ʒ�ͺ�:
            </td>
            <td>
                <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
                 ����ժҪ: <asp:TextBox ID="txtZhaiYao" runat="server"></asp:TextBox>
            </td>
            <td>
                Ʒ��:
            </td>
            <td>
                <asp:TextBox ID="txtGoodBrand" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE:
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                ���:
                    <asp:DropDownList ID="ddlType" runat="server" Width="100PX">
                    <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="��˰" Value="1"></asp:ListItem>
                         <asp:ListItem Text="����˰" Value="2"></asp:ListItem>
                              <asp:ListItem Text="����" Value="3"></asp:ListItem>                  
                </asp:DropDownList>��˾:
                  <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComName"
                    Width="200PX">
                </asp:DropDownList>
                <asp:CheckBox ID="cbAll" Text="ȫ��" runat="server" onchange="checkAll()" />
            </td>
            <td colspan="2">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="���" BackColor="Yellow" OnClick="btnAdd_Click" /></div>
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
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        ����
                    </td>
                    <td>
                        ���۵���
                    </td>
                    <td>
                        ���۸�Ҫ
                    </td>
                    <td>
                        �ͻ�����
                    </td>
                    <td>
                        �ܼ�
                    </td>
                    <td>
                        AE
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
            <asp:TemplateField HeaderText=" �༭">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="�༭" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText=" ����">
                <ItemTemplate>
                    <asp:ImageButton ID="btnCopy" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Copy" CommandArgument='<%# Eval("Id") %>'
                        AlternateText="����" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ɾ��">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"
                        CommandName="Delete" OnClientClick='return confirm( "ȷ��ɾ����") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"/>
            </asp:TemplateField>
              <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="QuoteDate" HeaderText="����" SortExpression="QuoteDate"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                   <asp:BoundField DataField="QPTypeString" HeaderText=" ���" SortExpression="QPTypeString"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="���۵���">
                <ItemTemplate>
                    <asp:Label ID="lblQuoteNo" runat="server" Text='<%# Eval("QuoteNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Remark" HeaderText="���۸�Ҫ" SortExpression="Remark" ItemStyle-HorizontalAlign="Left"
                />
        <asp:BoundField DataField="AllName" HeaderText="��λ" SortExpression="AllName" ItemStyle-HorizontalAlign="Left"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="Total" HeaderText="�ܼ�" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateUserName" HeaderText="AE" SortExpression="CreateUserName"
                ItemStyle-HorizontalAlign="Center" />

                  <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:ImageButton ID="btnPDF" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="PDF"
                        AlternateText=" PDF" Visible="false" CommandArgument='<%# Eval("Id") %>' OnClientClick="state()" />
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="PDF" CommandArgument='<%# Eval("Id") %>'>PDF</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:ImageButton ID="btnWord" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Word"
                        AlternateText="Word" Visible="false" CommandArgument='<%# Eval("Id") %>' OnClientClick="state()" />
                    <asp:LinkButton ID="lbtnWord" runat="server" CommandName="Word" CommandArgument='<%# Eval("Id") %>'>Word</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
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
