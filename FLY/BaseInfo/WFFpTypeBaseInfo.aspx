<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFFpTypeBaseInfo.aspx.cs" Inherits="VAN_OA.BaseInfo.WFFpTypeBaseInfo"  MasterPageFile="~/DefaultMaster.Master"  Title="商品档案"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
         <tr>
            <td colspan="2"  style=" height:20px; background-color:#336699; color:White;">发票类型</td>
            
            </tr>
            
            
	<tr>
		<tr>
	<td height="25" width="30%" align="right">
		发票类型
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtFpType" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		税率
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtTax" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	
	<tr>
	<td height="25" width="30%" align="right">
		发票长度
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtFpLength" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	
	 <tr>
   <td align="center" colspan="2">   <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" 
                    onclick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 "  BackColor="Yellow" 
                    onclick="btnUpdate_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
        
	
</table>

</asp:Content>