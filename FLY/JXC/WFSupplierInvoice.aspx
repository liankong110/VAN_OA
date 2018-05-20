<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSupplierInvoice.aspx.cs"
    Inherits="VAN_OA.JXC.WFSupplierInvoice" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="支付列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="TabPanel1" runat="server">
            <HeaderTemplate>
                支付款</HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                            支付列表
                        </td>
                    </tr>
                    <tr>
                        <td>
                            项目编号:
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                             项目时间:
                              <asp:TextBox ID="txtPoDateFrom" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPoDateTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateTo">
                </cc1:CalendarExtender>

                        </td>
                        <td>
                            项目名称:
                        </td>
                        <td>
                            <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            入库时间:
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
                            入库单状态:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatue" runat="server" Width="160px">
                                <asp:ListItem>通过</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            入库单号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProNo" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            检验单号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChcekProNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            供应商:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSupplier" runat="server" Width="300px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                TargetControlID="txtSupplier">
                            </cc1:AutoCompleteExtender>
                              <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
                        </td>
                        <td>
                            仓库：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlHouse" DataTextField="houseName" DataValueField="id" runat="server">
                            </asp:DropDownList>
                            AE:
                             <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="display: inline; float: left;">
                                商品编码:
                                <asp:TextBox ID="txtGoodNo" runat="server" Width="80PX"></asp:TextBox>
                                名称/小类/规格:
                                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="80PX"></asp:TextBox>
                                或者
                                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="80PX"></asp:TextBox>
                                入库数
                                <asp:DropDownList ID="ddlCaiNum" runat="server">
                                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCaiNum" runat="server" Width="80PX"></asp:TextBox>
                                采购单价：
                                <asp:DropDownList ID="ddlCaiPrice" runat="server">
                                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCaiPrice" runat="server" Width="80PX"></asp:TextBox>
                                临时：<asp:DropDownList ID="ddlTemp" runat="server">
                                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="临时" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="非临时" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="cbGuolv" runat="server" Text="过滤"  Checked="true"/>
                            </div>
                            <div style="display: inline; float: right;">
                                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button4" runat="server" Text=" 临时保存 " BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" Text="提交选中数据" BackColor="Yellow" OnClick="Button1_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
                实采总额：<asp:Label ID="lblTotalZhi" runat="server" Text="0" ForeColor="Red"></asp:Label>
                 剩余可支付实采金额：<asp:Label ID="lblShengYuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                剩余支价总额：<asp:Label ID="lblShengyuzhijiaTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                页面大小：
                <asp:Panel runat="server" ID="plMain" ScrollBars="Horizontal" Width="100%">
                    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                        DataKeyNames="Ids" AllowPaging="True" AutoGenerateColumns="False" Width="120%" PageSize="40"
                        OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound">
                        <PagerTemplate>
                        </PagerTemplate>
                        <EmptyDataTemplate>
                            <table width="100%">
                                <tr style="height: 20px; background-color: #336699; color: White;">
                                    <td>
                                        入库单号
                                    </td>
                                    <td>
                                        入库日期
                                    </td>
                                    <td>
                                        供应商
                                    </td>
                                    <td>
                                        仓库
                                    </td>
                                    <td>
                                        项目编码
                                    </td>
                                    <td>
                                        项目名称
                                    </td>
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
                                        采购数
                                    </td>
                                    <td>
                                        采购单价
                                    </td>
                                    <td>
                                        金额
                                    </td>
                                    <%-- <td>
                                    发票号
                                </td>
                                <td>
                                    出款日期
                                </td>--%>
                                    <td>
                                        付款金额
                                    </td>
                                    <td>
                                        采购人
                                    </td>
                                    <td>
                                        支付
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
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                    &nbsp;
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbAll_CheckedChanged" Text="选" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Ids" runat="server" Text='<%# Eval("Ids") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="临时">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbTemp" runat="server" Checked='<%# Eval("IsTemp") %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbTempAll" Text="临" runat="server" AutoPostBack="True" OnCheckedChanged="cbTempAll_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProNo" HeaderText="入库单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="RuTime" HeaderText="入库日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="GuestName" HeaderText="供应商" SortExpression="GuestName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="houseName" HeaderText="仓库" SortExpression="houseName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodNum" HeaderText="入库数" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="supplierTuiGoodNum" HeaderText="采退数" SortExpression="supplierTuiGoodNum"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="lastGoodNum" HeaderText="净数量" SortExpression="lastGoodNum"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodPrice" HeaderText="采购单价" SortExpression="GoodPrice"
                                ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="LastTruePrice" HeaderText="实采单价" SortExpression="LastTruePrice"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="LastTotal" HeaderText="金额" SortExpression="LastTotal"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                            <asp:BoundField DataField="DoPer" HeaderText="采购人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
                            <%--  <asp:BoundField DataField="SupplierFPNo" HeaderText="发票号" SortExpression="SupplierFPNo"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="出款日期" SortExpression="SupplierInvoiceDate"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="预付单价" SortExpression="SupplierInvoicePrice"
                            ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField DataField="zhengTotal" HeaderText="已付金额" SortExpression="zhengTotal"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="jianTotal" HeaderText="退款金额" SortExpression="jianTotal"
                                ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="ResultTotal" HeaderText="剩余金额" SortExpression="ResultTotal"
                                ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ShengYuZhiJia" HeaderText="剩余支价" SortExpression="ShengYuZhiJia"
                                ItemStyle-HorizontalAlign="Center" />
                            <%--<asp:BoundField DataField="SupplierInvoiceTotal" HeaderText="总款金额" SortExpression="SupplierInvoiceTotal"
                            ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField DataField="ChcekProNo" HeaderText="检验单号" SortExpression="ChcekProNo"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="isZhiFu" HeaderText="支付" SortExpression="isZhiFu" ItemStyle-HorizontalAlign="Center" />
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
                        PageSize="40" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
                    </webdiyer:AspNetPager>
                </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server">
            <HeaderTemplate>
                预付款</HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                            支付列表
                        </td>
                    </tr>
                    <tr>
                        <td>
                            项目编号:
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="txtAdvancePaymentPoNo" runat="server"></asp:TextBox>

                               项目时间:
                              <asp:TextBox ID="txtYuPoDateFrom" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtYuPoDateTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton4" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtYuPoDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton5" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtYuPoDateTo">
                </cc1:CalendarExtender>
                        </td>
                        <td>
                            项目名称:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdvancePaymentPoName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            采购单号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdvancePaymentProNo" runat="server" ></asp:TextBox>
                               AE:
                             <asp:DropDownList ID="ddlAdvUserList" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                        </td>
                        <td>
                            供应商:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdvancePaymentSupplierName" runat="server" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                TargetControlID="txtAdvancePaymentSupplierName">
                            </cc1:AutoCompleteExtender>
                             <asp:CheckBox ID="cbAdvancePiPei" runat="server" Text="全匹配" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="display: inline; float: left;">
                                商品编码:
                                <asp:TextBox ID="txtAdvancePaymentGoodNo" runat="server" Width="80PX"></asp:TextBox>
                                名称/小类/规格:
                                <asp:TextBox ID="txtNameOrTypeOrSpec1" runat="server" Width="80PX"></asp:TextBox>
                                或者
                                <asp:TextBox ID="txtNameOrTypeOrSpecTwo1" runat="server" Width="80PX"></asp:TextBox>
                                采购数
                                <asp:DropDownList ID="ddlCaiNum1" runat="server">
                                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCaiNum1" runat="server" Width="80PX"></asp:TextBox>
                                采购单价：
                                <asp:DropDownList ID="ddlCaiPrice1" runat="server">
                                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCaiPrice1" runat="server" Width="80PX"></asp:TextBox>
                                临时：<asp:DropDownList ID="ddlAdvanceTemp" runat="server">
                                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="临时" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="非临时" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                  <asp:CheckBox ID="cbAdvanceGuolv" runat="server" Text="过滤"  Checked="true"/>
                                    <asp:CheckBox ID="cbNoKuCun" runat="server" Text="不含库存"  Checked="true"/>
                            </div>
                            <div style="display: inline; float: right;">
                                <asp:Button ID="Button2" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelectAdvancePayment_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button5" runat="server" Text=" 临时保存 " BackColor="Yellow" OnClick="Btn_Save_AdvancePayment_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button3" runat="server" Text="提交选中数据" BackColor="Yellow" OnClick="Button1AdvancePayment_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                实采总额：<asp:Label ID="lblYuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                  剩余可预付金额总额：<asp:Label ID="lblYu_ShengYuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                剩余预价总额：<asp:Label ID="lblYu_ShengyuzhijiaTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>

                <asp:Panel runat="server" ID="Panel1" ScrollBars="Horizontal" Width="100%">
                    <asp:GridView ID="gvAdvancePayment" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                        DataKeyNames="Ids" AllowPaging="True" AutoGenerateColumns="False" Width="120%"
                        OnPageIndexChanging="gvAdvancePayment_PageIndexChanging" OnRowDataBound="gvAdvancePayment_RowDataBound">
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
                                        采购单号
                                    </td>
                                    <td>
                                        供应商
                                    </td>
                                    <td>
                                        项目编码
                                    </td>
                                    <td>
                                        项目名称
                                    </td>
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
                                        采购数
                                    </td>
                                    <td>
                                        采购单价
                                    </td>
                                    <td>
                                        金额
                                    </td>
                                    <%-- <td>
                                    发票号
                                </td>
                                <td>
                                    出款日期
                                </td>--%>
                                    <td>
                                        付款金额
                                    </td>
                                    <td>
                                        支付
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
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                    &nbsp;
                                </ItemTemplate>
                                <HeaderTemplate>                                    
                                     <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="cbAll_AdvancePayment_CheckedChanged" Text="选" />
                          
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Ids" runat="server" Text='<%# Eval("Ids") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbTemp" runat="server"  Checked='<%# Eval("IsTemp") %>' />
                                </ItemTemplate>
                                 <HeaderTemplate>
                                    <asp:CheckBox ID="cbTempAll" Text="临" runat="server" AutoPostBack="True" OnCheckedChanged="cbTempAll_AdvancePayment_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProNo" HeaderText="采购单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestName" HeaderText="供应商" SortExpression="GuestName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodNum" HeaderText="采购数" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center" />                              
                               

                            <asp:BoundField DataField="GoodPrice" HeaderText="采购单价" SortExpression="GoodPrice"
                                ItemStyle-HorizontalAlign="Center" />
                                  <asp:BoundField DataField="LastTruePrice" HeaderText="实采单价" SortExpression="LastTruePrice"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="LastTotal" HeaderText="实采总额" SortExpression="LastTotal"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                            <%-- <asp:BoundField DataField="SupplierFPNo" HeaderText="发票号" SortExpression="SupplierFPNo"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="出款日期" SortExpression="SupplierInvoiceDate"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="预付单价" SortExpression="SupplierInvoicePrice"
                            ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField DataField="SupplierInvoiceTotal" HeaderText="已预付金额" SortExpression="SupplierInvoiceTotal"
                                ItemStyle-HorizontalAlign="Center" />
                             <asp:BoundField DataField="ResultTotal" HeaderText="剩余可预付金额" SortExpression="ResultTotal"
                                ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ShengYuZhiJia" HeaderText="剩余预价" SortExpression="ShengYuZhiJia"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="isZhiFu" HeaderText="支付" SortExpression="isZhiFu" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
                        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                            HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                        <RowStyle CssClass="InfoDetail1" />
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" Width="100%" ShowPageIndexBox="Always"
                        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
                        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
                        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                        NextPageText="下页" OnPageChanged="AspNetPager2_PageChanged">
                    </webdiyer:AspNetPager>
                </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <br />
    <script language="javascript" type="text/javascript">
        function GetAllCheckBox(parentItem) {
            var items = document.getElementsByTagName("input");
            for (i = 0; i < items.length; i++) {
                if (parentItem.checked) {
                    if (items[i].type == "checkbox") {
                        items[i].checked = true;
                    }
                }
                else {
                    if (items[i].type == "checkbox") {
                        items[i].checked = false;
                    }
                }
            }
        }
  
  

    </script>
</asp:Content>
