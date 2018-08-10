<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFHouseGoods.aspx.cs" Inherits="VAN_OA.JXC.WFHouseGoods"
    MasterPageFile="~/DefaultMaster.Master" Title="��Ʒ����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ����ѯ
            </td>
        </tr>
        <tr>
            <td>
                ��Ʒ���������Ǵʣ���
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtZhuJi" runat="server" Width="400px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGoods" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtZhuJi">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                ����
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �ֿ⣺
            </td>
            <td>
                <asp:DropDownList ID="ddlHouse" DataTextField="houseName" DataValueField="id" runat="server"
                    Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                ���
            </td>
            <td>
                <asp:DropDownList ID="ddlGoodType" runat="server" DataTextField="GoodTypeName" DataValueField="GoodTypeName"
                    Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlGoodType_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGoodSmType" runat="server" DataTextField="GoodTypeSmName"
                    DataValueField="GoodTypeSmName" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlGoodSmType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                ����/С��/���:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="400PX"></asp:TextBox>
                ����
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="400PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ����/С��/���:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNameOrTypeOrSpec1" runat="server" Width="400PX"></asp:TextBox>
                ����
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo1" runat="server" Width="400PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                ��Ʒ����:
            
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtGoodAvgPrice" runat="server" Width="100px"></asp:TextBox>
                    ��λ:   <asp:DropDownList ID="ddlArea" runat="server">
                    <asp:ListItem Value="">ȫ��</asp:ListItem>
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
                <asp:CheckBox ID="cbEmpty" runat="server" Text="��λ�հ�" AutoPostBack="True" 
                    oncheckedchanged="cbEmpty_CheckedChanged" />
                    �������:
                <asp:DropDownList ID="ddlFuHao" runat="server">
                <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtNum" runat="server" Width="100px"></asp:TextBox>

                   �ɹ������
                <asp:DropDownList ID="ddlCaiKuNum" runat="server">
                   <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                  <asp:TextBox ID="txtCaiKuNum" runat="server" Width="100px"></asp:TextBox>
                ������棺
                <asp:DropDownList ID="ddlZhiLiuNum" runat="server">
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                   <asp:TextBox ID="txtZhiLiuNum" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
            </td>
        </tr>
    </table>
    ˵����A����¥�ֿ� B��һ¥���ֿ��һ¥��������   C��һ¥���ֿ� D: 3���¥�ֿ� E:��¥�������� F��һ¥���� G:��¥��������  H����¥������
    <br />
     ע������ɫ���������ʾ��������棬���Ƕ����ɹ����
    <br/>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"  DataKeyNames="GoodId"
         Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound" OnRowEditing="gvList_RowEditing"
        ShowFooter="true">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        �ֿ�
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
                        ����
                    </td>
                    <td>
                        ���
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
          <asp:BoundField DataField="No" HeaderText="��" SortExpression="No"  ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="HouseName" HeaderText="�ֿ�" SortExpression="HouseName" />
             <asp:BoundField DataField="GoodAreaNumber" HeaderText="��λ" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
          
             <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                        <asp:LinkButton CommandName="Edit" runat="server" ID="btnEdit" Text='<%# Eval("GoodNo") %>' ></asp:LinkButton>
                <a href="/JXC/Pro_JSXDetailInfoList.aspx?goodNo=<%#Eval("GoodNo") %>" target="_blank">
                           ����</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>' ></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" />
            <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec"  HeaderStyle-Width="200px"/>
            <asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />
            <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
              <asp:BoundField DataField="SumKuXuCai" HeaderText="�ɹ����" SortExpression="SumKuXuCai" />
              <asp:BoundField DataField="ZhiLiuKuCun" HeaderText="�������" SortExpression="ZhiLiuKuCun" />

            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# GetValue(Eval("GoodAvgPrice")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="�ܼ�">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text=' <%# GetValue(Eval("Total")) %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# GetValue(Eval("Total")) %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
              <asp:BoundField DataField="HadInvoice" HeaderText="��֧��" SortExpression="GoodOutNum" DataFormatString="{0:n2}"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NoInvoice" HeaderText="δ֧��" SortExpression="GoodResultNum" DataFormatString="{0:n2}"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
     ��֧���ϼƣ�<asp:Label ID="lblHadInvoice" runat="server" Text="0" ForeColor="Red" style="margin-right:20px"></asp:Label>
     δ֧���ϼƣ�<asp:Label ID="lblNoInvoice" runat="server" Text="0" ForeColor="Red" style="margin-right:20px"></asp:Label>
</asp:Content>
