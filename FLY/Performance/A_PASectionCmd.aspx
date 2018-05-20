<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_PASectionCmd.aspx.cs" Inherits="VAN_OA.Performance.A_PASectionCmd" MasterPageFile="~/DefaultMaster.Master"  Title="绩效考核科目管理" %>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="50%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">绩效考核科目信息</td>
        </tr>
        <tr>
            <td>绩效考核科目名称：</td>
            <td><asp:TextBox ID="txtPASectionName" runat="server" Width="300px"></asp:TextBox></td>
        </tr>         
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" 
                    onclick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 "  BackColor="Yellow" 
                    onclick="btnUpdate_Click"/>&nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" 重置 "  BackColor="Yellow" 
                    onclick="btnSet_Click"/>&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>
 </asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    
</asp:Content>
