<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFXianJinliuList.aspx.cs"
    Inherits="VAN_OA.JXC.WFXianJinliuList" MasterPageFile="~/DefaultMaster.Master"
    Title="�ֽ�������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                �ֽ�������
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ����:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONO" runat="server" Width="200px"></asp:TextBox>
                ��Ŀ����:
                <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                �ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>
         AE��
        </td>
        <td> <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList></td>     
            <td>
                ��Ʊ��:
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ����ʱ��:
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
            <td>
                ��Ʊ״̬:
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
            <td>
                ��Ŀ��
            </td>
            <td colspan="3"> 
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">="  Value=">=" ></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;  <asp:CheckBox ID="cbPOHeBing" runat="server" Text="������ϲ�"  Enabled="false" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel1" runat="server" Enabled="true" Style="display: inline">
                    ��Ŀ���
                    <asp:DropDownList ID="ddlJinECha" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlJinECha_SelectedIndexChanged">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                    </asp:DropDownList>
                     ������&nbsp;&nbsp;&nbsp;&nbsp; δ����ʱ��:
                    <asp:DropDownList ID="ddlDiffDays" runat="server" Enabled="false">
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
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<=" ></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanTotal" runat="server"></asp:TextBox><br />
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
                <br />
                <div style="display: inline">
                    <asp:DropDownList ID="ddlNoSpecial" runat="server">
                        <asp:ListItem Value="0">��������</asp:ListItem>
                        <asp:ListItem Value="1">����</asp:ListItem>
                        <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlShui" runat="server">
                        <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                        <asp:ListItem Value="1">��˰</asp:ListItem>
                        <asp:ListItem Value="0">����˰</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="cbHadJiaoFu" runat="server" Text="�ѽ���" />
                    <asp:CheckBox ID="cbPoNoZero" runat="server" Text="��Ŀ��Ϊ0" />
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;��Ŀ���
                    <asp:DropDownList ID="dllCompareSell" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    ���۽��&nbsp;&nbsp; ��Ŀ���
                    <asp:DropDownList ID="dllCompareFP" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    ��Ʊ��� &nbsp;&nbsp; ��Ŀ���
                    <asp:DropDownList ID="dllCompareInvoice" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    ʵ���� 
                    <br />
                    ������: 
                    <asp:TextBox ID="txtDaoKLvFrom" runat="server" Width="50px" Text="0"></asp:TextBox><label style=" color:Red">%</label>~<asp:TextBox ID="txtDaoKLvTo" Text="100" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>
                    ӯ������:
                    <asp:TextBox ID="txtYLFrom" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>~<asp:TextBox ID="txtYLTo" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>
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
    ע���ʽ������=�����/��֧���ܶ�+������ɱ��ܶ�+�����ܶ���ʽ�ռ��=֧���ܶ�+������ɱ��ܶ�+�����ܶ�-����ӯ������=ʵ������/��֧���ܶ�+������ɱ��ܶ�+�����ܶ��ӯ��Ч��=ӯ�������������� ��������ɱ��ܶ��Ǹ���Ŀ�ɹ����Կ�����Ʒ���ѳ����
    <div>
    1.     �ʽ�Ͷ��=֧���ܶ�+������ɱ��ܶ�+�����ܶδ�����ܶ�=���δ��������������+�ɿ�δ�������������ۣ���������δ�������۳��ɹ�δ�����������δ�տ� �� ��Ŀ���-����� �����˱���=���ʶ�/��Ŀ�������Ŀ���=0������հף���֧������=֧���ܶ�/�ɹ������ܽ�� ������ɹ����=0������հף���
    </div>
        <div>
