<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFInventory.aspx.cs" Inherits="VAN_OA.BaseInfo.WFInventory" MasterPageFile="~/DefaultMaster.Master"  Title="存货档案"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
         <tr>
            <td colspan="4"  style=" height:20px; background-color:#336699; color:White;">存货档案</td>
            
            </tr>
	<tr>
	<td height="25" width="30%" align="right">
		存货名称
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		数量
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvNum" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		单位
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvUnit" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		序列号
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtInvNo" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		使用人
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtUser" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	 <tr>

      <tr>
            <td height="25" width="30%" align="right">
                仓位(区域货架号-层数-部位)：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlArea" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
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
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
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