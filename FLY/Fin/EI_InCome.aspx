<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EI_InCome.aspx.cs" Inherits="VAN_OA.Fin.EI_InCome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script language="javascript" type="text/javascript">
        window.resizeTo(screen.width, screen.height);

    </script>
    <style type="text/css">
        .Date {
            position: absolute;
            left: 215px;
            top: 44px;
            width: 200px;
        }

        .CompanyName {
            position: absolute;
            left: 80px;
            top: 70px;
            width: 200px;
        }

        .CompanyCardNo {
            position: absolute;
            left: 80px;
            top: 92px;
            width: 200px;
        }

        .CompanyBrandName {
            position: absolute;
            left: 80px;
            top: 115px;
            width: 200px;
        }

        .DaTotal {
            position: absolute;
            left: 70px;
            top: 150px;
            width: 240px;
        }

        .SupplierName {
            position: absolute;
            left: 370px;
            top: 70px;
            width: 210px;
        }

        .SupplierCardNo {
            position: absolute;
            left: 370px;
            top: 92px;
            width: 200px;
        }

        .SupplierBrandName {
            position: absolute;
            left: 370px;
            top: 115px;
            width: 200px;
        }

        .DaNum {
            position: absolute;
            left: 395px;
            top: 155px;
            width: 170px;
            text-align: right;
            letter-spacing: 5.5px;
        }

         .RightUse {
            position: absolute;
            left: 365px;
            top: 193px;
            width: 200px;
        }

        #print {
            position: absolute;
            left: 465px;
            top: 355px;
        }
        #content input {
       border:0px;background:rgba(0, 0, 0, 0)
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
<body style="background-image: url('../Image/InCome.png'); background-repeat: no-repeat; vertical-align: top; margin: 0;">
    <form id="form1" runat="server">
        <div id="content" style="vertical-align: top">
            <asp:TextBox ID="txtDate" runat="server" Text="123" class="Date"></asp:TextBox>
            <asp:TextBox ID="txtCompanyName" runat="server" Text="万邦" class="CompanyName"></asp:TextBox>
            <asp:TextBox ID="txtCompanyCardNo" runat="server" Text="身份证" class="CompanyCardNo"></asp:TextBox>
            <asp:TextBox ID="txtCompanyBrandName" runat="server" Text="123" class="CompanyBrandName"></asp:TextBox>
            <asp:TextBox ID="txtDaTotal" runat="server" Text="123" class="DaTotal"></asp:TextBox>

            <asp:TextBox ID="txtSupplierName" runat="server" Text="123" class="SupplierName"></asp:TextBox>
            <asp:TextBox ID="txtSupplierCardNo" runat="server" Text="123" class="SupplierCardNo"></asp:TextBox>
            <asp:TextBox ID="txtSupplierBrandName" runat="server" Text="123" class="SupplierBrandName"></asp:TextBox>
            <asp:TextBox ID="txtNum" runat="server" Text="123" class="DaNum"></asp:TextBox>
            <asp:TextBox ID="txtUse" runat="server" Text="111111111" class="RightUse"></asp:TextBox>
        </div>
         <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WebBrowser"
            width="0">
        </object>
        <div id="print">
            <center class="Noprint">
            <input id="btnyulan" type="button" value="预览" onclick="check();document.all.WebBrowser.ExecWB(7, 1);" />
            <input id="btnymsz" type="button" value="页面设置" onclick="check();document.all.WebBrowser.ExecWB(8, 1);" />
            <input id="btndy" type="button" value="打印" onclick="check();document.all.WebBrowser.ExecWB(6, 1);" />
            <input id="btnclose" type="button" value="关闭" onclick="window.close();" />
        </center>
        </div>
    </form>
      <script type="text/javascript">

        function check() {
            var supplierName = document.getElementById('<%= txtSupplierName.ClientID %>').value;
            
            if (supplierName.length > 15) {
                document.getElementById('<%= txtSupplierName.ClientID %>').style.fontSize = "10px";
            } else {
                  document.getElementById('<%= txtSupplierName.ClientID %>').style.fontSize = "14px";
            }
           
        }
    </script>
</body>
</html>
