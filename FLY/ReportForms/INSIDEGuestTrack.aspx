<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="INSIDEGuestTrack.aspx.cs" Inherits="VAN_OA.ReportForms.INSIDEGuestTrack" MasterPageFile="~/DefaultMaster.Master" Title="�ͻ���ϵ����"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">INSIDE�ͻ���ϵ����-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
	<td height="25" width="30%" align="right">
		�ͻ�����
	��</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblGuestName" runat="server"  Width="300px"></asp:Label>
	</td></tr>
	
	
	<tr>
	<td height="25" width="30%" align="right">
		 �ϼ������۶�
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtGuestTotal" runat="server" Width="300px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		�ϼ�������
	��</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtGuestLiRun" runat="server" Width="300px"></asp:TextBox>
	</td>
	
	
	
	</tr>
	 
	 <tr>
	<td height="25" width="30%" align="right">
		�ϼ����տ���/��
	��</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtGuestDays" runat="server" Width="300px"></asp:TextBox>
	</td>
	</tr>
	
 
	
	
	 <tr>
	<td height="25" width="30%" align="right">
		AE
	��</td>
	<td height="25" width="*" align="left">
		  <asp:Label id="lblAE" runat="server"></asp:Label></td>
	</tr>
	
	
	 <tr>
	<td height="25" width="30%" align="right">
		��ϵ��
	��</td>
	<td height="25" width="*" align="left">&nbsp;
		  <asp:Label id="lblContrPer" runat="server"></asp:Label></td>
	</tr>
	 <tr>
	<td height="25" width="30%" align="right">
		�绰
	��</td>
	<td height="25" width="*" align="left">&nbsp;
		  <asp:Label id="lblTel" runat="server"></asp:Label></td>
	</tr>
	 <tr>
	<td height="25" width="30%" align="right">
		�ʼ�
	��</td>
	<td height="25" width="*" align="left">&nbsp;
		  <asp:Label id="lblEMail" runat="server"></asp:Label></td>
	</tr>
	
	 <tr>
	<td height="25" width="30%" align="right">
		QQ/MSN
	��</td>
	<td height="25" width="*" align="left">&nbsp;
		  <asp:Label id="lblQQ" runat="server"></asp:Label></td>
	</tr>
	
	
 <tr>
	<td height="25" width="30%" align="right">
		INSIDE��ע
	��</td>
	<td height="25" width="*" align="left">
		 <asp:TextBox id="txtINSIDERemark" runat="server" Width="600px"></asp:TextBox>
	</td>
	</tr>
          
         
        <tr>
            <td colspan="2" align="center">
               <asp:Button ID="btnUpdate" runat="server" Text=" �޸� "  BackColor="Yellow" 
                      onclick="btnUpdate_Click"/>&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>
 </asp:Content>
