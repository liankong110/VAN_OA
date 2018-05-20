<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFHouseInfo.aspx.cs" Inherits="VAN_OA.BaseInfo.WFHouseInfo"  MasterPageFile="~/DefaultMaster.Master"  Title="商品档案"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
         <tr>
            <td colspan="2"  style=" height:20px; background-color:#336699; color:White;">仓库档案</td>
            
            </tr>
            
            
	<tr>
	<td height="25" width="30%" align="right">
		编码
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtGoodNo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>  
	</td></tr> 
	<tr>
	<td height="25" width="30%" align="right">
		名称
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtGoodName" runat="server" Width="200px"></asp:TextBox> <font style="color: #FF0000">*</font> 
	</td></tr>
	<tr>
	<td></td>
	<td   colspan="2" align="left">
        <asp:CheckBox ID="cbDefault" runat="server" Text="是否为默认仓库" />
		 
		 </tr>
	
	<tr>
	<td height="25" width="30%" align="right">
		备注
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtRemark" runat="server" Width="400px"></asp:TextBox>
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