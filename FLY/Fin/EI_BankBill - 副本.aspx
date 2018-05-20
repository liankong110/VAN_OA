<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EI_BankBill.aspx.cs" Inherits="VAN_OA.Fin.EI_BankBill" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script language="javascript" type="text/javascript">
        window.resizeTo(screen.width, screen.height);

    </script>
    <style type="text/css">
        .CompanyName {
            position: absolute;
            left: 75px;
            top: 100px;
            width: 170px;
        }

        .CardNo {
            position: absolute;
            left: 75px;
            top: 128px;
            width: 170px;
        }

        .DaTotal {
            position: absolute;
            left: 75px;
            top: 228px;
            width: 200px;
        }


        .SupplierName {
            position: absolute;
            left: 305px;
            top: 100px;
            width: 200px;
        }

        .SupplierCardNo {
            position: absolute;
            left: 305px;
            top: 128px;
            width: 200px;
        }

        .BrandName {
            position: absolute;
            left: 305px;
            top: 168px;
            width: 200px;
        }

        .BrandAddress {
            position: absolute;
            left: 305px;
            top: 190px;
            width: 200px;
        }

        .DaNum {
            position: absolute;
            left: 305px;
            top: 230px;
            width: 180px;
            text-align: right;
            letter-spacing: 7px;
        }

        .Phone {
            position: absolute;
            left: 325px;
            top: 290px;
            width: 180px;
        }

        .Id {
            position: absolute;
            left: 140px;
            top: 305px;
            width: 180px;
        }


        .RightCompanyName {
            position: absolute;
            left: 565px;
            top: 120px;
            width: 170px;
        }

        .RightCompanyCardNo {
            position: absolute;
            left: 565px;
            top: 145px;
            width: 170px;
        }

        .Total {
            position: absolute;
            left: 555px;
            top: 225px;
            width: 170px;
        }

        .RightSupplierName {
            position: absolute;
            left: 555px;
            top: 265px;
            width: 170px;
        }

        .RightSupplierCardNo {
            position: absolute;
            left: 555px;
            top: 292px;
            width: 170px;
        }

        .RightPhone {
            position: absolute;
            left: 555px;
            top: 315px;
            width: 170px;
        }

        .RightBrandName {
            position: absolute;
            left: 555px;
            top: 335px;
            width: 170px;
        }

        .RightBrandAddress {
            position: absolute;
            left: 555px;
            top: 355px;
            width: 170px;
        }
        .RightUse{
            position: absolute;
            left: 525px;
            top: 405px;
            width: 170px;
        }
        #print {
            position: absolute;
            left: 465px;
            top: 555px;
        }
        /*#content input {
       border:0px;background:rgba(0, 0, 0, 0)
             }*/
        input {
            font-size: 14px;
        }
    </style>
    <style media="print" type="text/css">
        .Noprint {
            display: none;
        }

        .PageNext {
            page-break-after: always;
        }
    </style>
</head>
<body style="background-image: url('../Image/BankBill1.png'); background-repeat: no-repeat; vertical-align: top; margin: 0;">
    <form id="form1" runat="server">
        <div id="content" style="vertical-align: top">
            <asp:TextBox ID="txtCompanyName" runat="server" Text="123" class="CompanyName"></asp:TextBox>
            <asp:TextBox ID="txtCardNo" runat="server" Text="123" class="CardNo"></asp:TextBox>
            <asp:TextBox ID="txtDaTotal" runat="server" Text="123" class="DaTotal"></asp:TextBox>

            <asp:TextBox ID="txtSupplierName" runat="server" Text="123" class="SupplierName"></asp:TextBox>
            <asp:TextBox ID="txtSupplierCardNo" runat="server" Text="123" class="SupplierCardNo"></asp:TextBox>
            <asp:TextBox ID="txtBrandName" runat="server" Text="123" class="BrandName"></asp:TextBox>
            <asp:TextBox ID="txtBrandAddress" runat="server" Text="111111111" class="BrandAddress"></asp:TextBox>

            <asp:TextBox ID="txtNum" runat="server" Text="123" class="DaNum"></asp:TextBox>
            <asp:TextBox ID="txtPhone" runat="server" Text="手机号" class="Phone"></asp:TextBox>
            <asp:TextBox ID="txtId" runat="server" Text="身份证" class="Id"></asp:TextBox>

            <asp:TextBox ID="txtRightCompanyName" runat="server" Text="身份证" class="RightCompanyName"></asp:TextBox>
            <asp:TextBox ID="txtRightCompanyCardNo" runat="server" Text="身份证" class="RightCompanyCardNo"></asp:TextBox>
            <asp:TextBox ID="txtTotal" runat="server" Text="身份证" class="Total"></asp:TextBox>

            <asp:TextBox ID="txtRightSupplierName" runat="server" Text="123" class="RightSupplierName"></asp:TextBox>
            <asp:TextBox ID="txtRightSupplierCardNo" runat="server" Text="123" class="RightSupplierCardNo"></asp:TextBox>
            <asp:TextBox ID="txtRightPhone" runat="server" Text="手机号" class="RightPhone"></asp:TextBox>
            <asp:TextBox ID="txtRightBrandName" runat="server" Text="123" class="RightBrandName"></asp:TextBox>
            <asp:TextBox ID="txtRightBrandAddress" runat="server" Text="111111111" class="RightBrandAddress"></asp:TextBox>

             <asp:TextBox ID="txtUse" runat="server" Text="111111111" class="RightUse"></asp:TextBox>
        </div>

        <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WebBrowser"
            width="0">
        </object>
        <div id="print">
            <center class="Noprint">
            <input id="btnyulan" type="button" value="预览" onclick="document.all.WebBrowser.ExecWB(7, 1);" />
            <input id="btnymsz" type="button" value="页面设置" onclick="document.all.WebBrowser.ExecWB(8, 1);" />
            <input id="btndy" type="button" value="打印" onclick="document.all.WebBrowser.ExecWB(6, 1);" />
            <input id="btnclose" type="button" value="关闭" onclick="window.close();" />
        </center>
            </div>
    </form>
</body>
</html>
