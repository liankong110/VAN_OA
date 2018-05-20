<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WfAttachmentList.aspx.cs" Inherits="VAN_OA.WfAttachmentList" MasterPageFile="~/DefaultMaster.Master" Title="文档管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="server">

    <table cellpadding="0" cellspacing="0" width="100%" style="border-left: 1px solid #999999;" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">查询</td>
        </tr>
        <tr>
            <td>文件名称</td>
            <td>
                <asp:TextBox ID="txtMainName" runat="server"></asp:TextBox>
            </td>
            <td>文件夹
            </td>
            <td>
                <asp:DropDownList ID="ddlFolders" runat="server" DataTextField="Folder_NAME" DataValueField="Folder_ID">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>

        <tr>

            <td>创建时间</td>
            <td colspan="3">

                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1"
                    runat="server" TargetControlID="txtFrom" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>
                --<asp:TextBox ID="txtTo"
                    runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2"
                    runat="server" TargetControlID="txtTo" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>
            </td>

        </tr>
        <tr>

            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                    <asp:Button ID="Button1" runat="server" Text=" 添加 "
                        BackColor="Yellow" OnClick="Button1_Click" />&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB"
        BorderStyle="Solid" DataKeyNames="Id"
        Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnRowEditing="gvList_RowEditing"
        OnPageIndexChanged="gvList_PageIndexChanged"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowCommand="gvList_RowCommand"
        OnRowDataBound="gvList_RowDataBound" OnRowDeleting="gvList_RowDeleting">
        <PagerTemplate>
            <br />
            <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
            <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
            <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
            <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
            <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
            <br />
        </PagerTemplate>

        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">



                    <td>文件名称
                    </td>

                    <td>文件夹
                    </td>
                    <td>文件列表
                    </td>

                    <td>版本号
                 
                
                    </td>

                    <td>更新用户
                 
                
                    </td>
                    <td>更新时间
                 
                
                    </td>
                    <td>操作
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---暂无数据---</td>
                </tr>
            </table>
        </EmptyDataTemplate>

        <Columns>






            <asp:BoundField DataField="MainName" HeaderText="文件名称">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>

            <asp:BoundField DataField="FolderName" HeaderText="文件夹">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>



            <asp:TemplateField HeaderText="文件列表">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("fileName") %>' CommandName="down" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:BoundField DataField="version" HeaderText="版本号">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>

            <asp:BoundField DataField="userName" HeaderText="更新用户">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>

            <asp:BoundField DataField="createTime" HeaderText="更新时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText=" 操作">
                <ItemTemplate>



                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑" />
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除" CommandName="Delete"
                        OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>





        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>

