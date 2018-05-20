<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovePAFormList.aspx.cs"
    Inherits="VAN_OA.Performance.ApprovePAFormList" MasterPageFile="~/DefaultMaster.Master"
    Title="绩效考核模版管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                评估绩效考核
            </td>
        </tr>
        <tr>
            <td>
                用户
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" Height="16px" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Value ="">--选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                年月
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" Height="16px" AppendDataBoundItems="True">
                <asp:ListItem Value ="">--选择--</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server">
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
                <asp:Button ID="btnQuery" runat="server" BackColor="Yellow" Text="查询" 
                    onclick="btnQuery_Click" />
                <asp:CheckBox ID="Chk_All" runat="server" Text="全部显示" />
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="PAFormID" Width="100%" AutoGenerateColumns="False" OnRowEditing="gvList_RowEditing"
        OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        详细
                    </td>
                    <td>
                        用户名
                    </td>
                    <td>
                        年月
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
            <asp:TemplateField HeaderText="详细">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Approve.png"
                        CommandName="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="loginName" HeaderText="用户名">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField HeaderText="年月" DataField="Month" />
            <asp:BoundField DataField="Status" HeaderText="状态" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
