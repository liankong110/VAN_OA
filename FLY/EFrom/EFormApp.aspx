<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EFormApp.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.EFormApp" MasterPageFile="~/DefaultMaster.Master" Title="表单申请"%>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
     <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">表单申请</td>
    </tr>
   
</table><br>

    <asp:DataList ID="DataList1" runat="server" RepeatColumns="4" 
         onitemcommand="DataList1_ItemCommand" >
     <ItemTemplate>
      <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="0">
     <tr>
     <td>
         <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("pro_Type") %>' CommandArgument='<%# Eval("pro_Id") %>' CommandName="shenqi"></asp:LinkButton>
         </td>
         
         </tr>
         <tr>
                <td colspan="2"> <div style='border-top:1px dashed #cccccc;height: 1px;overflow:hidden;margin-top:2px;margin-bottom:7px'></div></td>
                </tr>
         </table>
        </ItemTemplate>
        <ItemStyle  Width="200px"/>
    </asp:DataList>
 
 </asp:Content>