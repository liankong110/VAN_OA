<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QPInvDetails.aspx.cs" Inherits="VAN_OA.EFrom.QPInvDetails"
    Culture="auto" UICulture="auto" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <%--  <meta http-equiv="Content-Type" content="text/html; charset=gb2312"></meta>--%>
    <base target="_self" />
    <title>����������</title>
    <script type="text/javascript">
        function count1() {

            var sl = document.getElementById("txtNum").value;
            var dj = document.getElementById("txtCostPrice").value;
            if ((sl != "") && (dj != "")) {

                var total = sl * dj;
                document.getElementById("txtCostTotal").value = total.toFixed(3).toString();
            }
            else {
                document.getElementById("txtCostTotal").value = "0.00";


            }



        }



        function count3() {

            count1();
        }

        function CheckId() {

            var sl = document.getElementById("txtGoodNo").value;
            if (sl != "") {

                var arr = sl.split("\\");

                if (arr.length == 7) {
                    document.getElementById("txtGoodNo").value = arr[0];
                    document.getElementById("txtInvName").value = arr[1];
                    document.getElementById("lblGoodSmTypeName").value = arr[2];
                    document.getElementById("lblSpec").value = arr[3];
                    document.getElementById("txtModel").value = arr[4];
                    document.getElementById("txtUnit").value = arr[5];
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
                <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                    ��������
                </td>
            </tr>
            <tr>
                <td height="25" width="*" align="right">
                    ���루�������Ǵ�\���룩 ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtGoodNo" runat="server" Width="280px" onblur="CheckId();"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                        ServiceMethod="GetGoods" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                        TargetControlID="txtGoodNo">
                    </cc1:AutoCompleteExtender>
                  
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    ���� ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtInvName" runat="server" Width="280px"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtInvName"> </asp:RequiredFieldValidator>    <font style="color: Red">*</font>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    С�ࣺ
                </td>
                <td>
                    <asp:TextBox ID="lblGoodSmTypeName" runat="server" Text="" Width="280px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    �ͺţ�
                </td>
                <td>
                    <asp:TextBox ID="lblSpec" runat="server" Text="" Width="280px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="lblSpec"> </asp:RequiredFieldValidator>    <font style="color: Red">*</font>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    ��� ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtModel" runat="server" Width="280px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    ��λ ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtUnit" runat="server" Width="280px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtUnit"> </asp:RequiredFieldValidator>    <font style="color: Red">*</font>
                </td>
            </tr>
             <tr>
                <td height="25" width="30%" align="right">
                    Ʒ�� ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtGoodBrand" runat="server" Width="280px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td height="25" width="30%" align="right">
                    ���� ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtProduct" runat="server" Width="280px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td height="25" width="30%" align="right">
                    ��ע ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtRemark" runat="server" Width="280px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    ���� ��
                </td>
                <td height="25" width="*" align="left">
                    <asp:TextBox ID="txtNum" runat="server" Width="280px" onKeyUp="count3();" OnTextChanged="txtNum_TextChanged"></asp:TextBox>               
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtNum"> </asp:RequiredFieldValidator>    <font style="color: Red">*</font>
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="style1">
                    ���� ��
                </td>
                <td width="*" align="left" class="style1">
                    <asp:TextBox ID="txtCostPrice" runat="server" Width="280px" onKeyUp="count1();" OnTextChanged="txtCostPrice_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        ValidationGroup="a" ControlToValidate="txtCostPrice"> </asp:RequiredFieldValidator>    <font style="color: Red">*</font>
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="style1">
                    �ܶ� ��
                </td>
                <td width="*" align="left" class="style1">
                    <asp:TextBox ID="txtCostTotal" runat="server" value="0.00" ReadOnly="true" Width="280px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="height: 80%">
                  ע��<font style="color: Red">*</font> Ϊ ������
                    <asp:Button ID="btnSave" runat="server" Text="����" BackColor="Yellow" OnClick="btnSave_Click"
                        ValidationGroup="a" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="ȡ��" BackColor="Yellow" OnClick="btnCancel_Click" />&nbsp;
                    <br />
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
