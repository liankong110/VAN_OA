<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PO_Order.aspx.cs" Inherits="VAN_OA.EFrom.PO_Order" Culture="auto" UICulture="auto"  MasterPageFile="~/DefaultMaster.Master" Title="订单报批表" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
   
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 
 
        <script type="text/javascript"> 
    function count1() 
{
 
 var sl = document.getElementById('<%= txtNum.ClientID %>').value;
  
 var dj = document.getElementById('<%= txtSupperPrice.ClientID %>').value;
 if ((sl!="")&&(dj!=""))
 {    
   var total=sl*dj;
   document.getElementById('<%= txtTotal1.ClientID %>').value=total.toFixed(3).toString(); 
 }
 else
 {
   document.getElementById('<%= txtTotal1.ClientID %>').value = "0.00";
   
    
 }
 }
 
 
 
 function count2() 
{
 
 var sl = document.getElementById('<%= txtNum.ClientID %>').value;
 var dj = document.getElementById('<%= txtPrice2.ClientID %>').value;
 if ((sl!="")&&(dj!=""))
 {
    
   var total=sl*dj;
   document.getElementById('<%= txtTotal2.ClientID %>').value=total.toFixed(3).toString(); 
 }
 else
 {
   document.getElementById('<%= txtTotal2.ClientID %>').value = "0.00";
   
    
 }
 
 }
 function count3() 
{
 
 var sl = document.getElementById('<%= txtNum.ClientID %>').value;
 var dj = document.getElementById('<%= txtPrice3.ClientID %>').value;
 if ((sl!="")&&(dj!=""))
 {
    
   var total=sl*dj;
   document.getElementById('<%= txtTotal3.ClientID %>').value=total.toFixed(3).toString(); 
 }
 else
 {
   document.getElementById('<%= txtTotal3.ClientID %>').value = "0.00";
   
    
 }
 }
 
 
 
   function cou1() 
{
 
 var sl = document.getElementById('<%= txtNum.ClientID %>').value;
  
 var dj = document.getElementById('<%= txtFinPrice1.ClientID %>').value;
 if ((sl!="")&&(dj!=""))
 {    
   var total=sl*dj;
   document.getElementById('<%= txtTotal1.ClientID %>').value=total.toFixed(3).toString(); 
 }
 else
 {
   document.getElementById('<%= txtTotal1.ClientID %>').value = "0.00";
   
    
 }
 }
 
 
 
 function cou2() 
{
 
 var sl = document.getElementById('<%= txtNum.ClientID %>').value;
 var dj = document.getElementById('<%= txtFinPrice2.ClientID %>').value;
 if ((sl!="")&&(dj!=""))
 {
    
   var total=sl*dj;
   document.getElementById('<%= txtTotal2.ClientID %>').value=total.toFixed(3).toString(); 
 }
 else
 {
   document.getElementById('<%= txtTotal2.ClientID %>').value = "0.00";
   
    
 }
 
 }
 function cou3() 
{
 
 var sl = document.getElementById('<%= txtNum.ClientID %>').value;
 var dj = document.getElementById('<%= txtFinPrice1.ClientID %>').value;
 if ((sl!="")&&(dj!=""))
 {
    
   var total=sl*dj;
   document.getElementById('<%= txtTotal3.ClientID %>').value=total.toFixed(3).toString(); 
 }
 else
 {
   document.getElementById('<%= txtTotal3.ClientID %>').value = "0.00";
   
    
 }
 }
