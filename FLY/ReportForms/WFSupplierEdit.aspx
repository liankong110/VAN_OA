<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="WFSupplierEdit.aspx.cs" Inherits="VAN_OA.ReportForms.WFSupplierEdit" MasterPageFile="~/DefaultMaster.Master" Title="供应商联系跟踪"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="4" style=" height:20px; background-color:#336699; color:White;">供应商申请表-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        
        	<tr>
	<td height="25" width="30%" align="right">
		登记日期
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtTime" runat="server" Width="270px"  onfocus="setday(this)"></asp:TextBox>
		 <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
		  <cc1:CalendarExtender  ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1" TargetControlID="txtTime">
             </cc1:CalendarExtender><font style=" color:Red">*</font>
	</td>
	 
	<td height="25" width="30%" align="right">
		联系人网址
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierHttp" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		供应商名称
	：</td>
	<td height="25" width="*" align="left" >
		<asp:TextBox id="txtSupplierName" runat="server" Width="95%" ></asp:TextBox><font style=" color:Red">*</font>
	</td>
	<td height="25" width="30%" align="right">
		<font style=" color:Red">*</font>助记词
	：</td>
	<td height="25" width="*" align="left" >
		<asp:TextBox id="txtZhuJi" runat="server" Width="95%"></asp:TextBox>
	</td>
	</tr>
	
	<tr>
	<td height="25" width="30%" align="right">
		<font style=" color:Red">*</font>供应商简称
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierSimpleName" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		供应商税务登记号
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierShui" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		电话/手机
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtPhone" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		供应商工商注册号
	：</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtSupplierGong" runat="server" Width="300px"></asp:TextBox>
	</td>
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		联系人<font style=" color:Red">*</font>
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtLikeMan" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		银行账号
	：</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtSupplierBrandNo" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		职务<font style=" color:Red">*</font>
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtJob" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		开户行
	：</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtSupplierBrandName" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		传真或邮箱
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtFoxOrEmail" runat="server" Width="300px"></asp:TextBox>
	</td>
	
	<td height="25" width="30%" align="right"  rowspan="5">
		主营范围
	：</td>
	<td height="25" width="*" align="left" rowspan="5">
        <asp:TextBox ID="txtMainRange" runat="server" TextMode="MultiLine" Width="95%" Height="120PX"></asp:TextBox>		    
		    </td>
	 
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		是否留资料
	：</td>
	<td height="25" width="*" align="left">
		<asp:CheckBox ID="chkIfSave" Text="是否留资料" runat="server" Checked="False" />
	</td>
	 
	 
	 
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		QQ/MSN联系
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtQQMsn" runat="server" Width="300px"></asp:TextBox>
	</td>
	
	 
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		供应商ID
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierId" runat="server" Width="300px"  ReadOnly="true"></asp:TextBox>
	</td>
	
	 
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		联系人地址
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierAddress" runat="server" Width="300px"></asp:TextBox>
	</td>
	 
	
	</tr>
	
	
	
 
	
	<tr>
	<td height="25" width="30%" align="right" >
		备注
	：</td>
	<td height="25" width="*" align="left"  colspan="3">
		 <asp:TextBox id="txtRemark" runat="server" Width="800px" ></asp:TextBox>
	</td>
	
 </tr>
 
 
        
         
        <tr>
            <td colspan="4" align="center">
                &nbsp;
                    
                        &nbsp;
                      
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 "  BackColor="Yellow" 
                      onclick="btnUpdate_Click"/>&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>
 </asp:Content>
