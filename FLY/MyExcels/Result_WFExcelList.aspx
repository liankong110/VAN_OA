<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result_WFExcelList.aspx.cs"
    Inherits="VAN_OA.MyExcels.Result_WFExcelList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <input type="button" value="关闭" style="background-color: Yellow;" name="btnClose"
        onclick="javascript: parent.TINY.box.hide();" />
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
    </form>
</body>
</html>
