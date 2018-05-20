<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CG_Orders.aspx.cs" Inherits="VAN_OA.JXC.CG_Orders"
    Culture="auto" UICulture="auto" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>订单报批表</title>

    <script type="text/javascript">        
function count1() 
{
 
 var sl = document.getElementById("txtNum").value;
 var dj = document.getElementById("txtCostPrice").value;
 if ((sl!="")&&(dj!=""))
 {
    
   var total=sl*dj;
   document.getElementById("txtCostTotal").value=total.toFixed(4).toString(); 
 }
 else
 {
   document.getElementById("txtCostTotal").value = "0.00";
   
    
 }
  
  

}

function count2() 
{
 
 var sl = document.getElementById("txtNum").value;
 var dj = document.getElementById("txtSellPrice").value;
 if ((sl!="")&&(dj!=""))
 {
   
   var total=sl*dj;
   document.getElementById("txtSellTotal").value=total.toFixed(4).toString(); 
 }
 else
 {
   document.getElementById("txtSellTotal").value = "0.00";
 }
    
}

function count3() 
{
     count2();
     count1();
 }

 function CheckId() {

     var sl = document.getElementById("txtInvName").value;
     if (sl != "") {
//        sl=sl.replace(/\\/g,",,,");
        var arr = sl.split("\\");
       
        if (arr.length == 7) {
            document.getElementById("txtGoodNo").value = arr[0];     
                document.getElementById("txtInvName").value = arr[1];
                document.getElementById("lblGoodSmTypeName").value = arr[2];
                document.getElementById("lblSpec").value = arr[3];
                document.getElementById("lblModel").value = arr[4];
                document.getElementById("lblUnit").value = arr[5];             
        }
     }
 }


 
    </script>

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
                <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                    订单报批表
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    编码 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtGoodNo" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    名称（输入助记词\编码） ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtInvName" runat="server" Width="300px" onblur="CheckId();"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                        ServiceMethod="GetGoods" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                        TargetControlID="txtInvName">
                    </cc1:AutoCompleteExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtInvName"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    小类：
                </td>
                <td>
                    <asp:TextBox ID="lblGoodSmTypeName" runat="server" Text="" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    规格：
                </td>
                <td>
                    <asp:TextBox ID="lblSpec" runat="server" Text="" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    型号：
                </td>
                <td>
                    <asp:TextBox ID="lblModel" runat="server" Text="" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    单位：
                </td>
                <td>
                    <asp:TextBox ID="lblUnit" runat="server" Text="" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    数量 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtNum" runat="server" Width="300px" onKeyUp="count3();" OnTextChanged="txtNum_TextChanged"></asp:TextBox>
                  <%--      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式有误"
                            ValidationGroup="a" ControlToValidate="txtNum" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtNum"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="style1">
                    成本单价 ：
                </td>
                <td width="*" align="left" class="style1">
                    <asp:TextBox ID="txtCostPrice" runat="server" Width="300px" onKeyUp="count1();" OnTextChanged="txtCostPrice_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtCostPrice"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="style1">
                    成本总额 ：
                </td>
                <td width="*" align="left" class="style1">
                    <asp:TextBox ID="txtCostTotal" runat="server" value="0.00" ReadOnly="true" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    销售单价 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtSellPrice" runat="server" Width="300px" onKeyUp="count2();" OnTextChanged="txtSellPrice_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtSellPrice"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    销售总额 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtSellTotal" ReadOnly="true" runat="server" Width="300px" value="0.00"
                        OnTextChanged="txtSellTotal_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    管理费 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtOtherCost" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    到帐日期 ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtToTime" runat="server" Width="300px"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToTime"
                        PopupButtonID="ImageButton1" Format="yyyy-MM-dd">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    利润% ：
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtProfit" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    备注：
                </td>
                <td>
                    <asp:TextBox ID="txtDetailRemark" runat="server" Text="" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="height: 80%">
                    <asp:Button ID="btnSave" runat="server" Text="保存" BackColor="Yellow" OnClick="btnSave_Click"
                        ValidationGroup="a" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" BackColor="Yellow" OnClick="btnCancel_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
