<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consignoring.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.Consignoring" MasterPageFile="~/DefaultMaster.Master" Title="被委托记录"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">被委托记录</td>
    </tr>
    <tr>
        <td>审批类型</td>
       <td>
           <asp:DropDownList ID="ddlProType" runat="server" Width="200px" DataTextField = "pro_Type" DataValueField="pro_Id">
           </asp:DropDownList>
       </td>
       
        <td>
          申请时间:
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
        
        
    </tr>
    
    
    <tr>
        <td>状态</td>
       <td colspan="1">
           <asp:DropDownList ID="ddlState" runat="server" Width="200px">
               <asp:ListItem></asp:ListItem>
               <asp:ListItem>执行中</asp:ListItem>
               <asp:ListItem>通过</asp:ListItem>
               <asp:ListItem>不通过</asp:ListItem>
           </asp:DropDownList>
       </td>
       
      
       <td>
    单据号:
    </td>
    
       <td colspan="1">
           <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
    </td>
       
        
        
        
    </tr>
    
    
  
    
    <tr>
    <td colspan="4">
    <div align=right>
     <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            
    </div>
    </td>
    </tr>
</table><br>
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="id"  
          Width="100%"  AllowPaging="True" AutoGenerateColumns="False" 
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
                查看
                </td>   
                 
                
                <td>
                文件类型
                </td>  
                  <td>
                单据号
                </td>
                 <td>
                创建人
                </td>  
                   <td>
                创建时间
                </td>   
                
               
                   <td>
                申请时间
                </td>   
                
                   <td>
                (委托人)
                </td>  
                   <td>
                状态
                </td>   
                                     
              </tr>
              <tr>
                  <td colspan="6" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
        <asp:TemplateField HeaderText=" 查看">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑"/>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="100px" />
             </asp:TemplateField>
             
        
            
             
         <asp:BoundField DataField="ProTyleName" HeaderText="文件类型"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
             
                 <asp:BoundField DataField="e_No" HeaderText="单据号"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
             
                 <asp:BoundField DataField="CreatePer_Name" HeaderText="创建人"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
                 <asp:BoundField DataField="createTime" HeaderText="创建时间"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
                 
                 <asp:BoundField DataField="appTime" HeaderText="申请时间"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
                 <asp:BoundField DataField="ToPer_Name" HeaderText=" (委托人)"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
                 <asp:BoundField DataField="state" HeaderText="状态"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
               
             
               <asp:BoundField DataField="E_Remark" HeaderText="备注"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
             
            
           
            
            
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
 
 </asp:Content>