<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSupplierInvoiceList.aspx.cs"
    Inherits="VAN_OA.JXC.WFSupplierInvoiceList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="支付列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">支付列表
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                项目时间: 
                <asp:TextBox ID="txtFromDate" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtToDate" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFromDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtToDate">
                </cc1:CalendarExtender>
            </td>
            <td>项目名称:
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
            <td>支付时间:
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
            <td>状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatue" runat="server" Width="160px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>执行中</asp:ListItem>
                    <asp:ListItem>通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
                类型：
                <asp:DropDownList ID="ddlType" runat="server" Width="160px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">支付款</asp:ListItem>
                    <asp:ListItem Value="1">预付款</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>支付单号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>入库单号:
            </td>
            <td>
                <asp:TextBox ID="txtRuCaiProNo" runat="server" Width="160px"></asp:TextBox>
                采购单号:
                   <asp:TextBox ID="txtCaiProNo" runat="server" Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>供应商:
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="350px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtSupplier">
                </cc1:AutoCompleteExtender>
                <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
            </td>
            <td>商品编码:
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                付款单价:
                <asp:DropDownList ID="ddlActJS" runat="server">
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value=">=">>=</asp:ListItem>
                    <asp:ListItem Value="<="><=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtActTotal" runat="server" Width="100px"></asp:TextBox>
                总支付金额:
                <asp:DropDownList ID="ddlSumActPay" runat="server">
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value=">=">>=</asp:ListItem>
                    <asp:ListItem Value="<="><=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtSumActPay" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td>支付
            </td>
            <td>
                <span>
                    <asp:DropDownList ID="ddlZhiFu" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Value="2">已支付</asp:ListItem>
                        <asp:ListItem Value="1">未付清</asp:ListItem>
                        <asp:ListItem Value="0">未支付</asp:ListItem>
                    </asp:DropDownList>
                    结清
                    <asp:DropDownList ID="ddlRePayClear" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Value="0">初始</asp:ListItem>
                        <asp:ListItem Value="1">结清</asp:ListItem>
                        <asp:ListItem Value="2">未结清</asp:ListItem>
                    </asp:DropDownList>
                    制单人
                     <asp:DropDownList ID="ddlCreateName" runat="server"
                         Width="200PX">
                     </asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="4">名称/小类/规格:
                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>
                支付数量 
                <asp:DropDownList ID="ddlCaiNum" runat="server">
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtCaiNum" runat="server" Width="100PX"></asp:TextBox>
                付款单价：
                <asp:DropDownList ID="ddlCaiPrice" runat="server">
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtCaiPrice" runat="server" Width="100PX"></asp:TextBox>
                含税类型: 
                <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="Id" DataTextField="FpType">
                </asp:DropDownList>
                 供应商特性 ：
                  <asp:DropDownList ID="ddlPeculiarity" runat="server">
                    <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                     <asp:ListItem Text="厂家" Value="厂家"></asp:ListItem>
                     <asp:ListItem Text="代理商" Value="代理商"></asp:ListItem>
                      <asp:ListItem Text="总代理" Value="总代理"></asp:ListItem>
                       <asp:ListItem Text="个人" Value="个人"></asp:ListItem>
                </asp:DropDownList>
                <br />
                业务类型:
                 <asp:DropDownList ID="ddlBusType" runat="server" Width="160px"
                     AutoPostBack="True" OnSelectedIndexChanged="ddlBusType_SelectedIndexChanged">
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
                含税:
                 <asp:DropDownList ID="ddlIsHanShui" runat="server" Width="50px">
                     <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                     <asp:ListItem Value="1" Text="含税"></asp:ListItem>
                     <asp:ListItem Value="0" Text="不含税"></asp:ListItem>
                 </asp:DropDownList>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>

                实采采价比对：
                 <asp:DropDownList ID="ddlComparePrice" runat="server" Width="150px">
                     <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                     <asp:ListItem Value=">" Text="实采单价>采购单价"></asp:ListItem>
                     <asp:ListItem Value="<" Text="实采单价<采购单价"></asp:ListItem>
                     <asp:ListItem Value=">=" Text="实采单价>=采购单价"></asp:ListItem>
                     <asp:ListItem Value="<=" Text="实采单价<=采购单价"></asp:ListItem>
                     <asp:ListItem Value="=" Text="实采单价=采购单价"></asp:ListItem>
                 </asp:DropDownList>
                <br />
                支付类型
                <asp:DropDownList ID="ddlZhiFuType" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="正常数据" Value="1"></asp:ListItem>
                    <asp:ListItem Text="过程数据" Value="2"></asp:ListItem>
                    <asp:ListItem Text="1.正数支付单  实际支付>0 制单人<>admin  是正在执行或完成的支付单据" Value="3"></asp:ListItem>
                    <asp:ListItem Text="2.负数支付单  实际支付<0制单人=admin  合并=1 结算=未结清  支付=未支付 是采购退货需要扣减的负数支付单据（尚未扣减）" Value="4"></asp:ListItem>
                    <asp:ListItem Text="3.负数支付单  实际支付<0制单人<>admin  合并=0 结算=未结清  支付=未支付 是采购退货时扣减的负数支付已经完成的记录显示" Value="5"></asp:ListItem>
                    <asp:ListItem Text="4.正数预付款单 实际支付>0 制单人<>admin  入库时间=“0001-01-01” ，是正在执行或完成的预付款支付单" Value="6"></asp:ListItem>
                    <asp:ListItem Text="5.正数支付单  实际支付>0 制单人=admin  是正在执行或完成的预付款转支付单据" Value="7"></asp:ListItem>
                    <asp:ListItem Text="6.负数支付单  实际支付<0制单人=admin  合并=0 结算=结清  支付=已支付 是采购退货时生成的一条无用数据，表明该负数金额已由其他记录拖出来来扣减" Value="8"></asp:ListItem>
                    <asp:ListItem Text="7. 已扣回款项的供应商预付款和供应商付款单" Value="9"></asp:ListItem>
                    <asp:ListItem Text="8. 等待扣回的供应商付款单" Value="10"></asp:ListItem>
                    <asp:ListItem Text="9. 实际提交已扣除的部分,全额采退+部分采退（不生成事后需要扣回的部分）" Value="11"></asp:ListItem>

                </asp:DropDownList>  <br />
                客户名称：
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>


                客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType" Width="50px">
                </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Width="50px">
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="plMain" ScrollBars="Horizontal" Width="100%">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="payId" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand" Width="180%">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>类型
                        </td>
                        <td>支付单号
                        </td>
                        <td>支付单/预付单号
                        </td>
                        <td>入库/采购单号
                        </td>
                        <td>供应商
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
                        <td>单位
                        </td>
                        <td>采购数
                        </td>
                        <td>采购单价
                        </td>
                        <td>金额
                        </td>
                        <td>发票号
                        </td>
                        <td>出款日期
                        </td>
                        <td>付款金额
                        </td>
                        <td>采购人
                        </td>
                        <td>支付
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnReEdit" runat="server" CommandName="ReEdit" CommandArgument='<% #Eval("PayType_Id") %>'
                            OnClientClick='return confirm( "确定要重新提交此单据吗？") '>编辑</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <%--  <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                            CommandName="Delete1" CommandArgument='<% #Eval("PayType_Id") %>' OnClientClick='return confirm( "确定删除吗？") ' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                        <asp:Label ID="lblbusType" runat="server" Text='<%# Eval("busType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:BoundField DataField="busType" HeaderText="类型" SortExpression="busType" ItemStyle-HorizontalAlign="Center" />--%>
                <%--<asp:BoundField DataField="InvProNo" HeaderText="支付单/预付单号" SortExpression="InvProNo"
                    ItemStyle-HorizontalAlign="Center" />--%>
                <asp:TemplateField HeaderText="支付单/预付单号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("PayType_Id") %>'> <%# Eval("InvProNo")%></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="SupplierProNo" HeaderText="支付流水号" SortExpression="SupplierProNo"
                    ItemStyle-HorizontalAlign="Center" />
                <%--  <asp:BoundField DataField="RuTime" HeaderText="入库时间" SortExpression="RuTime"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />--%>
                <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="支付时间" SortExpression="SupplierInvoiceDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Ids" runat="server" Text='<%# Eval("Ids") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProNo" HeaderText="入库单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CaiProNo" HeaderText="采购单号" SortExpression="CaiProNo" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="RuTime" HeaderText="入库时间" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="GuestName" HeaderText="供应商" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="Peculiarity" HeaderText="特性" SortExpression="Peculiarity" ItemStyle-HorizontalAlign="Center"  >
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
                <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POGuestName" HeaderText="客户名称" SortExpression="POGuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestType" HeaderText="客户类型" SortExpression="GuestType"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="客户属性" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetGestProInfo(Eval("GuestPro"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="POAE" HeaderText="AE" SortExpression="POAE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNum" HeaderText="数量" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplierTuiGoodNum" HeaderText="采退数" SortExpression="supplierTuiGoodNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceNum" HeaderText="支付数量" SortExpression="SupplierInvoiceNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodPrice" HeaderText="采购单价" SortExpression="GoodPrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="LastTotal" HeaderText="金额" SortExpression="LastTotal"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" Visible="false" />
                <asp:BoundField DataField="DoPer" HeaderText="制单人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierFPNo" HeaderText="发票号" SortExpression="SupplierFPNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="出款日期" SortExpression="SupplierInvoiceDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="付款单价" SortExpression="SupplierInvoicePrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceTotal" HeaderText="付款金额" SortExpression="SupplierInvoiceTotal"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FuShuTotal" HeaderText="负数合计" SortExpression="FuShuTotal"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ActPay" HeaderText="实际支付" SortExpression="ActPay" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="LastPayTotal" HeaderText="出价合计" SortExpression="LastPayTotal" DataFormatString="{0:n5}" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="RePayClearString" HeaderText="结算状态" SortExpression="RePayClearString"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="IsHeBingString" HeaderText="合并" SortExpression="IsHeBingString"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="isZhiFu" HeaderText="支付" SortExpression="isZhiFu" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="税">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CaiFpType" HeaderText="类型" SortExpression="CaiFpType"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="预">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("IsYuFu") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Status" HeaderText="审批状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="payId" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHiddPayId" runat="server" Text='<%# Eval("payId") %>'></asp:Label>
                        <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Eval("IsYuFu") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
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
    实际支付合计:
    <asp:Label ID="lblALLActTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    出价合计:
    <asp:Label ID="lblLastPayTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <br />
    <asp:Panel runat="server" ID="Panel1" ScrollBars="Horizontal" Width="100%">
        采购退货金额明细
        <asp:GridView ID="gvDiXiao" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="payId" AllowPaging="False" AutoGenerateColumns="False" OnRowDataBound="gvDiXiao_RowDataBound"
            Width="180%">
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>支付单号
                        </td>
                        <td>支付单号
                        </td>
                        <td>入库单号
                        </td>
                        <td>供应商
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
                        <td>单位
                        </td>
                        <td>采购数
                        </td>
                        <td>采购单价
                        </td>
                        <td>金额
                        </td>
                        <td>发票号
                        </td>
                        <td>出款日期
                        </td>
                        <td>付款金额
                        </td>
                        <td>采购人
                        </td>
                        <td>支付
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="InvProNo" HeaderText="支付单号" SortExpression="InvProNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierProNo" HeaderText="支付流水号" SortExpression="SupplierProNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreteTime" HeaderText="支付时间" SortExpression="CreteTime"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Ids" runat="server" Text='<%# Eval("Ids") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProNo" HeaderText=" 入库单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="RuTime" HeaderText="入库时间" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="GuestName" HeaderText="供应商" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POGuestName" HeaderText="客户名称" SortExpression="POGuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POAE" HeaderText="AE" SortExpression="POAE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNum" HeaderText="数量" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplierTuiGoodNum" HeaderText="采退数" SortExpression="supplierTuiGoodNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceNum" HeaderText="支付数量" SortExpression="SupplierInvoiceNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodPrice" HeaderText="采购单价" SortExpression="GoodPrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="LastTotal" HeaderText="金额" SortExpression="LastTotal"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" Visible="false" />
                <asp:BoundField DataField="DoPer" HeaderText="制单人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierFPNo" HeaderText="发票号" SortExpression="SupplierFPNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="出款日期" SortExpression="SupplierInvoiceDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="付款单价" SortExpression="SupplierInvoicePrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceTotal" HeaderText="付款金额" SortExpression="SupplierInvoiceTotal"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FuShuTotal" HeaderText="负数合计" SortExpression="FuShuTotal"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ActPay" HeaderText="实际支付" SortExpression="ActPay" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="RePayClearString" HeaderText="结算状态" SortExpression="RePayClearString"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="IsHeBingString" HeaderText="合并" SortExpression="IsHeBingString"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="isZhiFu" HeaderText="支付" SortExpression="isZhiFu" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="税">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CaiFpType" HeaderText="类型" SortExpression="CaiFpType"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="预">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("IsYuFu") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Status" HeaderText="审批状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
    </asp:Panel>
    <asp:Label ID="kkF" runat="server" Text="单据支付金额合计:"></asp:Label>
    <asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
