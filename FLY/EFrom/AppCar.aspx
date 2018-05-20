<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppCar.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.AppCar" MasterPageFile="~/DefaultMaster.Master" Title="申请用车单" %>


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
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">申请用车单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>部门：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDepartName" runat="server"></asp:TextBox></td>
          
            
              <td>姓名：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>
            
            
                <td>申请日期：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDateTime" runat="server"></asp:TextBox>
                   <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
             <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="Image1" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtDateTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
        <td>
        
           <asp:RadioButton ID="rdoDan" runat="server"  Text="单程" GroupName="a"/>
            <asp:RadioButton ID="rdoWang" runat="server"  Text="往返" GroupName="a"/></td>
             <td>
             具体地址：
             </td>
            <td colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
             <td >
             公司名称：
             <font style="color:Red">*</font></td>
            <td colspan="2">
                <asp:TextBox ID="txtCompName" runat="server"></asp:TextBox>
                
            </td>
            
                <td >
             货物名称：
             </td>
            <td colspan="2">
                <asp:TextBox ID="txtInvName" runat="server"></asp:TextBox>
                
            </td>
        </tr>
        
         <tr>
             <td>
             随车人：
             </td>
            <td colspan="5">
                <asp:TextBox ID="txtsuiChePer" runat="server" Height="100px" Width="99%"></asp:TextBox>
                
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

