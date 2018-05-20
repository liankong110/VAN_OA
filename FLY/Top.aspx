<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="VAN_OA.Top" %>

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="mian.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
      <div id="state" style="width:100%; background-color:White; height:20px; color:Black; margin-top:8px">
        
        欢迎你:<asp:Label ID="lblLoginName" runat="server" Text=""></asp:Label>
             
              <label id="Clock" style=" margin-left:100px"></label>
              
           
            
            
           
            <a href="UpdatePwd.aspx" target='right' style=" margin-left:50px" >密码修改</a>
           <%-- <asp:LinkButton ID="LinkButton2" runat="server" style=" margin-left:50px" PostBackUrl="~/UpdatePwd.aspx">密码修改</asp:LinkButton>--%>
            <asp:LinkButton ID="LinkButton1" runat="server"  style="  margin-left:200px; vertical-align:top" onclick="LinkButton1_Click">注销</asp:LinkButton>  

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
           
                <asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick">
                </asp:Timer>
                
              <a href="~/EFrom/MyEFormsTodo.aspx" target="right' style="margin-left:50px" >
                  <asp:Label ID="lblMessTodo" runat="server" Text=""></asp:Label></a>
              <%--  <asp:LinkButton ID="lblMessTodo" runat="server"  ForeColor="Red" PostBackUrl="~/EFrom/MyEFormsTodo.aspx"></asp:LinkButton>--%>
            </ContentTemplate>
            </asp:UpdatePanel>
              </div>
    </div>
    </form>
</body>
</html>
