<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFYunYingReport.aspx.cs"
    Inherits="VAN_OA.JXC.WFYunYingReport" MasterPageFile="~/DefaultMaster.Master" Title="���ݷ���" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="System.Web.UI" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>


    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">���ݷ���
            </td>
        </tr>
        <tr>
            <td>��Ŀ����:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONO" runat="server" Width="200px"></asp:TextBox>

            </td>
            <td>��Ŀ����:
             
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>�ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>AE��
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList></td>

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
            <td>��
            </td>
            <td>
                <asp:DropDownList ID="ddlPrice" runat="server">

                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="4">��Ŀ����:
                 <asp:DropDownList ID="ddlSpecial" runat="server">
                     <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                     <asp:ListItem Value="0" Text="������"></asp:ListItem>
                     <asp:ListItem Value="1" Text="����"></asp:ListItem>
                 </asp:DropDownList>
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
                ��Ŀģ��:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                ֧���꾻֧�۽�λ��
                <asp:DropDownList ID="ddlJinWei" runat="server">                  
                    <asp:ListItem Value="1" Text="��λ��С��λ 4λ "></asp:ListItem>
                    <asp:ListItem Value="0" Text="����λ����ԭ��"></asp:ListItem>
                </asp:DropDownList> 
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



    ע��Ԥ��Ӧ��=��Ŀ���--���ʶ�
���ɹ��ܶ�=ԭ�ɹ���-�ɹ��˻������Ľ���֧�����Ǹ���Ŀͨ����Ӧ��Ԥ����֧�����ܽ�ת֧���������ظ����룩=ԭ֧����-�º�ɹ��˻����ɵļ���������ɸ���֧������
Ӧ����淴Ӧ��=�����Ŀ�ɹ����Կ��� �ܽ�����ͨ��KC ֧����ȥ�Ľ�;Ӧ���ܶ� =���ɹ��ܶ�-��֧���ܶ�-Ӧ�����;��Ӫ������=����ܽ��+��ĿӦ�պϼ�-��ĿӦ���ϼ�+Ԥ��δ����ϼ�+ KCԤ��δ����ϼ�-���δ֧���ϼƣ����=Ӧ���ܶ��Ӫָ���е�Ӧ���ܶ-��Ӧ�̿�֧��-��Ӧ�̿�Ԥ��-�ɹ������п�֧�����-ʣ��δ���֧�����-֧�����п�-Ԥ�����п�;
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvList_PageIndexChanging"
            OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>��Ŀ���
                        </td>
                        <td>��Ŀ����
                        </td>
                        <td>AE
                        </td>
                        <td>˰
                        </td>
                        <td>��Ŀ���
                        </td>
                        <td>���۽��
                        </td>
                        <td>��Ʊ���
                        </td>
                        <td>δ��Ʊ��
                        </td>
                        <td>�����
                        </td>
                        <td>��ĿӦ��
                        </td>
                        <td>���ɹ��ܶ�
                        </td>
                        <td>��֧���ܶ�
                        </td>
                        <td>Ӧ���ܶ�
                        </td>

                    </tr>
                    <tr>
                        <td colspan="17" align="center" style="height: 80%">---��������---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="PONO" HeaderText="��Ŀ���" SortExpression="PONO" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PoName" HeaderText="��Ŀ����" SortExpression="PoName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="˰">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsHanShui" runat="server" Checked='<% #Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n6}" />
                <asp:BoundField DataField="GoodSellPriceTotal" HeaderText="���۽��" SortExpression="GoodSellPriceTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FPTotal" HeaderText="��Ʊ�ܶ�" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n6}" />
                <asp:BoundField DataField="NoFpTotal" HeaderText="δ��Ʊ��" SortExpression="NoFpTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n6}" />
                <asp:BoundField DataField="InvoiceTotal" HeaderText="���˶�" SortExpression="InvoiceTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YingShouTotal" HeaderText="��ĿӦ��" SortExpression="YingShouTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YuQiYingShou" HeaderText="Ԥ��Ӧ��" SortExpression="YuQiYingShou" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="LastCaiTotal" HeaderText="���ɹ��ܶ�" SortExpression="LastCaiTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SupplierTotal" HeaderText="��֧���ܶ�" SortExpression="SupplierTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />

                <asp:TemplateField HeaderText="��֧���ܶ�">
                    <ItemTemplate>
                        <asp:Label ID="lblLastSupplierTotal" runat="server" Text='<%# Eval("LastSupplierTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:BoundField DataField="LastSupplierTotal" HeaderText="��֧���ܶ�" SortExpression="LastSupplierTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />--%>

                <asp:BoundField DataField="YingFuTotal" HeaderText="Ӧ���ܶ�" SortExpression="YingFuTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YingFuKuCun" HeaderText="Ӧ�����" SortExpression="YingFuKuCun" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="ZhiTotal" HeaderText="��Ӧ�̿�֧��" SortExpression="ZhiTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                  <asp:BoundField DataField="CheckIngTotal" HeaderText="�ɹ������п�֧�����" SortExpression="CheckIngTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="ToSupplierTotal" HeaderText="ʣ��δ���֧�����" SortExpression="ToSupplierTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YuTotal" HeaderText="��Ӧ�̿�Ԥ��" SortExpression="YuTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="ZhiTotalIng" HeaderText="֧�����п�" SortExpression="ZhiTotalIng" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YuTotalIng" HeaderText="Ԥ�����п�" SortExpression="YuTotalIng" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="DiffTotal" HeaderText="���" SortExpression="DiffTotal" DataFormatString="{0:n6}"
                    ItemStyle-HorizontalAlign="Right" />
            </Columns>
            <PagerStyle HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle ForeColor="Black" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
            PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
            NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    ��Ŀ���ϼ�:
    <asp:Label ID="lblAllPoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ���۽��ϼ�:
     <asp:Label ID="lblSellTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ��Ʊ���ϼ�:
    <asp:Label ID="lblFPTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      δ��Ʊ��ϼ�:
    <asp:Label ID="lblNoFpTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       �����ϼ�:
    <asp:Label ID="lblInvoiceTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       ��ĿӦ�պϼ�:
    <asp:Label ID="lblYingShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      Ԥ��Ӧ�պϼ�:
    <asp:Label ID="lblYuQiYingShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

    <br />

    �ɹ����ϼ�:
    <asp:Label ID="lblLastCaiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ֧�����ϼ�:
    <asp:Label ID="lblSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ��֧�ۺϼ�:
    <asp:Label ID="lblLastSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      Ӧ���ϼ�:
    <asp:Label ID="lblYingFuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     Ӧ�����ϼ�:
    <asp:Label ID="lblYingFuKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
         

      
    <br />


    <br />
    ����ܽ��:
    <asp:Label ID="lblKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

       KCԤ��δ����ϼ�:
    <asp:Label ID="lblKCXuJianTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ���δ֧���ϼ�:
    <asp:Label ID="lblKCWeiZhiFuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    

</asp:Content>
