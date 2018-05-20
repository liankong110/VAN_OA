<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFInventoryList.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFInventoryList" MasterPageFile="~/DefaultMaster.Master"
    Title="存货档案" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                存货查询
            </td>
        </tr>
        <tr>
            <td>
                货品名称：
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                &nbsp; 仓位:
                <asp:DropDownList ID="ddlArea" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="A">A</asp:ListItem>
                    <asp:ListItem Value="B">B</asp:ListItem>
                    <asp:ListItem Value="C">C</asp:ListItem>
                    <asp:ListItem Value="D">D</asp:ListItem>
                    <asp:ListItem Value="E">E</asp:ListItem>
                    <asp:ListItem Value="F">F</asp:ListItem>
                    <asp:ListItem Value="G">G</asp:ListItem>
                    <asp:ListItem Value="H">H</asp:ListItem>
                    <asp:ListItem Value="I">I</asp:ListItem>
                    <asp:ListItem Value="J">J</asp:ListItem>
                    <asp:ListItem Value="K">K</asp:ListItem>
                    <asp:ListItem Value="L">L</asp:ListItem>
                    <asp:ListItem Value="M">M</asp:ListItem>
                    <asp:ListItem Value="N">N</asp:ListItem>
                    <asp:ListItem Value="O">O</asp:ListItem>
                    <asp:ListItem Value="P">P</asp:ListItem>
                    <asp:ListItem Value="Q">Q</asp:ListItem>
                    <asp:ListItem Value="R">R</asp:ListItem>
                    <asp:ListItem Value="S">S</asp:ListItem>
                    <asp:ListItem Value="T">T</asp:ListItem>
                    <asp:ListItem Value="U">U</asp:ListItem>
                    <asp:ListItem Value="V">V</asp:ListItem>
                    <asp:ListItem Value="W">W</asp:ListItem>
                    <asp:ListItem Value="X">X</asp:ListItem>
                    <asp:ListItem Value="Y">Y</asp:ListItem>
                    <asp:ListItem Value="Z">Z</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlNumber" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlRow" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlCol" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加存货档案" BackColor="Yellow" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="gvList_PageIndexChanging"
        OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" OnDataBinding="gvList_DataBinding"
        OnRowDataBound="gvList_RowDataBound">
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
                        存货名称
                    </td>
                    <td height="25" align="center">
                        数量
                    </td>
                    <td height="25" align="center">
                        单位
                    </td>
                    <td height="25" align="center">
                        序列号
                    </td>
                     <td height="25" align="center">
                        使用人
                    </td>
                    <tr>
                        <td colspan="9" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
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
            <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvName" HeaderText="存货名称" SortExpression="InvName" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="InvNum" HeaderText="数量" SortExpression="InvNum" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="InvUnit" HeaderText="单位" SortExpression="InvUnit" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="InvNo" HeaderText="序列号" SortExpression="InvNo" ItemStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="InvUser" HeaderText="使用人" SortExpression="InvUser" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
