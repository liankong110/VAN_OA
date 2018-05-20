<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FINPropertyList.aspx.cs" Inherits="VAN_OA.BaseInfo.FINPropertyList"
    MasterPageFile="~/DefaultMaster.Master" Title="费用类型列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
               费用类型列表
            </td>
        </tr>
        <tr>
            <td>
                类型：
            </td>
            <td>
                <asp:TextBox ID="txtCostType" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                属性：
            </td>
            <td>
                <asp:DropDownList ID="ddlPro" runat="server">
                 <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                <asp:ListItem Text="公共" Value="公共"></asp:ListItem>
                 <asp:ListItem Text="个性" Value="个性"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加属性信息" BackColor="Yellow" OnClick="btnAdd_Click" />
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
                    <td height="25" align="center">
                        流水号
                    </td>
                    <td height="25" align="center">
                        类型
                    </td>
                    <td height="25" align="center">
                        属性
                    </td>                    
                    <tr>
                        <td colspan="3" align="center" style="height: 80%">
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
         	<asp:BoundField DataField="ProNo" HeaderText="流水号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="CostType" HeaderText="类型" SortExpression="CostType" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="MyProperty" HeaderText="属性" SortExpression="MyProperty" ItemStyle-HorizontalAlign="Center"  /> 

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
