<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Folder.aspx.cs" Inherits="VAN_OA.Folder"  MasterPageFile="~/DefaultMaster.Master" Title="文件夹管理" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    
    <ContentTemplate>
    
    
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="3" style=" height:20px; background-color:#336699; color:White;">文件夹信息</td>
        </tr>
        <tr>
        <td style="vertical-align:top; width:60%" rowspan="2"> 
            <asp:TreeView ID="tvMain" runat="server"  
                onselectednodechanged="tvMain_SelectedNodeChanged">
            </asp:TreeView>
            
            
            </td>
<td  style="vertical-align:top">
            
            <table  cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >           
            
            <tr>
            <td>上级文件夹名称：</td>
            <td>
                <asp:Label ID="lblParent" runat="server" Text="" style="color:Red"></asp:Label></td>
            <asp:Label ID="lblPareId" runat="server" Text="Label"></asp:Label>
             <asp:Label ID="lblSonId" runat="server" Text="Label"></asp:Label>
            
            </tr>
            
             <tr>
             <td>文件夹名称：</td>
            <td><asp:TextBox ID="txtFolder" runat="server"></asp:TextBox></td>
         </tr>
         
         <tr>
         <td colspan="2">
             <asp:Button ID="btnAdd" runat="server" Text="添加" Width="65px" BackColor="Yellow" 
                 onclick="btnAdd_Click" />
          
            
             <asp:Button ID="btnUpdate" runat="server" Text="修改"  Width="65px"  BackColor="Yellow" 
                 onclick="btnUpdate_Click"/>             
              <asp:Button ID="btnDelete" runat="server" Text="删除"  Width="65px" 
                 OnClientClick="return confirm( '确定删除吗？')" onclick="btnDelete_Click" BackColor="Yellow" />              
             <asp:Button ID="btnSave" runat="server" Text="保存"  Width="65px"  BackColor="Yellow" 
                 onclick="btnSave_Click"/>
             <asp:Button ID="btnCancel" runat="server" Text="取消"  Width="65px" BackColor="Yellow" 
                 onclick="btnCancel_Click" />
             <asp:Label ID="lblMess" runat="server" Text="" style="color:Red"></asp:Label>
         </tr>
            
            </table>
            </td>
            
        </tr>
        
        
        
    </table>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

 
