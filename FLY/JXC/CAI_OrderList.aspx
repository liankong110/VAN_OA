<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CAI_OrderList.aspx.cs"
    Inherits="VAN_OA.JXC.CAI_OrderList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="采购订单列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                采购订单列表
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
            <td>
                项目名称:
            </td>
            <td>
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                项目时间:
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
                采购单状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatue" runat="server" Width="160px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>执行中</asp:ListItem>
                    <asp:ListItem>通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>
                项目单据号:
            </td>
            <td>
                <asp:TextBox ID="txtPoProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                请购人:
            </td>
            <td>
                <asp:TextBox ID="txtCaiugou" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>
                业务类型:
            </td>
            <td>
                <asp:DropDownList ID="ddlBusType" runat="server" Width="160px">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0">项目订单采购</asp:ListItem>
                    <asp:ListItem Value="1">库存采购</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                采购单号：
            </td>
            <td>
                <asp:TextBox ID="txtCaiGouNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            <div style="float:left; display:inline" >
                含税:
           
                <asp:DropDownList ID="ddlIsHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="含税"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不含税"></asp:ListItem>
                </asp:DropDownList>
               审批日期:<asp:TextBox ID="txtAuditDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                  <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtAuditDate">
                </cc1:CalendarExtender>
                </div>
                <div style="float:right; display:inline" >
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
        OnRowCommand="gvMain_RowCommand">
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
                        单据号
                    </td>
                    <td>
                        采购人
                    </td>
                    <td>
                        请购人
                    </td>
                    <td>
                        项目编码
                    </td>
                    <td>
                        项目名称
                    </td>
                    <td>
                        项目日期
                    </td>
                    <td>
                        项目金额
                    </td>
                    <td>
                        结算方式
                    </td>
                    <td>
                        客户ID
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        AE
                    </td>
                    <td>
                        INSIDE
                    </td>
                    <td>
                        业务类型
                    </td>
                    <td>
                        单据号
                    </td>
                    <td>
                        状态
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
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnReEdit" runat="server" CommandName="ReEdit" CommandArgument='<% #Eval("Id") %>'
                        OnClientClick='return confirm( "确定要重新提交此单据吗？") '>编辑</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id") %>'>查看</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCopy" runat="server" CommandName="Copy" CommandArgument='<% #Eval("Id") %>'
                        OnClientClick='return confirm( "确定要提交此单据吗？") '>复制</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
              <asp:BoundField DataField="LastTime" HeaderText="审批时间" SortExpression="LastTime" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ProNo" HeaderText="采购单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="LoginName" HeaderText="采购人" SortExpression="LoginName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CaiGou" HeaderText="请购人" SortExpression="CaiGou" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POPayStype" HeaderText="结算方式" SortExpression="POPayStype"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestNo" HeaderText="客户ID" SortExpression="GuestNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BusType" HeaderText="业务类型" SortExpression="BusType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CG_ProNo" HeaderText="项目单据号" SortExpression="CG_ProNo"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <table width="100%" border="0">
        <tr>
            <td>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
                    PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                    NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="TabContainer1" runat="server">
                    <cc1:TabPanel ID="TabPanel1" runat="server">
                        <HeaderTemplate>
                            销售</HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                                DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                                ShowFooter="true">
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr style="height: 20px; background-color: #336699; color: White;">
                                            <td>
                                                编码
                                            </td>
                                            <td>
                                                名称
                                            </td>
                                            <td>
                                                小类
                                            </td>
                                            <td>
                                                规格
                                            </td>
                                            <td>
                                                单位
                                            </td>
                                            <td>
                                                数量
                                            </td>
                                            <td>
                                                成本单价
                                            </td>
                                            <td>
                                                管理费
                                            </td>
                                            <td>
                                                到帐日期
                                            </td>
                                            <td>
                                                利润%
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" align="center" style="height: 80%">
                                                ---暂无数据---
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <Columns>
                                    
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
                                    <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" />
                                    <%--   <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />--%>
                                    <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="成本单价">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="成本总价">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="销售单价">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="销售总价">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="管理费">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="销售净利">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ToTime" HeaderText="到帐日期" SortExpression="ToTime" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:TemplateField HeaderText="利润%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
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
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server">
                        <HeaderTemplate>
                            采购</HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gvCai" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                                DataKeyNames="Ids" ShowFooter="true" Width="100%" AutoGenerateColumns="False"
                                OnRowDataBound="gvCai_RowDataBound" Style="border-collapse: collapse;">
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr style="height: 20px; background-color: #336699; color: White;">
                                            <td>
                                                编码
                                            </td>
                                            <td>
                                                名称
                                            </td>
                                            <td>
                                                小类
                                            </td>
                                            <td>
                                                规格
                                            </td>
                                            <td>
                                                型号
                                            </td>
                                            <td>
                                                单位
                                            </td>
                                            <td>
                                                数量
                                            </td>
                                            <td>
                                                日期
                                            </td>
                                            <td>
                                                供应商1
                                            </td>
                                            <td>
                                               实采价1/询价1
                                            </td>
                                            <td>
                                                小计1
                                            </td>
                                            <td>
                                                供应商2
                                            </td>
                                            <td>
                                               实采价2/询价2
                                            </td>
                                            <td>
                                                小计2
                                            </td>
                                            <td>
                                                供应商3
                                            </td>
                                            <td>
                                                实采价3/询价3
                                            </td>
                                            <td>
                                                小计3
                                            </td>
                                            <td>
                                                利润率
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" align="center" style="height: 80%">
                                                ---暂无数据---
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="GoodId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGoodId" runat="server" Text='<%# Eval("GoodId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="含税">
                            <ItemTemplate>
                                <asp:CheckBox ID="CBIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

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
                                    <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" />
                                    <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />
                                    <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox11" runat="server" Checked='<%# Eval("cbifDefault1") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier" HeaderText="供应商1" SortExpression="Supplier"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="实采价1/询价1">
                                        <ItemTemplate>
                                           <asp:Label ID="lblTruePrice1" runat="server" Text='<%# Eval("TruePrice1") %>'></asp:Label>/
                                            <asp:Label ID="lblSupperPrice" runat="server" Text='<%# Eval("SupperPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="小计1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox12" runat="server" Checked='<%# Eval("cbifDefault2") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier1" HeaderText="供应商2" SortExpression="Supplier1"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="实采价2/询价2">
                                        <ItemTemplate>
                                          <asp:Label ID="lblTruePrice2" runat="server" Text='<%# Eval("TruePrice2") %>'></asp:Label>/
                                            <asp:Label ID="lblSupperPrice1" runat="server" Text='<%# Eval("SupperPrice1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="小计2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox13" runat="server" Checked='<%# Eval("cbifDefault3") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier2" HeaderText="供应商3" SortExpression="Supplier2"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="实采价3/询价3">
                                        <ItemTemplate>
                                          <asp:Label ID="lblTruePrice3" runat="server" Text='<%# Eval("TruePrice3") %>'></asp:Label>/
                                            <asp:Label ID="lblSupperPrice2" runat="server" Text='<%# Eval("SupperPrice2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="小计3">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Idea" HeaderText="审批意见" SortExpression="Idea" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:BoundField DataField="UpdateUser" HeaderText="更新人" SortExpression="UpdateUser"
                                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                                    <asp:TemplateField HeaderText="利润%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
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
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