</script>
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">订单报批表-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>      
          
        <tr> 
              <td>请购人：<font style="color:Red">*</font></td>
            <td colspan="1"><asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox>            
            
        </td>
      
      <td>
      采购人:
      </td>
      <td colspan="2">
          <asp:TextBox ID="txtCaiGou" runat="server"></asp:TextBox>
      </td>
        </tr>
        <tr>
        <td colspan="6" >
        <div  align="right" style="width:100%;" >
       
                <asp:LinkButton ID="lblAttName" runat="server" onclick="lblAttName_Click" ForeColor="Red"></asp:LinkButton>
            <asp:Label ID="lblAttName_Vis" runat="server" Text="" Visible="false"></asp:Label>
                </div>
        </td>
        </tr>
         
        <tr>
        <td  colspan="6">
     
            <asp:LinkButton ID="lbtnAddFiles" runat="server" 
          OnClientClick="javascript:window.showModalDialog('PO_Orders.aspx',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')" 
          ForeColor="Red" onclick="LinkButton1_Click1" >
      添加文件</asp:LinkButton>
      <br />
 <asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Ids"  
          Width="100%"  AutoGenerateColumns="False" onrowdatabound="gvList_RowDataBound" 
                onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing"  ShowFooter="true"
       
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
                日期
                </td>   
                 <td>
                客户名称
                </td>        
                
                   <td>
                产品
                </td>        
                
                   <td>
                数量
                </td>        
                
                   <td>
                单位
                </td>  
                
                
                 
                   <td>
                成本单价
                </td>  
                
                
                 
                   <td>
                管理费
                </td>  
                
                
                 
                   <td>
                到帐日期
                </td>      
                
                  <td>
                利润%
                </td>                               
              </tr>
              <tr>
                  <td colspan="11" align="center" style="height:80%">---暂无数据---</td>
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
             <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"  CommandName="Delete" 
                    OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
        
            
             
         
		
		<asp:BoundField DataField="Time" HeaderText="日期" SortExpression="Time"  DataFormatString="{0:yyyy-MM-dd}"  /> 
		<%--<asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName" /> --%>
		
				<asp:TemplateField HeaderText="客户名称">		
		<ItemTemplate>
            <asp:Label ID="lblGuestName" runat="server" Text='<%# Eval("GuestName") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblGuestName" runat="server" Text='<%# Eval("GuestName") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
			
		<asp:BoundField DataField="InvName" HeaderText="产品" SortExpression="InvName"  /> 
		 
			<asp:TemplateField HeaderText="数量">		
		<ItemTemplate>
            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		<asp:BoundField DataField="Unit" HeaderText="单位" SortExpression="Unit" ItemStyle-HorizontalAlign="Center"  /> 
		 
		
			<asp:TemplateField HeaderText="成本单价">		
		<ItemTemplate>
            <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
			 
			
			<asp:TemplateField HeaderText="成本总价">		
		<ItemTemplate>
            <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		 
		
		<asp:TemplateField HeaderText="销售单价">		
		<ItemTemplate>
            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
		 
		
		<asp:TemplateField HeaderText="销售总价">		
		<ItemTemplate>
            <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
		
	 
		
			<asp:TemplateField HeaderText="管理费">		
		<ItemTemplate>
            <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
			 
			
			<asp:TemplateField HeaderText="销售净利">		
		<ItemTemplate>
            <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
		<asp:BoundField DataField="ToTime" HeaderText="到帐日期" SortExpression="ToTime"  DataFormatString="{0:yyyy-MM-dd}" /> 
		
            
            <asp:TemplateField HeaderText="利润%">		
		<ItemTemplate>
            <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'  ></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
              <FooterStyle BackColor="#D7E8FF" />
