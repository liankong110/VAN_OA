<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFGoods.aspx.cs" Inherits="VAN_OA.BaseInfo.WFGoods"
    MasterPageFile="~/DefaultMaster.Master" Title="��Ʒ����" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ��Ʒ����-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �Ƿ�Ϊ������Ʒ
            </td>
            <td>
                <asp:CheckBox ID="cbSpec" runat="server" />
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGoodNo" runat="server" Width="400px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGoodName" runat="server" Width="400px"></asp:TextBox>
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Ʒ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGoodBrand" runat="server" Width="400px"></asp:TextBox>
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���Ǵ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtZhuJi" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlGoodType" runat="server" DataTextField="GoodTypeName" DataValueField="GoodTypeName"
                    Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlGoodType_SelectedIndexChanged">
                </asp:DropDownList>
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                &nbsp;С�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlGoodSmType" runat="server" DataTextField="GoodTypeSmName"
                    DataValueField="GoodTypeSmName" Width="400px">
                </asp:DropDownList>
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSpec" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �ͺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtModel" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��λ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUnit" runat="server" Width="400px"></asp:TextBox>
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtProduct" runat="server" Width="400px"></asp:TextBox>
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��λ(������ܺ�-����-��λ)��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlArea" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
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
                <asp:DropDownList ID="ddlNumber" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlRow" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlCol" runat="server">
                </asp:DropDownList>
                <font style="color: #FF0000">*</font>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td>
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
            <td>
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" �ڶ��� ��� " BackColor="Yellow" OnClick="btnAdd_Click"
                    Visible="false" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="�ڶ��� �޸� " BackColor="Yellow" OnClick="btnUpdate_Click"
                    Visible="false" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click"
                    Visible="false" />&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnCheck" runat="server" Text=" ��һ�� ��� " BackColor="Yellow" OnClick="btnCheck_Click" />&nbsp;
                <asp:Button ID="btnSub" runat="server" Text="�ڶ��� �ύ" BackColor="Yellow" OnClick="Button1_Click"
                    Enabled="false" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnClose_Click" />
                &nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="GoodId" Width="100%" AutoGenerateColumns="False" AllowPaging="true"
        OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        ����
                    </td>
                    <td height="25" align="center">
                        ���
                    </td>
                    <td height="25" align="center">
                        ����
                    </td>
                    <td height="25" align="center">
                        С��
                    </td>
                    <td height="25" align="center">
                        ���
                    </td>
                    <td height="25" align="center">
                        �ͺ�
                    </td>
                    <tr>
                        <td colspan="9" align="center" style="height: 80%">
                            ---��������---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
            <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField DataField="GoodAreaNumber" HeaderText="��λ" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodName" HeaderText="����" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodModel" HeaderText="�ͺ�" SortExpression="GoodModel"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
