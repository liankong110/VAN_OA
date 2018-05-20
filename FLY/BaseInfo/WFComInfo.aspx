<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFComInfo.aspx.cs" Inherits="VAN_OA.BaseInfo.WFComInfo" MasterPageFile="~/DefaultMaster.Master" Title="公司信息" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="server">

<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">公司信息</td>
        </tr>
        	<tr>
	<td height="25" width="30%" align="right">
		公司名称
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtComName" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		纳税人识别号
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtNaShuiNo" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		地址/电话
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtAddress" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		开户行及帐号
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtComBrand" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		开票抬头
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvoHeader" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		联系人
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvContactPer" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		注册地址
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvAddress" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		注册电话
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvTel" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		纳税人登记号
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtNaShuiPer" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
    <tr>
	<td height="25" width="30%" align="right">
		开户行
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtBrand" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<tr>
	<td height="25" width="30%" align="right">
		帐号
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtbrandNo" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		公司电话
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtComTel" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		公司传真
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtComChuanZhen" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		业务电话
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtComBusTel" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
         
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text="保存" BackColor="Yellow" 
                    onclick="btnAdd_Click" Width="80px" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="保存"  BackColor="Yellow" 
                      onclick="btnUpdate_Click" Width="80px"/>&nbsp;
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
 
