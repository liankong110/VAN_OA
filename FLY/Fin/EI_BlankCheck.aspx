﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EI_BlankCheck.aspx.cs" Inherits="VAN_OA.Fin.EI_BlankCheck"  Title="支票"%>

<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script language="javascript" type="text/javascript">
        window.resizeTo(screen.width, screen.height);

    </script>
    <style type="text/css">

        .ProNo {
           position: absolute;
    left: 60px;
    top: 140px;
    width: 140px;
        }

        .Data {
             position: absolute;
    left: 97px;
    top: 168px;
    width: 140px;
        }

        .ShouKuanRen {
              position: absolute;
    left: 60px;
    top: 205px;
    width: 150px;     
        }

        .Total {
               position: absolute;
    left: 93px;
    top: 230px;
    width: 100px;
        }

        .Use {
            position: absolute;
    left: 93px;
    top: 250px;
    width: 100px;
        }

        .DaDate {
              position: absolute;
    left: 375px;
    top: 58px;
    width: 200px;
        }

        .DaShouKuan {
               position: absolute;
    left: 310px;
    top: 76px;
    width: 300px;
        }

        .DaTotal {
              position: absolute;
    left: 320px;
    top: 109px;
    width: 255px;
        }

        .DaNum {
           position: absolute;
    left: 675px;
    top: 118px;
    width: 170px;
    text-align: right;
    letter-spacing: 7px;
        }

        .DaUse {
             position: absolute;
    left: 295px;
    top: 145px;
    width: 100px;
        }

        .DaRemark {
            position: absolute;
            left: 465px;
            top: 245px;
            width: 200px;
        }

         #print {
            position: absolute;
            left: 465px;
            top:355px;           
        }
        #content input {
       border:0px;background:rgba(0, 0, 0, 0)
             }
        input {
        font-size:14px;
        }
    </style>

     <style media="print" type="text/css">
       
        .Noprint
        {
            display: none;
        }
         <!
        --用本样式在打印时隐藏非打印项目-- > .PageNext
        {
            page-break-after: always;
        }
         <!--控制分页-- >

     </style>
    
   
</head>
<body style="background-image: url('../Image/BlankCheck.png'); background-repeat: no-repeat;vertical-align:top;margin: 0; ">
    <form id="form1" runat="server">
        <div id="content" style="vertical-align:top">
            
            <asp:TextBox ID="txtPrNo" runat="server" Text="" class="ProNo"></asp:TextBox>
            <asp:TextBox ID="txtDate" runat="server" Text="" class="Data"></asp:TextBox>
            <asp:TextBox ID="txtShouKuanRen" runat="server" Text="" class="ShouKuanRen"></asp:TextBox>
            <asp:TextBox ID="txtTotal" runat="server" Text="" class="Total"></asp:TextBox>
            <asp:TextBox ID="txtUse" runat="server" Text="" class="Use"></asp:TextBox>

            <asp:TextBox ID="txtDaDate" runat="server" Text="" class="DaDate"></asp:TextBox>
            <asp:TextBox ID="txtDaShouKuan" runat="server" Text="" class="DaShouKuan"></asp:TextBox>
            <asp:TextBox ID="txtDaTotal" runat="server" Text="" class="DaTotal"></asp:TextBox>
            <asp:TextBox ID="txtDaNum" runat="server" Text="" class="DaNum"></asp:TextBox>
            <asp:TextBox ID="txtDaUse" runat="server" Text="" class="DaUse"></asp:TextBox>
            <asp:TextBox ID="txtDaRemark" runat="server" Text="" class="DaRemark"></asp:TextBox>
        </div>
         <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WebBrowser"
        width="0">
    </object>
         <div id="print" >
        <center class="Noprint">
               <asp:Button ID="btnPrint" runat="server" Text="打印" BackColor="Yellow" OnClick="btnPrint_Click"  />
        </center>
             
    </div>
    </form>
</body>
</html>
