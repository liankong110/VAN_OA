<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewWFSell_OrderOutHousePrint.aspx.cs"
    Inherits="VAN_OA.JXC.NewWFSell_OrderOutHousePrint" %>

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
            margin: 20px;
        }
        
        .MyTd
        {
            padding-top: 1px;
            font-size: 11px;
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
<body style="font-size: 12px; vertical-align: top; margin: 0;">
    <form id="form1" runat="server">
    <div style="vertical-align: top">
        <%
            //decimal iniPx = 1280;
            //decimal iniCm = 43;
            decimal iniPx = 1375;
            decimal iniCm = 43;
            decimal fristWidth = 2 * iniPx * Convert.ToDecimal(12) / iniCm;

            decimal height = iniPx * Convert.ToDecimal(8) / iniCm;
            decimal fristHeight = height / 5;
            decimal secondHeight = height * 2 / 3;

            for (int i = 0; i < allRows; i++)
            {
                if (i != 0)
                { 
        %>
        <br />
        <br />
        <br />
        <br />
        <%
                }
        %>
        <table>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="<%=fristWidth.ToString("f0") %>px"
                        style="border-collapse: collapse; height: <%=fristHeight.ToString("f0") %>px;
                        vertical-align: top;" bordercolor="threeddarkshadow" border="0">
                        <tr>
                            <td align="center" colspan="7">
                                <font style="font-family:KaiTi; margin:0; padding:0; font-size:26px;">
                                    苏州万邦电脑系统有限公司销售出库单</font>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                出库日期:
                                <%=mainModel.RuTime.ToString("yyyy-MM-dd")%>
                            </td>
                            <td>
                            </td>
                            <td>
                                单据号:
                                <%=mainModel.ProNo%>
                            </td>
                            <td>
                            </td>
                            <td>
                                项目编号:
                                <%=mainModel.PONo%>
                            </td>
                            <td>
                            </td>
                            <td>
                                经手人:
                                <%=mainModel.DoPer%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                收货单位:
                                <%=mainModel.Supplier%>
                            </td>
                            <td>
                            </td>
                            <td colspan="4">
                                第一联货票联/第二联客户联/第三联存根联
                            </td>
                            <td>
                                第<%= i+1 %>张/共<%= allRows %>张
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <table cellpadding="0" cellspacing="0" width="<%=fristWidth.ToString("f0") %>px"
                        class="all-line" >
                        <tr>
                            <td style="text-align: center">
                                序号
                            </td>
                            <td style="text-align: center">
                                商品编码
                            </td>
                            <td style="text-align: center">
                                仓位号
                            </td>
                            <td style="text-align: center">
                                商品名称
                            </td>
                            <td style="text-align: center">
                                数量
                            </td>
                            <td style="text-align: center">
                                单价(RMB)
                            </td>
                            <td style="text-align: center">
                                金额(RMB)
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
                    string name2 = m.GoodName + m.GoodSpec;
                    if (name2.Length > 50)
                    {
                        name2 = name2.Substring(0, 49);
                    }
                        %>
                        <tr>
                            <td class="MyTd" align="center">
                                <%=(index+1)%>
                            </td>
                            <td class="MyTd">
                                <%=m.GoodNo %>
                            </td>
                            <td class="MyTd">
                                <%=m.GoodAreaNumber %>
                            </td>
                            <td class="MyTd">
                                <%=name2 %>
                            </td>
                            <td style="text-align: right" class="MyTd">
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
                            <td class="MyTd">
                                小计
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td style="text-align: right" class="MyTd">
                                <%=  string.Format("{0:n2}",xiaoji) %>
                            </td>
                        </tr>
                        <% if (i == allRows - 1)
                           { 
                        %>
                        <tr>
                            <td class="style4" colspan="4" class="MyTd">
                                合计：<%= totalDa %>
                            </td>
                            <td class="MyTd">
                            </td>
                            <td class="MyTd">
                            </td>
                            <td class="MyTd">
                                ¥<%= string.Format("{0:n2}", total) %>
                            </td>
                        </tr>
                        <%
                           } %>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <table cellpadding="0" cellspacing="0" width="<%=fristWidth.ToString("f0") %>px"
                        style="border-collapse: collapse; vertical-align: top;" bordercolor="threeddarkshadow"
                        border="0">
                        <tr>
                            <td colspan="2">
                                地址：苏州市南门路707号领秀江南商铺10-102
                            </td>
                            <td>
                                电话：0512-65622270 65622279
                            </td>
                        </tr>
                        <tr>
                            <td>
                                销售部：<%= mainModel.CreateName %>
                            </td>
                            <td>
                                库房：
                            </td>
                            <td>
                                收货人：
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                此单据签收，等同于认定商品价款和支付条件，如无质量问题不可退货，此单签收后作为货款欠条
                            </td>
                        </tr>
                    </table>
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
