<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFToInvoiceList.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFToInvoiceList" MasterPageFile="~/DefaultMaster.Master"
    Title="�������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">�������
            </td>
        </tr>
        <tr>
            <td>����ʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>���ݺ�:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>��Ŀ����:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONO" runat="server" Width="200px"></asp:TextBox>
                ��Ŀ����: 
                <asp:TextBox ID="txtPOName" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td>�ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>����״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="200px">
                    <asp:ListItem>ͨ��</asp:ListItem>
                    <asp:ListItem>ִ����</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                    <asp:ListItem>ȫ��</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>��Ʊ��:
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>����ʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtPoDateFrom" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPoDateTo" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateTo">
                </cc1:CalendarExtender>
            </td>
            <td>��Ʊ״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlFPState" runat="server" Width="200px">
                    <asp:ListItem Value="0">����</asp:ListItem>
                    <asp:ListItem Value="1">�ѿ�ȫƱ</asp:ListItem>
                    <asp:ListItem Value="2">δ��ȫƱ</asp:ListItem>
                    <asp:ListItem Value="3">δ��Ʊ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>AE��
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;��Ŀ���
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="cbPOHeBing" runat="server" Text="������ϲ�" AutoPostBack="true" OnCheckedChanged="cbPOHeBing_CheckedChanged" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel1" runat="server" Enabled="true" Style="display: inline">
                    ������
                    <asp:DropDownList ID="ddlJinECha" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    </asp:DropDownList>
                    ��Ŀ��� &nbsp;&nbsp;&nbsp;&nbsp; �������:
                    <asp:DropDownList ID="ddlDiffDays" runat="server">

                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30��" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30��AND<=60��" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60��AND <=90��" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90��AND <=120��" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">90��" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">120��" Value="6"></asp:ListItem>
                        <asp:ListItem Text=">180��" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                &nbsp;&nbsp;&nbsp;&nbsp; ������:
                <asp:DropDownList ID="ddlDaoKuanTotal" runat="server">
                    <asp:ListItem Text=">" Value=">">
                    </asp:ListItem>
                    <asp:ListItem Text="<" Value="<">
                    </asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanTotal" runat="server"></asp:TextBox>
                �������ͣ�
                <asp:DropDownList ID="ddlBusType" runat="server" Enabled="false">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="0" Text="��Ʊ"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Ԥ����"></asp:ListItem>
                </asp:DropDownList>
                ���Ʊ����:
                    <asp:DropDownList ID="ddlFPDays" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30��" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30��AND<=60��" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60��AND <=90��" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90��AND <=120��" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">120��AND <=180��" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">180��" Value="6"></asp:ListItem>
                    </asp:DropDownList>

                δ��Ʊ����:
                    <asp:DropDownList ID="ddlWeiFPDays" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30��" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30��AND<=60��" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60��AND <=90��" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90��AND <=120��" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">120��AND <=180��" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">180��" Value="6"></asp:ListItem>
                    </asp:DropDownList>
                <br />
                ��Ŀ�ر� :
                <asp:DropDownList ID="ddlPoClose" runat="server">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="�ر�"></asp:ListItem>
                    <asp:ListItem Value="0" Text="δ�ر�"></asp:ListItem>
                </asp:DropDownList>
                ��Ŀѡ�У�
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="70px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">δѡ��</asp:ListItem>
                    <asp:ListItem Value="1">ѡ��</asp:ListItem>
                </asp:DropDownList>
                ����ѡ�У�
                   <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                       <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                       <asp:ListItem Value="1" Text="ѡ��"></asp:ListItem>
                       <asp:ListItem Value="0" Text="δѡ��"></asp:ListItem>
                   </asp:DropDownList>

                ��Ŀ���
                  <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                  </asp:DropDownList>
                �ͻ�����:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType">
                </asp:DropDownList>
                �ͻ�����:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;

                <br />
                <div style="display: inline">
                    <asp:DropDownList ID="ddlNoSpecial" runat="server">

                        <asp:ListItem Value="0">��������</asp:ListItem>
                        <asp:ListItem Value="1">����</asp:ListItem>
                        <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    </asp:DropDownList>

                    <asp:CheckBox ID="cbHadJiaoFu" runat="server" Text="�ѽ���" />
                    <asp:CheckBox ID="cbIsPoFax" runat="server" Text="����˰" AutoPostBack="True" OnCheckedChanged="cbIsPoFax_CheckedChanged" />
                    <asp:CheckBox ID="cbClose" runat="server" Text="δ�ر�" ForeColor="Red" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbHanShui" runat="server" Text="��˰" AutoPostBack="True" OnCheckedChanged="cbHanShui_CheckedChanged" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbPoNoZero" runat="server" Text="��Ŀ��Ϊ0" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    ��˾���ƣ�
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                    DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:Label ID="Label2" runat="server" Text="��Ŀ�ܽ�"></asp:Label>
    <asp:Label ID="lblPOTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="�����ܽ��:"></asp:Label>
    <asp:Label ID="lblMess" runat="server" Text="0" Style="color: Red;"></asp:Label>
    <asp:Label ID="Label3" runat="server" Text="δ�����ܽ�"></asp:Label>
    <asp:Label ID="lblWeiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>

    δ�������:
     <asp:Label ID="lblWeiTotalBiLi" runat="server" Text="0" ForeColor="Red"></asp:Label>%

    <asp:Label ID="Label4" runat="server" Text="��Ʊ�ܽ�"></asp:Label>
    <asp:Label ID="lblKaiPiaoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <br />
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="160%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
            OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
            <PagerTemplate>
                <br />
                <%--<asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="First"></asp:LinkButton>
                <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                <br />--%>
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>����
                        </td>
                        <td>���ݺ�
                        </td>
                        <td>������
                        </td>
                        <td>��������
                        </td>
                        <td>��������
                        </td>
                        <td>���
                        </td>
                        <td>��������ϵ��
                        </td>
                        <td>��Ŀ����
                        </td>
                        <td>��Ŀ����
                        </td>
                        <td>�ͻ�����
                        </td>
                        <td>��ע
                        </td>
                        <td>״̬
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---��������---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnReEdit" runat="server" CommandName="ReEdit" CommandArgument='<% #Eval("Id") %>'
                            OnClientClick='return confirm( "ȷ��Ҫ�����ύ�˵�����") '>�༭</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��Ŀ���">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" Text='<% #Eval("PONo") %>'
                            CommandArgument='<% #Eval("PONo_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PoName" HeaderText="��Ŀ����" SortExpression="PoName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MinPoDate" HeaderText="��Ŀ����" SortExpression="MinPoDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="˰">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="MinOutTime" HeaderText="��������" SortExpression="MinOutTime"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="BusTypeStr" HeaderText="����" SortExpression="BusTypeStr"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FPDate" HeaderText="��Ʊ����" SortExpression="FPDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                  <asp:TemplateField HeaderText="δ��Ʊ����">
                    <ItemTemplate>
                        <asp:Label ID="lblWeiFPDays" runat="server" Text='<% #Eval("WeiFPDays") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��Ʊ����">
                    <ItemTemplate>
                        <asp:Label ID="lblFPDays" runat="server" Text='<% #Eval("FPDays") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DaoKuanDate1" HeaderText="��������" SortExpression="DaoKuanDate1"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="Total" HeaderText="������" SortExpression="Total" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:n2}"/>
                   <asp:BoundField DataField="DaoTotal" HeaderText="�������" SortExpression="DaoTotal" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="WeiDaoTotal" HeaderText="δ������" SortExpression="WeiDaoTotal" ItemStyle-HorizontalAlign="Center" />

                <%--      <asp:BoundField DataField="Days" HeaderText="����" SortExpression="Days" ItemStyle-HorizontalAlign="Center" />--%>

                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:Label ID="lblDays" runat="server" Text='<% #Eval("Days") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="FPNo" HeaderText="��Ʊ��" SortExpression="FPNo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="40%" ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle ForeColor="Black" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%" CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle" CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
            PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
            NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    ��Ŀ���ϼ�:
    <asp:Label ID="lblAllPoTotal" runat="server" Text="0"></asp:Label>
    �ܵ�����:
    <asp:Label ID="lblALLInvoiceTotal" runat="server" Text="0"></asp:Label>
    ��Ӧ��:
    <asp:Label ID="lblAllWeiTotal" runat="server" Text="0"></asp:Label>
    ��˰��Ŀδ��Ʊ���:
    <asp:Label ID="lblhsWKpTotal" runat="server" Text="0"></asp:Label>
    <br />
    <asp:Label ID="lblFPDetail" runat="server" Text=""></asp:Label>
    <%
        DataTable dt = ViewState["fpList"] as DataTable;

        if (dt != null)
        {

            int rowCount = 0;
            if (dt.Rows.Count % 5 == 0)
            {
                rowCount = dt.Rows.Count / 5;
            }
            else
            {
                rowCount = dt.Rows.Count / 5 + 1;
            }

            int rows = dt.Rows.Count;
            int index = 0;
    %>
    <table border="1 solid ">

        <%
            for (int i = 0; i < rowCount; i++)
            {

        %>
        <tr>

            <%
                for (int j = 1; j <= 5; j++)
                {
                    //int index=(i+1) * j-1;
                    if (index < rows)
                    {
                        DataRow dr = dt.Rows[index];
                        index++;
            %>
            <td><%= string.Format("{0}��{1}-{2}",dr[0],dr[1],dr[2])%></td>

            <%
                }
                else
                {  %>


            <%

                    }
                }


            %>
        </tr>

        <%
            }

        %>
    </table>

    <%
        }

    %>
    <br />
    <asp:GridView ID="gvDetail" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="PoNo" HeaderText="��Ŀ���" SortExpression="PoNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateUser" HeaderText="������" SortExpression="CreateUser"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AppleDate" HeaderText="��������" SortExpression="AppleDate"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ZhangQi" HeaderText="����ϵ��" SortExpression="ZhangQi" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remark" HeaderText="��ע" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
