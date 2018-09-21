<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSell_OrderFP.aspx.cs"
    Inherits="VAN_OA.JXC.WFSell_OrderFP" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="����������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

    <script type="text/javascript"> 
    
 function CheckId() {
    
     var sl = document.getElementById('<%= txtSupplier.ClientID %>').value;
   
     if (sl != "") {
         sl = sl.replace(/\\/g, ",");
         var arr = sl.split(',');

         if (arr.length == 4) { 
             document.getElementById('<%= txtSupplier.ClientID %>').value = arr[1];           
         }
     }
 }
 
    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ���۷�Ʊ<asp:Label ID="lblDelete" runat="server" Text="ɾ��" ForeColor="Red" Visible="false"></asp:Label>-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>

                 <asp:Label ID="lbl12" runat="server" Text="Ԥ�����ת" ForeColor="Red"></asp:Label>
                  <asp:TextBox  ID="txtZhuanJie" runat="server" Enabled="false" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �Ƶ��ˣ�
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font><asp:Label ID="lblHiddGuid" runat="server" Text="" Visible="false"></asp:Label><asp:Label ID="lblHiddFpGuid" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            <td>
                ����:
            </td>
            <td>
                <asp:TextBox ID="txtRuTime" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRuTime"
                    Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1">
                </cc1:CalendarExtender>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ���룺
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <font style="color: Red">*</font>
                <asp:LinkButton ID="lbtnAddFiles" runat="server" OnClientClick="javascript:window.showModalDialog('DioSellOutOrderToFPList.aspx',null,'dialogWidth:900px;dialogHeight:520px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
      ѡ��</asp:LinkButton>
            </td>
            <td>
                ������:
            </td>
            <td>
                <asp:TextBox ID="txtDoPer" runat="server" Width="200px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetUserName" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtDoPer">
                </cc1:AutoCompleteExtender>
                <font style="color: Red">*</font><asp:HiddenField ID="hfZhengFu" Value="1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ����:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>
            <td>
                ��Ʊ���ͣ�
            </td>
            <td>
                <asp:DropDownList ID="dllFPstye" runat="server" Width="200px" DataValueField="FpType"
                    DataTextField="FpType">
                    <%-- <asp:ListItem>��ֵ˰��Ʊ</asp:ListItem>
                  <asp:ListItem>��ͨ��Ʊ</asp:ListItem>
                   <asp:ListItem>����</asp:ListItem>--%>
                </asp:DropDownList>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                �ͻ����ƣ�
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                ��Ʊ��:
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtFPNo_TextChanged"></asp:TextBox>
                <font style="color: Red">*</font>
                <asp:Label ID="lblTopFPNo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblTopTotal" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text="��Ʊ���:" style="margin-left:10px;" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblInvoiceTotal" runat="server" Text=""  ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                ��ע��
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="95%" TextMode="MultiLine" Height="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="left" style="width: 100%;">

                    <asp:Label ID="lblTopMess" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnReCala" runat="server" Text="���¼���" BackColor="Yellow" Width="100px"
                    OnClick="btnReCala_Click" />
                <%
                    System.Data.DataTable dt = null;
                    if (ViewState["Diff"] != null && ViewState["oriModel"] != null)
                    {
                        dt = ViewState["Diff"] as System.Data.DataTable;
                        if (dt.Rows.Count == 1)
                        {
                            VAN_OA.Model.JXC.Sell_OrderFP model = ViewState["oriModel"] as VAN_OA.Model.JXC.Sell_OrderFP;
                %>
                <table cellpadding="0" cellspacing="0" width="50%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                        </td>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            ԭ��Ʊ
                        </td>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            �·�Ʊ
                        </td>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            ������
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            ����
                        </td>
                        <td>
                            <%=dt.Rows[0]["FPNo"]%>
                        </td>
                      
                        <td>  <%=model.FPNo%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            ���
                        </td>
                        <td>
                            <%=dt.Rows[0]["Total"]%>
                        </td>
                        <td>
                            <%=model.Total%>
                        </td>
                        <td>
                            <%= (model.Total - Convert.ToDecimal(dt.Rows[0]["Total"]))%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            ����
                        </td>
                        <td>
                            <%=dt.Rows[0]["FPNoStyle"]%>
                        </td>
                        <td>
                            <%=model.FPNoStyle%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            ��Ŀ���
                        </td>
                        <td>
                            <%=dt.Rows[0]["PONo"]%>
                        </td>
                        <td>
                            <%=model.PONo%>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <%
                    }
                    }
                       
                %>
                <br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    �༭
                                </td>
                                <td>
                                    ɾ��
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    С��
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    �ͺ�
                                </td>
                                <td>
                                    ��λ
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    �ɱ�����
                                </td>
                                <td>
                                    �ɱ����
                                </td>
                                <td>
                                    ���۵���
                                </td>
                                <td>
                                    ���۽��
                                </td>
                                <td>
                                    ��ע
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="ɾ��">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"
                                    CommandName="Delete" OnClientClick='return confirm( "ȷ��ɾ����") ' />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SellOutPONO" HeaderText="���ⵥ��" SortExpression="SellOutPONO"
                            Visible="false" />
                        <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" />
                        <%--   <asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNum" runat="server" Text='<%# Eval("GoodNum") %>' Width="50px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ɱ�����">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ɱ��ܼ�">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���۵���">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���۵���">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'
                                    Width="80px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="txtCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�����ܼ�">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��ע">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��ע">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>' Style="width: 100%;"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
                    <asp:ListItem Selected="True">ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="�ύ" OnClientClick="return confirm('ȷ��Ҫ�ύ��')" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
