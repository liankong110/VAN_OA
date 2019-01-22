<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFFin_JieDate.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFFin_JieDate" MasterPageFile="~/DefaultMaster.Master"
    Title="年结算日设置" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">年结算日设置
            </td>
        </tr>

        <tr>
            <td height="25" width="30%" align="right">年份  ：
            </td>
            <td height="25" width="*" align="left">
                 
                <asp:DropDownList ID="txtYear" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">日期 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtJDate" runat="server" Width="200px"></asp:TextBox>
                  <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                   <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtJDate">
                </cc1:CalendarExtender>

            </td>
        </tr>
       
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
