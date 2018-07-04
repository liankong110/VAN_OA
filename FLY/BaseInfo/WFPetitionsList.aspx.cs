using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.JXC;

namespace VAN_OA.BaseInfo
{
    public partial class WFPetitionsList : BasePage
    {

        PetitionsService petitionsSer = new PetitionsService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFPetitions.aspx");
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
        CG_POOrderService POSer = new CG_POOrderService();

        public string getSql(out string totalWhere)
        {
            string sql = "1=1";
            if (!string.IsNullOrEmpty(txtNumber.Text))
            {
                sql += string.Format(" and Number like '%{0}%'", txtNumber.Text);
            }
            if (!string.IsNullOrEmpty(txtPONo.Text.Trim()))
            {
              

                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }

            if (!string.IsNullOrEmpty(txtGuestName.Text.Trim()))
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            //if (!string.IsNullOrEmpty(txtSalesUnit.Text))
            //{
            //    sql += string.Format(" and SalesUnit like '%{0}%'", txtSalesUnit.Text);
            //}
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                sql += string.Format(" and Name like '%{0}%'", txtName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtSummary.Text))
            {
                sql += string.Format(" and Summary like '%{0}%'", txtSummary.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtRemark.Text))
            {
                sql += string.Format(" and Remark like '%{0}%'", txtRemark.Text);
            }

            if (ddlType.Text != "-1")
            {
                sql += string.Format(" and Type='{0}'", ddlType.Text);
            }

            if (ddlHandler.Text != "全部")
            {
                sql += string.Format(" and Handler='{0}'", ddlHandler.Text);
            }

             totalWhere = "1=1";
            if (!string.IsNullOrEmpty(txtTotalTo.Text))
            {
                if (CommHelp.VerifesToNum(txtTotalTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('总金额 格式错误！');</script>");
                    return "";
                }
                totalWhere += string.Format(" and sum(LastTruePrice*Num){0}{1}", ddlTotalTo.Text, txtTotalTo.Text);
            }
            if (!string.IsNullOrEmpty(txtTotalFrom.Text))
            {
                if (CommHelp.VerifesToNum(txtTotalFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('总金额 格式错误！');</script>");
                    return "";
                }
                totalWhere += string.Format(" and {1}{0}sum(LastTruePrice*Num)", ddlTotal.Text, txtTotalFrom.Text);
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签订时间 格式错误！');</script>");
                    return "";
                }
                sql += string.Format(" and SignDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签订时间 格式错误！');</script>");
                    return "";
                }
                sql += string.Format(" and SignDate<='{0} 23:59:59'", txtTo.Text);
            }

