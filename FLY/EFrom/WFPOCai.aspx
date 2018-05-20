<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFPOCai.aspx.cs" Inherits="VAN_OA.EFrom.WFPOCai" Culture="auto" UICulture="auto"  ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <title>采购</title>
<%--
    <script type="text/javascript"> 
    function count1() 
    {
     
     var sl = document.getElementById("txtNum").value;
     var dj = document.getElementById("txtSupperPrice").value;
     if ((sl!="")&&(dj!=""))
     {
        
       var total=sl*dj;
       document.getElementById("txtTotal1").value=total.toFixed(3).toString(); 
     }
     else
     {
       document.getElementById("txtTotal1").value = "0.00";
       
        
     }
     }
 
 
// 
// function count2() 
//{
// 
// var sl = document.getElementById("txtNum").value;
// var dj = document.getElementById("txtPrice2").value;
// if ((sl!="")&&(dj!=""))
// {
//    
//   var total=sl*dj;
//   document.getElementById("txtTotal2").value=total.toFixed(3).toString(); 
// }
// else
// {
//   document.getElementById("txtTotal2").value = "0.00";
//   
//    
// }
// 
// }
// function count3() 
//{
// 
// var sl = document.getElementById("txtNum").value;
// var dj = document.getElementById("txtPrice3").value;
// if ((sl!="")&&(dj!=""))
// {
//    
//   var total=sl*dj;
//   document.getElementById("txtTotal3").value=total.toFixed(3).toString(); 
// }
// else
// {
//   document.getElementById("txtTotal3").value = "0.00";
//   
//    
// }
// }
    </script>
--%>
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                    采购
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    客户 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    产品 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtInvName" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    数量 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtNum" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    日期：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtCaiTime" runat="server" Width="300px"></asp:TextBox>
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCaiTime"
                        Format="yyyy-MM-dd" PopupButtonID="Image1">
                    </cc1:CalendarExtender>
                    <font style="color: Red">*</font>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    采购询供应商1 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtSupplier" runat="server" Width="300px"></asp:TextBox>
                    <font style="color: Red">*</font>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    采购询价1 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtSupperPrice" runat="server" Width="300px"  ></asp:TextBox>
                    <font style="color: Red">*</font>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    小计1 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtTotal1" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td height="25" width="30%" align="right">
                更新人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUpdateUser" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    审批意见 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtIdea" runat="server" Width="300px" Height="200px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="height: 80%">
                    <asp:Button ID="btnSave" runat="server" Text="保存" BackColor="Yellow" OnClick="btnSave_Click"
                        ValidationGroup="a" Width="74px" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" BackColor="Yellow" OnClick="btnCancel_Click"
                        Width="77px" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
