<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseView.aspx.cs" Inherits="VAN_OA.DatabaseView" MasterPageFile="~/DefaultMaster.Master" Title="资料库"%>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">资料库查询</td>
    </tr>
    <tr>
        <td>文件名称:</td>
        <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox> &nbsp;
            
        </td>
        
         
              
         
        
    </tr>
    <tr>
    <td colspan="2">
    <div style="float:right">
     <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            </div>
    </td>
    </tr>
    
</table><br>


<table width="100%" >
<tr>
<td valign="top" style="width:15%">
  <asp:TreeView ID="tvMain" runat="server"  
                onselectednodechanged="tvMain_SelectedNodeChanged">
            </asp:TreeView>

</td>
<td style="width:85%" valign="top">
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="id"  
          Width="100%" AutoGenerateColumns="False" 
         onpageindexchanging="gvList_PageIndexChanging" 
         onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
         onrowdatabound="gvList_RowDataBound" onrowcommand="gvList_RowCommand">
         
         
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
               
                <td>
                下载
                </td>
                <td>
                文件名称
                </td>   
                       
                        
              </tr>
              <tr>
                  <td colspan="6" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
        
             
               <asp:TemplateField HeaderText="下载">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDownLoad" runat="server" ImageUrl="~/Image/atchm.gif" AlternateText="下载"  CommandName="Down"  CommandArgument='<%# Eval("id") %>'/>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
        
            
             
         <asp:BoundField DataField="fileName" HeaderText="文件名称"  >
                <ItemStyle HorizontalAlign="Left" BorderColor="#E5E5E5"/>
             </asp:BoundField>
           
             
             
                     
        
            
            
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
</td>
</tr>
</table>
 

 
 </asp:Content>
