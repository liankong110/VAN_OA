﻿<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="UseCarDetailManList.aspx.cs" Inherits="VAN_OA.EFrom.UseCarDetailManList"  MasterPageFile="~/DefaultMaster.Master" Title="司机公里数统计"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
   
     <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">
            用车申请表-修改</td>
    </tr>
    <tr>
   
       
       
        <td>
            日期:
       </td>
       
         <td>
             <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
              <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
              -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
               <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
             <cc1:CalendarExtender  ID="CalendarExtender1" PopupButtonID="Image1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFrom">
             </cc1:CalendarExtender>
             <cc1:CalendarExtender  ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtTo">
             </cc1:CalendarExtender>
       </td>
       
        <td>公司名称:</td>
       <td>
           <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
       </td>
    </tr>
    
    <tr>
   
       
       
        <td>
            申请人:
       </td>
       
         <td>
             <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox> 
             
       </td>
       
        <td>司机:</td>
       <td>
           <asp:TextBox ID="txtDriver" runat="server"></asp:TextBox>
       </td>
    </tr>
    <tr>
   
       
       
        <td>
            车牌号:
       </td>
       
         <td >
             <asp:TextBox ID="txtCarNo" runat="server"></asp:TextBox> 
             
       </td>
       <td>
    单据号:
    </td>
    
       <td >
           <asp:TextBox ID="txtProNo" runat="server" ></asp:TextBox>
    </td>
     
    </tr>
  
    <tr>
        
        <td colspan="4">
        <div align="right">
             <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            </div> 
        </td>
    </tr>
</table><br/>
<br />
   
     <asp:Panel ID="Panel1" runat="server" Width="100%"   ScrollBars="Horizontal" Height="100%">
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="150%"  AllowPaging="True" AutoGenerateColumns="False" 
         onpageindexchanging="gvList_PageIndexChanging" 
         onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
         onrowdatabound="gvList_RowDataBound">
         
            <PagerTemplate>
        <br />
         <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />
    </PagerTemplate>
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
               
                 <td>
                修改
                </td>  
                <td>
                日期
                </td>   
                <td>单据号</td>
                  <td>
                时间
                </td>   
                 <td>
                公司名称
                </td>      
                
                  
                
                    <td>
                申请人
                </td>      
                
                
                    <td>
               司机
                </td>      
                
                 <td>
                始(KM)
                </td>  
                 <td>
                终(KM)
                </td> 
                
                    <td>
              公里数(KM)
                </td>      
                
                
                  
                
                  <td>
              车牌号
                </td>     
                
                   
                
                  <td>
              乘车人
                </td>     
                
                 
                  <td>
                备注
                </td>     
                                        
              </tr>
              <tr>
                  <td colspan="6" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
			 <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑"/>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
             
		<asp:BoundField DataField="AppTime" HeaderText="日期" SortExpression="AppTime" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" /> 
			<asp:BoundField DataField="GoEndTime" HeaderText="时间" SortExpression="GoEndTime" ItemStyle-HorizontalAlign="Center"  /> 
				<asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="GuestName" HeaderText="公司名称" SortExpression="GuestName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="Area" HeaderText="地址" SortExpression="Area" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="AppUserName" HeaderText="申请人" SortExpression="AppUserName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="Driver" HeaderText="司机" SortExpression="Driver" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:BoundField DataField="FromRoadLong" HeaderText="始(KM)" SortExpression="FromRoadLong" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ToRoadLong" HeaderText="终(KM)" SortExpression="ToRoadLong" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:BoundField DataField="RoadLong" HeaderText="公里数(KM)" SortExpression="RoadLong" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="CarNo" HeaderText="车牌号" SortExpression="CarNo" ItemStyle-HorizontalAlign="Center"  /> 
		 
		<asp:BoundField DataField="ByCarPers" HeaderText="乘车人" SortExpression="ByCarPers" ItemStyle-HorizontalAlign="Center"  /> 		
		<asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Left" />             
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
</asp:Panel>

 </asp:Content>
