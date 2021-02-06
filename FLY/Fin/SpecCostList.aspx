<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SpecCostList.aspx.cs" Inherits="VAN_OA.Fin.SpecCostList"
    MasterPageFile="~/DefaultMaster.Master" Title="个性费用输入界面" %>

<%@ Import Namespace="VAN_OA.Model.BaseInfo" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Linq" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                个性费用输入界面
            </td>
        </tr>
        <tr>
            <td>
                财年年月：
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
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
            <td>
                公司名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="Id"
                    Width="200PX" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                </asp:DropDownList>
                AE：
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                </asp:DropDownList>
                考核前期日期:
                <asp:TextBox ID="txtFrom" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />

                 <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div >
                        <div style="display:inline-block;">
                项目财务成本账期:<asp:TextBox ID="txtZhangQi" runat="server" Width="50"></asp:TextBox>天
                财务成本测算点:<asp:TextBox ID="txtCeSuanDian" runat="server" Width="50"></asp:TextBox>
                财务成本月利率:<asp:TextBox ID="txtMonthLiLv" runat="server" Width="50"></asp:TextBox>
                项目类别：    <asp:CheckBox ID="cbAll" runat="server" Text="全部" AutoPostBack="true" OnCheckedChanged="cbAll_CheckedChanged" />
                            </div>

                     <div style="display:inline-block;">
                      <asp:CheckBoxList ID="cbListPoType" CssClass="ckblstEffect" runat="server" DataTextField="BasePoType"
                                    DataValueField="Id" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
             </div>
                          <div style="display:inline-block;">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " Enabled="false" BackColor="Yellow"
                    OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="保存" BackColor="Yellow" Enabled="false"
                    OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="Button1" runat="server" Text="返回" BackColor="Yellow" OnClick="Button1_Click" />
                               </div>
                </div>

            </td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr style="height: 20px; background-color: #336699; color: White;">
            <td style="width: 200px" colspan="3">
                公司名称：
                <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                简称：
                <asp:Label ID="lblSimpName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                AE：
                <asp:Label ID="lblAE" runat="server" Text=""></asp:Label>
            </td>
            <td>
                日期：
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr style="height: 20px; background-color: #336699; color: White;">
            <td style="width: 20%;">
                <font style="float: right;">类型</font>
            </td>
            <td style="width: 20%;">
                当月值
            </td>
            <td style="width: 20%;">
                系统值(<asp:Label ID="lblYear" runat="server" Text=""></asp:Label>至<asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>)
            </td>
            <td style="width: 20%;">
                修正数值
            </td>
            <td colspan="2" style="width: 20%;">
                已有数值
            </td>
        </tr>
        <%
            foreach (FIN_Property m in propertyList)
            {
                string id = "pro_" + m.Id;
                string xitong = "";
                string month = "";
        %>
        <tr>
            <td>
                <font style="float: right;">
                    <%= m.CostType %></font>
            </td>
            <%if (m.CostType == "加班费")
              {
                  xitong = ViewState["OverTime"].ToString();
                  month = ViewState["OverTimeMonth"].ToString();
              }   
              if (m.CostType == "财务成本")
              {
                  xitong = ViewState["yearGoodTotal"].ToString();
                  month = ViewState["monthGoodTotal"].ToString();
              }   
            %>
            <td>
                <%= month %>
            </td>
            <td>
                <%= xitong %>
            </td>
            <td>
                <%if (btnSave.Visible)
                  { 
                %>
                <input type="text" name="<%=id %>" value="<%= m.Value %>" />
                <%
                  }
                  else
                  { 
                %>
                <%= m.Value %>
                <%
                  }
                    
                %>
            </td>
            <td colspan="2">
                <%= m.Value %>
            </td>
        </tr>
        <%
            }
            
        %>
        <tr style="height: 20px; background-color: Yellow;">
            <td style="width: 200px;">
                <font style="float: right;">合计</font>
            </td>
            <td>
                <%=ViewState["OverTimeMonth"]%>
            </td>
            <td>
                <%=ViewState["OverTime"]%>
            </td>
            <td>
                <%=propertyList.Sum(t => t.Value)%>
            </td>
            <td colspan="2">
                <%=propertyList.Sum(t => t.Value)%>
            </td>
        </tr>
    </table>
</asp:Content>
