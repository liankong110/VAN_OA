<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToolsApp.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.ToolsApp"  MasterPageFile="~/DefaultMaster.Master" Title="工 具 申 请 单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 <script type="text/javascript">
function show()
{   alert("1");
    document.getElementById("btnSub").disabled=false;
    
}
</script>
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">工具申请单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>部门：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDepartName" runat="server"></asp:TextBox></td>
              
            
              <td>姓名：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>
            
            
            <td>日期：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDateTime" runat="server"></asp:TextBox>
             <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd " TargetControlID="txtDateTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
             <td>
             工具名称：<font style="color:Red">*</font>
             </td>
            <td colspan="5">
                <asp:TextBox ID="txtToolName" runat="server" Width="85%"></asp:TextBox>
            </td>
        </tr>
        
         <tr>
             <td>
             工具用途：
             <font style="color:Red">*</font></td>
            <td colspan="5">
                <asp:TextBox ID="txtToolUse" runat="server" Width="85%"></asp:TextBox>
            </td>
        </tr>
        
      
        
         <tr>
             <td>
                 <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
             </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        
        
        
        <tr>
             <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
             </td>
            <td colspan="6"> 
            
            
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserName">
                    <asp:ListItem Selected="True">通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
             <td>
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见:"></asp:Label>
             </td>
            <td colspan="6"> 
            
            
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%"></asp:TextBox>
            </td>
        </tr>
         
        <tr>
            <td colspan="6" align="center">
            
              <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" 
                    onclick="Button1_Click" Width="51px" 
                     />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
                <br />
            </td>
        </tr>
    </table>
     <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    
 </asp:Content>