            if (!string.IsNullOrEmpty(txtSumPages.Text))
            {
                if (CommHelp.VerifesToNum(txtSumPages.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('总页数 格式错误！');</script>");
                    return "";
                }
                sql += string.Format(" and SumPages{0}{1}", ddlSumPages.Text, txtSumPages.Text);
            }
            if (!string.IsNullOrEmpty(txtSumCount.Text))
            {
                if (CommHelp.VerifesToNum(txtSumCount.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('总份数 格式错误！');</script>");
                    return ""; 
                }
                sql += string.Format(" and SumCount{0}{1}", ddlSumCount.Text, txtSumCount.Text);
            }
            if (!string.IsNullOrEmpty(txtBCount.Text))
            {
                if (CommHelp.VerifesToNum(txtBCount.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('己方份数 格式错误！');</script>");
                    return "";
                }
                sql += string.Format(" and BCount{0}{1}", ddlBCount.Text, txtBCount.Text);
            }
            if (ddlAE.Text != "全部")
            {
                sql += string.Format(" and AE='{0}'", ddlAE.Text);
            }

            if (ddlIsColse.Text != "-1")
            {
                sql += string.Format(" and IsColse={0}", ddlIsColse.Text);
            }
            if (ddlLocal.Text != "-1")
            {
                sql += string.Format(" and Local='{0}'", ddlLocal.Text);
            }
            if (ddlL_Year.Text != "-1")
            {
                sql += string.Format(" and L_Year={0}", ddlL_Year.Text);
            }
            if (ddlL_Month.Text != "-1")
            {
                sql += string.Format(" and L_Month={0}", ddlL_Month.Text);
            }
            if (ddlIsRequire.Text != "-1")
            {
                sql += string.Format(" and IsRequire={0}", ddlIsRequire.Text);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where Model='{0}' AND PONO=Petitions.PONO) ", ddlModel.Text);
            }
            return sql;
        }
        private void Show()
        {
            if (!string.IsNullOrEmpty(txtPONo.Text.Trim()))
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
            }
                string totalWhere;
            string sql = getSql(out totalWhere);
            if (sql == "")
            {
                return;
            }
            List<Petitions> caiList = this.petitionsSer.GetListArray(sql, totalWhere,txtSalesUnit.Text.Trim());

            lblTotal.Text = caiList.Sum(t => t.Total).ToString();

            AspNetPager1.RecordCount = caiList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            string pono = "";
            for (int i = gvList.PageIndex * 10; i < (gvList.PageIndex + 1) * 10; i++)
            {
                if (caiList.Count - 1 < i)
                {
                    break;
                }
                pono += string.Format("'{0}',",caiList[i].PoNo) ;
            }
            pono = pono.Trim(',');
            supplierList = new CAI_POCaiService().GetLastSupplier(pono);

         
            this.gvList.DataSource = caiList;
            this.gvList.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            this.gvList.AllowPaging = false;
            this.gvList.AllowSorting = false;
            gvList.Columns[0].Visible = false;
            gvList.Columns[1].Visible = false;
            AspNetPager1.CurrentPageIndex = 1;
            Show();//获取数据并绑定到GridView

            toExcel(this.gvList);

            this.gvList.AllowPaging = true;
            this.gvList.AllowSorting = true;
            gvList.Columns[0].Visible = true;
            gvList.Columns[1].Visible = true;
            Show();//获取数据并绑定到GridView
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="gv"></param>
        void toExcel(GridView gv)
        {
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string fileName = "export.xls";
            string style = @"<style> .text { mso-number-format:\@; } </script> ";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.gvList.RenderControl(htw);
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }
        /// <summary>
        /// 这个重写貌似是必须的
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control) { }


        List<CAI_POCaiView> supplierList = new List<CAI_POCaiView>();
       
        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                Petitions model = e.Row.DataItem as Petitions;
                
                var wherePoNo = supplierList.Where(t => t.PONo == model.PoNo);
                (e.Row.FindControl("SalesUnit") as Label).Text = string.Join(",", wherePoNo.Select(t => t.Supplier).ToArray());
                (e.Row.FindControl("Total") as Label).Text = wherePoNo.Sum(t => t.Total).ToString();
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.petitionsSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFPetitions.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                if (!NewShowAll_textName("签呈单档案管理", "编辑"))
                {
                    gvList.Columns[0].Visible = false;
                    btnAdd.Visible = false;
                }
                if (!NewShowAll_textName("签呈单档案管理", "删除"))
                {
                    gvList.Columns[1].Visible = false;
                }

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                user = userSer.getAllUserByLoginName("");
                user.Insert(0, new Model.User { LoginName="全部" });
                ddlHandler.DataSource = user;
                ddlHandler.DataBind();
                ddlHandler.DataTextField = "LoginName";
                ddlHandler.DataValueField = "LoginName";

                var aeList = userSer.getAllUserByPOList("");
                aeList.Insert(0, new Model.User { LoginName = "全部" });
                ddlAE.DataSource = aeList;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "LoginName";

                List<Contract> contractList = new List<Contract>();
                this.gvList.DataSource = contractList;
                this.gvList.DataBind();

                if (Request["PONO"] != null)
                {
                    txtPONo.Text = Request["PONO"].ToString();
                    Show();
                }
            }
        }
    }
}
