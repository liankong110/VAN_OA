<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSellFPReport.aspx.cs"
    Inherits="VAN_OA.JXC.WFSellFPReport" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="���ⷢƱ�嵥" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ���ⷢƱ�嵥
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ���:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                �ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE:
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                ȱ��Ʊ����:
            </td>
            <td>
                <asp:DropDownList ID="ddlDiffDate" Width="200px" runat="server">
                    <asp:ListItem Value="0">����</asp:ListItem>
                    <asp:ListItem Value="1">30</asp:ListItem>
                    <asp:ListItem Value="5">����30</asp:ListItem>
                    <asp:ListItem Value="2">60</asp:ListItem>
                    <asp:ListItem Value="3">90</asp:ListItem>
                    <asp:ListItem Value="4">����90��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ����״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlJiaoFu" runat="server" Width="200px">
                    <asp:ListItem Value="0">����</asp:ListItem>
                    <asp:ListItem Value="1">�ѽ���</asp:ListItem>
                    <asp:ListItem Value="2">δȫ����</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                ��Ʊ״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlFPState" runat="server" Width="200px">
                    <asp:ListItem Value="0">����</asp:ListItem>
                    <asp:ListItem Value="1">�ѿ�ȫƱ</asp:ListItem>
                    <asp:ListItem Value="2">δ��ȫƱ</asp:ListItem>
                    <asp:ListItem Value="3">δ��Ʊ</asp:ListItem>
                    <asp:ListItem Value="4">δ��Ʊ+δ��ȫƱ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ����ʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtPOTimeFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPOTimeTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPOTimeFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPOTimeTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                &nbsp;�ͻ����ͣ�
            </td>
            <td>
                <asp:DropDownList ID="ddlGuestType" runat="server">
                    <asp:ListItem Value="0">ȫ��</asp:ListItem>
                    <asp:ListItem Value="1">����������</asp:ListItem>
                    <asp:ListItem Value="2">������</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                ����ʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtOutFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtOutTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtOutFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton4" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtOutTo">
                </cc1:CalendarExtender>
            </td>
            <td colspan="2">
                <asp:CheckBox ID="cbNoClear" runat="server" Checked="true" Text="δ����" />
                <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" Text="��������" />
                <asp:CheckBox ID="cbIsPoFax" runat="server" Text="��˰" AutoPostBack="True" OnCheckedChanged="cbIsPoFax_CheckedChanged"
                    Checked="True" />
                <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="Id" DataTextField="FpType"> 
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:CheckBox ID="cbPOWCG" runat="server" AutoPostBack="true" Text="��Ŀδ�ɹ�" OnCheckedChanged="cbPOWCG_CheckedChanged" />
                <asp:CheckBox ID="cbCGWC" runat="server" AutoPostBack="true" Text="�ɹ�δ����" OnCheckedChanged="cbCGWC_CheckedChanged" />
                <asp:CheckBox ID="cbNumZero" runat="server" AutoPostBack="true" Text="����Ϊ0" OnCheckedChanged="cbNumZero_CheckedChanged" />
                ��˾���ƣ�
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>��Ŀ���ƣ� <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>         
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="����EXCEL" BackColor="Yellow" OnClick="Button1_Click" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            Width="160%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound">
            <PagerTemplate>
                <br />
                <%--<asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="First"></asp:LinkButton>
                <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                <br />--%>
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            ��Ŀ���
                        </td>
                        <td>
                            AE
                        </td>
                        <td>
                            �ͻ�����
                        </td>
                        <td>
                            ��Ʒ���
                        </td>
                        <td>
                            ����
                        </td>
                        <td>
                            ���
                        </td>
                        <td>
                            ����
                        </td>
                        <td>
                            �ɹ�����
                        </td>
                        <td>
                            ���۵���
                        </td>
                        <td>
                            ���ⵥ��
                        </td>
                        <td>
                            ����ʱ��
                        </td>
                        <td>
                            ��Ʊ���
                        </td>
                        <td>
                            ��Ŀ���
                        </td>
                        <td>
                            ��Ʊ���
                        </td>
                        <td>
                            ȱ��Ʊ����
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
                <asp:BoundField DataField="PONo" HeaderText="��Ŀ���" SortExpression="PONo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="5%" />
                <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="5%" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                    ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="GoodAreaNumber" HeaderText="��λ" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
                <%--<asp:BoundField DataField="GoodId" HeaderText="GoodId" SortExpression="GoodId" ItemStyle-HorizontalAlign="Center"  /> --%>
                <asp:BoundField DataField="GoodNo" HeaderText="��Ʒ���" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodName" HeaderText="����" SortExpression="GoodName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="totalNum" HeaderText="��������" SortExpression="totalNum"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SellInNums" HeaderText="��������" SortExpression="SellInNums"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="avgLastPrice" HeaderText="������" SortExpression="avgLastPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" />
                  <asp:BoundField DataField="TotalAvgPrice" HeaderText="����ɱ��ܼ�" SortExpression="TotalAvgPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" />
                <asp:BoundField DataField="OutProNo" HeaderText="���ⵥ��" SortExpression="OutProNo"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="avgSellPrice" HeaderText="���۵���" SortExpression="avgSellPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" />
                <asp:BoundField DataField="GoodSellPrice" HeaderText="���ⵥ��" SortExpression="GoodSellPrice"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="RuTime" HeaderText="����ʱ��" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="PODate" HeaderText="����ʱ��" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="SellOutTotal" HeaderText="�����ܼ�" SortExpression="SellOutTotal"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="hadFpTotal" HeaderText="��Ʊ���" SortExpression="hadFpTotal"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="diffDate" HeaderText="ȱƱ����" SortExpression="diffDate"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FPTotal" HeaderText="��Ʊ���" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="20%" ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
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
    </asp:Panel>
    <asp:Label ID="Label1" runat="server" Text="��˰��Ŀ���ϼ�:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblHSTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Text="��Ʊ���ϼ�:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblKPTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label5" runat="server" Text="δ��Ʊ���ϼ�:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblWHPTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label7" runat="server" Text="����˰��Ŀ���:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblNoHSTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

     <asp:Label ID="Label2" runat="server" Text="����ɱ��ϼƼ�:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblAvgPriceTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
