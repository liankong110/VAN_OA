<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFGoodsTypeList.aspx.cs" Inherits="VAN_OA.BaseInfo.WFGoodsTypeList" MasterPageFile="~/DefaultMaster.Master"  Title="商品类别"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
    
   
    <cc1:TabPanel ID="TabPanel1" runat="server">
    <HeaderTemplate>
    商品类别
    </HeaderTemplate>
    <ContentTemplate>
        
   
    
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">商品类别</td>
    </tr>
    <tr>
        <td>商品类别：</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox> &nbsp;
             <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            <asp:Button ID="btnAdd" runat="server" Text="添加商品类别"  BackColor="Yellow" 
                onclick="btnAdd_Click" />
        </td>
    </tr>
</table><br>
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="100%"  AutoGenerateColumns="False" 
         onpageindexchanging="gvList_PageIndexChanging" 
         onrowdeleting="gvList_RowDeleting" onrowediting="gvList_RowEditing" 
         ondatabinding="gvList_DataBinding" onrowdatabound="gvList_RowDataBound">
           
         
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
                <td>
                编辑
                </td>   
                 <td>
                删除
                </td>    
                
     
 
	<td height="25"  align="center">
		商品类别
	</td>
              <tr>
                  <td colspan="9" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
      <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑"/>
                </ItemTemplate>
                <ItemStyle  HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"  CommandName="Delete" 
                    OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>  
	<asp:BoundField DataField="GoodTypeName" HeaderText="商品类别" SortExpression="GoodTypeName" ItemStyle-HorizontalAlign="Left" /> 		
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2"  BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
 </ContentTemplate>
    </cc1:TabPanel>
    
    
    
       <cc1:TabPanel ID="TabPanel2" runat="server">
    <HeaderTemplate>
    商品小类
    </HeaderTemplate>
    <ContentTemplate>


 <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">商品小类</td>
    </tr>
    <tr>
    <td>商品分类：</td><td> <asp:DropDownList ID="ddlGoodType" runat="server" DataTextField="GoodTypeName" DataValueField="GoodTypeName" Width="200px">
        </asp:DropDownList></td>
        <td>商品小类名称：</td>
        <td><asp:TextBox ID="txtGoodSmType" runat="server" Width="300px"></asp:TextBox> &nbsp;
             <asp:Button ID="Button1" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelectGoodSmType_Click"/>&nbsp;
            <asp:Button ID="Button2" runat="server" Text="添加商品小类"  BackColor="Yellow" 
                onclick="btnAddGoodSmType_Click" />
        </td>
    </tr>
</table><br>
<asp:GridView ID="gvGoodSmType" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="100%"  AutoGenerateColumns="False" 
         onpageindexchanging="gvGoodSmType_PageIndexChanging" 
         onrowdeleting="gvGoodSmType_RowDeleting" onrowediting="gvGoodSmType_RowEditing" 
         ondatabinding="gvGoodSmType_DataBinding" onrowdatabound="gvGoodSmType_RowDataBound">
           
         
         <EmptyDataTemplate>
       <table width="100%"  >
              <tr  style=" height:20px; background-color:#336699; color:White;" >
                <td>
                编辑
                </td>   
                 <td>
                删除
                </td>    
                
     
 
	<td height="25"  align="center">
		商品类别
	</td>
	<td height="25"  align="center">
		小类
	</td>
              <tr>
                  <td colspan="9" align="center" style="height:80%">---暂无数据---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
      <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑"/>
                </ItemTemplate>
                <ItemStyle  HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"  CommandName="Delete" 
                    OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>  
	<asp:BoundField DataField="GoodTypeName" HeaderText="商品类别" SortExpression="GoodTypeName" ItemStyle-HorizontalAlign="Left" /> 	
	<asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" ItemStyle-HorizontalAlign="Left" /> 		
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2"  BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>

 </ContentTemplate>
    </cc1:TabPanel>
  </cc1:TabContainer>
 </asp:Content>