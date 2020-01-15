using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class JXC_REPORTList : BasePage
    {


        JXC_REPORTService POSer = new JXC_REPORTService();

        protected void ddlGoodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGoodType.SelectedItem.Text == "")
            {
                ddlGoodSmType.Text = "";

            }
            else
            {
                TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
                List<TB_GoodsSmType> goodsSmTypeList = goodsSmTypeSer.GetListArrayToddl(" 1=1 and GoodTypeName='" + ddlGoodType.SelectedItem.Value + "'");
                goodsSmTypeList.Insert(0, new TB_GoodsSmType());
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeSmName";
                ddlGoodType.DataValueField = "GoodTypeSmName";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                basePoTypeList.Insert(0, new TB_BasePoType { BasePoType = "全部", Id = -1 });
                ddlPOTyle.DataSource = basePoTypeList;
                ddlPOTyle.DataBind();
                ddlPOTyle.DataTextField = "BasePoType";
                ddlPOTyle.DataValueField = "Id";

                TB_GoodsTypeService typeSer = new TB_GoodsTypeService();
                List<TB_GoodsType> allType = typeSer.GetListArray("");
                allType.Insert(0, new TB_GoodsType());
                ddlGoodType.DataSource = allType;
                ddlGoodType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeName";
                ddlGoodType.DataValueField = "GoodTypeName";


                TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
                List<TB_GoodsSmType> goodsSmTypeList = goodsSmTypeSer.GetListArrayToddl("");
                goodsSmTypeList.Insert(0, new TB_GoodsSmType());
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodSmType.DataTextField = "GoodTypeSmName";
                ddlGoodSmType.DataValueField = "GoodTypeSmName";

                
                //主单
                List<JXC_REPORT> pOOrderList = new List<JXC_REPORT>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();


                bool showAll = true;
                if (QuanXian_ShowAll(SysObj.JXC_REPORTList) == false)                
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }

               

                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll(SysObj.JXC_REPORTList, Session["currentUserId"], "WFScanDepartList") == true)
                {
                    ViewState["WFScanDepartList"] = false;
                    WFScanDepartList = false;                  
                }
                List<VAN_OA.Model.User> user=new List<VAN_OA.Model.User> ();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (showAll == true)
                {
                    user = userSer.getAllUserByPOList();
                     user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                else if (WFScanDepartList == true)
                {
                    user = userSer.getAllUserByLoginName(string.Format(" and loginIPosition in (select loginIPosition from tb_User where id={0}) and loginIPosition<>''", Session["currentUserId"]));
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = 0 });
                }
                else
                {
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                }
               
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                if (Request["IsSpecial"] != null)
                {
                    ddlIsSpecial.Text = Request["IsSpecial"].ToString();
                     
                }
                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();
                    Show();
                }
            }
        }      


        private void Show()
        {
            string sql = " 1=1 ";
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from tb_User where  IFZhui=0 and {0} and CG_POOrder.appName=id)", where);
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and JXC_REPORT.PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }
          

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and Supplier  like '%{0}%'", txtGuestName.Text.Trim());
            }

            var poWhere = "";
            if (ddlIsSpecial.Text!="-1")
            {
                poWhere = string.Format(" and IsSpecial={0}", ddlIsSpecial.Text);
            }
            if (ddlFax.Text != "-1")
            {
                poWhere += string.Format(" and IsPoFax={0}", ddlFax.Text);
            }
            if (ddlPOTyle.Text != "-1")
            {
                poWhere +=" and POType="+ddlPOTyle.Text;
            }
            if (ddlModel.Text != "全部")
            {
                poWhere += string.Format(" and Model='{0}'", ddlModel.Text);
            }
            if (ddlUser.Text == "-1")//显示所有用户
            {
                //if (cbIsSpecial.Checked)
                //{
                //    sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=0 AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
                //}
                if (!string.IsNullOrEmpty(poWhere))
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where PONO=JXC_REPORT.PONO {0})", poWhere);
                }
             }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                if (!string.IsNullOrEmpty(poWhere))
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO  {1} )", model.LoginIPosition,poWhere);
                }
                else
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO )", model.LoginIPosition);
                }
               
            }
            else
            { 
                if (!string.IsNullOrEmpty(poWhere))
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO  {1} )", ddlUser.Text,poWhere);
                }
                else
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO  )", ddlUser.Text);
                }
               
            }

            if (ddlPoType.Text != "-1" && ddlPoType.Text!="12")
            {
                sql += string.Format(" and JXC_REPORT.poType={0}", ddlPoType.Text);
            }
            if (ddlPoType.Text == "12")
            {
                sql += string.Format(" and JXC_REPORT.poType in(3,4,5,6,7,8,9,10,11)", ddlPoType.Text);
            }

            if (ddlGoodSmType.Text != "" && ddlGoodType.Text != "")
            {
                sql += string.Format(" and GoodTypeSmName ='{0}' and GoodTypeName ='{1}' ",
                    ddlGoodSmType.SelectedItem.Value, ddlGoodType.SelectedItem.Value);
            }
            else
            {
                if (ddlGoodSmType.Text != "" && ddlGoodType.Text == "")
                {
                    sql += string.Format(" and GoodTypeSmName ='{0}' ", ddlGoodSmType.SelectedItem.Value);
                }
                if (ddlGoodSmType.Text == "" && ddlGoodType.Text != "")
                {
                    sql += string.Format(" and  GoodTypeName ='{0}'", ddlGoodType.SelectedItem.Value);
                }

            }

            if (txtGoodNo.Text != "" || txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
            {
                string goodInfo = "";
                if (txtGoodNo.Text != "")
                {
                    goodInfo += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
                }
                if (txtNameOrTypeOrSpec.Text != "" && txtNameOrTypeOrSpecTwo.Text != "")
                {
                    goodInfo += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec.Text, txtNameOrTypeOrSpecTwo.Text);
                }
                else if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec.Text;
                    if (txtNameOrTypeOrSpecTwo.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo.Text;

                    goodInfo += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
                sql += goodInfo;
            }
            if (!string.IsNullOrEmpty(txtGoodNum.Text))
            {
                if (CommHelp.VerifesToNum(txtGoodNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and GoodNum{0}{1}", ddlFuHao.Text,txtGoodNum.Text);
            }

            if (!string.IsNullOrEmpty(txtChengBenPrice.Text))
            {
                if (CommHelp.VerifesToNum(txtChengBenPrice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成本单价 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and GoodPrice{0}{1}", ddlChengBen.Text, txtChengBenPrice.Text);
            }

            if (!string.IsNullOrEmpty(txtSellPrice.Text))
            {
                if (CommHelp.VerifesToNum(txtSellPrice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售单价 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and GoodSellPrice{0}{1}", ddlPrice.Text, txtSellPrice.Text);
            }
            //if (txtGoodNo.Text != "")
            //{
            //    sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
            //}
            List<JXC_REPORT> pOOrderList = this.POSer.GetListArray(sql);

            decimal allTotal = 0;
            decimal ZCBTotal = 0;
            decimal SunChaTotal = 0;
            decimal MaoLiTotal = 0;
            foreach (var model in pOOrderList)
            {
                
                allTotal += model.goodSellTotal??0;
                ZCBTotal += model.goodTotal??0;
                SunChaTotal += model.t_GoodTotalChas ?? 0;
                MaoLiTotal += model.maoli ?? 0;
            }
            lblAllTotal.Text = allTotal.ToString();
            lblMaoLiTotal.Text = MaoLiTotal.ToString();
            lblSunChaTotal.Text = SunChaTotal.ToString();
            lblZCBTotal.Text = ZCBTotal.ToString();


            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }
        JXC_REPORT SumModel = new JXC_REPORT();
        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                if (SumModel.goodTotal == null) SumModel.goodTotal = 0;
                if (SumModel.maoli == null) SumModel.maoli = 0;
                if (SumModel.t_GoodTotalChas == null) SumModel.t_GoodTotalChas = 0;
                if (SumModel.goodSellTotal == null) SumModel.goodSellTotal = 0;

                JXC_REPORT model = e.Row.DataItem as JXC_REPORT;
                SumModel.goodTotal += model.goodTotal;
                SumModel.maoli += model.maoli;
                SumModel.t_GoodTotalChas += model.t_GoodTotalChas;
                SumModel.goodSellTotal += model.goodSellTotal;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("goodTotal") as Label, SumModel.goodTotal == null ? "" : SumModel.goodTotal.ToString());//数量
                setValue(e.Row.FindControl("maoli") as Label, SumModel.maoli == null ? "" : SumModel.maoli.ToString());//数量
                setValue(e.Row.FindControl("t_GoodTotalChas") as Label, SumModel.t_GoodTotalChas == null ? "" : SumModel.t_GoodTotalChas.ToString());//数量
                setValue(e.Row.FindControl("goodSellTotal") as Label, SumModel.goodSellTotal == null ? "" : SumModel.goodSellTotal.ToString());//数量
    
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {


               
            
            }
        }



        


        private void setValue(Label control, string value)
        {
            if(control!=null)
            control.Text = value;
        }
    }
}
