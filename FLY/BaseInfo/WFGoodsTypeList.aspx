<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFGoodsTypeList.aspx.cs" Inherits="VAN_OA.BaseInfo.WFGoodsTypeList" MasterPageFile="~/DefaultMaster.Master"  Title="��Ʒ���"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
    
   
    <cc1:TabPanel ID="TabPanel1" runat="server">
    <HeaderTemplate>
    ��Ʒ���
    </HeaderTemplate>
    <ContentTemplate>
        
   
    
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">��Ʒ���</td>
    </tr>
    <tr>
        <td>��Ʒ���</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox> &nbsp;
             <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ "  BackColor="Yellow" 
                    onclick="btnSelect_Click"/>&nbsp;
            <asp:Button ID="btnAdd" runat="server" Text="�����Ʒ���"  BackColor="Yellow" 
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
                �༭
                </td>   
                 <td>
                ɾ��
                </td>    
                
     
 
	<td height="25"  align="center">
		��Ʒ���
	</td>
              <tr>
                  <td colspan="9" align="center" style="height:80%">---��������---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
      <asp:TemplateField HeaderText=" �༭">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="�༭"/>
                </ItemTemplate>
                <ItemStyle  HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="ɾ��">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"  CommandName="Delete" 
                    OnClientClick='return confirm( "ȷ��ɾ����") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>  
	<asp:BoundField DataField="GoodTypeName" HeaderText="��Ʒ���" SortExpression="GoodTypeName" ItemStyle-HorizontalAlign="Left" /> 		
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
    ��ƷС��
    </HeaderTemplate>
    <ContentTemplate>


 <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
     <tr>
        <td colspan="8" style=" height:20px; background-color:#336699; color:White;">��ƷС��</td>
    </tr>
    <tr>
    <td>��Ʒ���ࣺ</td><td> <asp:DropDownList ID="ddlGoodType" runat="server" DataTextField="GoodTypeName" DataValueField="GoodTypeName" Width="200px">
        </asp:DropDownList></td>
        <td>��ƷС�����ƣ�</td>
        <td><asp:TextBox ID="txtGoodSmType" runat="server" Width="300px"></asp:TextBox> &nbsp;
             <asp:Button ID="Button1" runat="server" Text=" �� ѯ "  BackColor="Yellow" 
                    onclick="btnSelectGoodSmType_Click"/>&nbsp;
            <asp:Button ID="Button2" runat="server" Text="�����ƷС��"  BackColor="Yellow" 
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
                �༭
                </td>   
                 <td>
                ɾ��
                </td>    
                
     
 
	<td height="25"  align="center">
		��Ʒ���
	</td>
	<td height="25"  align="center">
		С��
	</td>
              <tr>
                  <td colspan="9" align="center" style="height:80%">---��������---</td>
               </tr>
        </table>
    </EmptyDataTemplate>

    <Columns>
      <asp:TemplateField HeaderText=" �༭">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="�༭"/>
                </ItemTemplate>
                <ItemStyle  HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="ɾ��">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"  CommandName="Delete" 
                    OnClientClick='return confirm( "ȷ��ɾ����") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
             </asp:TemplateField>  
	<asp:BoundField DataField="GoodTypeName" HeaderText="��Ʒ���" SortExpression="GoodTypeName" ItemStyle-HorizontalAlign="Left" /> 	
	<asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" ItemStyle-HorizontalAlign="Left" /> 		
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