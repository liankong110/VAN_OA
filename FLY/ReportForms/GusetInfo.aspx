<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="GusetInfo.aspx.cs" Inherits="VAN_OA.ReportForms.GusetInfo" MasterPageFile="~/DefaultMaster.Master" Title="客户资料"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">客户资料</td>
        </tr>
        <tr>
            <td align="right">客户名称：</td>
            <td><asp:TextBox ID="txtGuestName" Width="300px" runat="server"></asp:TextBox> 
                <font style="color:Red">*</font></td>
        </tr>
        <tr>
            <td align="right">注册电话：</td>
            <td><asp:TextBox ID="txtZhuTel" Width="300px"  runat="server"></asp:TextBox>
               </td>
        </tr>
           <tr>
            <td align="right">联系人：</td>
            <td><asp:TextBox ID="txtLinkMan" runat="server" Width="300px"></asp:TextBox>
                <font style="color:Red">*</font></td>
        </tr>
           <tr>
            <td align="right">联系电话1：</td>
            <td><asp:TextBox ID="txttel1" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
           <tr>
            <td align="right"> 联系电话2：</td>
            <td><asp:TextBox ID="txttel2" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
           <tr>
            <td align="right">账期：</td>
            <td><asp:TextBox ID="txtAccount" runat="server" Width="300px"   ></asp:TextBox>
            
            <font style="color:Red">
                *</font>
                
              
                </td>
        </tr>
        
          
         
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" 
                    onclick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 "  BackColor="Yellow" 
                      onclick="btnUpdate_Click"/>&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>
 </asp:Content>
