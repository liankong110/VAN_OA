<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSellFPReport.aspx.cs"
    Inherits="VAN_OA.JXC.WFSellFPReport" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="出库发票清单" %>

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
                出库发票清单
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                客户名称:
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
                缺开票天数:
            </td>
            <td>
                <asp:DropDownList ID="ddlDiffDate" Width="200px" runat="server">
                    <asp:ListItem Value="0">所有</asp:ListItem>
                    <asp:ListItem Value="1">30</asp:ListItem>
                    <asp:ListItem Value="5">超过30</asp:ListItem>
                    <asp:ListItem Value="2">60</asp:ListItem>
                    <asp:ListItem Value="3">90</asp:ListItem>
                    <asp:ListItem Value="4">超过90天</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                交付状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlJiaoFu" runat="server" Width="200px">
                    <asp:ListItem Value="0">所有</asp:ListItem>
                    <asp:ListItem Value="1">已交付</asp:ListItem>
                    <asp:ListItem Value="2">未全交付</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                发票状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlFPState" runat="server" Width="200px">
                    <asp:ListItem Value="0">所有</asp:ListItem>
                    <asp:ListItem Value="1">已开全票</asp:ListItem>
                    <asp:ListItem Value="2">未开全票</asp:ListItem>
                    <asp:ListItem Value="3">未开票</asp:ListItem>
                    <asp:ListItem Value="4">未开票+未开全票</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                订单时间:
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
                &nbsp;客户类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlGuestType" runat="server">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">不含本部门</asp:ListItem>
                    <asp:ListItem Value="2">本部门</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                出库时间:
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
                <asp:CheckBox ID="cbNoClear" runat="server" Checked="true" Text="未结清" />
                <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" Text="不含特殊" />
                <asp:CheckBox ID="cbIsPoFax" runat="server" Text="含税" AutoPostBack="True" OnCheckedChanged="cbIsPoFax_CheckedChanged"
                    Checked="True" />
                <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="Id" DataTextField="FpType"> 
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:CheckBox ID="cbPOWCG" runat="server" AutoPostBack="true" Text="项目未采购" OnCheckedChanged="cbPOWCG_CheckedChanged" />
                <asp:CheckBox ID="cbCGWC" runat="server" AutoPostBack="true" Text="采购未出库" OnCheckedChanged="cbCGWC_CheckedChanged" />
                <asp:CheckBox ID="cbNumZero" runat="server" AutoPostBack="true" Text="数量为0" OnCheckedChanged="cbNumZero_CheckedChanged" />
                公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>项目名称： <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>         
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="导出EXCEL" BackColor="Yellow" OnClick="Button1_Click" />
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
                <%--<asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                            AE
                        </td>
                        <td>
                            客户名称
                        </td>
                        <td>
                            商品编号
                        </td>
                        <td>
                            名称
                        </td>
                        <td>
                            规格
                        </td>
                        <td>
                            数量
                        </td>
                        <td>
                            采购均价
                        </td>
                        <td>
                            销售单价
                        </td>
                        <td>
                            出库单价
                        </td>
                        <td>
                            订单时间
                        </td>
                        <td>
                            发票编号
                        </td>
                        <td>
                            项目金额
                        </td>
                        <td>
                            开票金额
                        </td>
                        <td>
                            缺开票天数
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
                <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="5%" />
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="5%" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
                <%--<asp:BoundField DataField="GoodId" HeaderText="GoodId" SortExpression="GoodId" ItemStyle-HorizontalAlign="Center"  /> --%>
                <asp:BoundField DataField="GoodNo" HeaderText="商品编号" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="totalNum" HeaderText="销售数量" SortExpression="totalNum"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SellInNums" HeaderText="销退数量" SortExpression="SellInNums"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="avgLastPrice" HeaderText="库存均价" SortExpression="avgLastPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" />
                  <asp:BoundField DataField="TotalAvgPrice" HeaderText="出库成本总价" SortExpression="TotalAvgPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" />
                <asp:BoundField DataField="OutProNo" HeaderText="出库单号" SortExpression="OutProNo"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="avgSellPrice" HeaderText="销售单价" SortExpression="avgSellPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" />
                <asp:BoundField DataField="GoodSellPrice" HeaderText="出库单价" SortExpression="GoodSellPrice"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="RuTime" HeaderText="出库时间" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="PODate" HeaderText="订单时间" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="SellOutTotal" HeaderText="出库总价" SortExpression="SellOutTotal"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="hadFpTotal" HeaderText="开票金额" SortExpression="hadFpTotal"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="diffDate" HeaderText="缺票天数" SortExpression="diffDate"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FPTotal" HeaderText="发票编号" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Left"
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
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    <asp:Label ID="Label1" runat="server" Text="含税项目金额合计:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblHSTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Text="开票金额合计:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblKPTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label5" runat="server" Text="未开票金额合计:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblWHPTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label7" runat="server" Text="不含税项目金额:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblNoHSTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

     <asp:Label ID="Label2" runat="server" Text="出库成本合计价:" Style="color: Red;"></asp:Label>
    <asp:Label ID="lblAvgPriceTotal" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
