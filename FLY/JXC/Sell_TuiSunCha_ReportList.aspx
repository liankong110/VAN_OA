<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sell_TuiSunCha_ReportList.aspx.cs"
    Inherits="VAN_OA.JXC.Sell_TuiSunCha_ReportList" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="�����˻���ϸ����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                �����˻���ϸ����
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
        OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand" ShowFooter="true">
        <PagerTemplate>
            <br />
            <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
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
                        ���ⵥ��
                    </td>
                    <td>
                        ��������
                    </td>
                    <td>
                        �ͻ�����
                    </td>
                    <td>
                        ��������
                    </td>
                    <td>
                        ����
                    </td>
                    <td>
                        ��������
                    </td>
                    <td>
                        ���۶�
                    </td>
                    <td>
                        ���۳ɱ�
                    </td>
                    <td>
                        �ܳɱ�
                    </td>
                    <td>
                        �˻�����
                    </td>
                    <td>
                        �˻����
                    </td>
                    <td>
                        ë�����
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
            <asp:BoundField DataField="PONo" HeaderText="��Ŀ���" SortExpression="PONo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ProNo" HeaderText="����" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="RuTime" HeaderText="����" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Supplier" HeaderText="�ͻ�����" SortExpression="Supplier"
                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                <HeaderStyle Width="100px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="goodInfo" HeaderText="��������" SortExpression="goodInfo"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GoodNum" HeaderText="����" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GoodSellPrice" HeaderText="��������" SortExpression="GoodSellPrice"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="���۶�">
                <ItemTemplate>
                    <asp:Label ID="goodSellTotal" runat="server" Text='<%# Eval("goodSellTotal") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="goodSellTotal" runat="server" Text='<%# Eval("goodSellTotal") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GoodPrice" HeaderText="���۳ɱ�" SortExpression="GoodPrice"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="�ܳɱ�">
                <ItemTemplate>
                    <asp:Label ID="goodTotal" runat="server" Text='<%# Eval("goodTotal") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="goodTotal" runat="server" Text='<%# Eval("goodTotal") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="t_GoodNums" HeaderText="�ɱ�ȷ�ϼ�" SortExpression="t_GoodNums"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="��ʧ���">
                <ItemTemplate>
                    <asp:Label ID="t_GoodTotalChas" runat="server" Text='<%# Eval("t_GoodTotalChas") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="t_GoodTotalChas" runat="server" Text='<%# Eval("t_GoodTotalChas") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ë�����">
                <ItemTemplate>
                    <asp:Label ID="maoli" runat="server" Text='<%# Eval("maoli") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="maoli" runat="server" Text='<%# Eval("maoli") %>'></asp:Label>
                </FooterTemplate>
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