2.     ������=�����ܶ�/��Ŀ�������Ŀ���=0������հף�
</div>    <div>
3.     Ԥ���ɱ�������ѡ�Ԥ������ ����Ŀ�����е� �ɱ������ܶ����Ѻ������ܶ��;����� �ɹ���δ���Ľ�ֻ����ɲ��֣���δ�����ܶ��� �����������δ���� �� �ڲ�δ����������� �����ۼƣ��� ����ɱ�Ϊ������ܳɱ����ɹ��ܶ��ǲɹ����ܽ�
��Ԥ������=ԭԤ������-
�������˻������۶�-�����˻��ܳɱ�����������ɱ�=ԭ����ɱ�-�����˻�����ĳ�
��ɱ������ɹ��ܶ�=ԭ�ɹ���-�ɹ��˻������Ľ�
��֧�����Ǹ���Ŀͨ����Ӧ��Ԥ����֧�����ܽ�ת֧���������ظ����룩=ԭ֧��
��-�º�ɹ��˻����ɵļ���������ɸ���֧������
</div>
4.     ֧���ܶ��Ǹ���Ŀͨ����Ӧ��Ԥ����֧�����ܽ�ת֧���������ظ����룩�������ܶ���ָͨ�� �������ѣ�˽���ͺķѣ��ó������ͺķѣ���������Ԥ�ڱ�������Ԥ�ڱ��������ͷѣ����Ӱ൥ ���ܶ ������ɱ��ܶ�=����������������-�۳�������������Ʒ�ĳ���ɱ���
������������=MIN(����Ʒ��������, �ɹ����Կ�������), �۳�����= MIN(����Ʒ
�����˻�������,�ɹ����Կ�������)
   <div> 5.���е��б�����ʾ����Ϊ ���ʺ��ʵ�ֵ��λ���� ��<label style=" color:Red">%</label>����ע��ٷֺ��ú�ɫ��ʾ</div>
   <div>6.��֧���ܶ�=SUM(֧��������֧�����ۡ��ɹ�����/ʵ�ɵ���)- SUM���º�ɹ��˻����ɵļ���������ɸ���֧���������ɹ����/ʵ�ɵ��ۣ�</div>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
             Width="200%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
            OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            ��Ŀ���
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            AE
                        </td>
                        <td>
                            ��Ŀ���
                        </td>
                        <td>
                            ��˰
                        </td>
                        <td>
                            Ԥ���ɱ�
                        </td>
                        <td>
                            �����
                        </td>
                        <td>
                            Ԥ������
                        </td>
                        <td>
                            ��;���
                        </td>
                        <td>
                            δ�����ܶ�
                        </td>
                        <td>
                            ����ɱ�
                        </td>
                        <td>
                            �ɹ��ܶ�
                        </td>
                        <td>
                            ֧���ܶ�
                        </td>
                        <td>
                            �����ܶ�
                        </td>
                        <td>
                            ��Ʊ�ܶ�
                        </td>
                        <td>
                            ���˶�
                        </td>
                        <td>
                            ����
                        </td>
                        <td>
                            δ�տ�
                        </td>
                        <td>
                            ���˱���
                        </td>
                        <td>
                            ֧������
                        </td>
                        <td>
                            ������
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">
                            ---��������---
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
                 <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" ItemStyle-HorizontalAlign="Right" />
                  <asp:BoundField DataField="GoodSellPriceTotal" HeaderText="���۽��" SortExpression="GoodSellPriceTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="CostPrice" HeaderText="Ԥ���ɱ�" SortExpression="CostPrice"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="OtherCostm" HeaderText="�����" SortExpression="OtherCostm"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Profit" HeaderText="��Ԥ������" SortExpression="Profit" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="NotRuTotal" HeaderText="��;���" SortExpression="NotRuTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="NotRuSellTotal" HeaderText="δ�����ܶ�" SortExpression="NotRuSellTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SellOutTotal" HeaderText="������ɱ�" SortExpression="SellOutTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="LastCaiTotal" HeaderText="���ɹ��ܶ�" SortExpression="LastCaiTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SupplierTotal" HeaderText="��֧���ܶ�" SortExpression="SupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="LastSupplierTotal" HeaderText="��֧���ܶ�" SortExpression="LastSupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="ItemTotal" HeaderText="�����ܶ�" SortExpression="ItemTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FPTotal" HeaderText="��Ʊ�ܶ�" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="InvoiceTotal" HeaderText="���˶�" SortExpression="InvoiceTotal"
                    ItemStyle-HorizontalAlign="Right" />

                <asp:TemplateField HeaderText="ʵ������">
                    <HeaderTemplate>
                       <span>ʵ������</span>
                        <asp:Button ID="Button1" runat="server" Text="-" Width="20px" OnClick="btnAdd_Click"  BackColor="Yellow" />
                         
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLiRunTotal" runat="server" Text='<%# Eval("LiRunTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

               
                  <asp:BoundField DataField="NotKuCunTotal" HeaderText="������ɱ��ܶ�" SortExpression="NotKuCunTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                       <asp:BoundField DataField="ZJTouRu" HeaderText="�ʽ�Ͷ��" SortExpression="ZJTouRu"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>

                      <asp:BoundField DataField="ZJHLV" HeaderText="�ʽ������" SortExpression="ZJHLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopZJHLV" HeaderText="����ʽ������" SortExpression="TopZJHLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="ZJZY" HeaderText="�ʽ�ռ��" SortExpression="ZJZY"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopZJZY" HeaderText="����ʽ�ռ��" SortExpression="TopZJZY"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="YLNL" HeaderText="ӯ������" SortExpression="YLNL"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopYLNL" HeaderText="���ӯ������" SortExpression="TopYLNL"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="YLLV" HeaderText="ӯ��Ч��" SortExpression="YLLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopYLLV" HeaderText="���ӯ��Ч��" SortExpression="TopYLLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>

                <asp:BoundField DataField="NotShouTotal" HeaderText="δ�տ�" SortExpression="NotShouTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="InvoiceBiLiTotal" HeaderText="���˱���" SortExpression="InvoiceBiLiTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                <asp:BoundField DataField="SupplierBiliTotal" HeaderText="֧������" SortExpression="SupplierBiliTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
                <asp:BoundField DataField="FeiYongTotal" HeaderText="������" SortExpression="FeiYongTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                     <asp:BoundField DataField="FPNoTotal" HeaderText="��Ʊ��" SortExpression="FPNoTotal" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="30%" ItemStyle-CssClass="item" />
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
    ����;���:
    <asp:Label ID="lblNotRuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ��δ�����ܶ�:
    <asp:Label ID="lblNotRuSellTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    �ܳ���ɱ�:
    <asp:Label ID="lblSellOutTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     �ܲɹ���:
    <asp:Label ID="lblLastCaiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label><br />
     ��֧�����:
    <asp:Label ID="lblSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ��֧�ۺϼ�:
    <asp:Label ID="lblLastSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     �ܷ��ö�:
    <asp:Label ID="lblItemTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     �ܿ�Ʊ��:
    <asp:Label ID="lblFPTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     �ܵ�����:
    <asp:Label ID="lblInvoiceTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ������:
    <asp:Label ID="lblLiRunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ��δ��:
    <asp:Label ID="lblNotShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label><br />
    �ܿ�����ɱ��  <asp:Label ID="lblKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;  
    ���ʽ�Ͷ��  <asp:Label ID="lblZiJinTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    <br />
  
     �����ܱ���:
    <asp:Label ID="lblInvoiceBiLiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
     ֧���ܱ���:
    <asp:Label ID="lblSupplierBiliTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
     �ܷ�����:
    <asp:Label ID="lblFeiYongTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
    <br />


      �ʽ������:
    <asp:Label ID="lblZJHLV" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
      �ʽ�ռ��:
    <asp:Label ID="lblZJZY" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ӯ������:
    <asp:Label ID="lblYLNL" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
      ӯ��Ч��:
    <asp:Label ID="lblYLLV" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
    
</asp:Content>
