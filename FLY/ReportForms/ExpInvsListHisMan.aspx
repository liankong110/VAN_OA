<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExpInvsListHisMan.aspx.cs"
    Inherits="VAN_OA.ReportForms.ExpInvsListHisMan" MasterPageFile="~/DefaultMaster.Master"
    Title="部门领料单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                部门领料管理
            </td>
        </tr>
        <tr>
            <td>
                货品名称
            </td>
            <td>
                <asp:DropDownList ID="ddlInvs" runat="server" Width="400px" DataTextField="InvName"
                    DataValueField="ID">
                </asp:DropDownList>
            </td>
            <td>
                序列号
            </td>
            <td>
                <asp:TextBox ID="txtInvNo" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                借出人
            </td>
            <td>
                <asp:TextBox ID="txtExpUser" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                归还状态
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="300px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>未归还</asp:ListItem>
                    <asp:ListItem>已经归还</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" AllowPaging="true" Width="100%" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
        <PagerTemplate>
            <br />
            <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                        货品名称
                    </td>
                    <td>
                        序列号
                    </td>
                    <td>
                        借出数量
                    </td>
                    <td>
                        领用时间
                    </td>
                    <td>
                        用途
                    </td>
                    <td>
                        使用状态
                    </td>
                    <td>
                        借出人
                    </td>
                    <td>
                        备注
                    </td>
                    <td>
                        归还状态
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
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="InvName" HeaderText="货品名称" SortExpression="InvId" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="InvNo" HeaderText="序列号" SortExpression="InvNo" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ExpNum" HeaderText="借出数量" SortExpression="ExpNum" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpTime" HeaderText="领用时间" SortExpression="ExpTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ExpUse" HeaderText="用途" SortExpression="ExpUse" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpState" HeaderText="使用状态" SortExpression="ExpState"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="LoginName" HeaderText="借出人" SortExpression="LoginName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpRemark" HeaderText="备注" SortExpression="ExpRemark"
                ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ExpInvState" HeaderText="归还状态" SortExpression="ExpInvState"
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
</asp:Content>
