<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DioSellOutOrderList.aspx.cs"
    Inherits="VAN_OA.JXC.DioSellOutOrderList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>销售出库信息</title>
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ScriptManager>
        <div>
            <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                border="1">
                <tr>
                    <td colspan="4" style="height: 20px; background-color: #336699; color: White;">销售出库信息
                    </td>
                </tr>
                <tr>
                    <td>项目编号:
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                    </td>
                    <td>项目名称:
                    </td>
                    <td>
                        <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">商品编码:
                        <asp:TextBox ID="txtGoodNo1" runat="server" Width="100px"></asp:TextBox>名称/小类/规格:
                        <asp:TextBox ID="txtNameOrTypeOrSpec1" runat="server" Width="100PX"></asp:TextBox>
                        或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo1" runat="server" Width="100PX"></asp:TextBox>
                        数量：  
                        <asp:DropDownList ID="ddlFuHao" runat="server">
                            <asp:ListItem Text="=" Value="="></asp:ListItem>
                            <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                            <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                            <asp:ListItem Text=">" Value=">"></asp:ListItem>
                            <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtGoodNum" runat="server" Width="100px"></asp:TextBox>
                        单位：
    <asp:DropDownList ID="ddlGoodUnit" runat="server">
    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtsmallChenBen" runat="server" Width="100px"></asp:TextBox>
                        <asp:DropDownList ID="ddlSmallChenBen" runat="server">
                            <asp:ListItem Text="=" Value="="></asp:ListItem>
                            <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                            <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                            <asp:ListItem Text=">" Value=">"></asp:ListItem>
                            <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        </asp:DropDownList>
                        成本单价：
                   <asp:DropDownList ID="ddlFuHao1" runat="server">
                       <asp:ListItem Text="=" Value="="></asp:ListItem>
                       <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                       <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                       <asp:ListItem Text=">" Value=">"></asp:ListItem>
                       <asp:ListItem Text="<" Value="<"></asp:ListItem>
                   </asp:DropDownList>
                        <asp:TextBox ID="txtChenBen" runat="server" Width="100px"></asp:TextBox>

                        <asp:TextBox ID="txtSmallSellPrice" runat="server" Width="100px"></asp:TextBox>
                        <asp:DropDownList ID="ddlSmallSellPrice" runat="server">
                            <asp:ListItem Text="=" Value="="></asp:ListItem>
                            <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                            <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                            <asp:ListItem Text=">" Value=">"></asp:ListItem>
                            <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        </asp:DropDownList>
                        销售单价：
                   <asp:DropDownList ID="ddlBigSellPrice" runat="server">
                       <asp:ListItem Text="=" Value="="></asp:ListItem>
                       <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                       <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                       <asp:ListItem Text=">" Value=">"></asp:ListItem>
                       <asp:ListItem Text="<" Value="<"></asp:ListItem>
                   </asp:DropDownList>
                        <asp:TextBox ID="txtBigSellPrice" runat="server" Width="100px"></asp:TextBox>

                    </td>
                    </tr>
                <tr>
                    <td colspan="4">出库时间：
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
                        单据号: 
                        <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
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
            <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnPageIndexChanging="gvList_PageIndexChanging" OnRowEditing="gvList_RowEditing"
                OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand">
                <PagerTemplate>
                    <br />
                    <%--  <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id") %>'>选择</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="RuTime" HeaderText="日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                        DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Supplier" HeaderText="客户名称" SortExpression="Supplier"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="DoPer" HeaderText="经手人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
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
        </div>
    </form>
</body>
</html>
