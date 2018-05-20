<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFPost.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.WFPost" MasterPageFile="~/DefaultMaster.Master" Title="邮寄文档快递表" %>

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
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">邮寄文档快递表-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
        <td>姓名：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td>日期：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtDateTime" runat="server"></asp:TextBox>
              <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
             <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateTime">
                </cc1:CalendarExtender>
            </td>
             
            
              
        </tr>
        <tr><td>
     项目编号:<font style="color:Red">*</font>
      </td>
      <td  colspan="3">
          <asp:TextBox ID="txtPONo" runat="server" Width="200px"  ReadOnly="true"></asp:TextBox >
             <asp:LinkButton ID="lbtnAddFiles" runat="server" 
          OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOListNO1.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')" 
          ForeColor="Red" onclick="LinkButton1_Click1" >
       选择</asp:LinkButton>
      </td>
    
      
      </tr>
        <tr>
         <td >
      项目名称:
        </td>
        <td>
          <asp:TextBox ID="txtPOName" runat="server" Width="95%" ReadOnly="true"></asp:TextBox>
            </td>
         
              <td >客户名称：
              </td><td >
               <asp:TextBox ID="txtPOGuestName" runat="server" Width="95%" ReadOnly="true" ></asp:TextBox>       </td>    
            </tr>      
        
        
        <tr>
       
            
        <td>邮寄地址：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtPostAddress" runat="server"  Width="95%"></asp:TextBox>
         
            
            </td>
            <td>收件人：
             <font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtToPer" runat="server"></asp:TextBox>
            
             
            </td>
               
              
        </tr>
        <tr>
             <td>
             电话：<font style="color:Red">*</font>
             </td>
            <td >
                <asp:TextBox ID="txtTel" runat="server" Width="95%"></asp:TextBox>
            </td>
            
             <td >
            
             物流名称： <font style="color:Red">*</font></td>
            <td >
                <asp:TextBox ID="txtWuliuName" runat="server" Width="95%"></asp:TextBox>                
            </td>
        </tr>
        
        <tr>
            <td>
            寄件人：
            </td>
            <td>
             <asp:TextBox ID="txtFromPer" runat="server" Width="95%"></asp:TextBox>
            </td>
            
             <td >
             物流编号：
             </td>
            <td  >
                <asp:TextBox ID="txtPostCode" runat="server" Width="95%"></asp:TextBox>
                
            </td>
        </tr>
        
        <tr>
             <td >
             备注：
             </td>
            <td colspan="3">
                <asp:TextBox ID="txtremark" runat="server" Width="95%"  TextMode="MultiLine" Height="100px"></asp:TextBox>                
            </td>
            
            
        </tr>
        
          <tr>
          
          <td colspan="6">
              <asp:Panel ID="plEmail" runat="server">
             
          <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
             <tr>
            <td colspan="5" style=" height:20px; background-color:#D7E8FF; ">邮寄费</td>
        </tr>
              
        <tr>
        
        <td >
          内容： 
        </td>
        <td colspan="4">
             <asp:TextBox ID="txtPostContext" runat="server" style="width:95%" ></asp:TextBox>  
        </td>
        </tr>
        
        
               <tr>
        
        <td>
            <asp:CheckBox ID="cbPostFrom" runat="server"  Text="寄出"  style="margin-right:10px"/>  苏州至
        </td>
        <td>
            <asp:TextBox ID="txtPostFromAddress" runat="server" Width="220px"></asp:TextBox>
        </td>
        
         <td >
            <asp:CheckBox ID="cbPostTo" runat="server"  Text="到付"  style="margin-right:10px"/>  
       
            <asp:TextBox ID="txtPostToAddress" runat="server" Width="220px"></asp:TextBox>至苏州
        </td>
        
        
        <td>
        金额：
        </td>
        <td >
            <asp:TextBox ID="txtPostTotal" runat="server"></asp:TextBox>元
        </td>
        
        </tr>    
        
        
           <tr>
            <td  >备注</td>    <td colspan="4" >
                <asp:TextBox ID="txtPostRemark" runat="server" style="width:95%"></asp:TextBox>
            
            </td>
        </tr>
          </table>
          
           </asp:Panel>
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
                &nbsp;
              <asp:Button ID="btnBaoXiao" runat="server" Text="提交至报销单" BackColor="Yellow"  OnClientClick="return confirm( '确定要提交吗？') "
                   Width="100px" onclick="btnBaoXiao_Click" 
                     />
            &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
              <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" 
                    onclick="Button1_Click" Width="51px"  OnClientClick="return confirm( '确定要提交吗？') "
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

