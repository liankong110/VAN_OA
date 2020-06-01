<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KIS.aspx.cs"
	Inherits="VAN_OA.KingdeeInvoice.KIS" MasterPageFile="~/DefaultMaster.Master"
	Title="金蝶发票处理工具" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
	<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
		border="1">
		<tr>
			<td colspan="4" style="height: 20px; background-color: #336699; color: White;">金蝶发票处理工具
			</td>
		</tr>

		<tr>
			<td height="25" width="30%" align="right">账套/数据库名称：：
			</td>
			<td height="25" width="*" align="left" colspan="3">
				<asp:DropDownList ID="ddlAccount" runat="server"></asp:DropDownList>
				<br />
				<asp:Panel ID="plConnString" runat="server">
			
				IP 地址: 
				<asp:TextBox ID="txtIP" runat="server"></asp:TextBox>
				用户名: 
				<asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
				密码: 
				<asp:TextBox ID="txtPwd" runat="server"></asp:TextBox>
						</asp:Panel>
			</td>
		</tr>
		<tr>
			<td height="25" width="30%" colspan="4" style="height: 20px; background-color: #336699; color: White;">应收
			</td>
		</tr>
		<tr>
			<td height="25" width="30%" align="right">发票开始时间 ：
			</td>
			<td height="25" width="*" align="left">
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
			<td>运行时间点：</td>
			<td>
				<asp:TextBox ID="txtInvoiceDate" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td height="25" width="30%" colspan="4" style="height: 20px; background-color: #336699; color: White;">应付
			</td>
		</tr>
		<tr>
			<td height="25" width="30%" align="right">发票开始时间 ：
			</td>
			<td height="25" width="*" align="left">
				<asp:TextBox ID="txtpayableFrom" runat="server"></asp:TextBox>
				<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
				-<asp:TextBox ID="txtpayableTo" runat="server"></asp:TextBox>
				<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
				<cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
					Format="yyyy-MM-dd" TargetControlID="txtpayableFrom">
				</cc1:CalendarExtender>
				<cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
					Format="yyyy-MM-dd" TargetControlID="txtpayableTo">
				</cc1:CalendarExtender>
			</td>
			<td>运行时间点：</td>
			<td>
				<asp:TextBox ID="txtpayableDate" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="center" colspan="4">
				<asp:Button ID="btnAdd" runat="server" Text=" 保存信息 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 发票处理 " BackColor="Yellow" OnClick="btnUpdate_Click" OnClientClick="return confirm('确定要提交吗？时间可能会很久请耐心等待!');" />&nbsp;&nbsp;                    
                 <asp:Button ID="btnQuery" runat="server" Text=" 查询 " BackColor="Yellow" OnClientClick="return confirm('确定要提交吗？时间可能会很久请耐心等待!');" OnClick="btnQuery_Click" />&nbsp;&nbsp;     
			</td>
		</tr>
		<tr>
			<td colspan="4">
				<asp:Label ID="lblMessage" runat="server" Text="就绪"></asp:Label>
			</td>
		</tr>
	</table>


	<asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
		DataKeyNames="id" Width="100%" AutoGenerateColumns="False" AllowPaging="false" OnRowDataBound="gvList_RowDataBound">
		<EmptyDataTemplate>
			<table width="100%">
				<tr style="height: 20px; background-color: #336699; color: White;">
					<td height="25" align="center">客户名称
					</td>
					<td height="25" align="center">激励系数
					</td>
					<td height="25" align="center">金额
					</td>
					<td height="25" align="center">开具日期
					</td>
					<td height="25" align="center">到账否
					</td>
					<td height="25" align="center">金额
					</td>
					<td height="25" align="center">未到款金额 </td>
					<tr>
						<td colspan="4" align="center" style="height: 80%">---暂无数据---
						</td>
					</tr>
			</table>
		</EmptyDataTemplate>
		<PagerTemplate>
			<br />
		</PagerTemplate>
		<Columns>
			<asp:TemplateField HeaderText="客户名称">
				<ItemTemplate>
					<asp:Label ID="GuestName" runat="server" Text='<%# Eval("GuestName") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="InvoiceNumber" HeaderText="发票号码" SortExpression="InvoiceNumber" ItemStyle-HorizontalAlign="Center" />
			<asp:BoundField DataField="FPNoStyle" HeaderText="发票类型" SortExpression="FPNoStyle" ItemStyle-HorizontalAlign="Center" />
			<asp:TemplateField HeaderText="发票金额">
				<ItemTemplate>
					<asp:Label ID="Total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="CreateDate" HeaderText="开具日期" SortExpression="CreateDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
			<asp:BoundField DataField="BillDate" HeaderText="发票日期" SortExpression="BillDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
			<asp:BoundField DataField="IsAccountString" HeaderText="到账否" SortExpression="IsAccountString" ItemStyle-HorizontalAlign="Center" />
			<asp:TemplateField HeaderText="到款金额">
				<ItemTemplate>
					<asp:Label ID="Received" runat="server" Text='<%# Eval("Received") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="到款金额" Visible="false" ItemStyle-Width="100px">
				<ItemTemplate>
					<asp:TextBox ID="EditReceived" runat="server" Text='<% #Eval("Received") %>' Width="100%"></asp:TextBox>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="到款比例">
				<ItemTemplate>
					<asp:Label ID="DaoKuanBL" runat="server" Text='<%# GetValue(Eval("DaoKuanBL")) %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="未到款金额">
				<ItemTemplate>
					<asp:Label ID="NoReceived" runat="server" Text='<%# Eval("NoReceived") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="未到款比例">
				<ItemTemplate>
					<asp:Label ID="WeiDaoKuanBL" runat="server" Text='<%# GetValue(Eval("WeiDaoKuanBL")) %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

		</Columns>
		<PagerStyle HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
		<HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
			HorizontalAlign="Center" />
		<AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
		<RowStyle CssClass="InfoDetail1" />
	</asp:GridView>



	<asp:GridView ID="gvPayable" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
		DataKeyNames="id" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
		OnRowDataBound="gvList_RowDataBound">
		<EmptyDataTemplate>
			<table width="100%">
				<tr style="height: 20px; background-color: #336699; color: White;">

					<td height="25" align="center">供应商名称
					</td>
					<td height="25" align="center">激励系数
					</td>
					<td height="25" align="center">金额
					</td>
					<td height="25" align="center">开具日期
					</td>
					<td height="25" align="center">到账否
					</td>
					<td height="25" align="center">付款金额
					</td>
					<td height="25" align="center">未付款金额 </td>
					<tr>
						<td colspan="4" align="center" style="height: 80%">---暂无数据---
						</td>
					</tr>
			</table>
		</EmptyDataTemplate>
		<PagerTemplate>
			<br />

		</PagerTemplate>
		<Columns>
			<asp:TemplateField HeaderText="供应商名称">
				<ItemTemplate>
					<asp:Label ID="SupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:BoundField DataField="InvoiceNumber" HeaderText="发票号码" SortExpression="InvoiceNumber" ItemStyle-HorizontalAlign="Center" />
			<asp:BoundField DataField="FPNoStyle" HeaderText="发票类型" SortExpression="FPNoStyle" ItemStyle-HorizontalAlign="Center" />
			<asp:TemplateField HeaderText="总金额">
				<ItemTemplate>
					<asp:Label ID="Total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="CreateDate" HeaderText="开具日期" SortExpression="CreateDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
			<asp:BoundField DataField="BillDate" HeaderText="发票日期" SortExpression="BillDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
			<asp:BoundField DataField="IsAccountString" HeaderText="付款否" SortExpression="IsAccountString" ItemStyle-HorizontalAlign="Center" />
			<asp:TemplateField HeaderText="付款金额">
				<ItemTemplate>
					<asp:Label ID="Received" runat="server" Text='<%# Eval("Received") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="付款金额" Visible="false" ItemStyle-Width="100px">
				<ItemTemplate>
					<asp:TextBox ID="EditReceived" runat="server" Text='<% #Eval("Received") %>' Width="100%"></asp:TextBox>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="付款比例">
				<ItemTemplate>
					<asp:Label ID="DaoKuanBL" runat="server" Text='<%# GetValue(Eval("DaoKuanBL")) %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="未付款金额">
				<ItemTemplate>
					<asp:Label ID="NoReceived" runat="server" Text='<%# Eval("NoReceived") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="未付款比例">
				<ItemTemplate>
					<asp:Label ID="WeiDaoKuanBL" runat="server" Text='<%# GetValue(Eval("WeiDaoKuanBL")) %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

		</Columns>
		<PagerStyle HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
		<HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
			HorizontalAlign="Center" />
		<AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
		<RowStyle CssClass="InfoDetail1" />
	</asp:GridView>
</asp:Content>

