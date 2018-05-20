<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DispatchListRep.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.ReportForms.DispatchListRep" MasterPageFile="~/DefaultMaster.Master" Title="预期报销单列表"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
     <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">预期报销单列表</td>
    </tr>
    <tr>
   
       
       
        <td>
            发生时间:
       </td>
       
         <td>
             <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox> 
              <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
             -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
              <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>
             <cc1:CalendarExtender  ID="CalendarExtender1"  PopupButtonID="Image1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFrom">
             </cc1:CalendarExtender>
             <cc1:CalendarExtender  ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtTo">
             </cc1:CalendarExtender>
       </td>
       
        <td>报销单号:</td>
       <td>
            <asp:TextBox ID="txtNo" runat="server"></asp:TextBox>
       </td>
    </tr>
    
    
    <tr>
   
       
       
        <td>
            用户:        </td>
       
         <td colspan="1">
              <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
       </td>
      
      <td>
          类型:&nbsp;</td>
      <td>
          <asp:DropDownList ID="ddlType" runat="server"  Width="200px">
          <asp:ListItem></asp:ListItem>
          <asp:ListItem>公交费</asp:ListItem>
          <asp:ListItem>餐饮费</asp:ListItem>
          <asp:ListItem>住宿费</asp:ListItem>
          <asp:ListItem>汽油补贴</asp:ListItem>
          <asp:ListItem>过路费</asp:ListItem>
          <asp:ListItem>邮寄费</asp:ListItem>
          <asp:ListItem>小额采购</asp:ListItem>
          <asp:ListItem>其它费用</asp:ListItem>
          </asp:DropDownList>  &nbsp;</td>
    </tr>
  
    <tr>
        
        <td colspan="4">
        <div align="right">
             <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;&nbsp;&nbsp;  
             </div> 
        </td>
    </tr>
</table><br>
    <asp:Label ID="lblMess" runat="server" Text="" style="color:Red;"></asp:Label>
