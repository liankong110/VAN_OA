<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSupplierInvoiceToAllFpList.aspx.cs"
    Inherits="VAN_OA.JXC.WFSupplierInvoiceToAllFpList" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="支付列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">预付款发票列表
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>项目模型: 
                <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
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
            </td>
        </tr>
        <tr>
            <td>支付单号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>入库/采购单号:
            </td>
            <td>
                <asp:TextBox ID="txtRuCaiProNo" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>供应商简称:
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="300px"></asp:TextBox>
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
            <td>发票状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlFpState" runat="server">
                    <asp:ListItem Value="-1">所有</asp:ListItem>
                    <asp:ListItem Value="0">含税已到票</asp:ListItem>
                    <asp:ListItem Value="1">含税未到票</asp:ListItem>
                    <asp:ListItem Value="2">不含税</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    小计：
    <asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp;&nbsp;&nbsp;合计数： 
    <asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <asp:Panel runat="server" ID="plMain" ScrollBars="Horizontal" Width="100%">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="payId" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand" Width="150%">
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
                        <td>类型
                        </td>
                        <td>支付单/预付单号
                        </td>
                        <td>支付单号
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
                <asp:BoundField DataField="busType" HeaderText="类型" SortExpression="busType" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="InvProNo" HeaderText="支付单/预付单号" SortExpression="InvProNo"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50px"/>
                <asp:BoundField DataField="SupplierProNo" HeaderText="支付流水号" SortExpression="SupplierProNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="RuTime" HeaderText="入库时间" SortExpression="RuTime" ItemStyle-Width="65px"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="支付时间" SortExpression="SupplierInvoiceDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="65px" />
                <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Ids" runat="server" Text='<%# Eval("Ids") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProNo" HeaderText="入库/采购单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="GuestName" HeaderText="供应商简称" SortExpression="GuestName" ItemStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="POGuestName" HeaderText="客户名称" SortExpression="POGuestName" ItemStyle-Width="180px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POAE" HeaderText="AE" SortExpression="POAE" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="40px"/>
                <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="30px"/>
                <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodNum" HeaderText="数量" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplierTuiGoodNum" HeaderText="采退数" SortExpression="supplierTuiGoodNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="lastGoodNum" HeaderText="净数量" SortExpression="lastGoodNum"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GoodPrice" HeaderText="采购单价" SortExpression="GoodPrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="LastTotal" HeaderText="金额" SortExpression="LastTotal"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="DoPer" HeaderText="制单人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierFPNo" HeaderText="发票号" SortExpression="SupplierFPNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="出款日期" SortExpression="SupplierInvoiceDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="65px" />
                <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="付款单价" SortExpression="SupplierInvoicePrice"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierInvoiceTotal" HeaderText="付款金额" SortExpression="SupplierInvoiceTotal"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="含税">
                    <ItemTemplate>
                        <asp:CheckBox ID="CBIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Status" HeaderText="支付状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="isZhiFu" HeaderText="支付" SortExpression="isZhiFu" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="预">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("IsYuFu") %>' Enabled="false" />
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
</asp:Content>
