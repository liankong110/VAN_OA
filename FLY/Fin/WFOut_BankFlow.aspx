<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFOut_BankFlow.aspx.cs" Inherits="VAN_OA.Fin.WFOut_BankFlow"
    MasterPageFile="~/DefaultMaster.Master" Title="������ϸ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:content id="Content1" runat="server" contentplaceholderid="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">������ϸ
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">������ˮ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblReferenceNumber" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                -�տ������ƣ�
                <asp:Label ID="lblInPayeeName" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                ���˽�  
                <asp:Label ID="lblTradeAmount" runat="server" Text="0" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                  ������ˮʣ�����ӽ�  
                <asp:Label ID="lblLastTotal" runat="server" Text="0"></asp:Label>

                <asp:Label ID="lblTime" Visible="false" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">�������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlOutType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOutType_SelectedIndexChanged">
                    <asp:ListItem value="֧����">1.֧����</asp:ListItem>
                    <asp:ListItem value="Ԥ���">2.Ԥ���</asp:ListItem>
                    <asp:ListItem value="������">3.������</asp:ListItem>
                    <asp:ListItem value="Ԥ�ڱ�����">4.Ԥ�ڱ�����</asp:ListItem>
                    <asp:ListItem value="��˰��˰">5.��˰��˰</asp:ListItem>
                    <asp:ListItem value="2%�ط���������+3%��������+7%����ά������">6.2%�ط���������+3%��������+7%����ά������</asp:ListItem>
                    <asp:ListItem value="ӡ��˰">7.ӡ��˰</asp:ListItem>
                    <asp:ListItem value="��ҵ����˰">8.��ҵ����˰</asp:ListItem>
                    <asp:ListItem value="�籣�ɷ�">9.�籣�ɷ�</asp:ListItem>
                    <asp:ListItem value="������ɷ�">10.������ɷ�</asp:ListItem>
                    <asp:ListItem value="��������˰">11.��������˰</asp:ListItem>
                    <asp:ListItem value="���д�����Ϣ">12.���д�����Ϣ</asp:ListItem>
                    <asp:ListItem value="����֧��">13.����֧��</asp:ListItem>
                    <asp:ListItem value="����֧��">14.����֧��</asp:ListItem>
                    <asp:ListItem value="���ŷѴ���">15.���ŷѴ���</asp:ListItem>
                    <asp:ListItem value="ˮ�Ѵ���">16.ˮ�Ѵ���</asp:ListItem>
                    <asp:ListItem value="��Ѵ���">17.��Ѵ���</asp:ListItem>
                    <asp:ListItem value="����">18.����</asp:ListItem>
                    <asp:ListItem Value="����������">19.����������</asp:ListItem>
                    <asp:ListItem Value="Ͷ�걣֤��">20.Ͷ�걣֤��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">���ݺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txtTotal" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                <asp:TextBox ID="txtName" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                <asp:Button ID="btnTotal" runat="server" Text="��ȡ���" BackColor="Yellow" OnClick="btnTotal_Click" />
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">���˽�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOutTotal" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">ע�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRemark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" OnClick="btnAdd_Click" OnClientClick="return save();" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" OnClick="btnUpdate_Click" OnClientClick="return save();" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">ע�����ݺŸ�ʽ ��˰��˰--Ͷ�걣֤�� �����58888888---20888888��д</td>
        </tr>
    </table>

    <cc1:TabContainer ID="TabContainer1" runat="server">
                    <cc1:TabPanel ID="TabPanel1" runat="server">
                        <HeaderTemplate>
                            ���ݺ��б�
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gv_Out" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        ShowFooter="true" AllowPaging="False"
        PageSize="20">
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>���</td>
                    <td>������ˮ��
                    </td>
                    <td>����ʱ��
                    </td>
                    <td>��������
                    </td>

                    <td>���˽��
                    </td>
                    <td>ע��
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center" style="height: 80%">---��������---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Number" HeaderText="���" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ReferenceNumber" HeaderText="������ˮ��" SortExpression="ReferenceNumber" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Time" HeaderText="����ʱ��" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutType" HeaderText="��������" SortExpression="OutType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutTotal" HeaderText="���˽��" SortExpression="OutTotal" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remark" HeaderText="ע��" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
                            </ContentTemplate>
                        </cc1:TabPanel>

        <cc1:TabPanel ID="TabPanel2" runat="server">
                        <HeaderTemplate>
                            ������ˮ���б�
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gvLiuShui" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        ShowFooter="true" AllowPaging="False"
        PageSize="20">
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>���</td>
                    <td>������ˮ��
                    </td>
                    <td>����ʱ��
                    </td>
                    <td>��������
                    </td>

                    <td>���˽��
                    </td>
                    <td>ע��
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center" style="height: 80%">---��������---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Number" HeaderText="���" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ReferenceNumber" HeaderText="������ˮ��" SortExpression="ReferenceNumber" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Time" HeaderText="����ʱ��" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutType" HeaderText="��������" SortExpression="OutType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutTotal" HeaderText="���˽��" SortExpression="OutTotal" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remark" HeaderText="ע��" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
                            </ContentTemplate>
                        </cc1:TabPanel>
 </cc1:TabContainer>
    

    <script type="text/javascript">
        function save() {

            //��������ѡ�� Ԥ�����֧���� ʱ,�����ȡ����� �󣬵��ݵ�̧ͷ�����ϸ���տ�������һ�����˵���̧ͷ���Ա��������ţ������ţ���˰�����Ա�(��1.33˰) ��
            //���� �������̳� ���Բ�һ���� ���޸Ļ���� ���ܱ������ݣ�������ʾԤ�����֧���� ̧ͷ����һ��

            var InType = document.getElementById('<%= ddlOutType.ClientID %>').value;
            if (InType == "������" || InType == "Ԥ�ڱ�����") {
                var outPayerName = document.getElementById('<%= lblInPayeeName.ClientID %>').innerText;
                var GuestName = document.getElementById('<%= txtName.ClientID %>').value;
                console.log(outPayerName + "---" + GuestName);
                if (outPayerName != GuestName) {
                    if (confirm("�տ��˺͵���̧ͷ�в�һ��,�Ƿ��������")) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
            return true;
        }
    </script>

</asp:content>
