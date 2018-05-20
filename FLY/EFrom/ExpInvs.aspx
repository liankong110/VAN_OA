<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpInvs.aspx.cs" Inherits="VAN_OA.EFrom.ExpInvs"   MasterPageFile="~/DefaultMaster.Master" Title="部门领用单" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 
 
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="8" style=" height:20px; background-color:#336699; color:White;">部门领用单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
        <td>领用事件号:</td>
        <td>
        <asp:Label ID="lblEventNo" runat="server" Text=""></asp:Label>
        </td>
           <td>申请人：<font style="color:Red">*</font></td>
            <td><asp:TextBox ID="txtName" ReadOnly="true" runat="server"></asp:TextBox></td>
            <td>部门：</td>
            <td><asp:TextBox ID="txtDepartMent" runat="server" ReadOnly="true" ></asp:TextBox></td>
             
           
           
            
        </tr>      
        
        <tr>
         <td >
                 领用日期：
             <font style="color:Red">*</font>
             </td>
            <td>  
           
                <asp:TextBox ID="txtCreateTime" runat="server"></asp:TextBox> <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
               <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd " TargetControlID="txtCreateTime" PopupButtonID="Image1">
                </cc1:CalendarExtender> 
                </td>
        <td>
       发货状态:
        </td>
        <td >
            <asp:Label ID="lblOutState" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnOk" runat="server" Text="确认发货" BackColor="Yellow" 
                style=" margin-left:100px;" onclick="btnOk_Click" />
        </td>
        <td>
        发放日期:
        </td>
        
        <td>
            <asp:Label ID="lblOutTime" runat="server" Text=""></asp:Label>
            
        </td>
        </tr>
   
        
        <tr>
        
            <td colspan="8">
            
                <asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="100%"  AutoGenerateColumns="False" onrowdatabound="gvList_RowDataBound" 
                onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
       
       >        
         
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
                <td>
                编辑
                </td>   
                 <td>
                删除
                </td>    
                
                <td>
                货品名称
                </td>   
                 <td>
                数量
                </td>        
                
                   <td>
                用途
                </td>        
                
                <td>
                使用状态
                </td>
                   <td>
                备注
                </td>        
                
                   <td>
                归还时间
                </td>  
                
                
                 
                         
              </tr>
              <tr>
                  <td colspan="8" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
        <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑"/>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"  CommandName="Delete" 
                    OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle  HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>             
            
 
			 
		<asp:BoundField DataField="InvId" HeaderText="货品名称" SortExpression="InvId" ItemStyle-HorizontalAlign="Center"  Visible="false" /> 
		<asp:BoundField DataField="InvName" HeaderText="货品名称" SortExpression="InvId" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="ExpNum" HeaderText="数量" SortExpression="ExpNum" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ExpUse" HeaderText="用途" SortExpression="ExpUse" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="ExpState" HeaderText="使用状态" SortExpression="ExpState" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ExpRemark" HeaderText="备注" SortExpression="ExpRemark" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="ReturnTime" HeaderText="归还时间" SortExpression="ReturnTime" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:yyyy-MM-dd}" /> 
        
		 

   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>

 <br />
 
  
            <asp:Panel ID="plProInvs" runat="server">
            
              <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">部门领用单</td>
        </tr>
        
        
        

	
 	 
	<tr>
	<td height="25"  align="right">
		货品名称
	：</td>
	<td height="25" width="*" align="left">
		 
        <asp:DropDownList ID="ddlInvs" runat="server" Width="400px" DataTextField="InvName" DataValueField="ID">
        </asp:DropDownList>
		
	</td> 
	<td height="25"  align="right">
		数量
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtExpNum" runat="server" Width="200px"></asp:TextBox>
	</td> 
	<td height="25"  align="right">
		使用状态
	：</td>
	<td height="25" width="*" align="left">
	 
        <asp:DropDownList ID="ddlExpState" runat="server" Width="200px">
        <asp:ListItem Value="公司" Selected="True">公司</asp:ListItem>
         <asp:ListItem Value="私人">私人</asp:ListItem>
        </asp:DropDownList>
	</td></tr>
	<tr>
	<td height="25"  align="right" >
		用途
	：</td>
	<td height="25"  align="left" colspan="5">
		<asp:TextBox id="txtExpUse" runat="server" Width="95%"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" align="right" >
		备注
	：</td>
	<td height="25"  align="left" colspan="5">
		<asp:TextBox id="txtExpRemark" runat="server" Width="95%"></asp:TextBox>
	</td></tr>
	
		<tr>
	<td height="25" align="right" >
        <asp:Label ID="lblReturnTime" runat="server" Text="归还时间："></asp:Label>
	</td>
	<td height="25"  align="left" colspan="5">
		<asp:TextBox id="txtReturnTime" runat="server" Width="200px"></asp:TextBox>
		<asp:ImageButton ID="imgReturnTime" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
               <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtReturnTime" PopupButtonID="imgReturnTime">
                </cc1:CalendarExtender> 
	</td></tr>
	 
	
	
	   
	 
	 <tr>
                  <td colspan="6" align="center" style="height:80%"> 
                  
                  <asp:Button ID="btnAdd" 
                          runat="server" Text="添加" BackColor="Yellow"  
                           Width="74px" onclick="btnAdd_Click" 
                    />&nbsp; &nbsp; &nbsp; &nbsp;
                      
                       <asp:Button ID="btnSave" 
                          runat="server" Text="保存" BackColor="Yellow"  
                           Width="74px" onclick="btnSave_Click" ValidationGroup="a" 
                    />&nbsp; 
                      &nbsp;                    
                      
                      
                      <asp:Button ID="btnCancel" 
                          runat="server" Text="取消" BackColor="Yellow" onclick="btnCancel_Click" Width="74px" 
                    />&nbsp; 
                      &nbsp;
                      
                         </td>
               </tr>
        </table>
        <br />
         <br /> <br />
            </asp:Panel>
            </td>
        </tr>
        
        
         <tr>
             <td>
                 <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
             </td>
            <td colspan="8">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        
        
        
        <tr>
             <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
             </td>
            <td colspan="8"> 
            
            
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
            <td colspan="8"> 
            
            
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
         
        <tr>
            <td colspan="8" align="center">
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