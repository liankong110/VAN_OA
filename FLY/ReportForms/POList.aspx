<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="POList.aspx.cs" Inherits="VAN_OA.ReportForms.POList" MasterPageFile="~/DefaultMaster.Master" Title="采购流水账"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
     <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">采购流水账管理</td>
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
       
        <td>往来单位</td>
       <td>
           <asp:TextBox ID="txtUnitName" runat="server"></asp:TextBox>
       </td>
    </tr>
    <tr>
   
       
       
        <td>
          商品名称:
       </td>
       
         <td>
             <asp:TextBox ID="txtInvName" runat="server"></asp:TextBox> 
           
       </td>
       
        <td>销售人员:</td>
       <td>
           <asp:TextBox ID="txtSeller" runat="server"></asp:TextBox>
       </td>
    </tr>
    <tr>
        
        <td colspan="4">
        <div align="right">
             <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            <asp:Button ID="btnAdd" runat="server" Text="添加"  BackColor="Yellow" 
                onclick="btnAdd_Click" Width="98px" /></div> 
        </td>
    </tr>
</table><br>
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="100%"  AllowPaging="True" AutoGenerateColumns="False" 
         onpageindexchanging="gvList_PageIndexChanging" 
         onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
         onrowdatabound="gvList_RowDataBound">
            <PagerTemplate>
        <br />
      <%--   <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
    </PagerTemplate>
         
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
                往来单位
                </td>      
                
                    <td>
                商品名称
                </td>      
                
                    <td>
                单位
                </td>      
                
                
                    <td>
                数量
                </td>      
                
                
                    <td>
                单价
                </td>      
                
                
                    <td>
                小计
                </td>   
                
                  <td>
                销售人员
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
             <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"  CommandName="Delete" 
                    OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
        
            
             
         <asp:BoundField DataField="DataTime" HeaderText="日期"  DataFormatString="{0:yyyy-MM-dd}" >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
             <asp:BoundField DataField="UnitName" HeaderText="往来单位"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
              <asp:BoundField DataField="InvName" HeaderText="商品名称"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
              <asp:BoundField DataField="Unit" HeaderText="单位"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
              <asp:BoundField DataField="Num" HeaderText="数量"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
              <asp:BoundField DataField="Price" HeaderText="数量"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
             
                 <asp:BoundField DataField="Total" HeaderText="小计"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
             
                <asp:BoundField DataField="Seller" HeaderText="销售人员"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
             
                <asp:BoundField DataField="CreateUser_Name" HeaderText="创建人"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
            
            
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
 </asp:Content>