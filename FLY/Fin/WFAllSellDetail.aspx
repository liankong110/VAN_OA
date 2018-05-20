<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFAllSellDetail.aspx.cs"
    Inherits="VAN_OA.JXC.WFAllSellDetail" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="销售结算明细表" %>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="VAN_OA.Model.BaseInfo" %>
<%@ Import Namespace="VAN_OA.Model" %>
<%@ Import Namespace="VAN_OA.Model.Fin" %>
<%@ Import Namespace="VAN_OA.Model.EFrom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                销售结算明细表
            </td>
        </tr>
        <tr>
            <td>
                公司名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" AutoPostBack="true" runat="server" DataTextField="ComName"
                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
                AE：
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                财年年月：
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                    <asp:ListItem Value="12" Text="12" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
              项目关闭：
                <asp:DropDownList ID="ddlIsClose" runat="server">
                    <asp:ListItem Text="全部" Value="-1"  Selected="True"> </asp:ListItem>
                    <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="未关闭" Value="0"> </asp:ListItem>
                </asp:DropDownList>
                结算选中：
                <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="选中" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                </asp:DropDownList> 
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="Button2" runat="server" Text=" 打印版面 " BackColor="Yellow" 
                        onclick="Button2_Click"  />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 返 回 " BackColor="Yellow" OnClick="Button1_Click" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="<%= allCount %>" style="height: 20px; background-color: #336699; color: White;">
                    公司名称:<asp:Label ID="lblSimpName1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    财年年月:<asp:Label ID="lblYearMonth" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    公司年度总销售额:<asp:Label ID="lblSellTotal" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    公司年度总用车里程数:<asp:Label ID="lblCarTotal" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px; background-color: #336699; color: White;">
                <td>
                    费用类型
                </td>
                <%
                    foreach (var pro in all_propertyList)
                    { 
                %>
                <td>
                    <%= pro.CostType %>
                </td>
                <%
                    }
                %>
                <td>
                    总计
                </td>
                <td>
                    用车公里数
                </td>
                <td>
                    销售额百分比
                </td>
            </tr>
            <%
                decimal sum_AllTotal = 0;
                decimal sum_RoadLong = 0;
                System.Collections.Generic.Dictionary<int,decimal> tsTbComm = new System.Collections.Generic.Dictionary<int,decimal>();
                System.Collections.Generic.Dictionary<int,decimal> tsTbSpec = new System.Collections.Generic.Dictionary<int,decimal>();
                foreach (var u in user)
                {
                    decimal RoadLong = 0;
                    var carM = UseCarDetailList.Find(t => t.UserId == u.Id);
                    if (carM != null)
                    {
                        RoadLong = carM.RoadLong;
                    }

                    decimal sellT = 0;
                    var sellM = reportDetailList.Find(t => t.AE == u.LoginName);
                    if (sellM != null)
                    {
                        sellT = sellM.SellTotal;
                    }
                    string baifenbi = "0%";

                    //所有费用汇总
                    decimal allTotal = 0;
                    var user_spec_propertyList = spec_propertyList.FindAll(uu => uu.UserID == u.Id);


                    if (RoadLongTotal == 0 && SellTotal == 0 && user_spec_propertyList.Sum(t => t.Total) == 0)
                    {
                        continue;
                    }
            %><tr>
                <td>
                    <%=u.LoginName%>
                </td>
                <%

                    foreach (var m in all_propertyList)
                    {
                        
                        if (m.MyProperty == "公共")
                        {
                            decimal m_value = 0;
                            var com_m = comm_propertyList.Find(t => t.CostTypeId == m.Id&&t.CompId==u.CompanyId);

                            if (com_m != null)
                            {
                                if (m.CostType == "汽油费补差" || m.CostType == "汽车维修费" || m.CostType == "汽车保险")
                                {
                                    if (RoadLongTotal != 0)
                                    {
                                        m_value = (RoadLong / RoadLongTotal) * com_m.Total;
                                    }
                                   
                                }
                                else
                                {
                                    if (SellTotal != 0)
                                    {
                                        m_value = (sellT / SellTotal) * com_m.Total;
                                        baifenbi = ((sellT / SellTotal) * 100).ToString("n2") + "%";
                                    }

                                }
                            }
                            if (!tsTbComm.ContainsKey(m.Id))
                            {
                                tsTbComm.Add(m.Id, m_value);
                            }
                            else
                            {
                                tsTbComm[m.Id] = tsTbComm[m.Id] + m_value;
                            }
                            allTotal += m_value;
                %>
                <td>
                    <%=m_value.ToString("n2")%>
                </td>
                <%
    }
    if (m.MyProperty == "个性")
    {
        decimal m_value = 0;
        var spec_m = user_spec_propertyList.Find(t => t.CostTypeId == m.Id && t.CompId == u.CompanyId);
        if (spec_m != null)
        {
            m_value = spec_m.Total;
        }
        if (!tsTbSpec.ContainsKey(m.Id))
        {
            tsTbSpec.Add(m.Id, m_value);
        }
        else
        {
            tsTbSpec[m.Id]= tsTbSpec[m.Id]+m_value;
        }
        allTotal += m_value;      
                %>
                <td>
                    <%= m_value%>
                </td>
                <%
    }

}
u.Total = allTotal;
sum_RoadLong += RoadLong;
                %>
                <td>
                    <%= allTotal.ToString("n2") %>
                </td>
                <td>
                    <%= RoadLong %>
                </td>
                <td>
                    <%=baifenbi%>
                </td>
            </tr>
            <%
sum_AllTotal += allTotal;
                }
            %>
            <tr style="height: 20px; background-color: Yellow;">
                <td>
                    合计
                </td>
                <%
                    foreach (var m in all_propertyList)
                    {
                        if (m.MyProperty == "公共")
                        {
                            decimal m_value = 0;
                            if (tsTbComm.ContainsKey(m.Id))
                            {
                                m_value += Convert.ToDecimal(tsTbComm[m.Id]);// comm_propertyList.Find(t => t.CostTypeId == m.Id);
                            }
                        
                %>
                <td>
                    <%=m_value.ToString("n4")%>
                </td>
                <%
                        }
                        if (m.MyProperty == "个性")
                        {
                            decimal m_value = 0;
                           // var m_value = spec_propertyList.FindAll(t => t.CostTypeId == m.Id).Sum(t => t.Total);
                            if (tsTbSpec.ContainsKey(m.Id))
                            {
                                 m_value = Convert.ToDecimal(tsTbSpec[m.Id]);
                            }                       
                       
                %>
                <td>
                    <%= m_value.ToString("n4")%>
                </td>
                <%
                        }

                    } 
                %>
                <td>
                    <%=sum_AllTotal.ToString("n4")%>
                </td>
                <td>
                    <asp:Label ID="lblSumCarTotal" runat="server" Text="0" Visible="false"></asp:Label>
                    <%=sum_RoadLong.ToString("n4")%>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Both">
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="21" style="height: 40px; background-color: #336699; color: White;">
                    公司：
                    <asp:Label ID="lblSimpName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height: 40px; background-color: #336699; color: White;">
                <td rowspan="2">
                    财年：<br />
                    <asp:Label ID="lblYearMonth1" runat="server" Text="Label"></asp:Label>
                </td>
                <td colspan="10">
                    全年合计
                </td>
                <td colspan="10">
                    选中合计
                </td>
            </tr>
            <tr style="height: 40px; background-color: #336699; color: White;">
                <td style="height: 20px; background-color: #336699; color: White;">
                    总销售额
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业销售额
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业利润
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府销售额
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府利润
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    总成本
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业成本
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府成本
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业净利
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府净利
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    总销售额
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业销售额
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业利润
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府销售额
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府利润
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    总成本
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业成本
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府成本
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    企业净利
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    政府净利
                </td>
            </tr>
            <% 
            
                decimal SUM_X1 = 0;
                decimal SUM_X6 = 0;
                decimal SUM_X7 = 0;
                decimal SUM_X8 = 0;
                decimal SUM_X9 = 0;
                decimal SUM_X10 = 0;
                decimal SUM_X11 = 0;
                decimal SUM_X16 = 0;
                decimal SUM_X17 = 0;
                decimal SUM_X18 = 0;
                decimal SUM_X19 = 0;
                decimal SUM_X20 = 0;

                decimal AllTotal = 0;
                foreach (var model in allList)
                {
                    decimal X1 = model.SellTotal_QZ + model.SellTotal_ZZ;
                    var u = user.Find(t => t.LoginName == model.AE);
                    decimal X6 = u != null ? u.Total : 0;
                    decimal X7 = X1 != 0 ? model.SellTotal_QZ / X1 * X6 : 0;
                    decimal X8 = X1 != 0 ? model.SellTotal_ZZ / X1 * X6 : 0;
                    decimal X9 = model.MaoLi_QZ - X7;
                    decimal X10 = model.MaoLi_ZZ - X8;
                    decimal X11 = model.SellTotal_QXZ + model.SellTotal_ZXZ;
                    decimal X16 = X1 != 0 ? X11 / X1 * X6 : 0;
                    decimal X17 = X11 != 0 ? model.SellTotal_QXZ / X11 * X16 : 0;
                    decimal X18 = X11 != 0 ? model.SellTotal_ZXZ / X11 * X16 : 0;
                    decimal X19 = model.MaoLi_QXZ - X17;
                    decimal X20 = model.MaoLi_ZXZ - X18;
                    SUM_X1 += X1;
                    SUM_X6 += X6;
                    SUM_X7 += X7;
                    SUM_X8 += X8;
                    SUM_X9 += X9;
                    SUM_X10 += X10;
                    SUM_X11 += X11;
                    SUM_X16 += X16;
                    SUM_X17 += X17;
                    SUM_X18 += X18;
                    SUM_X19 += X19;
                    SUM_X20 += X20;
                    
            %>
            <tr>
                <td>
                    <%=model.AE %>
                </td>
                <td>
                    <%=X1%>
                </td>
                <td>
                    <%//企业销售额 %>
                    <%=model.SellTotal_QZ %>
                </td>
                <td>
                    <%//企业利润 %>
                    <%=model.MaoLi_QZ %>
                </td>
                <td>
                    <%//政府销售额 %>
                    <%=model.SellTotal_ZZ %>
                </td>
                <td>
                    <%//政府利润 %>
                    <%=model.MaoLi_ZZ %>
                </td>
                <td>
                    <%=X6.ToString("n4")%>
                </td>
                <td>
                    <%=X7.ToString("n4")%>
                </td>
                <td>
                    <%=X8.ToString("n4")%>
                </td>
                <td>
                    <%=X9.ToString("n4")%>
                </td>
                <td>
                    <%=X10.ToString("n4")%>
                </td>
                <td>
                    <%=X11.ToString("n4")%>
                </td>
                <td>
                    <%//企业销售额 %>
                    <%=model.SellTotal_QXZ %>
                </td>
                <td>
                    <%//企业利润 %>
                    <%=model.MaoLi_QXZ %>
                </td>
                <td>
                    <%//政府销售额 %>
                    <%=model.SellTotal_ZXZ %>
                </td>
                <td>
                    <%//政府利润 %>
                    <%=model.MaoLi_ZXZ %>
                </td>
                <td>
                    <%=X16.ToString("n4")%>
                </td>
                <td>
                    <%=X17.ToString("n4")%>
                </td>
                <td>
                    <%=X18.ToString("n4")%>
                </td>
                <td>
                    <%=X19.ToString("n4")%>
                </td>
                <td>
                    <%=X20.ToString("n4")%>
                </td>
            </tr>
            <%} %>
            <tr style="height: 20px; background-color: Yellow;">
                <td>
                    合计
                </td>
                <td>
                    <%=SUM_X1%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_QZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_QZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_ZZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_ZZ)%>
                </td>
                <td>
                    <%=SUM_X6.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X7.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X8.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X9.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X10.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X11.ToString("n4")%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_QXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_QXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_ZXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_ZXZ)%>
                </td>
                <td>
                    <%=SUM_X16.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X17.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X18.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X19.ToString("n4")%>
                </td>
                <td>
                    <%=SUM_X20.ToString("n4")%>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
