<%@ Page Language="C#" AutoEventWireup="true"  Culture="auto" UICulture="auto"  CodeBehind="BreakRulesCar.aspx.cs" Inherits="VAN_OA.ReportForms.breakRulesCar" MasterPageFile="~/DefaultMaster.Master" Title="车辆违章"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">车辆违章</td>
        </tr>
        <tr>
	<td height="25" width="30%" align="right">
		发现机关
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtJiGuan" runat="server" Width="300px"></asp:TextBox>
	    <font style="color:Red">*</font></td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		违章时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtBreakTime" runat="server" Width="300px" ></asp:TextBox>
		   <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
		     <cc1:CalendarExtender  ID="CalendarExtender1" PopupButtonID="Image1" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtBreakTime">
             </cc1:CalendarExtender>
	    <font style="color:Red">*</font></td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		违章地点
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtAddress" runat="server" Width="300px"></asp:TextBox>
	    <font style="color:Red">*</font></td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		违章行为
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtDothing" runat="server" Width="300px"></asp:TextBox>
	    <font style="color:Red">*</font></td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		罚款金额
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtTotal" runat="server" Width="300px"></asp:TextBox>
	    <font style="color:Red">*</font></td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		处理标志
	：</td>
	<td height="25" width="*" align="left">
        <asp:DropDownList ID="ddlState" runat="server" Width="300px">
           <asp:ListItem>未处理</asp:ListItem>
        <asp:ListItem>已处理</asp:ListItem>
        </asp:DropDownList>
		 
	    <font style="color:Red">*</font></td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		车牌号
	：</td>
	<td height="25" width="*" align="left">
	
        <asp:DropDownList ID="ddlCarNo" runat="server" DataTextField="CarNo" 
            DataValueField="CarNo" Width="300px" AutoPostBack="True" 
            onselectedindexchanged="ddlCarNo_SelectedIndexChanged">
        </asp:DropDownList>
	 
	    <font style="color:Red">*</font></td></tr>
 
 <tr>
     <td height="25" width="30%" align="right">
		扣分
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtScore" runat="server" Width="300px" Text="0"></asp:TextBox>
	    <font style="color:Red">*</font></td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		年检时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtNianJian" runat="server" Width="300px" ></asp:TextBox>
		   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
		     <cc1:CalendarExtender  ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtNianJian">
             </cc1:CalendarExtender>
	    </td></tr>
 
	
	<tr>
	<td height="25" width="30%" align="right">
		保险时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtBaoxian" runat="server" Width="300px" ></asp:TextBox>
		   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
		     <cc1:CalendarExtender  ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtBaoxian">
             </cc1:CalendarExtender>
	    </td></tr>
	<tr>
 
	
	
		<tr>
	<td height="25" width="30%" align="right">
		开车人
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtDriver" runat="server" Width="300px"></asp:TextBox>
	     </td></tr>
	<tr>
	
	
	<td height="25" width="30%" align="right">
		备注
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtRemark" runat="server" Width="95%" Height="100px" TextMode="MultiLine"></asp:TextBox>
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