</asp:GridView>
 
 <br />
 
   
           <asp:FileUpload ID="fuAttach"
                    runat="server"  Width="400px"/>
           <br />
              <br />
      
  <asp:GridView ID="gvCai" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Ids"   ShowFooter="true"
          Width="100%"  AutoGenerateColumns="False" onrowdatabound="gvCai_RowDataBound" 
                onrowdeleting="gvCai_RowDeleting" onrowediting="gvCai_RowEditing"   style="border-collapse:collapse;">        
         
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
                <td>
                编辑
                </td>   
                
                 <td>
                客户
                </td>   
                
                 <td>
                产品
                </td>   
                
                 <td>
                数量
                </td>   
                 
                <td>
                日期
                </td>   
                
                
                 <td>
                供应商1
                </td>        
                
                   <td>
                询价1
                </td>  
                
                   <td>
                小计1
                </td>        
                
                <td>
                供应商2
                </td>        
                
                   <td>
                询价2
                </td>  
                
                   <td>
                小计2                </td>        
                
                <td>
                供应商3
                </td>        
                
                   <td>
                询价3
                </td>  
                
                   <td>
                小计3
                </td>        
                
                <td>
                利润率
                </td>
                   <td>
                审批意见
                </td>        
                
                   <td>
                更新人
                </td>  
                
                
                 
                            
              </tr>
              <tr>
                  <td colspan="11" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
        <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditCai" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑"/>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
            
        
            
        
         
         
         <asp:TemplateField HeaderText="客户">		
		<ItemTemplate>
            <asp:Label ID="lblGuestName" runat="server" Text='<%# Eval("GuestName") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		   <asp:Label ID="lblGuestName" runat="server" Text="合计"></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
		 
		
		  <asp:TemplateField HeaderText="产品">		
		<ItemTemplate>
            <asp:Label ID="lblInvName" runat="server" Text='<%# Eval("InvName") %>'></asp:Label>
		
		</ItemTemplate>
		</asp:TemplateField>
		
		 
			<asp:TemplateField HeaderText="数量">		
		<ItemTemplate>
            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		<asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		

		<asp:BoundField DataField="Supplier" HeaderText="供应商1" SortExpression="Supplier" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:TemplateField HeaderText="询价1">		
		<ItemTemplate>
            <asp:Label ID="lblSupperPrice" runat="server" Text='<%# Eval("SupperPrice") %>'></asp:Label>
		
		</ItemTemplate>
		</asp:TemplateField>
		 
		
			<asp:TemplateField HeaderText="确认价1">		
		<ItemTemplate>
            <asp:Label ID="lblFinPrice1" runat="server" Text='<%# Eval("FinPrice1") %>'></asp:Label>
		
		</ItemTemplate>
		</asp:TemplateField>
		
		
		<%--<asp:BoundField DataField="SupperPrice" HeaderText="询价1" SortExpression="SupperPrice" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:BoundField DataField="FinPrice1" HeaderText="确认价1" SortExpression="FinPrice1" ItemStyle-HorizontalAlign="Center"  /> --%>
		<%--<asp:BoundField DataField="Total1" HeaderText="小计1" SortExpression="Total1" ItemStyle-HorizontalAlign="Center"  /> --%>
		
		
		
			<asp:TemplateField HeaderText="小计1">		
		<ItemTemplate>
            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		<asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		
		<asp:BoundField DataField="Supplier1" HeaderText="供应商2" SortExpression="Supplier1" ItemStyle-HorizontalAlign="Center"  />
		
		
		
		<asp:TemplateField HeaderText="询价2">		
		<ItemTemplate>
            <asp:Label ID="lblSupperPrice1" runat="server" Text='<%# Eval("SupperPrice1") %>'></asp:Label>
		
		</ItemTemplate>
		</asp:TemplateField>
		 
		
			<asp:TemplateField HeaderText="确认价2">		
		<ItemTemplate>
            <asp:Label ID="FinPrice2" runat="server" Text='<%# Eval("FinPrice2") %>'></asp:Label>
		
		</ItemTemplate>
		</asp:TemplateField>
		 
		<%--<asp:BoundField DataField="SupperPrice1" HeaderText="询价2" SortExpression="SupperPrice1" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="FinPrice2" HeaderText="确认价2" SortExpression="FinPrice2" ItemStyle-HorizontalAlign="Center"  /> --%>
		<%--<asp:BoundField DataField="Total2" HeaderText="小计2" SortExpression="Total2" ItemStyle-HorizontalAlign="Center"  /> --%>
		<asp:TemplateField HeaderText="小计2">		
		<ItemTemplate>
            <asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		<asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
		
		<asp:BoundField DataField="Supplier2" HeaderText="供应商3" SortExpression="Supplier2" ItemStyle-HorizontalAlign="Center"  /> 
		
		
		<asp:TemplateField HeaderText="询价3">		
		<ItemTemplate>
            <asp:Label ID="lblSupperPrice2" runat="server" Text='<%# Eval("SupperPrice2") %>'></asp:Label>
		
		</ItemTemplate>
		</asp:TemplateField>
		 
		
			<asp:TemplateField HeaderText="确认价3">		
		<ItemTemplate>
            <asp:Label ID="FinPrice3" runat="server" Text='<%# Eval("FinPrice3") %>'></asp:Label>
		
		</ItemTemplate>
		</asp:TemplateField>
