<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WF_Role.aspx.cs" Inherits="VAN_OA.WF_Role"  MasterPageFile="~/DefaultMaster.Master" Title="角色权限设置"%>

 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
     
     
    
     <table cellpadding="0" cellspacing="0" width="50%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">角色权限</td>
        </tr>
        <tr>
            <td>角色名称：</td>
            <td>
                <asp:DropDownList ID="ddlRoles" runat="server" Width="150px" 
                    AutoPostBack="True" onselectedindexchanged="ddlRoles_SelectedIndexChanged">
                </asp:DropDownList>
           </td>
        </tr>
        <tr>
            <td>权限：</td>
            <td>
            
            
                <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows" 
                    ShowCheckBoxes="All" ExpandDepth="0" 
                    onselectednodechanged="TreeView1_SelectedNodeChanged" 
                    ontreenodecheckchanged="TreeView1_TreeNodeCheckChanged">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                        HorizontalPadding="0px" VerticalPadding="0px" />
                    <Nodes>
                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="新建节点">
                            <asp:TreeNode Text="新建节点" Value="新建节点">
                                <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            </asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="新建节点">
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                    </Nodes>
                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                </asp:TreeView>
          </td>
        </tr>
         
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " BackColor="Yellow" onclick="btnSave_Click" 
                    />&nbsp;
                
            </td>
        </tr>
    </table>
     </ContentTemplate>
     </asp:UpdatePanel>
 </asp:Content>