<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCAI_OrderInHouse.aspx.cs" Inherits="VAN_OA.JXC.WFCAI_OrderInHouse"   Culture="auto" UICulture="auto"  MasterPageFile="~/DefaultMaster.Master" Title="����������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
  
  
  
  
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="4" style=" height:20px; background-color:#336699; color:White;">�ɹ����-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>      
          
        <tr> 
              <td>�����ˣ�</td>
            <td><asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>     <font style="color:Red">*</font>       
            
        </td>
      <td>
           ����:
      </td>
      <td>
          <asp:TextBox ID="txtRuTime" runat="server" Width="200px" ></asp:TextBox >
          
          <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
          <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRuTime"  Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1">
          </cc1:CalendarExtender>
      </td>
     
        </tr>
        
             <tr> 
              <td>��Ӧ�̣�</td>
            <td ><asp:TextBox ID="txtSupplier" runat="server" Width="200px"  ReadOnly="true"  ></asp:TextBox>   
                     
            <font style="color:Red">*</font>
        </td>
        <td>
           ���鵥��:
      </td>
      <td>
          <asp:TextBox ID="txtChcekProNo" runat="server" Width="200px" 
              AutoPostBack="True" ontextchanged="txtChcekProNo_TextChanged"    ReadOnly="true"></asp:TextBox ><asp:LinkButton ID="lbtnAddFiles" runat="server" 
          OnClientClick="javascript:window.showModalDialog('DioPOOrderChecks_InHouse.aspx',null,'dialogWidth:600px;dialogHeight:450px;help:no;status:no')" 
          ForeColor="Red" onclick="LinkButton1_Click1" >
     ѡ��</asp:LinkButton>
      </td>
        </tr>
        
             <tr> 
              <td>�ֿ⣺</td>
            <td > 
                <asp:DropDownList ID="ddlHouse" DataTextField="houseName" DataValueField="id" runat="server" Width="200px">
                </asp:DropDownList>
            <font style="color:Red">*</font></td>
       <td>��Ŀ���룺</td>
            <td ><asp:TextBox ID="txtPONo" runat="server" Width="200px"    ReadOnly="true"></asp:TextBox>   
                     
        </td>
        </tr>
        
             <tr> 
             <td>
           ��Ʊ��:
      </td>
      <td>
          <asp:TextBox ID="txtFPNo" runat="server"  Width="200px"   ></asp:TextBox >
      </td>
       <td>
           ��Ŀ����:
      </td>
      <td>
          <asp:TextBox ID="txtPOName" runat="server"  ReadOnly="true" Width="200px"   ></asp:TextBox >
          <asp:Label ID="lblGuestName" runat="server" Text="" Visible="false"></asp:Label>
      </td>
      
      
       </tr>
        
        
        
    
        
                <tr> 
                 <td>�ɹ��ˣ�</td>
            <td ><asp:TextBox ID="txtCaiGou" runat="server" Width="200px"  ReadOnly="true"></asp:TextBox>       </td>
            
              <td>��ע��</td>
            <td ><asp:TextBox ID="txtRemark" runat="server" Width="95%" ></asp:TextBox>       </td>
      
      
        </tr>
        
        <tr>
        <td colspan="4" >
        <div  align="right" style="width:100%;" >
       
                </div>
        </td>
        </tr>
         
        <tr>
        <td  colspan="4">
     
            
      <br />
 <asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Ids"  
          Width="100%"  AutoGenerateColumns="False" onrowdatabound="gvList_RowDataBound" 
                onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing"  ShowFooter="true"
       
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
                                   ����
                                </td>
                
                   <td>
                ����
                </td>   
                    <td>
                С��
                </td> 
                 <td>
                ���
                </td>   
                  <td>
                �ͺ�
                </td> 
                  <td>
                ��λ
                </td>   
                     
                
                   <td>
                ����
                </td>        
                
             
                
                
                 
                   <td>
                ����
                </td>  
                
                
                 
                   <td>
                ���
                </td>  
                
                
                    <td>
                ��ע
                </td>  
                                           
              </tr>
              <tr>
                  <td colspan="11" align="center" style="height:80%">---��������---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
      
             <asp:TemplateField HeaderText="ɾ��">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"  CommandName="Delete" 
                    OnClientClick='return confirm( "ȷ��ɾ����") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="40px" />
             </asp:TemplateField>
		
			
		  <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo"  /> 
	 	<asp:TemplateField HeaderText="����">		
		<ItemTemplate>
            <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		  
		<asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName"  /> 
		<asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec"  /> 
	<%--	<asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model"  /> --%>
		<asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit"  /> 
		 
			<asp:TemplateField HeaderText="����">		
		<ItemTemplate>
              <asp:TextBox ID="txtNum" runat="server"  Text='<%# Eval("GoodNum") %>' Width="50px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtNum"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtNum" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
     
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
		<asp:TemplateField HeaderText="����">		
		<ItemTemplate>
            <asp:Label ID="lbVislNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblVisNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
	 
		 
		
			<asp:TemplateField HeaderText="����">		
		<ItemTemplate>
            <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
			 
			
			<asp:TemplateField HeaderText="�ܼ�">		
		<ItemTemplate>
            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
			
			<asp:TemplateField HeaderText="��ע">		
		<ItemTemplate>        
            <asp:TextBox ID="txtGoodRemark" runat="server"  Text='<%# Eval("GoodRemark") %>' Width="95%"></asp:TextBox>
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		<asp:BoundField DataField="GoodRemark" HeaderText="��ע" SortExpression="GoodRemark"  /> 
		 
	
		<asp:BoundField DataField="QingGouPer" HeaderText="�빺��" SortExpression="QingGouPer"  /> 
		
		
	 
		
		
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
              <FooterStyle BackColor="#D7E8FF" />
</asp:GridView>
 
 
 

 
           
    </td>
    </tr>
     
      
        
        
         
        
        
         <tr>
             <td>
                 <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
             </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        
        
        
        
        
        <tr>
             <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
             </td>
            <td colspan="3"> 
            
            
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
            <td colspan="3"> 
            
            
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
         
        <tr>
            <td colspan="4" align="center">
                &nbsp;
                     &nbsp;
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
    <br />
    
  
  
  </asp:Content>