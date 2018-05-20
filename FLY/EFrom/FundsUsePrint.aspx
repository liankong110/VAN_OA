<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundsUsePrint.aspx.cs" Inherits="VAN_OA.EFrom.FundsUsePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用款单</title>
    <script language="javascript" type="text/javascript">
    window.resizeTo(screen.width,screen.height);

    </script>
 <style media="print" type="text/css">
.Noprint{display:none;}<!--用本样式在打印时隐藏非打印项目-->
.PageNext{page-break-after: always;}<!--控制分页-->
</style>
</head>
<body style="font-size:12px">
    <form id="form1" runat="server">
     <OBJECT classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WebBrowser" width="0"></OBJECT>
      <div id="print">
         <center class="Noprint">
        <input id="btnyulan" type="button" value="预览" onclick="document.all.WebBrowser.ExecWB(7,1);" />
        <input id="btnymsz" type="button" value="页面设置" onclick="document.all.WebBrowser.ExecWB(8,1);" />
        <input id="btndy" type="button" value="打印" onclick="document.all.WebBrowser.ExecWB(6,1);" />
        <input id="btnclose" type="button" value="关闭" onclick="window.close();" />
        </center>
    </div>
    <br />
    <div> 
 
 
 
    <table cellpadding="0" cellspacing="0" width="590px" style="border-collapse:collapse;" bordercolor="threeddarkshadow" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">请款单</td>
        </tr>
        <tr>
            <td style="width:100px">申请部门：</td>
            <td>
                <asp:Label ID="lblDepartName1" runat="server" Text=""></asp:Label>
            
            </td>
             
            
              <td>姓名：</td>
            <td> 
            
            <asp:Label ID="lblName1" runat="server" Text=""></asp:Label>
            
            </td>
            
            
             <td>日期：</td>
            <td>   
            
              <asp:Label ID="lblDate1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
        
             <td>
             款项用途：
             </td>
            <td colspan="5">
               
                  <asp:Label ID="lblUseTo1" runat="server" Text="" style="word-break:break-all"></asp:Label>
            </td>
        </tr>
         <tr>
     
             <td>
             款项支付
             </td>
            <td colspan="5">
                
              
              <asp:Label ID="lblTyp1" runat="server" Text=""></asp:Label>
                
                       
                         
            </td>
        </tr>
        <tr>
             <td >
             用款金额(小写)：</td>
            <td colspan="5">
                 <asp:Label ID="lblTotal1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
         <tr>
             <td >
             用款金额(大写)：
             </td>
            <td colspan="5">
                <asp:Label ID="lblDaTotal1" runat="server" Text="0" Font-Size="16px" ForeColor="Red" Font-Bold="true" ></asp:Label>
            </td>
        </tr>
        
        
         
        
         
         
           <tr>
             <td >
              发票号：
             </td>
            <td colspan="1">
                <asp:Label ID="lblInvoce1" runat="server" Text=""  ></asp:Label>
            </td>
            
             <td >
              仓库清单号：
             </td>
            <td colspan="3">
                <asp:Label ID="lblHouseN01" runat="server" Text=""  ></asp:Label>
            </td>
        </tr>
        
          <tr>
             <td >
            本次意见：
             </td>
            <td colspan="5">
                <asp:Label ID="lblIdea1" runat="server" Text="" style="word-break:break-all"></asp:Label>
            </td>
        </tr>
        
    </table>
   <br />
   <br />
    
       <table cellpadding="0" cellspacing="0" width="590px"  style="border-collapse:collapse;" bordercolor="threeddarkshadow" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">请款单</td>
        </tr>
        <tr>
            <td style="width:100px">申请部门：</td>
            <td>
                <asp:Label ID="lblDepartName2" runat="server" Text=""></asp:Label>
            
            </td>
             
            
              <td>姓名：</td>
            <td> 
            
            <asp:Label ID="lblName2" runat="server" Text=""></asp:Label>
            
            </td>
            
            
             <td>日期：</td>
            <td>   
            
              <asp:Label ID="lblDate2" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
        
             <td>
             款项用途：
             </td>
            <td colspan="5">
               
                  <asp:Label ID="lblUserTo2" runat="server" Text="" style="word-break:break-all"></asp:Label>
            </td>
        </tr>
         <tr>
     
             <td>
             款项支付
             </td>
            <td colspan="5">
                
              
              <asp:Label ID="lblType2" runat="server" Text=""></asp:Label>
                
                       
                         
            </td>
        </tr>
        <tr>
             <td >
             用款金额(小写)：</td>
            <td colspan="5">
                 <asp:Label ID="lblTotal2" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
         <tr>
             <td >
             用款金额(大写)：
             </td>
            <td colspan="5">
                <asp:Label ID="lblDaTotal2" runat="server" Text="0" Font-Size="16px" ForeColor="Red" Font-Bold="true" ></asp:Label>
            </td>
        </tr>
        
         
        
           <tr>
             <td >
              发票号：
             </td>
            <td colspan="1">
                <asp:Label ID="lblInvoce2" runat="server" Text=""  ></asp:Label>
            </td>
            
             <td >
              仓库清单号：
             </td>
            <td colspan="3">
                <asp:Label ID="lblHouseNo2" runat="server" Text=""  ></asp:Label>
            </td>
        </tr>
        
          <tr>
             <td >
            本次意见：
             </td>
            <td colspan="5">
                <asp:Label ID="lblIdea2" runat="server" Text="" style="word-break:break-all"></asp:Label>
            </td>
        </tr>
         
        
    </table>
    
    
    <br />
   <br />
    
    
     <table cellpadding="0" cellspacing="0" width="590px"  style="border-collapse:collapse;" bordercolor="threeddarkshadow" border="1" >
         <tr>
            <td colspan="6" style=" height:20px; background-color:#336699; color:White;">请款单</td>
        </tr>
        <tr>
            <td style="width:100px">申请部门：</td>
            <td>
                <asp:Label ID="lblDepartName" runat="server" Text=""></asp:Label>
            
            </td>
             
            
              <td>姓名：</td>
            <td> 
            
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
            
            </td>
            
            
             <td>日期：</td>
            <td>   
            
              <asp:Label ID="lblDateTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
        
             <td>
             款项用途：
             </td>
            <td colspan="5">
               
                  <asp:Label ID="lblUseTo" runat="server" Text="" style="word-break:break-all"></asp:Label>
            </td>
        </tr>
         <tr>
     
             <td>
             款项支付
             </td>
            <td colspan="5">
                
              
              <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                
                       
                         
            </td>
        </tr>
        <tr>
             <td >
             用款金额(小写)：</td>
            <td colspan="5">
                 <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
         <tr>
             <td >
             用款金额(大写)：
             </td>
            <td colspan="5">
                <asp:Label ID="lblTotalDa" runat="server" Text="0" Font-Size="16px" ForeColor="Red" Font-Bold="true" ></asp:Label>
            </td>
        </tr>
        
         
         <tr>
             <td >
              发票号：
             </td>
            <td colspan="1">
                <asp:Label ID="lblInvoce" runat="server" Text=""  ></asp:Label>
            </td>
            
             <td >
              仓库清单号：
             </td>
            <td colspan="3">
                <asp:Label ID="lblHouseNo" runat="server" Text=""  ></asp:Label>
            </td>
        </tr>
        
          <tr>
             <td >
            本次意见：
             </td>
            <td colspan="5">
                <asp:Label ID="lblIdea" runat="server" Text="" style="word-break:break-all"></asp:Label>
            </td>
        </tr>
         
         
       
    </table>
    
 


    
    </div>
    </form>
</body>
</html>
