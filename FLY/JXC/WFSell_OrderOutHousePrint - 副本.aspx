<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSell_OrderOutHousePrint.aspx.cs"
    Inherits="VAN_OA.JXC.WFSell_OrderOutHousePrint" %>

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
        }
        .all-line td
        {
            border: 2px solid #000;
            margin: 30px;
        }
        
    .MyTd
    {
           padding-top:1px;
	   font-size:11px;
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
<body style="font-size: 12px;vertical-align:top;margin: 0;">
    <form id="form1" runat="server">
    
  
    <div style="vertical-align:top">
        <%
            //decimal iniPx = 1280;
            //decimal iniCm = 43;
            decimal iniPx = 1380;
            decimal iniCm = 43;
            decimal fristWidth = iniPx * Convert.ToDecimal(9.9) / iniCm;
            decimal secondWidth = iniPx * Convert.ToDecimal(13.5) / iniCm;
            decimal height = iniPx * Convert.ToDecimal(9.3) / iniCm;
            decimal fristHeight = height / 3;
            decimal secondHeight = height * 2 / 3;

            for (int i = 0; i < allRows; i++)
            {
                if (i != 0)
                { 
                    %>
                    <br /> <br /> <br /> <br />
                     <%
                }
        %>
        <table>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="<%=fristWidth.ToString("f0") %>px"
                        style="border-collapse: collapse; height: <%=fristHeight.ToString("f0") %>px;  vertical-align:top;"
                        bordercolor="threeddarkshadow" border="0">
                        <tr>
                            <td class="style8" style="font-size: 14px;">
                                苏州万邦电脑系统有限公司
                            </td>
                            <td>
                                票号
                            </td>
                            <td>
                                <%=mainModel.ProNo%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h4 align="center">
                                    销售出库单</h4>
                            </td>
                            <td>
                                日期
                            </td>
                            <td>
                                <%=mainModel.RuTime.ToString("yyyy-MM-dd")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                (存根联)
                            </td>
                            <td>
                                第<%= i+1 %>张/共<%= allRows %>张
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px;" colspan="2">
                                单位:
                                <%=mainModel.Supplier%>
                            </td>
                            <td>
                                <%=houseName%>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="3">
                            项目单号:  <%=mainModel.PONo%>
                        </td>
                        </tr>
                    </table>
                </td>
                <td style="padding-left:10px;">
                
                    <table cellpadding="0" cellspacing="0" width="<%=secondWidth.ToString("f0") %>px"
                        style="border-collapse: collapse; height: <%=fristHeight.ToString("f0") %>px;vertical-align:top;"
                        bordercolor="threeddarkshadow" border="0">
                        <tr>
                            <td class="style1" style="font-size: 14px;">
                                苏州万邦电脑系统有限公司
                            </td>
                            <td>
                                票号
                            </td>
                            <td>
                                <%=mainModel.ProNo%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h4 align="center">
                                    销售出库单</h4>
                            </td>
                            <td>
                                日期
                            </td>
                            <td>
                                <%=mainModel.RuTime.ToString("yyyy-MM-dd")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                (货票联)
                            </td>
                            <td>
                                第<%= i+1 %>张/共<%= allRows %>张
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px" colspan="2">
                                单位:
                                <%=mainModel.Supplier%>
                            </td>
                            <td>
                                <%=houseName%>
                            </td>
                        </tr>
                        <tr style="height: 10px;">
                           
                        <td colspan="3">
                            项目单号:  <%=mainModel.PONo%>
                        </td>
                        
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <table cellpadding="0" cellspacing="0" width="<%=fristWidth.ToString("f0") %>px"
                        class="all-line">
                        <tr>
                        <td style="text-align: center">
                                编码
                            </td>
                            <td style="text-align: center">
                                商品
                            </td>
                            <td style="text-align: center">
                                数量
                            </td>
                            <td style="text-align: center">
                                金额
                            </td>
                        </tr>
                        <%
                            decimal xiaoji = 0;
                            for (int index = pageSize * i; index < pageSize * (i + 1); index++)
                            {
                                if (index >= modelList.Count)
                                {
                                    break;
                                }
                                var m = modelList[index];
                                xiaoji += m.GoodSellPriceTotal;
                                string name2 =m.GoodTypeSmName+ m.GoodName + m.GoodSpec + m.Good_Model;
                                if (name2.Length > 32)
                                {
                                    name2 = name2.Substring(0, 31);
                                }
                        %>
                        <tr>
                         <td  class="MyTd">
                                <%=m.GoodNo %>
                            </td>
                            <td  class="MyTd">
                                <%=name2 %>
                            </td>
                            <td style="text-align: right" class="MyTd">
                                <%=m.GoodNum%>
                            </td>
                            <td style="text-align: right" class="MyTd">
                                <%= string.Format("{0:n2}", m.GoodSellPriceTotal) %>
                            </td>
                        </tr>
                        <%
                            }
                
                        %>
                        <tr>
                            <td class="MyTd">
                                小计
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td style="text-align: right"  class="MyTd">
                                <%=  string.Format("{0:n2}",xiaoji) %>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" class="MyTd">
                                合计：
                            </td>
                            <td colspan="3" class="MyTd">
                                ¥<%= string.Format("{0:n2}", total) %>
                            </td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;发票号： &nbsp;
                    
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;经办人：<%= mainModel.DoPer%>
                </td>
                <td style="vertical-align: top; padding-left:10px;">
                    <table cellpadding="0" cellspacing="0" width="<%=secondWidth.ToString("f0") %>px"
                         class="all-line">
                        <tr>
                            <td style="text-align: center">
                                商品
                            </td>
                            <td style="text-align: center">
                                单位
                            </td>
                            <td style="text-align: center">
                                数量
                            </td>
                            <td style="text-align: center">
                                单价
                            </td>
                            <td style="text-align: center">
                                金额
                            </td>
                        </tr>
                        <%
                            xiaoji = 0;
                            for (int index = pageSize * i; index < pageSize * (i + 1); index++)
                            {
                                if (index >= modelList.Count)
                                {
                                    break;
                                }
                                var m = modelList[index];
                                xiaoji += m.GoodSellPriceTotal;
                                string name1 = m.GoodTypeSmName + m.GoodName + m.GoodSpec + m.Good_Model;
                                if (name1.Length > 32)
                                {
                                    name1 = name1.Substring(0, 31);
                                }
                        %>
                        <tr>
                            <td class="MyTd">
                                <%=name1%>
                            </td>
                            <td style="text-align: center" class="MyTd">
                                <%=m.GoodUnit%>
                            </td>
                            <td style="text-align: center" class="MyTd">
                                <%=m.GoodNum%>
                            </td>
                            <td style="text-align: right" class="MyTd">
                                <%=m.GoodSellPrice%>
                            </td>
                            <td style="text-align: right" class="MyTd"> 
                                <%= string.Format("{0:n2}", m.GoodSellPriceTotal) %>
                            </td>
                        </tr>
                        <%
                            }
                
                        %>
                        <tr>
                            <td colspan="4" class="MyTd">
                                小计
                            </td>
                            <td style="text-align: right" class="MyTd">
                                <%= string.Format("{0:n2}", xiaoji) %>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyTd">
                                合计：
                            </td>
                            <td colspan="3" class="MyTd" >
                                <%= string.Format("{0:n2}", totalDa) %>
                            </td>
                            <td class="MyTd">
                                ¥<%= string.Format("{0:n2}", total) %>
                            </td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;经办人： &nbsp;
                    <%= mainModel.DoPer%>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;收货人：
                </td>
            </tr>
        </table>
    </div>
    <%
        } %>
        
        <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WebBrowser"
        width="0">
    </object>
    <div id="print">
        <center class="Noprint">
            <input id="btnyulan" type="button" value="预览" onclick="document.all.WebBrowser.ExecWB(7,1);" />
            <input id="btnymsz" type="button" value="页面设置" onclick="document.all.WebBrowser.ExecWB(8,1);" />
            <input id="btndy" type="button" value="打印" onclick="document.all.WebBrowser.ExecWB(6,1);" />
            <input id="btnclose" type="button" value="关闭" onclick="window.close();" />
        </center>
    </div>
    </form>
</body>
</html>
