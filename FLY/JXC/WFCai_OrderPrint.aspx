<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCai_OrderPrint.aspx.cs"
    Inherits="VAN_OA.JXC.WFCai_OrderPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        window.resizeTo(screen.width, screen.height);

    </script>
    <style type="text/css">
        .all-line
        {
            border-collapse: collapse;
            font-size: 7px;
        }
        .all-line td
        {
            border: 1px solid #000;
            margin: 0px;
          
        }
        
        .MyTd
        {           
            font-size: 6px;
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
         <!
        --控制分页-- ></style>
</head>
<body style="font-size:7px; vertical-align: top; margin: 0;">
    <form id="form1" runat="server">
    <div style="vertical-align: top">    
        <table cellpadding="0" cellspacing="0" width="100%"
            class="all-line">
            <tr>
                <td>
                    含税
                </td>
                <td>
                    发票
                </td>
                <td>
                    审批时间
                </td>
                <td class="MyTd">
                    采购单号
                </td>
                <td>
                    项目编码
                </td>
                <td style="width:8%">
                    项目名称
                </td>
                <td class="MyTd">
                    项目日期
                </td>
                
                <td>
                    AE
                </td>
                
                <td>
                    业务
                </td>
                <td class="MyTd">
                    项目单号
                </td>
                <td>
                    编码
                </td>
                <td>
                    名称
                </td>
                <td>
                    小类
                </td>
                <td style="width:4%">
                    规格
                </td>
                <td>
                    单位
                </td>
                <td>
                    数量
                </td>
                <td>
                    最终供应商
                </td>
                <td>
                    最终价格
                </td>
                <td>
                    最终总价
                </td>
            </tr>
            <%
                decimal xiaoji = 0;
                for (int index =0; index < modelList.Count; index++)
                { 
                    var m = modelList[index];
                    xiaoji += m.Num * m.lastPrice;           
            %>
            <tr>
                <td>
                    <%=(m.IsHanShui ? "√" : "")%>
                </td>
                <td>
                      <%=m.CaiFpType.Replace("发票","")%>
                </td>
                <td class="MyTd">
                      <%=m.LastTime.Value.ToString("yyyy-MM-dd")%>
                </td>
                <td class="MyTd">
                      <%=m.ProNo%>
                </td>
                <td class="MyTd">
                      <%=m.PONo%>
                </td>
                <td>
                      <%=m.POName%>
                </td>
                <td>
                      <%=m.PODate.ToString("yyyy-MM-dd")%>
                </td>
                
                <td>
                      <%=m.AE%>
                </td>
               
                <td>
                      <%=m.BusType%>
                </td>
                <td class="MyTd">
                      <%=m.CG_ProNo%>
                </td>
                <td>
                      <%=m.GoodNo%>
                </td>
                <td>
                      <%=m.GoodName%>
                </td>
                <td>
                      <%=m.GoodTypeSmName%>
                </td>
                <td>
                      <%=m.GoodSpec%>
                </td>
                <td>
                      <%=m.GoodUnit%>
                </td>
                <td>
                      <%=m.Num.ToString("f2")%>
                </td>
                <td>
                      <%=m.lastSupplier%>
                </td>
                <td>
                      <%=m.lastPrice.ToString("f2") %>
                </td>
                <td>
                      <%=(m.Num * m.lastPrice).ToString("f2")%>
                </td>
            </tr>
            <%} %>
        </table>
       最终总价合计:<%=xiaoji %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请时间:<%=DateTime.Now %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       公司经办:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司领导:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
     
    <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WebBrowser"
        width="0">
    </object>
    <div id="print">
        <center class="Noprint">            
            <input id="btnclose" type="button" value="关闭" onclick="window.close();" />
        </center>
    </div>
    </form>
</body>
</html>
