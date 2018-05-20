<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="MyProjectInvsList.aspx.cs"
    Inherits="VAN_OA.ReportForms.MyProjectInvsList" MasterPageFile="~/DefaultMaster.Master"
    Title="工程材料审计清单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                我的工程材料审计清单
            </td>
        </tr>
        <tr>
            <td>
                日期:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Image1"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                项目编码
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                项目名称
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtProName" runat="server" Width="200px"></asp:TextBox>
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
    <asp:Label ID="lbl" runat="server" Text="总计:" ForeColor="Red"></asp:Label><asp:Label
        ID="lblTotal" runat="server" Text="0"></asp:Label>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        日期
                    </td>
                    <td>
                        项目编码
                    </td>
                    <td>
                        项目名称
                    </td>
                    <td>
                        购买日期
                    </td>
                    <td>
                        材料型号
                    </td>
                    <td>
                        材料名称
                    </td>
                    <td>
                        单位
                    </td>
                    <td>
                        数量
                    </td>
                    <td>
                        材料费
                    </td>
                    <td>
                        运费
                    </td>
                    <td>
                        会务费
                    </td>
                    <td>
                        管理费
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
            <asp:BoundField DataField="CreateTime" HeaderText="日期" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ProNo" HeaderText="项目编码" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ProName" HeaderText="项目名称" SortExpression="ProName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="客户代表" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BuyTime" HeaderText="购买日期" SortExpression="BuyTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="InvModel" HeaderText="材料型号" SortExpression="InvModel"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvName" HeaderText="材料名称" SortExpression="InvName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvUnit" HeaderText="单位" SortExpression="InvUnit" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvNum" HeaderText="数量" SortExpression="InvNum" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvPrice" HeaderText="材料费" SortExpression="InvPrice" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvCarPrice" HeaderText="运费" SortExpression="InvCarPrice"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvTaskPrice" HeaderText="会务费" SortExpression="InvTaskPrice"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="InvManPrice" HeaderText="管理费" SortExpression="InvManPrice"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Total" HeaderText="小计" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="EfState" HeaderText="状态" SortExpression="EfState" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
