<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sell_OrderPFList.aspx.cs"
    Inherits="VAN_OA.JXC.Sell_OrderPFList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="采购订单列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">销售发票列表
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
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
            <td>日期:
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
            <td>发票状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatue" runat="server" Width="160px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>执行中</asp:ListItem>
                    <asp:ListItem>通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
                  项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px" >
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>单据号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">发票号:
            
                <asp:TextBox ID="txtFPNo" runat="server" Width="100px"></asp:TextBox>
                发票金额:
                 <asp:DropDownList ID="ddlPOFaTotal" runat="server">
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                     <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtFPTotal" runat="server" Width="100px"></asp:TextBox>
                原发票号码:
                <asp:TextBox ID="txtOldFPNo" runat="server" Width="100px"></asp:TextBox>
                原金额:
                 <asp:DropDownList ID="ddlOldPOFaTotal" runat="server">
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                     <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtOldFPTotal" runat="server"  Width="100px"></asp:TextBox>


                项目 & 发票数：
            
                <asp:DropDownList ID="ddlPOInvoiceState" runat="server">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">发票数 < 项目数 </asp:ListItem>
                    <asp:ListItem Value="2">发票数 > 项目数</asp:ListItem>
                    <asp:ListItem Value="3">发票数 = 项目数</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                 发票类型：<asp:DropDownList ID="ddlFPType" runat="server" DataValueField="Id" DataTextField="FpType"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            
         
            <td colspan="4">
                AE：
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
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
                  客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType"  Width="50px">
                </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString"  Width="50px">
                </asp:DropDownList>
                  <asp:DropDownList ID="ddlType" runat="server" Width="160px" Visible="false">
                        <asp:ListItem Text="" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="收到" Value="0"></asp:ListItem>
                        <asp:ListItem Text="未收到" Value="1"></asp:ListItem>
                    </asp:DropDownList>
           
                <div align="right">
                  
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                    <asp:Button ID="Button1" runat="server" Text="导出EXCEL" BackColor="Yellow" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;
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
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>单据号
                    </td>
                    <td>日期
                    </td>
                    <td>项目编码
                    </td>
                    <td>项目名称
                    </td>
                    <td>客户名称
                    </td>
                    <td>经手人
                    </td>
                    <td>发票类型
                    </td>
                    <td>发票号
                    </td>
                    <td>制单人
                    </td>
                    <td>状态
                    </td>
                    <td>备注
                    </td>
                    <td>原发票
                    </td>
                    <td>原金额
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
                    <asp:LinkButton ID="lbtnReEdit" runat="server" CommandName="ReEdit" CommandArgument='<% #Eval("Id")%>'
                        OnClientClick='return confirm( "确定要重新提交此单据吗？") '>编辑</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id")%>'>查看</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Del" CommandArgument='<% #Eval("Id")%>'
                        OnClientClick='return confirm( "确定要删除吗？") '>删除</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="RuTime" HeaderText="日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="金额">
                <ItemTemplate>
                    <asp:Label ID="Total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目金额">
                <ItemTemplate>
                    <asp:Label ID="AllPoTotal" runat="server" Text='<%# Eval("AllPoTotal") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发票类型">
                <ItemTemplate>
                    <asp:Label ID="FPNoStyle" runat="server" Text='<%# Eval("FPNoStyle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发票号">
                <ItemTemplate>
                    <asp:Label ID="FPNo" runat="server" Text='<%# Eval("FPNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DoPer" HeaderText="经手人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="制单人">
                <ItemTemplate>
                    <asp:Label ID="CreateName" runat="server" Text='<%# Eval("CreateName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:Label ID="Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="原发票">
                <ItemTemplate>
                    <asp:Label ID="TopFPNo" runat="server" Text='<%# Eval("TopFPNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="原金额">
                <ItemTemplate>
                    <asp:Label ID="TopTotal" runat="server" Text='<%# Eval("TopTotal") %>'></asp:Label>
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
            <td>发票总额:<asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                项目总额:<asp:Label ID="lblAllPoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                <br />
                <br />
                发票总金额:<asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label><asp:Label
                    ID="Label2" runat="server" Text="项目总金额："></asp:Label><asp:Label ID="lblPOTotal" runat="server"
                        Text="0" ForeColor="Red"></asp:Label><br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>出库单号
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
                                <td>数量
                                </td>
                                <td>成本单价
                                </td>
                                <td>成本金额
                                </td>
                                <td>销售单价
                                </td>
                                <td>销售金额
                                </td>
                                <td>备注
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">---暂无数据---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <%-- <asp:BoundField DataField="SellOutPONO" HeaderText="出库单号" SortExpression="SellOutPONO" />--%>
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
                        <%--  <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本单价">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本总价">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
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
            </td>
        </tr>
    </table>
</asp:Content>
