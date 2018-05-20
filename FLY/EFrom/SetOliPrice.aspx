<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetOliPrice.aspx.cs" Inherits="VAN_OA.EFrom.SetOliPrice" MasterPageFile="~/DefaultMaster.Master" Title="油价设置" %>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 
 油价系数:<asp:TextBox ID="txtPrice" runat="server" Width="232px"></asp:TextBox>
     <asp:Button ID="btnSave"
     runat="server" Text="保存"  BackColor="Yellow" onclick="btnSave_Click" />
 </asp:Content>