<%--		<asp:BoundField DataField="SupperPrice2" HeaderText="询价3" SortExpression="SupperPrice2" ItemStyle-HorizontalAlign="Center"  /> 
			<asp:BoundField DataField="FinPrice3" HeaderText="确认价3" SortExpression="FinPrice3" ItemStyle-HorizontalAlign="Center"  /> --%>
	<%--	<asp:BoundField DataField="Total3" HeaderText="小计3" SortExpression="Total3" ItemStyle-HorizontalAlign="Center"  /> --%>
			 <asp:TemplateField HeaderText="小计3">		
		<ItemTemplate>
            <asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		<asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
			
			
			
		
		<asp:BoundField DataField="Idea" HeaderText="审批意见" SortExpression="Idea" ItemStyle-HorizontalAlign="Center"  /> 
        <asp:BoundField DataField="UpdateUser" HeaderText="更新人" SortExpression="UpdateUser" ItemStyle-HorizontalAlign="Center"  /> 

		 <asp:TemplateField HeaderText="利润%">		
		<ItemTemplate>
            <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
		
		</ItemTemplate>
		<FooterTemplate>
		
		<asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
		</FooterTemplate>
		</asp:TemplateField>
            
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
          
              <FooterStyle BackColor="#D7E8FF" />
</asp:GridView>
 

 <br />
            <asp:Panel ID="plCiGou" runat="server">
            
              <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">采购</td>
        </tr>
        
        
        
        <tr>
	<td height="25" width="30%" align="right">
		客户
	：</td>
	<td height="25" align="left">
		<asp:TextBox id="txtGuestName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
	    </td>
	    
	    <td height="25" width="30%" align="right">
		产品
	：</td>
	<td height="25" width="*" align="left">
			<asp:TextBox id="txtInvName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox></td>
			<td height="25" width="30%" align="right">
		数量
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtNum" runat="server" Width="200px" ReadOnly="true"></asp:TextBox></td>
			
	    </tr>
	 
	
 
	 
	 
        
     <%--   <tr>
	<td height="25" width="30%" align="right">
		日期：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtCaiTime" runat="server" Width="200px"></asp:TextBox>
		
		
		    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>		  
		 
		 <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  TargetControlID="txtCaiTime" Format="yyyy-MM-dd"  PopupButtonID="Image1" >
                      </cc1:CalendarExtender>        
   
	    <font style="color:Red">*</font></td></tr>--%>
	<tr>
	<td height="25" width="30%" align="right">
		采购询供应商1
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupplier" runat="server" Width="200px"></asp:TextBox>
	   </td>
	    <td height="25" width="30%" align="right">
		采购询价1
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupperPrice" runat="server" Width="100px" onKeyUp="count1();"  ></asp:TextBox>
	     
             <asp:TextBox ID="txtFinPrice1" runat="server" Width="100px" onKeyUp="cou1();" ></asp:TextBox>
	     </td>
	    <td height="25" width="30%" align="right">
		小计1
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtTotal1" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
	    </td>
	    </tr>
	 
	    
	    
	    <tr>
	<td height="25" width="30%" align="right">
		采购询供应商2 
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupper2" runat="server" Width="200px"></asp:TextBox>
	   </td> 
	 
	<td height="25" width="30%" align="right">
		采购询价2
	：</td>
	<td height="25" width="210" align="left" >
		<asp:TextBox id="txtPrice2" runat="server" Width="100px" onKeyUp="count2();"  ></asp:TextBox>
	        
             <asp:TextBox ID="txtFinPrice2" runat="server" Width="100px" onKeyUp="cou2();" ></asp:TextBox>
	     </td>
	 
	<td height="25" width="30%" align="right">
		小计2
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtTotal2" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
	    </td>
	 
	 </tr>
	    
	    
	    
	    
	    <tr>
	<td height="25" width="30%" align="right">
		采购询供应商3
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSupper3" runat="server" Width="200px"></asp:TextBox>
	   </td> 
	 
	<td height="25" width="30%" align="right">
		采购询价3
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtPrice3" runat="server" Width="100px" onKeyUp="count3();" ></asp:TextBox>
	       
             <asp:TextBox ID="txtFinPrice3" runat="server" Width="100px" onKeyUp="cou3();" ></asp:TextBox>
	     </td>
	 
	<td height="25" width="30%" align="right">
		小计3
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtTotal3" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
	    </td></tr>
	    
	    	<tr>
	<td height="25" width="30%" align="right">
		<asp:Label ID="lblIdea" runat="server" Text="审批意见:"></asp:Label>
	</td>
	<td height="25" align="left" colspan="5">
		<asp:TextBox id="txtIdea" runat="server" Width="95%" Height="50px"  TextMode="MultiLine"></asp:TextBox>
	</td></tr>
	   
	 
	 <tr>
                  <td colspan="6" align="center" style="height:80%"> 
                      <asp:Button ID="btnSave" 
                          runat="server" Text="保存" BackColor="Yellow" onclick="btnSave_Click" 
                          ValidationGroup="a" Width="74px" 
                    />&nbsp; 
                      &nbsp;</td>
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
           
                <asp:Button ID="btnReSubEdit" runat="server" Text="再次编辑"  BackColor="Yellow" 
                    onclick="btnReSubEdit_Click"  />&nbsp;
                     &nbsp;
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
    <br />
    
    
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
         
            <td colspan="3" style=" height:20px; background-color:#336699; color:White;">审批依据</td>
        </tr>      
        <tr>
        <td>   
        </td>        
         <td>        
        X:=利润率底线
        </td>
         <td>
        备注        
        </td>
        </tr>
        
        
        <tr>
        <td>   
        一个月之内（含）
        </td>        
         <td>        
       6%
        </td>
         <td>
      按出货日期 到收账日期时间
        </td>
        </tr>
        
        
        
        
        <tr>
        <td>   
            一个月至二个月之内（含）&nbsp;</td>        
         <td>        
      11%
        </td>
         <td>
       按出货日期 到收账日期时间  
        </td>
        </tr>
        
        <tr>
        <td>   二个月至叁个月之内（含）
        </td>        
         <td>        
      16%
        </td>
         <td>
        按出货日期 到收账日期时间
        </td>
        </tr>
        
        <tr>
        <td colspan="3">
        备注：批单的X会按实际情况作细微调整。
        </td>
        </tr>
        
        <tr>
        <td colspan="3">
         三月以上的订单，原则上，公司不接。<br />
         金额在2000元以内的销售单价，因考虑人工费用，送货成本，原则上，<br />
      整单销售价格（一起送货）：利润率不但要考虑以上的底线，也要保证利润》= 100元
        
        </td>
        </tr>
        <tr>
        <td colspan="3">
        请注明费用费用是否（含/未税）
        </td>
        </tr>
        </table>
        
    
 </asp:Content>
