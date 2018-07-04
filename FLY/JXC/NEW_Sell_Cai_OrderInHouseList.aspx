<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NEW_Sell_Cai_OrderInHouseList.aspx.cs"
    Inherits="VAN_OA.JXC.NEW_Sell_Cai_OrderInHouseList" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="销售退货未采退列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">销退未采退清单-新
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server" Width="200PX"></asp:TextBox>
            </td>
            <td>项目名称:
            </td>
            <td>
                <asp:TextBox ID="ttxPOName" runat="server" Width="200PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>销退时间:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" Width="200PX"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" Width="200PX"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200PX"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>采购退货:
            </td>
            <td>
                <asp:DropDownList ID="ddlTui" runat="server" Width="200PX">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">未全部采购退货</asp:ListItem>
                    <asp:ListItem Value="2">全部采购退货</asp:ListItem>
                </asp:DropDownList>
            </td>


            <td>AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td colspan="4">商品编码:
                <asp:TextBox ID="txtGoodNo" runat="server" Width="100px"></asp:TextBox>名称/小类/规格:
                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>
                <%-- </td>
                </tr>
               <tr>
            <td colspan="4">--%>
                项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
                项目关闭：
                <asp:DropDownList ID="ddlIsClose" runat="server">
                    <asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
                    <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="未关闭" Value="0"> </asp:ListItem>
                </asp:DropDownList>
                项目选中：
                <asp:DropDownList ID="ddlIsSelect" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未选中</asp:ListItem>
                    <asp:ListItem Value="1">选中</asp:ListItem>
                </asp:DropDownList>
                结算选中：
                <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                </asp:DropDownList>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div style="float: left; display: inline;">
                    销退采退分析:
                    <asp:DropDownList ID="ddlFenXi" runat="server" Width="80%">
                        <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                        <%--  <asp:ListItem Value="1" Text="商品数量合计》=（销售退货合计-采购退货合计）×2-----浅绿色-正常"></asp:ListItem>
                    <asp:ListItem Value="2" Text="商品销售退货合计-采购退货合计<0----浅灰色-不正常"></asp:ListItem>
                    <asp:ListItem Value="3" Text="商品数量合计<（销售退货合计-采购退货合计）×2------红色-不正常"></asp:ListItem>--%>

                        <asp:ListItem Value="1" Text="商品销售退货合计-采购退货合计<0--不正常（浅灰色）"></asp:ListItem>
                        <asp:ListItem Value="2" Text="商品销售退货合计-采购退货合计=0--正常（浅黄色）"></asp:ListItem>
                        <asp:ListItem Value="3" Text="项目中该商品数量合计-采购退货合计>=（销售退货合计-采购退货合计）×2 且采购单为库存的数量>=销售退货合计-采购退货合计-正常（浅绿色）"></asp:ListItem>
                        <asp:ListItem Value="4" Text="项目中该商品数量合计-采购退货合计<（销售退货合计-采购退货合计）×2 -正常库存消零（土黄色）"></asp:ListItem>
                         <asp:ListItem Value="4.1" Text="项目中该商品数量合计-采购退货合计>=（销售退货合计-采购退货合计）×2 -正常库存消零（淡绿色）"></asp:ListItem>

                        <asp:ListItem Value="5" Text="项目中该商品数量合计-采购退货合计>=（销售退货合计-采购退货合计）×2 且采购单为库存的数量<销售退货合计-采购退货合计--不正常有库存（淡红色）"></asp:ListItem>
                        <asp:ListItem Value="6" Text="项目中该商品数量合计-采购退货合计<（销售退货合计-采购退货合计）×2 -不正常库存不为零（红色）"></asp:ListItem>
                        <asp:ListItem Value="7" Text="正常"></asp:ListItem>
                        <asp:ListItem Value="8" Text="不正常"></asp:ListItem>

                    </asp:DropDownList>

                    标注正常：
                <asp:DropDownList ID="ddlNormal" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="正常"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不正常"></asp:ListItem>
                </asp:DropDownList>
                </div>
                <div style="float: right; display: inline;">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text=" 保 存 " BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
               
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">

        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid" PageSize="20"
            Width="150%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>销退日期
                        </td>
                        <td>项目编码
                        </td>
                        <td>项目名称
                        </td>
                        <td>客户名称
                        </td>
                        <td>AE
                        </td>
                        <td>编码
                        </td>
                        <td>名称
                        </td>
                        <td>小类
                        </td>
                        <td>规格
                        </td>
                        <td>型号
                        </td>
                        <td>单位
                        </td>
                        <td>销退数量
                        </td>
                        <td>成本单价
                        </td>
                        <td>成本金额
                        </td>
                        <td>成本确认价
                        </td>
                        <td>损失差额
                        </td>
                        <td>销售单价
                        </td>
                        <td>销售金额
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                 <asp:TemplateField HeaderText="正常">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbIsSelected1" runat="server" Text="正常" AutoPostBack="True" OnCheckedChanged="cbIsSelected_CheckedChanged"
                        Enabled="<%# IsSelectedEdit() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSelected" runat="server" Checked='<% #Eval("IsNormal") %>'
                        Enabled="<%# IsSelectedEdit() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="项目编码"  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="GooId" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="GooId" runat="server" Text='<%# Eval("GooId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

                <asp:BoundField DataField="RuTime" HeaderText="销退日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreateName" HeaderText="AE" SortExpression="CreateName"
                    ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" />
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" />
                <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-Width="10%" />
                <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                <asp:TemplateField HeaderText="销退数量">
                    <ItemTemplate>
                        <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="采退数量">
                    <ItemTemplate>
                        <asp:Label ID="lblCaiNum" runat="server" Text='<%# Eval("CaiGoodNum") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NeedNums" HeaderText="需补采退数" />
                <asp:BoundField DataField="CaiNums" HeaderText="库存采购数" />
                <asp:BoundField DataField="HouseGoodNum" HeaderText="库存" />
                <asp:BoundField DataField="PONums" HeaderText="项目数量" SortExpression="PONums" />
                <asp:BoundField DataField="CAIRuTime" HeaderText="采退日期" SortExpression="CAIRuTime"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="CAIGoodPrice" HeaderText="采退单价" SortExpression="CAIGoodPrice" />
                <asp:BoundField DataField="CAIGoodPriceTotal" HeaderText="采退总价" SortExpression="CAIGoodPriceTotal" />
                <asp:TemplateField HeaderText="销退成本单价">
                    <ItemTemplate>
                        <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="销退成本总价">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="销退成本确认价">
                    <ItemTemplate>
                        <asp:Label ID="txtGoodPriceSecond" runat="server" Text='<%# Eval("GoodPriceSecond") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="差额">
                    <ItemTemplate>
                        <asp:Label ID="lblGoodTotalCha" runat="server" Text='<%# Eval("GoodTotalCha") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblGoodTotalCha" runat="server" Text='<%# Eval("GoodTotalCha") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="销售单价">
                    <ItemTemplate>
                        <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="销售总价">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>

    </asp:Panel>
    <table width="100%" border="0">
        <tr>
            <td>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
                    PageSize="20" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                    NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    销退成本 :
    <asp:Label ID="lblSellTuiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp;&nbsp;&nbsp;采退成本 :
    <asp:Label ID="lblCaiTuiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    销退总成本 :
    <asp:Label ID="lblSellTuiTotal_Sum" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp;&nbsp;&nbsp;采退总成本 :
    <asp:Label ID="lblCaiTuiTotal_Sum" runat="server" Text="0" ForeColor="Red"></asp:Label>
</asp:Content>
