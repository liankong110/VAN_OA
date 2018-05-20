<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFPOEnclosure.aspx.cs" Inherits="VAN_OA.JXC.WFPOEnclosure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">

        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            AllowPaging="true" Width="100%" AutoGenerateColumns="False" PageSize="20"
            OnPageIndexChanging="gvList_PageIndexChanging"
            OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand">
            <PagerTemplate>
                <br />
                <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
                <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="First"></asp:LinkButton>
                <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>下载
                        </td>
                        <td>单据号
                        </td>
                        <td>文件名称
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="down" CommandArgument='<% #Eval("File_Id")%>'>下载</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="fileName"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="fileName" HeaderText="文件名称" SortExpression="fileName"
                    ItemStyle-HorizontalAlign="Left" />

            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
        <input type="button" value="关闭" style="background-color: Yellow;" name="btnClose" onclick="javascript: parent.TINY.box.hide();" />
        <br />
        <asp:Panel ID="plAddFile" runat="server" Width="100%">
        <h3>附件修改/添加</h3>
        项目订单号:<asp:DropDownList ID="ddlProList" runat="server" DataTextField="ProNo" DataValueField="ProNo"></asp:DropDownList>
      
        附件：<asp:FileUpload ID="fuAttach" runat="server" Width="400px" />
        <br />
        <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
            Width="51px" OnClientClick="return confirm('确定要提交吗？')" />&nbsp; &nbsp; &nbsp;   
            </asp:Panel>
    </form>
</body>
</html>
