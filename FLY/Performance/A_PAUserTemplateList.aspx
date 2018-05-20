<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_PAUserTemplateList.aspx.cs"
    Inherits="VAN_OA.Performance.A_PAUserTemplateList" MasterPageFile="~/DefaultMaster.Master"
    Title="绩效考核模版管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                用户绩效考核模版管理
            </td>
        </tr>
        <tr>
            <td>
                复制年月
            </td>
            <td>
                &nbsp;<asp:DropDownList ID="ddlYear" runat="server" Height="16px">
                </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="">--选择--</asp:ListItem>
                    <asp:ListItem Value="01">01</asp:ListItem>
                    <asp:ListItem Value="02">02</asp:ListItem>
                    <asp:ListItem Value="03">03</asp:ListItem>
                    <asp:ListItem Value="04">04</asp:ListItem>
                    <asp:ListItem Value="05">05</asp:ListItem>
                    <asp:ListItem Value="06">06</asp:ListItem>
                    <asp:ListItem Value="07">07</asp:ListItem>
                    <asp:ListItem Value="08">08</asp:ListItem>
                    <asp:ListItem Value="09">09</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSelectAll" runat="server" Text="全选" BackColor="Yellow" OnClick="btnSelectAll_Click" />
                <asp:Button ID="btnUnSelectAll" runat="server" Text="全不选" BackColor="Yellow" OnClick="btnUnSelectAll_Click" />
                <asp:Button ID="btnDisSelect" runat="server" Text="反选" BackColor="Yellow" OnClick="btnDisSelect_Click" />
                <asp:Button ID="btnCopy" runat="server" Text="复制" BackColor="Yellow" OnClick="btnCopy_Click" />
            </td>
        </tr>
    </table>
    <br>
    如果用户没有绩效考核模版，请先在“绩效考核模版”中复制。
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id,PATemplateID" Width="50%" AutoGenerateColumns="False" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        编辑
                    </td>
                    <td>
                        删除
                    </td>
                    <td>
                        模版名称
                    </td>
                    <td>
                        选择
                    </td>
                    <td>
                        用户名
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
            <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="编辑" Width="16px" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" Enabled='<%# (DataBinder.Eval(Container.DataItem, "PATemplateID").ToString()!=""? true:false)%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模版名称">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlTemplateID" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="">--选择--</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="loginName" HeaderText="用户名">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
