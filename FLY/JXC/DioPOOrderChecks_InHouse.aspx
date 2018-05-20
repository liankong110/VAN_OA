﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DioPOOrderChecks_InHouse.aspx.cs"
    Inherits="VAN_OA.JXC.DioPOOrderChecks_InHouse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>项目信息</title>
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                    采购订单检验查询
                </td>
            </tr>
            <tr>
                <td>
                    项目编号:
                </td>
                <td colspan="1">
                    <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                </td>
                <td>
                    项目名称:
                </td>
                <td>
                    <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
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
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvList_PageIndexChanging"
            OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand">
            <PagerTemplate>
                <br />
                <%--<asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            单据号
                        </td>
                        <td>
                            项目编码
                        </td>
                        <td>
                            项目名称
                        </td>
                        <td>
                            客户名称
                        </td>
                        <td>
                            供应商
                        </td>
                        <td>
                            检验时间
                        </td>
                        <td>
                            采购人
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
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<% #Eval("PONo") %>'>选择</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单据号">
                    <ItemTemplate>
                        <asp:Label ID="CaiProNo" runat="server" Text='<%# Eval("CaiProNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目编码">
                    <ItemTemplate>
                        <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目名称">
                    <ItemTemplate>
                        <asp:Label ID="POName" runat="server" Text='<%# Eval("POName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="客户名称">
                    <ItemTemplate>
                        <asp:Label ID="GuestName" runat="server" Text='<%# Eval("GuestName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供应商">
                    <ItemTemplate>
                        <asp:Label ID="SupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CheckTime" HeaderText="检验时间" SortExpression="CheckTime"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="采购人">
                    <ItemTemplate>
                        <asp:Label ID="CaiGouPer" runat="server" Text='<%# Eval("CaiGouPer") %>'></asp:Label>
                    </ItemTemplate>
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
    </div>
    </form>
</body>
</html>
