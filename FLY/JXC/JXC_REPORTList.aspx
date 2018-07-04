<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JXC_REPORTList.aspx.cs"
    Inherits="VAN_OA.JXC.JXC_REPORTList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="销售报表明细" %>

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
                销售报表明细
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
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
                销售时间:
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
                客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
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
                项目归类
            </td>
            <td>
              <asp:DropDownList ID="ddlIsSpecial" runat="server">
                    <asp:ListItem Value="0">不含特殊</asp:ListItem>
                     <asp:ListItem Value="1">特殊</asp:ListItem>
                     <asp:ListItem Value="-1">全部</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                销售类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlPoType" runat="server" Width="200PX">
                    <asp:ListItem Text="1.全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="2.销售出库" Value="1"></asp:ListItem>
                    <asp:ListItem Text="3.销售退货" Value="2"></asp:ListItem>
                    <asp:ListItem Text="4.公交车费" Value="3"></asp:ListItem>
                    <asp:ListItem Text="5.私车油耗费" Value="4"></asp:ListItem>
                    <asp:ListItem Text="6.用车申请油耗费" Value="5"></asp:ListItem>
                    <asp:ListItem Text="7.行政采购金额" Value="6"></asp:ListItem>
                    <asp:ListItem Text="8.会务费用" Value="7"></asp:ListItem>
                    <asp:ListItem Text="9.人工费" Value="8"></asp:ListItem>
                    <asp:ListItem Text="10.非材料报销（除邮寄费）" Value="9"></asp:ListItem>
                    <asp:ListItem Text="11.非材料报销（邮寄费）" Value="10"></asp:ListItem>
                    <asp:ListItem Text="12.加班单" Value="11"></asp:ListItem>
                      <asp:ListItem Text="13.费用合计（4—12）" Value="12"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                类别：
            </td>
            <td>
                <asp:DropDownList ID="ddlGoodType" runat="server" DataTextField="GoodTypeName" DataValueField="GoodTypeName"
                    Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlGoodType_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGoodSmType" runat="server" DataTextField="GoodTypeSmName"
                    DataValueField="GoodTypeSmName" Width="200px">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
            <div style=" display:inline">
                商品编码
            
                <asp:TextBox ID="txtGoodNo" runat="server" Width="80px"></asp:TextBox>
            
                     
           名称/小类/规格: <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>
                
                 项目类别：
                  <asp:DropDownList ID="ddlPOTyle" runat="server"  DataTextField="BasePoType" DataValueField="Id">
                 <%--   <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1">零售</asp:ListItem>
                    <asp:ListItem Value="2">工程</asp:ListItem>--%>
                </asp:DropDownList>  
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>

                <br />
                数量：   <asp:DropDownList ID="ddlFuHao" runat="server">
                      <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                      <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>                  
                   <asp:ListItem Text="<" Value="<"></asp:ListItem>  
                </asp:DropDownList>
                <asp:TextBox ID="txtGoodNum" runat="server" Width="60px"></asp:TextBox>
                成本单价 ：   <asp:DropDownList ID="ddlChengBen" runat="server">
                      <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                      <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>                  
                   <asp:ListItem Text="<" Value="<"></asp:ListItem>  
                </asp:DropDownList>
                <asp:TextBox ID="txtChengBenPrice" runat="server" Width="60px"></asp:TextBox>
                
                销售单价：   <asp:DropDownList ID="ddlPrice" runat="server">
                      <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                      <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>                  
                   <asp:ListItem Text="<" Value="<"></asp:ListItem>  
                </asp:DropDownList>
                <asp:TextBox ID="txtSellPrice" runat="server" Width="60px"></asp:TextBox> 
               </div>
                <div align="right" style=" display:inline; float:right;">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            Width="160%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
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
                            项目名称
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
                        <td>
                            发票号码
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
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
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
                <asp:BoundField DataField="FPNo" HeaderText="发票号码" SortExpression="FPNo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="40%" ItemStyle-CssClass="item"></asp:BoundField>
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
    <asp:Label ID="Label1" runat="server" Text="销售额合计"></asp:Label>
    <asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label4" runat="server" Text="总成本合计"></asp:Label>
    <asp:Label ID="lblZCBTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label7" runat="server" Text="损失差额合计"></asp:Label>
    <asp:Label ID="lblSunChaTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label2" runat="server" Text="毛利润合计"></asp:Label>
    <asp:Label ID="lblMaoLiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
</asp:Content>
