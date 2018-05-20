<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePwd.aspx.cs" Inherits="VAN_OA.UpdatePwd"  MasterPageFile="~/DefaultMaster.Master"%>

<asp:Content ContentPlaceHolderID="SampleContent"  runat="server">

   <table style="border-left: 1px solid #999999;" cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">密码修改</td>
    </tr>
    <tr>
        <td>原始密码</td>
        <td>
            <asp:TextBox ID="txtOrgPwd" runat="server" TextMode="Password"></asp:TextBox> 
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"  ValidationGroup="a" ControlToValidate="txtOrgPwd"></asp:RequiredFieldValidator>
        </td>
        
       
        
    </tr>
    
    <tr>
    
    <td>新密码</td>
     <td>    
          <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txtNewPwd"></asp:RequiredFieldValidator>
    
     </td>
       
    </tr>
    
     <tr>
    
    <td>确认密码</td>
     <td>    
          
    <asp:TextBox ID="txtReNewPwd" runat="server" TextMode="Password"></asp:TextBox> 
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txtReNewPwd"></asp:RequiredFieldValidator>
     </td>
       
    </tr>
    <tr>
    
    <td colspan="2">
    <div align="right">
      <asp:Button ID="btnOK" runat="server" Text=" 保存 "  BackColor="Yellow" ValidationGroup="a" onclick="btnOK_Click"  
                  />&nbsp;</div>
    </td>
    </tr>
</table><br>
</asp:Content>