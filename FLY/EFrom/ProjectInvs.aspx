<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectInvs.aspx.cs" Inherits="VAN_OA.EFrom.ProjectInvs" MasterPageFile="~/DefaultMaster.Master" Title="工程材料审计清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">



    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">工程材料审计清单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>申请人：<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td>项目编码：</td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" ReadOnly="true"></asp:TextBox></td>


            <td>项目名称：<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtProName" runat="server"></asp:TextBox></td>
            <td>日期：
             <font style="color: Red">*</font>
            </td>
            <td>

                <asp:TextBox ID="txtCreateTime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd " TargetControlID="txtCreateTime" PopupButtonID="Image1">
                </cc1:CalendarExtender>
            </td>
        </tr>


        <tr>

            <td>客户代表：<font style="color: Red">*</font></td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id" Width="155PX">
                </asp:DropDownList>
            </td>
            <td>当前状态:
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlProState" runat="server" Width="155px">
                    <asp:ListItem Value="未完工" Selected="True">未完工</asp:ListItem>
                    <asp:ListItem Value="已完工">已完工</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblProInvState" runat="server" Text="" Font-Bold="true" Font-Size="14px"></asp:Label>


            </td>

            <td>
                <asp:Label ID="ss" runat="server" Text="总计：" Style="margin-left: 100px;"></asp:Label>
            </td>

            <td>
                <asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </td>
        </tr>

        <tr>

            <td colspan="8">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="250px" Width="100%">

                    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB"
                        BorderStyle="Solid" DataKeyNames="Id"
                        Width="98%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                        OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing">

                        <EmptyDataTemplate>
                            <table width="100%">
                                <tr style="height: 20px; background-color: #336699; color: White;">
                                    <td>编辑
                                    </td>
                                    <td>删除
                                    </td>

                                    <td>购买日期
                                    </td>
                                    <td>材料型号
                                    </td>

                                    <td>材料名称
                                    </td>

                                    <td>单位
                                    </td>
                                    <td>数量
                                    </td>

                                    <td>材料费
                                    </td>



                                    <td>运费
                                    </td>



                                    <td>会务费
                                    </td>



                                    <td>管理费
                                    </td>
                                    <td>小计
                                    </td>

                                    <td>备注
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="11" align="center" style="height: 80%">---暂无数据---</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>

                        <Columns>
                            <asp:TemplateField HeaderText=" 编辑">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" AlternateText="编辑" />
                                </ItemTemplate>
                                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除" CommandName="Delete"
                                        OnClientClick='return confirm( "确定删除吗？") ' />
                                </ItemTemplate>
                                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>


                            <asp:BoundField DataField="BuyTime" HeaderText="购买日期" SortExpression="BuyTime" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="InvModel" HeaderText="材料型号" SortExpression="InvModel" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvName" HeaderText="材料名称" SortExpression="InvName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvUnit" HeaderText="单位" SortExpression="InvUnit" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvNum" HeaderText="数量" SortExpression="InvNum" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvPrice" HeaderText="材料费" SortExpression="InvPrice" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvCarPrice" HeaderText="运费" SortExpression="InvCarPrice" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvTaskPrice" HeaderText="会务费" SortExpression="InvTaskPrice" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvManPrice" HeaderText="管理费" SortExpression="InvManPrice" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Total" HeaderText="小计" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />


                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                        <HeaderStyle BackColor="#336699" Height="24px" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                        <RowStyle CssClass="InfoDetail1" />
                    </asp:GridView>


                </asp:Panel>

                <br />


                <asp:Panel ID="plProInvs" runat="server">

                    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
                        <tr>
                            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">工程材料审计清单</td>
                        </tr>





                        <tr>
                            <td height="25" align="right">购买日期
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtBuyTime" runat="server" Width="200px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd " TargetControlID="txtBuyTime" PopupButtonID="ImageButton1"></cc1:CalendarExtender>
                            </td>
                            <td height="25" align="right">材料型号
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvModel" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td height="25" align="right">材料名称
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="right">单位
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvUnit" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td height="25" align="right">数量
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvNum" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td height="25" align="right">材料费
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvPrice" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="right">运费
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvCarPrice" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td height="25" align="right">会务费
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvTaskPrice" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td height="25" align="right">管理费
	：</td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtInvManPrice" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="right">备注
	：</td>
                            <td height="25" width="*" align="left" colspan="5">
                                <asp:TextBox ID="txtRemark" runat="server" Width="90%"></asp:TextBox>
                            </td>
                        </tr>


                        <tr>
                            <td colspan="6" align="center" style="height: 80%">

                                <asp:Button ID="btnAdd"
                                    runat="server" Text="添加" BackColor="Yellow"
                                    Width="74px" OnClick="btnAdd_Click" />&nbsp; &nbsp; &nbsp; &nbsp;
                      
                       <asp:Button ID="btnSave"
                           runat="server" Text="保存" BackColor="Yellow"
                           Width="74px" OnClick="btnSave_Click" ValidationGroup="a" />&nbsp; 
                      &nbsp;                    
                      
                      
                      <asp:Button ID="btnCancel"
                          runat="server" Text="取消" BackColor="Yellow" OnClick="btnCancel_Click" Width="74px" />&nbsp; 
                      &nbsp;
                      
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                </asp:Panel>
            </td>
        </tr>


        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="8">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="8">


                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserName">
                    <asp:ListItem Selected="True">通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见:"></asp:Label>
            </td>
            <td colspan="8">


                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="8" align="center">&nbsp;
                &nbsp;
                &nbsp;&nbsp;
                 <asp:Button ID="btnReSet" runat="server" Text="重启启动" BackColor="Yellow"
                     OnClientClick='return confirm( "确定将此工程重启启动吗？") '
                     OnClick="btnReSet_Click" />
                &nbsp;
                &nbsp;
                &nbsp;&nbsp;
             <asp:Button ID="btnEdit" runat="server" Text="修改->保存" BackColor="Yellow"
                 OnClick="btnEdit_Click" />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
              <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow"
                  OnClick="Button1_Click" Width="51px" />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow"
                    OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>


</asp:Content>
