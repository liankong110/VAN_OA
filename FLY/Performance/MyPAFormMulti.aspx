<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPAFormMulti.aspx.cs"
    Inherits="VAN_OA.Performance.MyPAFormMulti" MasterPageFile="~/DefaultMaster.Master"
    Title="绩效考核模版管理" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                绩效考核结果
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                姓名</td>
            <td class="style1">
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
            <td class="style1">
                部门</td>
            <td>
                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
            </td>
            <td>
                评定月份</td>
            <td>
                <asp:Label ID="lblMonth" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                出勤天数</td>
            <td class="style1">
                <asp:Label ID="lblAttendDays" runat="server"></asp:Label> 
            </td>
            <td class="style1">
                假期</td>
            <td>
                <asp:Label ID="lblLeaveDays" runat="server"></asp:Label> 
            </td>
            <td>
                全勤奖</td>
            <td>
                <asp:Label ID="lblFullAttendBonus" runat="server"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="6">
                &nbsp;
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="PAItemId" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvList_RowDataBound" ShowFooter="True">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    选择
                                </td>
                                <td>
                                    科目
                                </td>
                                <td>
                                    评分项
                                </td>
                                <td>
                                    分值
                                </td>
                                <td>
                                    奖惩金额
                                </td>
                                <td>
                                    是否初评
                                </td>
                                <td>
                                    初评人
                                </td>
                                <td>
                                    是否复评
                                </td>
                                <td>
                                    复评人
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
                        <asp:BoundField DataField="loginName" HeaderText="评定人" />
                        <asp:BoundField DataField="ReviewScore" HeaderText="众评值" />
                        <asp:BoundField DataField="Note" HeaderText="注释" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
        }
    </style>
</asp:Content>
