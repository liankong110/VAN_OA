<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainExcelList.aspx.cs"
    Inherits="VAN_OA.MyExcels.MainExcelList" MasterPageFile="~/DefaultMaster.Master"
    Title="项目类别" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                Excel 列表
            </td>
        </tr>
        <tr>
            <td>
                名称：
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                  <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
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
                        删除
                    </td>
                    <td height="25" align="center">
                        名称
                    </td>
                    <td height="25" align="center">
                        sheet
                    </td>

                    <td height="25" align="center">
                        创建时间
                    </td>
                     <td height="25" align="center">
                        创建人
                    </td>
                    <td height="25" align="center">
                        备注
                    </td>
                    <tr>
                        <td colspan="4" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
            <%--  <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="编辑" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                        CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" ItemStyle-HorizontalAlign="Left" />
               <asp:BoundField DataField="SheetName" HeaderText="Sheet" SortExpression="SheetName" ItemStyle-HorizontalAlign="Left" />
                  <asp:BoundField DataField="UpStateString" HeaderText="状态" SortExpression="UpStateString" ItemStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="UserName" HeaderText="创建人" SortExpression="UserName"
                ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Left" />
                   <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark"
                ItemStyle-HorizontalAlign="Left" />
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
