<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="WFSupplierEdit.aspx.cs" Inherits="VAN_OA.ReportForms.WFSupplierEdit" MasterPageFile="~/DefaultMaster.Master" Title="��Ӧ����ϵ����"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="4" style=" height:20px; background-color:#336699; color:White;">��Ӧ�������-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        
        	<tr>
	<td height="25" width="30%" align="right">
		�Ǽ�����
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtTime" runat="server" Width="270px"  onfocus="setday(this)"></asp:TextBox>
		 <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
		  <cc1:CalendarExtender  ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1" TargetControlID="txtTime">
             </cc1:CalendarExtender><font style=" color:Red">*</font>
	</td>
	 
	<td height="25" width="30%" align="right">
		��ϵ����ַ
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierHttp" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		��Ӧ������
	��</td>
	<td height="25" width="*" align="left" >
		<asp:TextBox id="txtSupplierName" runat="server" Width="95%" ></asp:TextBox><font style=" color:Red">*</font>
	</td>
	<td height="25" width="30%" align="right">
		<font style=" color:Red">*</font>���Ǵ�
	��</td>
	<td height="25" width="*" align="left" >
		<asp:TextBox id="txtZhuJi" runat="server" Width="95%"></asp:TextBox>
	</td>
	</tr>
	
	<tr>
	<td height="25" width="30%" align="right">
		<font style=" color:Red">*</font>��Ӧ�̼��
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierSimpleName" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		��Ӧ��˰��ǼǺ�
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierShui" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		�绰/�ֻ�
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtPhone" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		��Ӧ�̹���ע���
	��</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtSupplierGong" runat="server" Width="300px"></asp:TextBox>
	</td>
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		��ϵ��<font style=" color:Red">*</font>
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtLikeMan" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		�����˺�
	��</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtSupplierBrandNo" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		ְ��<font style=" color:Red">*</font>
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtJob" runat="server" Width="300px"></asp:TextBox>
	</td>
	<td height="25" width="30%" align="right">
		������
	��</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtSupplierBrandName" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		���������
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtFoxOrEmail" runat="server" Width="300px"></asp:TextBox>
	</td>
	
	<td height="25" width="30%" align="right"  rowspan="5">
		��Ӫ��Χ
	��</td>
	<td height="25" width="*" align="left" rowspan="5">
        <asp:TextBox ID="txtMainRange" runat="server" TextMode="MultiLine" Width="95%" Height="120PX"></asp:TextBox>		    
		    </td>
	 
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		�Ƿ�������
	��</td>
	<td height="25" width="*" align="left">
		<asp:CheckBox ID="chkIfSave" Text="�Ƿ�������" runat="server" Checked="False" />
	</td>
	 
	 
	 
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		QQ/MSN��ϵ
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtQQMsn" runat="server" Width="300px"></asp:TextBox>
	</td>
	
	 
	
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		��Ӧ��ID
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierId" runat="server" Width="300px"  ReadOnly="true"></asp:TextBox>
	</td>
	
	 
	</tr>
	<tr>
	<td height="25" width="30%" align="right">
		��ϵ�˵�ַ
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplierAddress" runat="server" Width="300px"></asp:TextBox>
	</td>
	 
	
	</tr>
	
	
	
 
	
	<tr>
	<td height="25" width="30%" align="right" >
		��ע
	��</td>
	<td height="25" width="*" align="left"  colspan="3">
		 <asp:TextBox id="txtRemark" runat="server" Width="800px" ></asp:TextBox>
	</td>
	
 </tr>
 
 
        
         
        <tr>
            <td colspan="4" align="center">
                &nbsp;
                    
                        &nbsp;
                      
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� "  BackColor="Yellow" 
                      onclick="btnUpdate_Click"/>&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>
 </asp:Content>
