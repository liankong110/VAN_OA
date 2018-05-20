<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFTeamInfoList.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFTeamInfoList" MasterPageFile="~/DefaultMaster.Master"
    Title="工程队长维护" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style>
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
            border-left: 1px solid #888;
            border-top: 1px solid #888;
        }
        th, td
        {
            border-right: 1px solid #888;
            border-bottom: 1px solid #888;
            padding: 1px 1px;
        }
        th
        {
            font-weight: bold;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                工程队长维护
            </td>
        </tr>
        <tr>
            <td>
                姓名：
            </td>
            <td>
                <asp:TextBox ID="txtTeamLever" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加工程队长" BackColor="Yellow" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AutoGenerateColumns="False" AllowPaging="true"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        编辑
                    </td>
                    <td>
                        删除
                    </td>
                    <td height="25" align="center">
                        姓名
                    </td>
                    <td height="25" align="center">
                        性别
                    </td>
                    <td height="25" align="center">
                        身份证号
                    </td>
                    <td height="25" align="center">
                        出生年月
                    </td>
                    <td height="25" align="center">
                        合作起始时间
                    </td>
                    <td height="25" align="center">
                        人员规模
                    </td>
                      <td height="25" align="center">
                        电话
                    </td>
                    <tr>
                        <td colspan="9" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
            <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="编辑" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                        CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="TeamLever" HeaderText="姓名" SortExpression="TeamLever"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Sex" HeaderText="性别" SortExpression="Sex" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CardNo" HeaderText="身份证号" SortExpression="CardNo" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="出生年月" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Eval("BrithdayYear")%>
                    -<%# Eval("BrithdayMonth")%>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:BoundField DataField="ContractStartTime" HeaderText="合作起始时间" SortExpression="ContractStartTime"
                DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="TeamPersonCount" HeaderText="人员规模" SortExpression="TeamPersonCount"
                ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField DataField="Phone" HeaderText="电话" SortExpression="Phone"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
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
