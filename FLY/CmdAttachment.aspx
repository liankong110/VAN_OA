<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmdAttachment.aspx.cs" Inherits="VAN_OA.CmdAttachment" MasterPageFile="~/DefaultMaster.Master" Title="文档管理"%>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">文档管理</td>
        </tr>
        <tr>
            <td>资料名称：</td>
            <td><asp:TextBox ID="txtAttName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>资料文件夹：</td>
            <td>
                <asp:TreeView ID="tvMain" runat="server" 
                    onselectednodechanged="tvMain_SelectedNodeChanged">
                </asp:TreeView>
                
                选中文件夹： <asp:Label ID="lblParent" runat="server" Text="" style="color:Red"></asp:Label>
                 <asp:Label ID="lblFolderId" runat="server" Text="Label"></asp:Label>
                &nbsp;</td>
        </tr>
        
         <tr>
            <td>文件：</td>
            <td><asp:FileUpload ID="fuAttach"
                    runat="server" />
                <asp:LinkButton ID="lblAttName" runat="server"></asp:LinkButton>
                    </td>
        </tr>
            <tr>
            <td>版本号：</td>
            <td><asp:TextBox ID="txtVersion" runat="server"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td>描述：</td>
            <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" 
                    onclick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 "  BackColor="Yellow" 
                    onclick="btnUpdate_Click"/>&nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>
 </asp:Content>
