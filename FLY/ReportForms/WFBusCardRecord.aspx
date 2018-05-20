<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFBusCardRecord.aspx.cs" Inherits="VAN_OA.ReportForms.WFBusCardRecord"  MasterPageFile="~/DefaultMaster.Master" Title="公交卡充值记录"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="server">
<table  cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
<tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">公交卡充值记录</td>
    </tr>

	<tr>
	<td height="25" width="30%" align="right">
		公交卡号
	：</td>
	<td height="25" width="*" align="left">
	 
        <asp:DropDownList ID="ddlCardNo" runat="server" Width="200px" DataTextField="CardNo" DataValueField="CardNo">
        </asp:DropDownList>
	</td></tr>
	 
	<tr>
	<td height="25" width="30%" align="right">
		充值日期
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtBusCardDate" runat="server" Width="200px"  ></asp:TextBox><asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/><cc1:CalendarExtender
            ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtBusCardDate" PopupButtonID="Image1">
        </cc1:CalendarExtender>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		金额
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtBusCardTotal" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		备注
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtBusCardRemark" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	
	 <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" 
                    onclick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 "  BackColor="Yellow" 
                      onclick="btnUpdate_Click"/>&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
</table>

</asp:Content>