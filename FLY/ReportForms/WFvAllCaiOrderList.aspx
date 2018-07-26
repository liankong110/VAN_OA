<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WFvAllCaiOrderList.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFvAllCaiOrderList" MasterPageFile="~/DefaultMaster.Master" EnableEventValidation = "false"
    Title="采购订单列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
            <td>
                项目名称:
            </td>
            <td>
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
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
                <asp:DropDownList ID="ddlBusType" runat="server" Width="160px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlBusType_SelectedIndexChanged">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0">项目订单采购</asp:ListItem>
                    <asp:ListItem Value="1">库存采购</asp:ListItem>
                </asp:DropDownList>
                项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px" Enabled="false">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                商品编码:
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>
                供应商:
            </td>
            <td>
                <asp:TextBox ID="txtLastSupplier" runat="server" Width="200px"></asp:TextBox>
                <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
                采购类型:
                <asp:DropDownList ID="ddlCaiGou" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="外采" Value="1"></asp:ListItem>
                    <asp:ListItem Text="库存" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                审批日期:<asp:TextBox ID="txtAuditDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtAuditDate">
                </cc1:CalendarExtender>
            </td>
            <td colspan="2">
                含税：
                <asp:DropDownList ID="ddlHanShui" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">含税</asp:ListItem>
                    <asp:ListItem Value="0">不含税</asp:ListItem>
                </asp:DropDownList>
                发票类型:<asp:DropDownList ID="dllSelectFPstye" runat="server" DataValueField="FpType"
                    DataTextField="FpType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                名称/小类/规格:
                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>
                数量
                <asp:DropDownList ID="ddlCaiNum" runat="server">
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtCaiNum" runat="server" Width="100PX"></asp:TextBox>
                最终采购单价：
                <asp:DropDownList ID="ddlCaiPrice" runat="server">
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtCaiPrice" runat="server" Width="100PX"></asp:TextBox>
                采购单号：<asp:TextBox ID="txtProNo" runat="server" Width="100PX"></asp:TextBox>
                <br />
                忽略项目编号:<asp:TextBox ID="txtPONO1" runat="server" Width="100PX"></asp:TextBox>-<asp:TextBox ID="txtPONO2" runat="server" Width="100PX"></asp:TextBox>
                -<asp:TextBox ID="txtPONO3" runat="server" Width="100PX"></asp:TextBox>
                项目单据日期：
                  <asp:TextBox ID="txtProDateFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtProDateTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtProDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton4" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtProDateTo">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text=" 保 存 " BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnPrint" runat="server" Text="打印" BackColor="Yellow" OnClick="btnPrint_Click" />&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="320%" AutoGenerateColumns="False" ShowFooter="true"
            AllowPaging="True" OnRowDataBound="gvMain_RowDataBound" OnPageIndexChanging="gvMain_PageIndexChanging">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            单据号
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
                <asp:TemplateField HeaderText="含税">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsHanShui" runat="server" Checked='<% #Eval("IsHanShui") %>' />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="含税">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsHanShui123" runat="server" Checked='<% #Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发票类型">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hidtxt" Value='<%#Eval("CaiFpType")%>' />
                        <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="FpType" DataTextField="FpType">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="myId" runat="server" Text='<%# Eval("ids") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

               <asp:BoundField DataField="IsHanShui" HeaderText="含税" SortExpression="IsHanShui"
                    ItemStyle-HorizontalAlign="Center"  Visible="false"/>
                <asp:BoundField DataField="CaiFpType" HeaderText="发票类型" SortExpression="CaiFpType"
                    ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:BoundField DataField="LastTime" HeaderText="审批时间" SortExpression="LastTime"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ProNo" HeaderText="采购单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CaiGou" HeaderText="请购人" SortExpression="CaiGou" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PODate" HeaderText="项目单据日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
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
                <asp:TemplateField HeaderText="预估成本单价">
                    <ItemTemplate>
                        <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="预估成本总价">
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
                <asp:TemplateField HeaderText="预估销售净利">
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
                <asp:BoundField DataField="lastSupplier" HeaderText="最终供应商" SortExpression="lastSupplier"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="lastPrice" HeaderText="最终价格" SortExpression="lastPrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="实采价/最终价格">
                    <ItemTemplate>
                        <asp:Label ID="lblLastTruePrice" runat="server" Text='<%# Eval("LastTruePrice") %>'></asp:Label>/
                        <asp:Label ID="lbllastPrice" runat="server" Text='<%# Eval("lastPrice") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="最终价格小计">
                    <ItemTemplate>
                        <asp:Label ID="lblLastTrueTotal" runat="server" Text='<%# Eval("LastTotal") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblLastTrueTotal" runat="server" Text='<%# Eval("LastTotal") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Idea" HeaderText="审批意见" SortExpression="Idea" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:BoundField DataField="UpdateUser" HeaderText="更新人" SortExpression="UpdateUser"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField HeaderText="预估利润率%">
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
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    合计采购金额：<asp:Label ID="lblTotal" runat="server" Text="0" Style="color: Red;"></asp:Label>
</asp:Content>
