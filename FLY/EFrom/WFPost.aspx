<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFPost.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.WFPost" MasterPageFile="~/DefaultMaster.Master" Title="�ʼ��ĵ���ݱ�" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 <script type="text/javascript">
function show()
{   alert("1");
    document.getElementById("btnSub").disabled=false;
    
}

</script>
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">�ʼ��ĵ���ݱ�-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
        <td>������<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td>���ڣ�<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDateTime" runat="server"></asp:TextBox>
              <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
             <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateTime">
                </cc1:CalendarExtender>
            </td>
             
            
              
        </tr>
        <tr><td>
     ��Ŀ���:<font style="color:Red">*</font>
      </td>
      <td  colspan="3">
          <asp:TextBox ID="txtPONo" runat="server" Width="200px"  ReadOnly="true"></asp:TextBox >
             <asp:LinkButton ID="lbtnAddFiles" runat="server" 
          OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOListNO1.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')" 
          ForeColor="Red" onclick="LinkButton1_Click1" >
       ѡ��</asp:LinkButton>
      </td>
    
      
      </tr>
        <tr>
         <td >
      ��Ŀ����:
        </td>
        <td>
          <asp:TextBox ID="txtPOName" runat="server" Width="95%" ReadOnly="true"></asp:TextBox>
            </td>
         
              <td >�ͻ����ƣ�
              </td><td >
               <asp:TextBox ID="txtPOGuestName" runat="server" Width="95%" ReadOnly="true" ></asp:TextBox>       </td>    
            </tr>      
        
        
        <tr>
       
            
        <td>�ʼĵ�ַ��<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtPostAddress" runat="server"  Width="95%"></asp:TextBox>
         
            
            </td>
            <td>�ռ��ˣ�
             <font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtToPer" runat="server"></asp:TextBox>
            
             
            </td>
               
              
        </tr>
        <tr>
             <td>
             �绰��<font style="color:Red">*</font>
             </td>
            <td >
                <asp:TextBox ID="txtTel" runat="server" Width="95%"></asp:TextBox>
            </td>
            
             <td >
            
             �������ƣ� <font style="color:Red">*</font></td>
            <td >
                <asp:TextBox ID="txtWuliuName" runat="server" Width="95%"></asp:TextBox>                
            </td>
        </tr>
        
        <tr>
            <td>
            �ļ��ˣ�
            </td>
            <td>
             <asp:TextBox ID="txtFromPer" runat="server" Width="95%"></asp:TextBox>
            </td>
            
             <td >
             ������ţ�
             </td>
            <td  >
                <asp:TextBox ID="txtPostCode" runat="server" Width="95%"></asp:TextBox>
                
            </td>
        </tr>
        
        <tr>
             <td >
             ��ע��
             </td>
            <td colspan="3">
                <asp:TextBox ID="txtremark" runat="server" Width="95%"  TextMode="MultiLine" Height="100px"></asp:TextBox>                
            </td>
            
            
        </tr>
        
          <tr>
          
          <td colspan="6">
              <asp:Panel ID="plEmail" runat="server">
             
          <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
             <tr>
            <td colspan="5" style=" height:20px; background-color:#D7E8FF; ">�ʼķ�</td>
        </tr>
              
        <tr>
        
        <td >
          ���ݣ� 
        </td>
        <td colspan="4">
             <asp:TextBox ID="txtPostContext" runat="server" style="width:95%" ></asp:TextBox>  
        </td>
        </tr>
        
        
               <tr>
        
        <td>
            <asp:CheckBox ID="cbPostFrom" runat="server"  Text="�ĳ�"  style="margin-right:10px"/>  ������
        </td>
        <td>
            <asp:TextBox ID="txtPostFromAddress" runat="server" Width="220px"></asp:TextBox>
        </td>
        
         <td >
            <asp:CheckBox ID="cbPostTo" runat="server"  Text="����"  style="margin-right:10px"/>  
       
            <asp:TextBox ID="txtPostToAddress" runat="server" Width="220px"></asp:TextBox>������
        </td>
        
        
        <td>
        ��
        </td>
        <td >
            <asp:TextBox ID="txtPostTotal" runat="server"></asp:TextBox>Ԫ
        </td>
        
        </tr>    
        
        
           <tr>
            <td  >��ע</td>    <td colspan="4" >
                <asp:TextBox ID="txtPostRemark" runat="server" style="width:95%"></asp:TextBox>
            
            </td>
        </tr>
          </table>
          
           </asp:Panel>
          </td>
          </tr>
        
        
         <tr>
             <td>
                 <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
             </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        
        
        
        <tr>
             <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
             </td>
            <td colspan="6"> 
            
            
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
            <td colspan="6"> 
            
            
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
         
        <tr>
            <td colspan="6" align="center">
                &nbsp;
              <asp:Button ID="btnBaoXiao" runat="server" Text="�ύ��������" BackColor="Yellow"  OnClientClick="return confirm( 'ȷ��Ҫ�ύ��') "
                   Width="100px" onclick="btnBaoXiao_Click" 
                     />
            &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
              <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow" 
                    onclick="Button1_Click" Width="51px"  OnClientClick="return confirm( 'ȷ��Ҫ�ύ��') "
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

