<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFAccountPeriodList.aspx.cs" Inherits="VAN_OA.BaseInfo.WFAccountPeriodList" MasterPageFile="~/DefaultMaster.Master"  Title="账期"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
 
        
   
    
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">账期系数列表</td>
    </tr>
    <tr>
        <td>账期：</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox> &nbsp;
             <asp:Button ID="btnSelect" runat="server" Text=" 查 询 "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            <asp:Button ID="btnAdd" runat="server" Text="账期系数"  BackColor="Yellow" 
                onclick="btnAdd_Click" />
        </td>
    </tr>
</table><br>
<asp:GridView ID="gvList" runat="server"   BorderColor="#FBFBFB" 
    BorderStyle="Solid"  DataKeyNames="Id"  
          Width="100%"  AutoGenerateColumns="False" 
          
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
		账期
	</td>
	<td height="25"  align="center">
		系数
	</td>
	
	<td height="25"  align="center">
		备注
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
	<asp:BoundField DataField="AccountName" HeaderText="账期" SortExpression="AccountName" ItemStyle-HorizontalAlign="Left" /> 	
		<asp:BoundField DataField="AccountXiShu" HeaderText="系数" SortExpression="AccountXiShu" ItemStyle-HorizontalAlign="Left" /> 	
		<asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Left" /> 	
   </Columns>
     <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px"/>
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="InfoDetail2"  BackColor="#FBFBFB"/>
            <RowStyle CssClass="InfoDetail1"/>
</asp:GridView>
  
 </asp:Content>