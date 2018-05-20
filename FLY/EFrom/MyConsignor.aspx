<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyConsignor.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.MyConsignor"  MasterPageFile="~/DefaultMaster.Master" Title="我的委托"%>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">我的委托</td>
        </tr>
        <tr>
            <td>选择流程：</td>
            <td>  <asp:DropDownList ID="ddlProType" runat="server" Width="200px" DataTextField = "pro_Type" DataValueField="pro_Id">
           </asp:DropDownList>
           
                <asp:CheckBox ID="cbAll" runat="server"  Text="选择全部流程" 
                    oncheckedchanged="cbAll_CheckedChanged" AutoPostBack="true"/>
           </td>
        </tr>
        <tr>
             
            <td >
               被委托人:
            </td>
             <td >
               
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id" Width="200PX">
                </asp:DropDownList>
            
            </td>
        </tr>
        
        <tr>
             
            <td >
               有效期:
            </td>
             <td >
                生效日期:  <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox> 
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
                
                终止日期:<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
             <cc1:CalendarExtender  ID="CalendarExtender1" PopupButtonID="Image1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFrom">
             </cc1:CalendarExtender>
             <cc1:CalendarExtender  ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtTo">
             </cc1:CalendarExtender> 
                 <asp:CheckBox ID="cbYouXiao" runat="server"  Text="一直有效"/>
            </td>
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