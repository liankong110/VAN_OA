<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpInvs.aspx.cs" Inherits="VAN_OA.EFrom.ExpInvs"   MasterPageFile="~/DefaultMaster.Master" Title="�������õ�" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 
 
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="8" style=" height:20px; background-color:#336699; color:White;">�������õ�-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
        <td>�����¼���:</td>
        <td>
        <asp:Label ID="lblEventNo" runat="server" Text=""></asp:Label>
        </td>
           <td>�����ˣ�<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtName" ReadOnly="true" runat="server"></asp:TextBox></td>
            <td>���ţ�</td>
            <td><asp:TextBox ID="txtDepartMent" runat="server" ReadOnly="true" ></asp:TextBox></td>
             
           
           
            
        </tr>      
        
        <tr>
         <td >
                 �������ڣ�
             <font style="color:Red">*</font>
             </td>
            <td>  
           
                <asp:TextBox ID="txtCreateTime" runat="server"></asp:TextBox> <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
               <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd " TargetControlID="txtCreateTime" PopupButtonID="Image1">
                </cc1:CalendarExtender> 
                </td>
        <td>
       ����״̬:
        </td>
        <td >
            <asp:Label ID="lblOutState" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnOk" runat="server" Text="ȷ�Ϸ���" BackColor="Yellow" 
                style=" margin-left:100px;" onclick="btnOk_Click" />
        </td>
        <td>
        ��������:
        </td>
        
        <td>
            <asp:Label ID="lblOutTime" runat="server" Text=""></asp:Label>
            
        </td>
        </tr>
   
        
        <tr>
        
            <td colspan="8">
            
                <asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="100%"  AutoGenerateColumns="False" onrowdatabound="gvList_RowDataBound" 
                onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
       
       >        
         
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
                <td>
                �༭
                </td>   
                 <td>
                ɾ��
                </td>    
                
                <td>
                ��Ʒ����
                </td>   
                 <td>
                ����
                </td>        
                
                   <td>
                ��;
                </td>        
                
                <td>
                ʹ��״̬
                </td>
                   <td>
                ��ע
                </td>        
                
                   <td>
                �黹ʱ��
                </td>  
                
                
                 
                         
              </tr>
              <tr>
                  <td colspan="8" align="center" style="height:80%">---��������---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
        <asp:TemplateField HeaderText=" �༭">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="�༭"/>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="ɾ��">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"  CommandName="Delete" 
                    OnClientClick='return confirm( "ȷ��ɾ����") ' />
                </ItemTemplate>
                <ItemStyle  HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>             
            
 
			 
		<asp:BoundField DataField="InvId" HeaderText="��Ʒ����" SortExpression="InvId" ItemStyle-HorizontalAlign="Center"  Visible="false" /> 
		<asp:BoundField DataField="InvName" HeaderText="��Ʒ����" SortExpression="InvId" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="ExpNum" HeaderText="����" SortExpression="ExpNum" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ExpUse" HeaderText="��;" SortExpression="ExpUse" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="ExpState" HeaderText="ʹ��״̬" SortExpression="ExpState" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ExpRemark" HeaderText="��ע" SortExpression="ExpRemark" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="ReturnTime" HeaderText="�黹ʱ��" SortExpression="ReturnTime" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:yyyy-MM-dd}" /> 
        
		 

   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>

 <br />
 
  
            <asp:Panel ID="plProInvs" runat="server">
            
              <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">�������õ�</td>
        </tr>
        
        
        

	
 	 
	<tr>
	<td height="25"  align="right">
		��Ʒ����
	��</td>
	<td height="25" width="*" align="left">
		 
        <asp:DropDownList ID="ddlInvs" runat="server" Width="400px" DataTextField="InvName" DataValueField="ID">
        </asp:DropDownList>
		
	</td> 
	<td height="25"  align="right">
		����
	��</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtExpNum" runat="server" Width="200px"></asp:TextBox>
	</td> 
	<td height="25"  align="right">
		ʹ��״̬
	��</td>
	<td height="25" width="*" align="left">
	 
        <asp:DropDownList ID="ddlExpState" runat="server" Width="200px">
        <asp:ListItem Value="��˾" Selected="True">��˾</asp:ListItem>
         <asp:ListItem Value="˽��">˽��</asp:ListItem>
        </asp:DropDownList>
	</td></tr>
	<tr>
	<td height="25"  align="right" >
		��;
	��</td>
	<td height="25"  align="left" colspan="5">
		<asp:TextBox id="txtExpUse" runat="server" Width="95%"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" align="right" >
		��ע
	��</td>
	<td height="25"  align="left" colspan="5">
		<asp:TextBox id="txtExpRemark" runat="server" Width="95%"></asp:TextBox>
	</td></tr>
	
		<tr>
	<td height="25" align="right" >
        <asp:Label ID="lblReturnTime" runat="server" Text="�黹ʱ�䣺"></asp:Label>
	</td>
	<td height="25"  align="left" colspan="5">
		<asp:TextBox id="txtReturnTime" runat="server" Width="200px"></asp:TextBox>
		<asp:ImageButton ID="imgReturnTime" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
               <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtReturnTime" PopupButtonID="imgReturnTime">
                </cc1:CalendarExtender> 
	</td></tr>
	 
	
	
	   
	 
	 <tr>
                  <td colspan="6" align="center" style="height:80%"> 
                  
                  <asp:Button ID="btnAdd" 
                          runat="server" Text="���" BackColor="Yellow"  
                           Width="74px" onclick="btnAdd_Click" 
                    />&nbsp; &nbsp; &nbsp; &nbsp;
                      
                       <asp:Button ID="btnSave" 
                          runat="server" Text="����" BackColor="Yellow"  
                           Width="74px" onclick="btnSave_Click" ValidationGroup="a" 
                    />&nbsp; 
                      &nbsp;                    
                      
                      
                      <asp:Button ID="btnCancel" 
                          runat="server" Text="ȡ��" BackColor="Yellow" onclick="btnCancel_Click" Width="74px" 
                    />&nbsp; 
                      &nbsp;
                      
                         </td>
               </tr>
        </table>
        <br />
         <br /> <br />
            </asp:Panel>
            </td>
        </tr>
        
        
         <tr>
             <td>
                 <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
             </td>
            <td colspan="8">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        
        
        
        <tr>
             <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
             </td>
            <td colspan="8"> 
            
            
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserName">
                    <asp:ListItem Selected="True">ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
             <td>
                <asp:Label ID="lblYiJian" runat="server" Text="�����������:"></asp:Label>
             </td>
            <td colspan="8"> 
            
            
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
         
        <tr>
            <td colspan="8" align="center">
             <asp:Button ID="btnEdit" runat="server" Text="�޸�->����"  BackColor="Yellow" 
                    onclick="btnEdit_Click" />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
              <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow" 
                    onclick="Button1_Click" Width="51px" 
                     />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" ���� "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
                <br />
            </td>
        </tr>
    </table>
     <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
     
    
 </asp:Content>