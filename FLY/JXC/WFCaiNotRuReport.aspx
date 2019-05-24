<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCaiNotRuReport.aspx.cs"
    Inherits="VAN_OA.JXC.WFCaiNotRuReport" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="采购未检验清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                采购未检验清单
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
            <td>
                客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
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
                商品编码:
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
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
        </tr>
        <tr>
            <td colspan="4">
                供应商:
                <asp:DropDownList ID="ddlSupplier" runat="server" Width="50">
                    <asp:ListItem Value="2">厂商</asp:ListItem>
                    <asp:ListItem Value="1">库存</asp:ListItem>
                    <asp:ListItem Value="0">全部</asp:ListItem>
                </asp:DropDownList>
                含税:
                <asp:DropDownList ID="ddlIsHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="含税"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不含税"></asp:ListItem>
                </asp:DropDownList>
                项目名称：
                <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
                供应商名称：
                <asp:TextBox ID="txtSupplierName" runat="server" Width="200px"></asp:TextBox>
                 <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
     <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="120%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
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
                        采购单号
                    </td>
                    <td>
                        项目编号
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
                        客户名称
                    </td>
                    <td>
                        AE
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
                        已检数量
                    </td>
                    <td>
                        已检数量
                    </td>
                    <td>
                        需采数量
                    </td>
                    <td>
                        需检数量
                    </td>
                    <td>
                        供应商简称
                    </td>
                    <td>
                        最终价格
                    </td>
                    <td>
                        入库时间
                    </td>
                    <td>
                        库存数量
                    </td>
                    <td>
                        库存均价
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
            <asp:BoundField DataField="ProNo" HeaderText="采购单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="180px"/>
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="65px"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Right"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName" ItemStyle-Width="180px"
                ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px"/>
            <asp:TemplateField HeaderText="含税">
                <ItemTemplate>
                    <asp:Label ID="lblIsHanShui" runat="server" Text=""></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber" ItemStyle-Width="40px"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30px" />
            <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="Num" HeaderText="已采数量" SortExpression="Num" ItemStyle-HorizontalAlign="Right"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="totalOrderNum" HeaderText="已检数量" SortExpression="totalOrderNum"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="InHouseSum" HeaderText="已入数量" SortExpression="InHouseSum"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="POGoodSum" HeaderText="需采数量" SortExpression="POGoodSum"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="CaiGoodSum" HeaderText="需检数量" SortExpression="CaiGoodSum"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="lastSupplier" HeaderText="供应商简称" SortExpression="lastSupplier"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="lastPrice" HeaderText="最终价格" SortExpression="lastPrice"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" />
            <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="预付单价" SortExpression="SupplierInvoicePrice"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" />
                 <asp:BoundField DataField="SupplierInvoiceTotal" HeaderText="预付未到库总价" SortExpression="SupplierInvoiceTotal"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" /> 

            <asp:BoundField DataField="MinInHouseDate" HeaderText="入库时间" SortExpression="MinInHouseDate" ItemStyle-Width="65px"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="GoodNum" HeaderText="库存数量" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Right"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="GoodAvgPrice" HeaderText="库存均价" SortExpression="GoodAvgPrice"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" />
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
    需检金额合计：<asp:Label ID="lblAllPoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    预付未到库合计：<asp:Label ID="lblYuFuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
</asp:Content>
