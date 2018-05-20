<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFTAdminDelete.aspx.cs" Inherits="VAN_OA.WFTAdminDelete"  MasterPageFile="~/DefaultMaster.Master" Title="删除权限管理"%>
<asp:Content  runat="server" ContentPlaceHolderID="SampleContent">

 <asp:Button ID="btnAdd" runat="server" Text="添加用户"  BackColor="Yellow" 
                onclick="btnAdd_Click" />
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="100%"  AutoGenerateColumns="False" 
         
         onrowdeleting="gvList_RowDeleting" 
         ondatabinding="gvList_DataBinding" onrowdatabound="gvList_RowDataBound" 
         onselectedindexchanged="gvList_SelectedIndexChanged">
         
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
                删除
                </td>    
                
                <td>
                用户名称
                </td>   
                                             
              </tr>
              <tr>
                  <td colspan="6" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
      
             <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"  CommandName="Delete" 
                    OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>  
             
         <asp:BoundField DataField="UserName" HeaderText="用户名称"  >
                <ItemStyle HorizontalAlign="Center"  BorderColor="#E5E5E5"/>
             </asp:BoundField>
             
             
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2"  BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
 

</asp:Content>

 