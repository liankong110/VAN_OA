<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachmentTreeView.aspx.cs" Inherits="VAN_OA.AttachmentTreeView"  Title="文档管理" MasterPageFile="~/DefaultMaster.Master"%>

 <asp:Content ContentPlaceHolderID="SampleContent" runat="server">
     <asp:Label ID="Label1" runat="server" Text="搜索内容:"></asp:Label><asp:TextBox ID="txtCon"
         runat="server"></asp:TextBox>   <asp:Button ID="btnSea" runat="server" 
         Text="搜索"  BackColor="Yellow" onclick="btnSea_Click"  />
   <asp:TreeView ID="tvMain" runat="server"  
                onselectednodechanged="tvMain_SelectedNodeChanged">
            </asp:TreeView>
 </asp:Content>