<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="PO.aspx.cs" Inherits="VAN_OA.ReportForms.PO" MasterPageFile="~/DefaultMaster.Master" Title="采购流水账"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">采购流水账</td>
        </tr>
        <tr>
            <td align="right">日期：</td>
            <td><asp:TextBox ID="txtDataTime" Width="300px" runat="server"></asp:TextBox> 
              <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
            <cc1:CalendarExtender  ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDataTime" PopupButtonID="Image1">
             </cc1:CalendarExtender>
                <font style="color:Red">*</font></td>
        </tr>
        <tr>
            <td align="right">往来单位：</td>
            <td><asp:TextBox ID="txtUnitName" Width="300px"  runat="server"></asp:TextBox>
                <font style="color:Red">*</font></td>
        </tr>
           <tr>
            <td align="right">商品名称：</td>
            <td><asp:TextBox ID="txtInvName" runat="server" Width="300px"></asp:TextBox>
                <font style="color:Red">*</font></td>
        </tr>
           <tr>
            <td align="right">单位：</td>
            <td><asp:TextBox ID="txtUnit" runat="server" Width="300px"></asp:TextBox><font style="color:Red">*</font></td>
        </tr>
           <tr>
            <td align="right">数量：</td>
            <td><asp:TextBox ID="txtNum" runat="server" Width="300px" AutoPostBack="True" 
                    ontextchanged="txtNum_TextChanged"></asp:TextBox><font style="color:Red">*</font></td>
        </tr>
           <tr>
            <td align="right">单价：</td>
            <td><asp:TextBox ID="txtPrice" runat="server" Width="300px" AutoPostBack="True" 
                    ontextchanged="txtPrice_TextChanged"></asp:TextBox><font style="color:Red">
                *</font></td>
        </tr>
        
            <tr>
            <td align="right"> 小计：</td>
            <td><asp:TextBox ID="txtTotal"  ReadOnly="true" runat="server" Width="300px"></asp:TextBox>
                <font style="color:Red">*</font></td>
        </tr>
        
             <tr>
            <td align="right">销售人员：</td>
            <td><asp:TextBox ID="txtSeller" runat="server" Width="300px"></asp:TextBox><font style="color:Red">
                *</font></td>
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
