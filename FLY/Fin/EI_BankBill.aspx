<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EI_BankBill.aspx.cs" Inherits="VAN_OA.Fin.EI_BankBill" Title="结算业务申请单" %>

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
            left: 90px;
            top: 125px;
            width: 170px;
        }

        .CardNo {
            position: absolute;
            left: 90px;
            top: 160px;
            width: 170px;
        }

        .DaTotal {
            position: absolute;
            left: 90px;
            top: 270px;
            width: 300px;
        }


        .SupplierName {
            position: absolute;
            left: 355px;
            top: 125px;
            width: 200px;
        }

        .SupplierCardNo {
            position: absolute;
            left: 355px;
            top: 160px;
            width: 200px;
        }

        .BrandName {
            position: absolute;
            left: 350px;
            top: 208px;
            width: 200px;
        }

        .BrandAddress {
            position: absolute;
            left: 350px;
            top: 233px;
            width: 200px;
        }

        .DaNum {
            position: absolute;
            left: 367px;
            top: 280px;
            width: 180px;
            text-align: right;
            letter-spacing: 6px;
        }

        .Phone {
            position: absolute;
            left: 350px;
            top: 342px;
            width: 180px
        }

        .Ida {
            position: absolute;
            left: 160px;
            top: 360px;
            width: 180px;
        }


        .RightCompanyName {
            position: absolute;
            left: 630px;
            top: 150px;
            width: 170px;
        }

        .RightCompanyCardNo {
            position: absolute;
            left: 630px;
            top: 185px;
            width: 170px;
        }

        .Total {
            position: absolute;
            left: 630px;
            top: 270px;
            width: 170px;
        }

        .RightSupplierName {
            position: absolute;
            left: 630px;
            top: 315px;
            width: 170px;
        }

        .RightSupplierCardNo {
            position: absolute;
            left: 630px;
            top: 352px;
            width: 170px;
        }

        

        .RightPhone {
            position: absolute;
            left: 630px;
            top: 375px;
            width: 170px;
        }

        .RightBrandName {
            position: absolute;
            left: 630px;
            top: 398px;
            width: 170px;
        }

        .RightBrandAddress {
            position: absolute;
            left: 630px;
            top: 420px;
            width: 170px;
        }

        .RightUse {
            position: absolute;
            left: 595px;
            top: 495px;
            width: 170px;
        }

        #print {
            position: absolute;
            left: 465px;
            top: 605px;
        }

        #content input {
            border-style: none;
            border-color: inherit;
            border-width: 0px;
            right: 2283px;
        }
         #content input {
            border: 0px;
            background: rgba(0, 0, 0, 0)
        }
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
            <asp:TextBox ID="txtId" runat="server" Text="320382198610227010" class="Ida"></asp:TextBox>

            <asp:TextBox ID="txtRightCompanyName" runat="server" Text="身份证" class="RightCompanyName"></asp:TextBox>
            <asp:TextBox ID="txtRightCompanyCardNo" runat="server" Text="身份证" class="RightCompanyCardNo"></asp:TextBox>
            <asp:TextBox ID="txtTotal" runat="server" Text="123456" class="Total"></asp:TextBox>

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
               <asp:Button ID="btnPrint" runat="server" Text="打印" BackColor="Yellow" OnClick="btnPrint_Click"  />
        </center>
        </div>
    </form>


</body>
</html>
