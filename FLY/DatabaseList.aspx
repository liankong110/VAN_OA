﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseList.aspx.cs" Inherits="VAN_OA.DatabaseList" MasterPageFile="~/DefaultMaster.Master" Title="资料库管理" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">资料库查询</td>
    </tr>
    <tr>
        <td>文件名称:</td>
        <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox> &nbsp;
            
        </td>
        
         <td>文件路径:</td>
        <td><asp:TextBox ID="txtURL" runat="server"></asp:TextBox> &nbsp;
              
        </td>
        
    </tr>
    <tr>
    <td colspan="4">
    <div style="float:right">
     <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            <asp:Button ID="btnAdd" runat="server" Text="添加"  BackColor="Yellow" 
                onclick="btnAdd_Click" /> </div>
    </td>
    </tr>
    
</table><br>
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="id"  
          Width="100%"  AllowPaging="True" AutoGenerateColumns="False" 
         onpageindexchanging="gvList_PageIndexChanging" 
         onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
         onrowdatabound="gvList_RowDataBound" onrowcommand="gvList_RowCommand">
         
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
                编辑
                </td>   
                 <td>
                删除
                </td>    
                <td>
                下载
                </td>
                <td>
                文件名称
                </td>   
                 <td>
                文件路径
                </td>            
                <tr>
                上传人
                </tr>  
                 <tr>
                上传时间
                </tr>                   
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
             
               <asp:TemplateField HeaderText="下载">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDownLoad" runat="server" ImageUrl="~/Image/atchm.gif" AlternateText="下载"  CommandName="Down"  CommandArgument='<%# Eval("id") %>'/>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
        
            
             
         <asp:BoundField DataField="fileName" HeaderText="文件名称"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
             <asp:BoundField DataField="fileURL" HeaderText="文件路径"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
             
             
                     
         <asp:BoundField DataField="createPer" HeaderText="上传人"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
             <asp:BoundField DataField="createTime" HeaderText="上传时间"  >
                    <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
             </asp:BoundField>
            
            
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
 
 </asp:Content>
