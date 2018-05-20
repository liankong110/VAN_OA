<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFEFormAssess.aspx.cs"
    Inherits="VAN_OA.JXC.WFEFormAssess" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="销售月考核" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                人事考核
            </td>
        </tr>
        <tr>
            <td>
                STAFF：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td style="height: 20px; background-color: #336699; color: White;">
                派工单列表
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">
                外出业务联系单列表
            </td>
        </tr>
        <% if (ds != null)
           {%>
        <%

            //--出库单签回单-------需要显示 每个AE的未签回单  项目编号
            var dataTable1 = ds.Tables[0];
            var dataTable2 = ds.Tables[1];
           
        %>
        <tr>
            <td style="vertical-align: top;">
                <%
                    foreach (DataRow dr in dataTable1.Rows)
                    {
                %>
                <a href="/EFrom/Dispatching.aspx?ProId=1&allE_id=<%=dr[2]%>&EForm_Id=<%=dr[3]%>"
                    target="_blank">
                    <%=dr[0]%>—<%=dr[1]%>
                </a>
                <br />
                <%
                    }
                %>
                &nbsp;
            </td>
            <td style="vertical-align: top;">
                <%
                    foreach (DataRow dr in dataTable2.Rows)
                    {
                %>
                <a href="/EFrom/BusContact.aspx?ProId=2&allE_id=<%=dr[2]%>&EForm_Id=<%=dr[3]%>" target="_blank">
                    <%=dr[0]%>—<%=dr[1]%>
                </a>
                <br />
                <%
                    }
                %>
                &nbsp;
            </td>
        </tr>
        <%
            }%>
    </table>
</asp:Content>
