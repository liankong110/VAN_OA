<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sell_TuiSunCha_ReportList.aspx.cs"
    Inherits="VAN_OA.JXC.Sell_TuiSunCha_ReportList" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="销售退货明细损差表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                销售退货明细损差表
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
        OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand" ShowFooter="true">
        <PagerTemplate>
            <br />
            <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
            <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                CommandName="Page" CommandArgument="First"></asp:LinkButton>
            <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
            <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                CommandName="Page" CommandArgument="Next"></asp:LinkButton>
            <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                CommandName="Page" CommandArgument="Last"></asp:LinkButton>
            <br />--%>
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        项目编号
                    </td>
                    <td>
                        出库单号
                    </td>
                    <td>
                        出货日期
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        销售内容
                    </td>
                    <td>
                        数量
                    </td>
                    <td>
                        出货单价
                    </td>
                    <td>
                        销售额
                    </td>
                    <td>
                        单价成本
                    </td>
                    <td>
                        总成本
                    </td>
                    <td>
                        退货数量
                    </td>
                    <td>
                        退货金额
                    </td>
                    <td>
                        毛利润额
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">
                        ---暂无数据---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ProNo" HeaderText="单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="RuTime" HeaderText="日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Supplier" HeaderText="客户名称" SortExpression="Supplier"
                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                <HeaderStyle Width="100px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="goodInfo" HeaderText="销售内容" SortExpression="goodInfo"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GoodNum" HeaderText="数量" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GoodSellPrice" HeaderText="出货单价" SortExpression="GoodSellPrice"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="销售额">
                <ItemTemplate>
                    <asp:Label ID="goodSellTotal" runat="server" Text='<%# Eval("goodSellTotal") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="goodSellTotal" runat="server" Text='<%# Eval("goodSellTotal") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GoodPrice" HeaderText="单价成本" SortExpression="GoodPrice"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="总成本">
                <ItemTemplate>
                    <asp:Label ID="goodTotal" runat="server" Text='<%# Eval("goodTotal") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="goodTotal" runat="server" Text='<%# Eval("goodTotal") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="t_GoodNums" HeaderText="成本确认价" SortExpression="t_GoodNums"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="损失差额">
                <ItemTemplate>
                    <asp:Label ID="t_GoodTotalChas" runat="server" Text='<%# Eval("t_GoodTotalChas") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="t_GoodTotalChas" runat="server" Text='<%# Eval("t_GoodTotalChas") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="毛利润额">
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
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
