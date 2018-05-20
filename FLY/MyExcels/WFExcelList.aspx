<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WFExcelList.aspx.cs" Inherits="VAN_OA.MyExcels.WFExcelList"
    MasterPageFile="~/DefaultMaster.Master" Title="Excel清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
<script src="../Scripts/tinybox.js" type="text/javascript"></script>

    <script src="../Scripts/tinyboxCu.js" type="text/javascript"></script>

    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                Excel
            </td>
        </tr>
        <tr>
            <td>
                Excel:
            </td>
            <td>
                <asp:DropDownList ID="ddlExcel" runat="server" DataValueField="Table_Name" DataTextField="Name"
                    Style="left: 0px;">
                </asp:DropDownList>
            </td>
            <td>
                搜索内容:
            </td>
            <td>
                <asp:TextBox ID="txtContent" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <tr>
                        <td colspan="4" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
          <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <a href="javascript:tinybox_Showiframe('Result_WFExcelList.aspx?Id=<%# Eval("Id") %>&Excel=<%# Eval("Excel") %>',1100,550);">
                           查看</a>                        
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="A" HeaderText="A" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="B" HeaderText="B" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="C" HeaderText="C" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="D" HeaderText="D" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="E" HeaderText="E" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="F" HeaderText="G" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="G" HeaderText="G" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="H" HeaderText="H" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="I" HeaderText="I" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="J" HeaderText="J" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="K" HeaderText="K" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="L" HeaderText="L" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="M" HeaderText="M" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="N" HeaderText="N" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="O" HeaderText="O" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="P" HeaderText="P" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Q" HeaderText="Q" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="R" HeaderText="R" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="S" HeaderText="S" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="T" HeaderText="T" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="U" HeaderText="U" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="V" HeaderText="V" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="W" HeaderText="W" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="X" HeaderText="X" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Y" HeaderText="Y" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Z" HeaderText="Z" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="50" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
