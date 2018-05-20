<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExpInvsGoodListHisMan.aspx.cs"
    Inherits="VAN_OA.ReportForms.ExpInvsGoodListHisMan" Title="部门领料单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>后台系统登录</title>
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        AllowPaging="true" Width="100%" AutoGenerateColumns="False" PageSize="20" OnPageIndexChanging="gvList_PageIndexChanging"
        OnRowDataBound="gvList_RowDataBound">
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
                    <td>
                        领用事件号
                    </td>
                    <td>
                        借用人
                    </td>
                    <td>
                        领用时间
                    </td>
                    <td>
                        发放时间
                    </td>
                    <td>
                        货品名称
                    </td>
                    <td>
                        用途
                    </td>
                    <td>
                        使用状态
                    </td>
                    <td>
                        归还时间
                    </td>
                    <td>
                        备注
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
            <asp:BoundField DataField="EventNo" HeaderText="领用事件号" SortExpression="EventNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="loginName" HeaderText="借用人" SortExpression="loginName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpTime" HeaderText="领用时间" SortExpression="ExpTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="OutTime" HeaderText="发放时间" SortExpression="OutTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="InvName" HeaderText="货品名称" SortExpression="InvName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpUse" HeaderText="用途" SortExpression="ExpUse" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpState" HeaderText="状态" SortExpression="ExpState" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ReturnTime" HeaderText="归还时间" SortExpression="ReturnTime"
                DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpRemark" HeaderText="备注" SortExpression="ExpRemark"
                ItemStyle-HorizontalAlign="Center" />
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
    <input type="button" value="关闭" style="background-color: Yellow;" name="btnClose"
        onclick="javascript:parent.TINY.box.hide();" />
    </form>
</body>
</html>
