<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JXCSum_SkilllList.aspx.cs"
    Inherits="VAN_OA.JXC.JXCSum_SkilllList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="工程技术派工统计报表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <style type="text/css">
        .item {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>

    <script type="text/javascript">
        function GetDetail(PONO) {

            var url = "../JXC/JXC_REPORTList.aspx?PONo=" + PONO + "&IsSpecial=true";

            window.open(url, 'newwindow')
        }
    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">工程技术派工统计报表
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                    DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>项目名称:
            </td>
            <td>
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>项目时间:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>

            </td>
            <td>项目归类
            </td>
            <td>
                <asp:DropDownList ID="ddlIsSpecial" runat="server">
                    <asp:ListItem Value="0">不含特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                </asp:DropDownList>
                <%-- <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" /> --%>
            </td>
        </tr>
        <tr>
            <td colspan="2">客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                DataTextField="GuestType">
            </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td colspan="2">项目金额
                <asp:DropDownList ID="ddlFuHao" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                销售金额&nbsp;&nbsp;&nbsp;&nbsp;
                 项目金额
                <asp:DropDownList ID="ddlPOFaTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                发票金额

                  项目金额
                <asp:DropDownList ID="ddlShiJiDaoKuan" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                实到帐
            </td>
        </tr>
        <tr>
            <td colspan="4">项目关闭：
                <asp:DropDownList ID="ddlIsClose" runat="server">
                    <asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
                    <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="未关闭" Value="0"> </asp:ListItem>
                </asp:DropDownList>
                项目选中：
                <asp:DropDownList ID="ddlIsSelect" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未选中</asp:ListItem>
                    <asp:ListItem Value="1">选中</asp:ListItem>
                </asp:DropDownList>
                结算选中：
                   <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                       <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                       <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                       <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                   </asp:DropDownList>项目类别：
                  <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                      <%-- <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1">零售</asp:ListItem>
                    <asp:ListItem Value="2">工程</asp:ListItem>--%>
                  </asp:DropDownList>
                项目模型: 
                <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                实际帐期: 
                <asp:DropDownList ID="ddlTrueZhangQI" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<=30天" Value="1"></asp:ListItem>
                    <asp:ListItem Text="30天<实际帐期<=60天" Value="2"></asp:ListItem>
                    <asp:ListItem Text="60天<实际帐期<=90天" Value="3"></asp:ListItem>
                    <asp:ListItem Text="90天<实际帐期<=120天" Value="4"></asp:ListItem>
                    <asp:ListItem Text="实际帐期>120天" Value="5"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:CheckBox ID="CheckBox1" runat="server" Text="初始状态" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="已交付" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="已开票" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="已结清" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox5" runat="server" Text="发票金额不一致" /><asp:CheckBox
                    ID="CheckBox6" runat="server" Text="实际金额不一致" />

                项目净利
                <asp:DropDownList ID="ddlProProfit" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtProProfit" runat="server" Width="80px"></asp:TextBox>
                实际净利
                <asp:DropDownList ID="ddlProTureProfit" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtProTureProfit" runat="server" Width="80px"></asp:TextBox>
                项目净利
                <asp:DropDownList ID="ddlJingLiTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                实际净利
                <br />
                项目金额
                <asp:DropDownList ID="ddlEquPOTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtEquTotal" runat="server" Width="80px"></asp:TextBox>

                净利润率
                 <asp:DropDownList ID="ddlJingLi" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtJingLi" runat="server" Width="80px"></asp:TextBox>
                被派工员:<asp:DropDownList ID="ddlPaiGong" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>派工日期:   
                <asp:TextBox ID="txtPGFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPGTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPGFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPGTo">
                </cc1:CalendarExtender>
                反馈总得分: 
                <asp:DropDownList ID="ddlFKDF" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtFKDF" runat="server" Width="80px"></asp:TextBox>
                综合总得分: 
                <asp:DropDownList ID="ddlAllScore" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtAllScore" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    派工日期:<asp:Label ID="lblPaiDateMess" runat="server" Text=""></asp:Label>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid" 
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand" PageSize="20" OnRowEditing="gvList_RowEditing">
            <PagerTemplate>
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>被派员工
                        </td>
                        <td>派工总小时
                        </td>
                        <td>工程反馈得分
                        </td>
                        <td>零售反馈得分
                        </td>
                        <td>系统反馈得分
                        </td>
                        <td>反馈均得分
                        </td>
                        <td>反馈总得分
                        </td>
                        <td>工程综合得分
                        </td>
                        <td>工程综合占比
                        </td>
                        <td>零售综合得分
                        </td>
                        <td>零售综合占比
                        </td>
                        <td>系统综合得分
                        </td>
                        <td>系统综合占比
                        </td>
                        <td>综合均分
                        </td>
                        <td>综合总得分
                        </td>
                        <td>公司综合总得分
                        </td>
                        <td>综合得分占比
                        </td>
                        <td>项目总金额
                        </td>
                        <td>总到款率
                        </td>
                    </tr>
                    <tr>
                        <td colspan="19" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>

                <asp:BoundField DataField="Name" HeaderText="被派员工" SortExpression="Name" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Hours" HeaderText="派工总小时" SortExpression="Hours" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Gong_Value" HeaderText="工程反馈得分" SortExpression="Gong_Value">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Ling_Value" HeaderText="零售反馈得分" SortExpression="Ling_Value"
                    HeaderStyle-Width="30" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Xi_Value" HeaderText="系统反馈得分" SortExpression="Xi_Value"
                    HeaderStyle-Width="30" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="AvgValue" HeaderText="反馈均得分" DataFormatString="{0:n2}"
                    SortExpression="AvgValue" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SumValue" HeaderText="反馈总得分" DataFormatString="{0:n2}"
                    SortExpression="SumValue" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Gong_Score" HeaderText="工程综合得分" DataFormatString="{0:n2}"
                    SortExpression="Gong_Score" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Gong_Score_Per" HeaderText="工程综合占比" DataFormatString="{0:n2}"
                    SortExpression="Gong_Score_Per" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Ling_Score" HeaderText="零售综合得分" DataFormatString="{0:n2}"
                    SortExpression="Ling_Score" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Ling_Score_Per" HeaderText="零售综合占比" SortExpression="Ling_Score_Per"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Xi_Score" HeaderText="系统综合得分" SortExpression="Xi_Score"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Xi_Score_Per" HeaderText="系统综合占比" SortExpression="Xi_Score_Per"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="AvgScore" HeaderText="综合均分" SortExpression="AvgScore"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SumScore" HeaderText="综合总得分" SortExpression="SumScore"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CompanyScore" HeaderText="公司综合总得分" SortExpression="CompanyScore"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CompanyScore_Per" HeaderText="综合得分占比" SortExpression="CompanyScore_Per"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n6}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PoTotal" HeaderText="项目总金额" SortExpression="PoTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DaoKuan_Per" HeaderText="总到款率" SortExpression="DaoKuan_Per"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
        <br />
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="20" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>

    <br />
    综合得分总计：<asp:Label ID="lblAllScore" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    反馈得分总计：<asp:Label ID="lblFanKuiScore" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
     平均综合得分：<asp:Label ID="lblAvgScore" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    平均反馈得分：<asp:Label ID="lblAvgFKscore" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;

      <br />
    工程综合得分总计：<asp:Label ID="lblGong_Socre" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    零售综合得分总计：<asp:Label ID="lblLing_Score" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
     系统综合得分总计：<asp:Label ID="lblXi_Score" runat="server" Text="0" ForeColor="Red"></asp:Label>
</asp:Content>
