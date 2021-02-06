<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFContractList.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFContractList" MasterPageFile="~/DefaultMaster.Master"
    Title="合同档案管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style>
        table {
            border-collapse: collapse;
            border-spacing: 0;
            border-left: 1px solid #888;
            border-top: 1px solid #888;
        }

        th, td {
            border-right: 1px solid #888;
            border-bottom: 1px solid #888;
            padding: 1px 1px;
        }

        th {
            font-weight: bold;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">合同档案管理
            </td>
        </tr>
        <tr>
            <td>合同编号：
            </td>
            <td>
                <asp:TextBox ID="txtContract_No" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>合约单位：
            </td>
            <td>
                <asp:TextBox ID="txtContract_Unit" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>合同名称：
            </td>
            <td>
                <asp:TextBox ID="txtContract_Name" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>合同摘要：
            </td>
            <td>
                <asp:TextBox ID="txtContract_Summary" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>项目编号：
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>备注：
            </td>
            <td>
                <asp:TextBox ID="txtContract_Remark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>合同类别：
            </td>
            <td>
                <asp:DropDownList ID="ddlContract_Type" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">采购合同</asp:ListItem>
                    <asp:ListItem Value="2">销售合同</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>合同类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlContract_Use" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="供货">供货</asp:ListItem>
                    <asp:ListItem Value="系统集成">系统集成</asp:ListItem>
                    <asp:ListItem Value="人工费">人工费</asp:ListItem>
                    <asp:ListItem Value="工程">工程</asp:ListItem>
                    <asp:ListItem Value="咨询">咨询</asp:ListItem>
                    <asp:ListItem Value="设计">设计</asp:ListItem>
                    <asp:ListItem Value="服务">服务</asp:ListItem>
                    <asp:ListItem Value="服务">服务</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>总金额
            </td>
            <td>
                <asp:DropDownList ID="ddlContract_Total" runat="server">
                    <asp:ListItem Value=">">></asp:ListItem>
                    <asp:ListItem Value="<"><</asp:ListItem>
                    <asp:ListItem Value=">=">>=</asp:ListItem>
                    <asp:ListItem Value="<="><=</asp:ListItem>
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value="<>"><></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtContract_Total" runat="server" Width="50px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>签订时间:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td colspan="4">合同总页数
                <asp:DropDownList ID="ddlContract_PageCount" runat="server">
                    <asp:ListItem Value=">">></asp:ListItem>
                    <asp:ListItem Value="<"><</asp:ListItem>
                    <asp:ListItem Value=">=">>=</asp:ListItem>
                    <asp:ListItem Value="<="><=</asp:ListItem>
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value="<>"><></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtContract_PageCount" runat="server" Width="50px"></asp:TextBox>

                合同总份数
                <asp:DropDownList ID="ddlContract_AllCount" runat="server">
                    <asp:ListItem Value=">">></asp:ListItem>
                    <asp:ListItem Value="<"><</asp:ListItem>
                    <asp:ListItem Value=">=">>=</asp:ListItem>
                    <asp:ListItem Value="<="><=</asp:ListItem>
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value="<>"><></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtContract_AllCount" runat="server" Width="50px"></asp:TextBox>

                己方份数
                <asp:DropDownList ID="ddlContract_BCount" runat="server">
                    <asp:ListItem Value=">">></asp:ListItem>
                    <asp:ListItem Value="<"><</asp:ListItem>
                    <asp:ListItem Value=">=">>=</asp:ListItem>
                    <asp:ListItem Value="<="><=</asp:ListItem>
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value="<>"><></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtContract_BCount" runat="server" Width="50px"></asp:TextBox>

                AE:
             <asp:DropDownList ID="ddlAE" runat="server" DataTextField="LoginName"
                 DataValueField="LoginName">
             </asp:DropDownList>
                项目模型: 
                <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">经手人:
             <asp:DropDownList ID="ddlContract_Brokerage" runat="server" DataTextField="LoginName"
                 DataValueField="LoginName">
             </asp:DropDownList>

                签收:
            <asp:DropDownList ID="ddlContract_IsSign" runat="server">
                <asp:ListItem Value="-1">全部</asp:ListItem>
                <asp:ListItem Value="0">否</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td colspan="4">存放位置 ：
              <asp:DropDownList ID="ddlContract_Local" runat="server">
                  <asp:ListItem Value="-1">全部</asp:ListItem>

                  <asp:ListItem Value="A">A</asp:ListItem>
                  <asp:ListItem Value="B">B</asp:ListItem>
                  <asp:ListItem Value="C">C</asp:ListItem>
                  <asp:ListItem Value="D">D</asp:ListItem>
                  <asp:ListItem Value="E">E</asp:ListItem>
                  <asp:ListItem Value="F">F</asp:ListItem>
                  <asp:ListItem Value="G">G</asp:ListItem>
                  <asp:ListItem Value="H">H</asp:ListItem>
                  <asp:ListItem Value="I">I</asp:ListItem>
                  <asp:ListItem Value="J">J</asp:ListItem>
                  <asp:ListItem Value="K">K</asp:ListItem>
                  <asp:ListItem Value="L">L</asp:ListItem>
                  <asp:ListItem Value="M">M</asp:ListItem>
                  <asp:ListItem Value="N">N</asp:ListItem>
                  <asp:ListItem Value="O">O</asp:ListItem>
                  <asp:ListItem Value="P">P</asp:ListItem>
                  <asp:ListItem Value="Q">Q</asp:ListItem>
                  <asp:ListItem Value="R">R</asp:ListItem>
                  <asp:ListItem Value="S">S</asp:ListItem>
                  <asp:ListItem Value="T">T</asp:ListItem>
                  <asp:ListItem Value="U">U</asp:ListItem>
                  <asp:ListItem Value="V">V</asp:ListItem>
                  <asp:ListItem Value="W">W</asp:ListItem>
                  <asp:ListItem Value="X">X</asp:ListItem>
                  <asp:ListItem Value="Y">Y</asp:ListItem>
                  <asp:ListItem Value="Z">Z</asp:ListItem>
              </asp:DropDownList>
                <asp:DropDownList ID="ddlContract_Year" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="2012">2012</asp:ListItem>
                    <asp:ListItem Value="2013">2013</asp:ListItem>
                    <asp:ListItem Value="2014">2014</asp:ListItem>
                    <asp:ListItem Value="2015">2015</asp:ListItem>
                    <asp:ListItem Value="2016">2016</asp:ListItem>
                    <asp:ListItem Value="2017">2017</asp:ListItem>
                    <asp:ListItem Value="2018">2018</asp:ListItem>
                    <asp:ListItem Value="2019">2019</asp:ListItem>
                    <asp:ListItem Value="2020">2020</asp:ListItem>

                    <asp:ListItem Value="2021">2021</asp:ListItem>
                    <asp:ListItem Value="2022">2022</asp:ListItem>
                    <asp:ListItem Value="2023">2023</asp:ListItem>
                    <asp:ListItem Value="2024">2024</asp:ListItem>
                    <asp:ListItem Value="2025">2025</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlContract_Month" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                合同序号:
                  <asp:TextBox ID="txtContract_ProNo" runat="server" Width="200px"></asp:TextBox>
                过单:
            <asp:DropDownList ID="ddlContract_IsRequire" runat="server">
                <asp:ListItem Value="-1">全部</asp:ListItem>
                <asp:ListItem Value="0">否</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加合同信息" BackColor="Yellow" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="id" Width="130%" AutoGenerateColumns="False" AllowPaging="true"
            OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
            OnRowEditing="gvList_RowEditing" OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound">
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>编辑
                        </td>
                        <td>删除
                        </td>
                        <td height="25" align="center">合同类别
                        </td>
                        <td height="25" align="center">合同类型
                        </td>
                        <td height="25" align="center">合同编号
                        </td>
                        <td height="25" align="center">合约单位
                        </td>
                        <td height="25" align="center">合同名称
                        </td>
                        <td height="25" align="center">合同摘要
                        </td>
                        <td height="25" align="center">总金额
                        </td>
                        <td height="25" align="center">签订日期
                        </td>
                        <td height="25" align="center">总页数
                        </td>

                        <td height="25" align="center">总份数/己份数
                        </td>
                        <td height="25" align="center">项目编号
                        </td>
                        <td height="25" align="center">AE
                        </td>
                        <td height="25" align="center">客户名称
                        </td>
                        <td height="25" align="center">经手人
                        </td>
                        <td height="25" align="center">签收
                        </td>
                        <td height="25" align="center">存放位置
                        </td>
                        <td height="25" align="center">备注
                        </td>

                        <tr>
                            <td colspan="19" align="center" style="height: 80%">---暂无数据---
                            </td>
                        </tr>
                </table>
            </EmptyDataTemplate>
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <Columns>
                <asp:TemplateField HeaderText=" 编辑">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                            AlternateText="编辑" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                            CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Contract_IsRequireString" HeaderText="过单" SortExpression="Contract_IsRequireString" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_ProNo" HeaderText="合同序号" SortExpression="Contract_ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Type_String" HeaderText="合同类别" SortExpression="Contract_Type_String" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Use" HeaderText="合同类型" SortExpression="Contract_Use" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_No" HeaderText="合同编号" SortExpression="Contract_No" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Unit" HeaderText="合约单位" SortExpression="Contract_Unit" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Name" HeaderText="合同名称" SortExpression="Contract_Name" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Summary" HeaderText="合同摘要" SortExpression="Contract_Summary" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Total" HeaderText="总金额" SortExpression="Contract_Total" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Date" HeaderText="签订日期" SortExpression="Contract_Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Contract_PageCount" HeaderText="总页数" SortExpression="Contract_PageCount" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="Contract_AllCount_BCount" HeaderText="总份数/己份数" SortExpression="Contract_AllCount_BCount" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Brokerage" HeaderText="经手人" SortExpression="Contract_Brokerage" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_IsSign_String" HeaderText="签收" SortExpression="Contract_IsSign_String" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contract_Local_String" HeaderText="存放位置" SortExpression="Contract_Local_String" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="Contract_Remark" HeaderText="备注" SortExpression="Contract_Remark" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
    </asp:Panel>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>

    总金额合计:
    <asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
</asp:Content>