<br />
   <asp:Label ID="lbl" runat="server" Text="总计:" ForeColor="Red"></asp:Label><asp:Label
        ID="lblTotal" runat="server" Text="0"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal">
   
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="250%" AutoGenerateColumns="False" AllowPaging="true"
         onpageindexchanging="gvList_PageIndexChanging" 
         onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
         onrowdatabound="gvList_RowDataBound">
          <PagerTemplate>
        <br />
        <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
    </PagerTemplate>
          
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >                  
                                            <td colspan="6"></td>
              </tr>
              <tr>
                  <td colspan="6"  style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
      
             
             
    
	 
       <asp:BoundField DataField="UserName" HeaderText="用户" SortExpression="UserName" ItemStyle-HorizontalAlign="Center" /> 
		<asp:BoundField DataField="EvTime" HeaderText="事件发生日期" SortExpression="EvTime" ItemStyle-HorizontalAlign="Center"    DataFormatString="{0:yyyy-MM-dd}"  /> 
		<asp:BoundField DataField="CreateTime" HeaderText="填写日期" SortExpression="CreateTime" ItemStyle-HorizontalAlign="Center"    DataFormatString="{0:yyyy-MM-dd}"  /> 
		<asp:BoundField DataField="CardNo" HeaderText="报销序号" SortExpression="CardNo" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="BusFromAddress" HeaderText="公交费-> 地点开始" SortExpression="BusFromAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="BusToAddress" HeaderText="地点结束" SortExpression="BusToAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="BusTotal" HeaderText="金额" SortExpression="BusTotal" ItemStyle-HorizontalAlign="Center"  /> 
		 
		
		
		<asp:TemplateField HeaderText="打的">		
		<ItemTemplate>
            
            <asp:CheckBox ID="CheckBox10" runat="server"  Checked='<%# Eval("IfTexi") %>' Enabled="false"/>
		</ItemTemplate>
		</asp:TemplateField>
		
		
	 
		
			<asp:TemplateField HeaderText="公交">		
		<ItemTemplate>
            
            <asp:CheckBox ID="CheckBox11" runat="server"  Checked='<%# Eval("IfBus") %>' Enabled="false"/>
		</ItemTemplate>
		</asp:TemplateField>
		
		
		<asp:BoundField DataField="BusFromTime" HeaderText="开始时间" SortExpression="BusFromTime" ItemStyle-HorizontalAlign="Center"   DataFormatString="{0:yyyy-MM-dd}"/> 
		<asp:BoundField DataField="BusToTime" HeaderText="结束时间" SortExpression="BusToTime" ItemStyle-HorizontalAlign="Center"   DataFormatString="{0:yyyy-MM-dd}"/> 
		
		<asp:BoundField DataField="BusRemark" HeaderText="备注" SortExpression="BusRemark" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:BoundField DataField="RepastAddress" HeaderText="餐饮->地点" SortExpression="RepastAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="RepastTotal" HeaderText=" 金额" SortExpression="RepastTotal" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="RepastPerNum" HeaderText="人数" SortExpression="RepastPerNum" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="RepastPers" HeaderText="参与者" SortExpression="RepastPers" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="RepastType" HeaderText=" 类型" SortExpression="RepastType" ItemStyle-HorizontalAlign="Center"  /> 
		
			<asp:BoundField DataField="RepastRemark" HeaderText="备注" SortExpression="RepastRemark" ItemStyle-HorizontalAlign="Center"  /> 
			
		<asp:BoundField DataField="HotelAddress" HeaderText="住宿费->地点" SortExpression="HotelAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="HotelName" HeaderText="酒店名称" SortExpression="HotelName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="HotelTotal" HeaderText="金额" SortExpression="HotelTotal" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="HotelType" HeaderText="类型" SortExpression="HotelType" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:BoundField DataField="HotelRemark" HeaderText="备注" SortExpression="HotelRemark" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:BoundField DataField="OilFromAddress" HeaderText="汽油补贴->开始地点" SortExpression="OilFromAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="OilToAddress" HeaderText= "结束地点" SortExpression="OilToAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="OilLiCheng" HeaderText="里程" SortExpression="OilLiCheng" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="OilTotal" HeaderText="金额" SortExpression="OilTotal" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="OilRemark" HeaderText="备注" SortExpression="OilRemark" ItemStyle-HorizontalAlign="Center"  /> 
		
		
		<asp:BoundField DataField="GuoBeginAddress" HeaderText="过路费-> 开始地点" SortExpression="GuoBeginAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="GuoToAddress" HeaderText="结束地点" SortExpression="GuoToAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="GuoTotal" HeaderText="金额" SortExpression="GuoTotal" ItemStyle-HorizontalAlign="Center"  /> 
		 <asp:BoundField DataField="GuoRemark" HeaderText="备注" SortExpression="GuoRemark" ItemStyle-HorizontalAlign="Center"  /> 
		 
		 
		 
		 <asp:BoundField DataField="PostNo" HeaderText="邮寄编号->寄出地点" SortExpression="PostNo" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="PostCompany" HeaderText="快递公司" SortExpression="PostCompany" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="PostContext" HeaderText="内容" SortExpression="PostContext" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="PostToPer" HeaderText="寄件人" SortExpression="PostToPer" ItemStyle-HorizontalAlign="Center"  /> 
		
			<asp:TemplateField HeaderText="寄出">		
		<ItemTemplate>
            
            <asp:CheckBox ID="CheckBox12" runat="server"  Checked='<%# Eval("PostFrom") %>' Enabled="false"/>
		</ItemTemplate>
		</asp:TemplateField>
		
		
		<asp:BoundField DataField="PostFromAddress" HeaderText="邮寄费" SortExpression="PostFromAddress" ItemStyle-HorizontalAlign="Center"  /> 
	 
		
			<asp:TemplateField HeaderText="到付">		
		<ItemTemplate>
            
            <asp:CheckBox ID="CheckBox13" runat="server"  Checked='<%# Eval("PostTo") %>' Enabled="false"/>
		</ItemTemplate>
		</asp:TemplateField>
		
		
		<asp:BoundField DataField="PostToAddress" HeaderText="到付地点" SortExpression="PostToAddress" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="PostTotal" HeaderText="金额" SortExpression="PostTotal" ItemStyle-HorizontalAlign="Center"  /> 
		
			<asp:BoundField DataField="PostRemark" HeaderText="备注" SortExpression="PostRemark" ItemStyle-HorizontalAlign="Center"  /> 
			
		<asp:BoundField DataField="PoContext" HeaderText="采购->内容" SortExpression="PoContext" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="PoTotal" HeaderText="金额" SortExpression="PoTotal" ItemStyle-HorizontalAlign="Center"  /> 
			<asp:BoundField DataField="PoRemark" HeaderText="备注" SortExpression="PoRemark" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="OtherContext" HeaderText="其它->内容" SortExpression="OtherContext" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="OtherTotal" HeaderText="金额" SortExpression="OtherTotal" ItemStyle-HorizontalAlign="Center"  /> 
	<asp:BoundField DataField="OtherRemark" HeaderText="备注" SortExpression="OtherRemark" ItemStyle-HorizontalAlign="Center"  /> 
<asp:BoundField DataField="Total" HeaderText="小计" SortExpression="Total" ItemStyle-HorizontalAlign="Center"  />


   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
<webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%" CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left"   ButtonImageAlign="Middle"  CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager> 
  </asp:Panel>
 </asp:Content>
