<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverGoods.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.DeliverGoods" MasterPageFile="~/DefaultMaster.Master" Title="外出送货单" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 <script type="text/javascript">
function show()
{   alert("1");
    document.getElementById("btnSub").disabled=false;
    
}
</script>

   <%--  <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>--%>
     
      <%-- Enter Time in this format: 99:99:99 (type 'A' or 'P' to switch between AM/PM).<br />
        <asp:TextBox ID="TextBox3" runat="server" Width="130px" Height="16px" ValidationGroup="Demo1" /><br />
        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
            TargetControlID="TextBox3" 
            Mask="99:99"
            MessageValidatorTip="true"
            OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError"
            MaskType="Time"
            AcceptAMPM="True"
            CultureName="en-US" />
        <cc1:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
            ControlExtender="MaskedEditExtender3"
            ControlToValidate="TextBox3"
            IsValidEmpty="False"
            EmptyValueMessage="Time is required"
            InvalidValueMessage="Time is invalid"
            ValidationGroup="Demo1"
            Display="Dynamic"
            TooltipMessage="Input a time" />
        <br />
        
        <asp:ValidationSummary runat="Server" ValidationGroup="Demo1" ID="validationSummary" ShowSummary="true" />
     <asp:Button ID="Button1" runat="server" Text="Button" 
         onclick="Button1_Click1" />--%>
        
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">外出送货单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
         <td>姓名：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtName" ReadOnly="true" runat="server"></asp:TextBox></td>
            
            <td>部门：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDepartName" runat="server"></asp:TextBox></td>
              <td>申请日期：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDateTime" runat="server" ReadOnly="true"></asp:TextBox>           
            </td>
            
             
        </tr>
        
         <tr>
             <td>
             送货人：
             </td>
            <td colspan="5">
                <asp:TextBox ID="txtSongHuo"   Width="95%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr >
             <td rowspan="2">
             公司名称：<font style="color:Red">*</font></td>
            <td colspan="3"  rowspan="2">
                <asp:TextBox ID="txtCompName"   Width="95%" runat="server" Height="40px" TextMode="MultiLine"></asp:TextBox>
            </td>
             <td >
             外出时间：</td>
            <td>
             <asp:TextBox ID="txtGoTime" runat="server" ></asp:TextBox>
              
                
            </td>
        </tr>
        
        <tr>
               <td >
             回来时间：
             </td>
            <td>
             <asp:TextBox ID="txtBackTime" runat="server"></asp:TextBox>
             
                
            </td>
        </tr>
        
         <tr>
             <td>
             货物名称：
             <font style="color:Red">*</font></td>
            <td colspan="2">
                <asp:TextBox ID="txtInvName" runat="server"  Width="99%"></asp:TextBox>
                
            </td>
            
              <td>
             具体地址：
             <font style="color:Red">*</font></td>
            <td colspan="2">
                <asp:TextBox ID="txtAddress" runat="server"  Width="95%"></asp:TextBox>
                
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
            
            
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
         
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnEdit" runat="server" Text="修改->保存"  BackColor="Yellow" 
                    onclick="btnEdit_Click" />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
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

