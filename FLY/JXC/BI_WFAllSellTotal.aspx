<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BI_WFAllSellTotal.aspx.cs"
	Inherits="VAN_OA.JXC.BI_WFAllSellTotal" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
	Title="商业BI图表" %>

<%@ Import Namespace="System.Linq" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
	<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
		border="1">
		<tr>
			<td colspan="4" style="height: 20px; background-color: #336699; color: White;">商业BI图表
			</td>
		</tr>
		<tr>
			<td colspan="2">项目年度:
                <asp:DropDownList ID="ddlYear" runat="server">
				</asp:DropDownList>

			</td>
			<td>公司名称：
			</td>
			<td>
				<asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComCode"
					Width="200PX" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
				</asp:DropDownList>
				AE：
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
					Width="200PX">
				</asp:DropDownList>
			</td>
		</tr>

		<tr>
			<td colspan="4">项目关闭：
                <asp:DropDownList ID="ddlIsClose" runat="server">
					<asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
					<asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
					<asp:ListItem Text="未关闭" Value="0" Selected="True"> </asp:ListItem>
				</asp:DropDownList>
				结算选中：
                <asp:DropDownList ID="ddlJieIsSelected" runat="server">
					<asp:ListItem Value="-1" Text="全部"></asp:ListItem>
					<asp:ListItem Value="1" Text="选中" Selected="True"></asp:ListItem>
					<asp:ListItem Value="0" Text="未选中"></asp:ListItem>
				</asp:DropDownList>
				客户属性：<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
					DataTextField="GuestProString" Width="50px">
				</asp:DropDownList>
				项目归类：
              <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
			  </asp:DropDownList>
				<div align="right">
					<asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
				</div>
			</td>
		</tr>
	</table>
	<asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Both">
		<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
			border="1">
			<tr>
				<td colspan="8" style="height: 20px; background-color: #336699; color: White;">
					<asp:Label ID="lblSimpName" runat="server" Text=""></asp:Label>
				</td>
				<td colspan="6" style="height: 20px; background-color: #336699; color: White;">企业总计
				</td>
				<td colspan="6" style="height: 20px; background-color: #336699; color: White;">政府总计
				</td>

			</tr>
			<tr>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;"></td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额占比
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售总额
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目计划PV
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目进度SPI
				</td>

				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润总额
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
				</td>
				<%-- 1 --%>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
				</td>

				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
				</td>
				<%-- 1 --%>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
				</td>
				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
				</td>

				<td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
				</td>

			</tr>
			<% 
				decimal poSumTotal = allList.Sum(t => t.SumPOTotal);
				decimal PoLiRunTotal = allList.Sum(t => t.PoLiRunTotal);
				decimal MaoLi_QZ = allList.Sum(t => t.MaoLi_QZ);
				decimal MaoLi_ZZ = allList.Sum(t => t.MaoLi_ZZ);
				foreach (var model in allList)
				{%>
			<tr>
				<td>
					<%=model.AE %>
				</td>
				<td style="background-color:lightgreen">
					<%=string.Format("{0:n2}",model.SumPOTotal) %>
				</td>
				<td style="background-color:lightblue">
					<%= poSumTotal==0?"0":string.Format("{0:n2}",model.SumPOTotal/poSumTotal) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.PoTotal) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.PV) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.SPI) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.PoLiRunTotal) %>
				</td>
				<td style="background-color:Khaki">
					<%= PoLiRunTotal==0?"0":string.Format("{0:n2}",model.PoLiRunTotal/PoLiRunTotal) %>
				</td>
				<%-- 1 --%>
				<td style="background-color:lightgreen">
					<%=string.Format("{0:n2}",model.SumPOTotal_QZ) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.SellTotal_QZ) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.MaoLi_QZ )%>
				</td>
				<td style="background-color:Khaki">
					<%= MaoLi_QZ==0?"0":string.Format("{0:n2}",model.MaoLi_QZ/MaoLi_QZ) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.TureliRun_QZ) %>
				</td>

				<td>
					<%=string.Format("{0:n2}",model.sellFPTotal_QZ )%>
				</td>
				<%-- 1 --%>
				<td style="background-color:lightgreen">
					<%=string.Format("{0:n2}",model.SumPOTotal_ZZ )%>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.SellTotal_ZZ) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.MaoLi_ZZ) %>
				</td>
				<td style="background-color:Khaki">
					<%= MaoLi_ZZ==0?"0":string.Format("{0:n2}",model.MaoLi_ZZ/MaoLi_ZZ) %>
				</td>
				<td>
					<%=string.Format("{0:n2}",model.TureliRun_ZZ) %>
				</td>

				<td>
					<%=string.Format("{0:n2}",model.sellFPTotal_ZZ )%>
				</td>

			</tr>
			<%} %>
			<tr>
				<td>合计
				</td>
				<td>
					<%= string.Format("{0:n2}",allList.Sum(t => t.SumPOTotal))%>
				</td>
				<td></td>
				<td>
					<%= string.Format("{0:n2}",allList.Sum(t => t.PoTotal))%>
				</td>
					<td></td>
					<td></td>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.PoLiRunTotal))%>
				</td>
				<td></td>
				<%-- 1 --%>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.SumPOTotal_QZ))%>
				</td>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.SellTotal_QZ))%>
				</td>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.MaoLi_QZ))%>
				</td>
				<td></td>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.TureliRun_QZ))%>
				</td>

				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.sellFPTotal_QZ))%>
				</td>
				<%-- 1 --%>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.SumPOTotal_ZZ))%>
				</td>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.SellTotal_ZZ))%>
				</td>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.MaoLi_ZZ))%>
				</td>
				<td></td>
				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.TureliRun_ZZ))%>
				</td>

				<td>
					<%=string.Format("{0:n2}",allList.Sum(t => t.sellFPTotal_ZZ))%>
				</td>

			</tr>
		</table>
	</asp:Panel>
</asp:Content>
