<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeverDispatching.aspx.cs" Inherits="VAN_OA.ReportForms.LeverDispatching"  MasterPageFile="~/DefaultMaster.Master" Title="提醒记录"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="TabPanel1" runat="server">
        <HeaderTemplate>
        请假单
        </HeaderTemplate>
        
        
        <ContentTemplate>
        
     
 

<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
            AutoGenerateColumns="False"     Width="100%"   
        
         onrowdatabound="gvList_RowDataBound">
         
            <PagerTemplate>
      
       
    </PagerTemplate>
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
               
                
                <td>
                姓名
                </td>   
                  <td>
                部门
                </td>   
                 <td>
                请假类别
                </td>      
                
                  
                  
                
                
                    <td>
                从
                </td>      
                
                
                    <td>
                到
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
        
             
     <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="depart" HeaderText="部门" SortExpression="depart" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="leverType" HeaderText="请假类别" SortExpression="leverType" ItemStyle-HorizontalAlign="Center"  /> 		 
		<asp:BoundField DataField="dateForm" HeaderText="从" SortExpression="dateForm" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="dateTo" HeaderText="到" SortExpression="dateTo" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="remark" HeaderText="备注" SortExpression="remark" ItemStyle-HorizontalAlign="Center"  /> 
		 

            
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
   </ContentTemplate>
        </cc1:TabPanel>
        
        <cc1:TabPanel ID="TabPanel2" runat="server">
        <HeaderTemplate>派工单</HeaderTemplate>
        <ContentTemplate>
        <asp:Panel ID="Panel1" runat="server" Width="100%"   ScrollBars="Horizontal" Height="100%">
      
<asp:GridView ID="gvDispatching" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
            AutoGenerateColumns="False"     Width="150%"   
        
         onrowdatabound="gvDispathing_RowDataBound">
         
            <PagerTemplate>
      
       
    </PagerTemplate>
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
               
                
                <td>
                派工人
                </td>   
                  <td>
                派工日期
                </td>   
                 <td>
                被派工人
                </td>      
                
                  
                  
                
                
                    <td>
                随同人
                </td>      
                
                
                    <td>
                客户名称
                </td>      
                
                
                  
                
                  <td>
               具体地址
                </td>   
                  <td>
               外派出去时间
                </td>   
                  <td>
               外派回来时间
                </td>    
                
                    <td>
               故障现象
                </td>     
                
                
                  
                                        
              </tr>
              <tr>
                  <td colspan="6" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
        
             
     <asp:BoundField DataField="Dispatcher" HeaderText="派工人" SortExpression="Dispatcher" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="DisDate" HeaderText="派工日期" SortExpression="DisDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/> 
		<asp:BoundField DataField="OutdispaterName" HeaderText="被派工人" SortExpression="OutdispaterName" ItemStyle-HorizontalAlign="Center"  /> 		 
		<asp:BoundField DataField="SuiTongRen" HeaderText="随同人" SortExpression="SuiTongRen" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="GueName" HeaderText="客户名称" SortExpression="GueName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="Address" HeaderText="具体地址" SortExpression="Address" ItemStyle-HorizontalAlign="Center"  /> 
		 
	    <asp:BoundField DataField="GoDate" HeaderText="外派出去时间" SortExpression="GoDate" ItemStyle-HorizontalAlign="Center"  /> 
		 	<asp:BoundField DataField="BackDate" HeaderText="外派回来时间" SortExpression="BackDate" ItemStyle-HorizontalAlign="Center"  /> 
		 <asp:BoundField DataField="Question" HeaderText="故障现象" SortExpression="Question" ItemStyle-HorizontalAlign="Center"  /> 
		 
            
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
</asp:Panel>
  </ContentTemplate>
        </cc1:TabPanel>


   </cc1:TabContainer>
</asp:Content>
 