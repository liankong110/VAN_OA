<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoSellAndCaiGoods.aspx.cs"
    Inherits="VAN_OA.JXC.NoSellAndCaiGoods" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="采库需出清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
  
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">采库需出清单
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
                  项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px" >
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
                  客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType"  Width="50px">   </asp:DropDownList>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>

            </td>
            <td>客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>AE:
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>商品编码:
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>入库时间:
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
            <td>订单时间:
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
        </tr>
        <tr>
            <td>未出类型 :
            </td>
            <td>
                <asp:DropDownList ID="ddlWeiType" runat="server" Width="200px">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="未开具出库单" Value="0"></asp:ListItem>
                    <asp:ListItem Text="出库单执行中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已出又销退" Value="2"></asp:ListItem>
                    <asp:ListItem Text="出库单未通过" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>项目类型:
            </td>
            <td>
                <asp:DropDownList ID="ddlPoType" runat="server" Width="200px">
                    <asp:ListItem Text="全部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="库存采购" Value="1"></asp:ListItem>
                    <asp:ListItem Text="订单项目" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:CheckBox ID="cbZero" runat="server" Text="需出不同时含0" Checked="true" />
                <asp:CheckBox ID="cbRuZero" runat="server" Text="入库需出不含0" />
                <asp:CheckBox ID="cbCaiKu" runat="server" Text="采库需出不含0" />
                <asp:CheckBox ID="cbCaiGou" runat="server" Text="采购需出不含0" />
                含税:
                <asp:DropDownList ID="ddlIsHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="含税"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不含税"></asp:ListItem>
                </asp:DropDownList>
                项目名称：
                <asp:TextBox ID="txtPOName" runat="server" Width="150px"></asp:TextBox>
                KC模式：
                 <asp:DropDownList ID="ddlKCType" runat="server">
                     <asp:ListItem Value="1" Text="精简"></asp:ListItem>
                     <asp:ListItem Value="0" Text="正常"></asp:ListItem>
                        <asp:ListItem Value="2" Text="无KC"></asp:ListItem>
                 </asp:DropDownList>
                供应商简称：
                <asp:TextBox ID="txtSupplier" runat="server" Width="150px"></asp:TextBox>
                <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
                 直发:
                <asp:DropDownList ID="ddlZhifa" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="直发"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不直发"></asp:ListItem>
                </asp:DropDownList>
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="导出EXCEL" BackColor="Yellow" OnClick="Button1_Click" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal" >
        <asp:GridView ID="gvMain" runat="server"  PagerSettings-Visible="false"
            Width="130%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound"  >
            <PagerTemplate>
                
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
                        <td>项目编号
                        </td>
                        <td>AE
                        </td>
                        <td>客户名称
                        </td>
                        <td>商品编号
                        </td>
                        <td>名称
                        </td>
                        <td>规格
                        </td>
                        <td>项目需出
                        </td>
                        <td>入库需出
                        </td>
                        <td>已出数量
                        </td>
                        <td>库存数量
                        </td>
                        <td>库存均价
                        </td>
                        <td>销售单价
                        </td>
                        <td>入库时间
                        </td>
                        <td>订单时间
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="含税">
                    <ItemTemplate>
                        <asp:Label ID="lblIsHanShui" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber"
                    ItemStyle-HorizontalAlign="Center" />
                <%--<asp:BoundField DataField="GoodId" HeaderText="GoodId" SortExpression="GoodId" ItemStyle-HorizontalAlign="Center"  /> --%>
                <asp:BoundField DataField="GoodNo" HeaderText="商品编号" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Left" />
                  <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="totalNum" HeaderText="项目需出" SortExpression="totalNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="RuChuNum" HeaderText="入库需出" SortExpression="RuChuNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                  <asp:BoundField DataField="ZHIFA" HeaderText="直发" SortExpression="ZHIFA" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CaIKuNum" HeaderText="采库需出" SortExpression="CaIKuNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="CaiGouNum" HeaderText="采购需出" SortExpression="CaiGouNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="OutNum" HeaderText="已出数量" SortExpression="OutNum" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <asp:BoundField DataField="HouseNum" HeaderText="库存数量" SortExpression="HouseNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="avgGoodPrice" HeaderText="库存均价" SortExpression="avgGoodPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" />
                <asp:BoundField DataField="avgSellPrice" HeaderText="销售单价" SortExpression="avgSellPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" />
                <asp:TemplateField HeaderText="供应商简称" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblPONOS" runat="server" Text='<%# GetlastSupplier(Eval("PONo"),Eval("goodId")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CaiDate" HeaderText="采购时间" SortExpression="CaiDate" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="minPODate" HeaderText="订单时间" SortExpression="minPODate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>        
    </asp:Panel>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    项目需出库存成本合计：<asp:Label ID="lbltotalNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    入库需出库存成本合计：<asp:Label ID="lblRuChuNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    采库需出库存成本合计：<asp:Label ID="lblCaIKuNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    采购需出库存成本合计：<asp:Label ID="lblCaiGouNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    <br />
    项目需出销售总价合计：<asp:Label ID="lbltotalNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    入库需出销售总价合计：<asp:Label ID="lblRuChuNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    采库需出销售总价合计：<asp:Label ID="lblCaIKuNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    采购需出销售总价合计：<asp:Label ID="lblCaiGouNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>

      
</asp:Content>
