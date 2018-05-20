<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="VAN_OA.Left" %>

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统菜单</title>
      <link href="mian.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       <div style="width:200px">
           <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0"
            HeaderCssClass="accordionHeader" ContentCssClass="accordionContent"
            FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" 
            AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true" >
            
            <HeaderTemplate>
                <div style="background-image:url("../Image/contact_large.gif")" ID="large">
                    <span><%# DataBinder.Eval(Container.DataItem, "HeardText")%></span>
                   
                </div>
                
               
            </HeaderTemplate>

            <ContentTemplate>
                <%# Eval("Body")%>


            </ContentTemplate>
      
        </ajaxToolkit:Accordion>
        </div>
    </div>
    </form>
</body>
</html>
