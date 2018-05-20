<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpInvsList.aspx.cs" Inherits="VAN_OA.ReportForms.ExpInvsList"
    MasterPageFile="~/DefaultMaster.Master" Title="部门领料单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">

<script src="../Scripts/tinybox.js" type="text/javascript"></script>
<script src="../Scripts/tinyboxCu.js" type="text/javascript"></script>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                部门领料单
            </td>
        </tr>
        <tr>
            <td>
                货品名称
            </td>
            <td>
                <asp:DropDownList ID="ddlInvs" runat="server" Width="400px" DataTextField="InvName"
                    DataValueField="ID">
                </asp:DropDownList>
            </td>
            <td>
                序列号
            </td>
            <td>
                <asp:TextBox ID="txtInvNo" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br/>
   
<%--     <a href="javascript:tinybox_Showiframe('ExpInvsGoodListHisMan.aspx?GoodId=136',1000,600);">点我</a>--%>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="gvList_PageIndexChanging"
         OnRowDataBound="gvList_RowDataBound"
      >
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        货品名称
                    </td>
                    <td>
                        序列号
                    </td>
                    <td>
                        借出数量
                    </td>
                    <td>
                        领用时间
                    </td>
                    <td>
                        用途
                    </td>
                    <td>
                        使用状态
                    </td>
                    <td>
                        借出人
                    </td>
                    <td>
                        归还状态
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">
                        ---暂无数据---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="历史">
                <ItemTemplate>
                     
                        
                         <a href="javascript:tinybox_Showiframe('ExpInvsGoodListHisMan.aspx?GoodId=<%# Eval("InvId") %>',1000,600);" 
                          style='display:<%# GetDisPlay(Eval("InvName")) %>' >查看</a>
                        
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="InvName" HeaderText="货品名称" SortExpression="InvId" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="InvNo" HeaderText="序列号" SortExpression="InvNo" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ExpNum" HeaderText="借出数量" SortExpression="ExpNum" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpTime" HeaderText="领用时间" SortExpression="ExpTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ExpUse" HeaderText="用途" SortExpression="ExpUse" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpState" HeaderText="使用状态" SortExpression="ExpState"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="LoginName" HeaderText="借出人" SortExpression="LoginName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ExpInvState" HeaderText="归还状态" SortExpression="ExpInvState"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
