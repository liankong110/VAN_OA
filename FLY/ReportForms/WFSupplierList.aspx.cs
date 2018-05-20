using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.ReportForms;
using System.Data.SqlClient;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Dal.EFrom;

namespace VAN_OA.ReportForms
{
    public partial class WFSupplierList : BasePage
    {
        private TB_SupplierInfoService supplierSer = new TB_SupplierInfoService();

        public string Query = "QuerySupplierTrack";



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/SupplierList.aspx";
            base.Response.Redirect("~/ReportForms/WFSupplierInfo.aspx?type=man");
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        private void Show()
        {
            string sql = " 1=1 ";

            QuerySession.QueryGuestTrack QSupplierTrack = new VAN_OA.QuerySession.QueryGuestTrack();
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('登记日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Time>='{0} 00:00:00'", txtFrom.Text);
                QSupplierTrack.FromTime = txtFrom.Text;
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('登记日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Time<='{0} 23:59:59'", txtTo.Text);
                QSupplierTrack.ToTime = txtTo.Text;
            }

            if (txtSupplierName.Text.Trim() != "")
            {
                if (cbPiPei.Checked == false)
                {
                    sql += string.Format(" and SupplierName like '%{0}%'", txtSupplierName.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and SupplierName='{0}'", txtSupplierName.Text.Trim());
                }
                QSupplierTrack.GuestName = txtSupplierName.Text;
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }
            if (txtSupplieSimpeName.Text.Trim() != "")
            {
                sql += string.Format(" and SupplieSimpeName like '%{0}%'", txtSupplieSimpeName.Text.Trim());
            }
            if (txtPhone.Text != "")
            {
                sql += string.Format(" and Phone like '%{0}%'", txtPhone.Text);
            }
            if (txtLikeMan.Text != "")
            {
                sql += string.Format(" and LikeMan like '%{0}%'", txtLikeMan.Text);
            }

            if (ddlShow.Text != "-1")
            {
                sql += string.Format(" and IsUse={0} ", ddlShow.Text);
            }
            if (ddlSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial={0} ", ddlSpecial.Text);
            }
            sql += string.Format(@" and Status='通过' ");

            List<TB_SupplierInfo> SupplierTracks = this.supplierSer.GetListArray(sql);
            AspNetPager1.RecordCount = SupplierTracks.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            Session[Query] = QSupplierTrack;
            this.gvList.DataSource = SupplierTracks;
            this.gvList.DataBind();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = string.Format("delete from tb_EForms where e_Id=( select id from tb_EForm where proId=30 and allE_id={0});delete from tb_EForm where proId=30 and allE_id={0};", gvList.DataKeys[e.RowIndex].Value);
              if (DBHelp.ExeCommand(sql))
              {
                  this.supplierSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                  Show();
              }

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            string sql = "select pro_Id from A_ProInfo where pro_Type='供应商申请表'";

            string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='供应商申请表')", this.gvList.DataKeys[e.NewEditIndex].Value);
            object eformIdObj = DBHelp.ExeScalar(efromId);

            object proId = DBHelp.ExeScalar(sql);
            if ((eformIdObj is DBNull) || eformIdObj == null)
            {
                sql = "select ProNo from TB_SupplierInfo where id=" + gvList.DataKeys[e.NewEditIndex].Value;
                var proNo = DBHelp.ExeScalar(sql);
                string strProNo = "";
                if (proNo is DBNull || proNo == null || proNo.ToString() == "")
                {
                    strProNo = new tb_EFormService().GetAllE_No("TB_SupplierInfo");
                    DBHelp.ExeCommand(string.Format(" update TB_SupplierInfo set ProNo='{0}' where id={1}", strProNo, gvList.DataKeys[e.NewEditIndex].Value));
                }
                else
                {
                    strProNo = proNo.ToString();
                }
                string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',GETDATE())", proId,
                    gvList.DataKeys[e.NewEditIndex].Value, strProNo);
                DBHelp.ExeCommand(insertEform);
                efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='供应商申请表')", this.gvList.DataKeys[e.NewEditIndex].Value);
                eformIdObj = DBHelp.ExeScalar(efromId);
            }

            string url = "~/ReportForms/WFSupplierInfo.aspx?ProId=" + proId + "&allE_id=" + this.gvList.DataKeys[e.NewEditIndex].Value + "&EForm_Id=" + eformIdObj + "&&ReAudit=true";
            Response.Redirect(url);


            //Session["POUrl"] = "~/ReportForms/WFSupplierList.aspx";
            //base.Response.Redirect("~/ReportForms/WFSupplierEdit.aspx?type=man&Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {


                if (Request["IsUse"] != null)
                {
                    gvList.Columns[0].Visible = false;
                    gvList.Columns[1].Visible = false;
                    gvList.Columns[3].Visible = false;
                    gvList.Columns[5].Visible = false;

                }
                else
                {

                    #region 是否有删除功能
                    if (Session["currentUserId"] != null)
                    {
                        VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                        if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                        {
                            gvList.Columns[1].Visible = false;
                        }
                    }
                    #endregion
                    gvList.Columns[2].Visible = false;
                    btnSave.Visible = false;
                    gvList.Columns[4].Visible = false;
                    btnSpecial.Visible = false;
                }





                WebQuerySessin Sess = new WebQuerySessin(Query);
                //加载SESSION中的数据
                if (Session[Query] != null)
                {

                    //QueryEForms
                    QuerySession.QueryGuestTrack QSupplierTrack = Session[Query] as QuerySession.QueryGuestTrack;
                    if (QSupplierTrack == null)
                    {
                        return;
                    }

                    if (QSupplierTrack.FromTime != "")
                    {

                        txtFrom.Text = QSupplierTrack.FromTime;
                    }

                    if (QSupplierTrack.ToTime != "")
                    {
                        txtTo.Text = QSupplierTrack.ToTime;
                    }

                    if (QSupplierTrack.GuestName != "")
                    {
                        txtSupplierName.Text = QSupplierTrack.GuestName;
                    }
                    Show();
                }
                else
                {
                    List<TB_SupplierInfo> TB_SupplierInfos = new List<TB_SupplierInfo>();
                    this.gvList.DataSource = TB_SupplierInfos;
                    this.gvList.DataBind();



                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string where = " ID  in (";
            string expWhere = " ID  in (";
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("cbIsUse")) as CheckBox;
                if (cb.Checked)
                {
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    where += "'" + lblIds.Text + "',";
                }
                else
                {
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    expWhere += "'" + lblIds.Text + "',";
                }
            }

            if (where != " ID  in (")
            {
                where = where.Substring(0, where.Length - 1) + ")";
                var sql = "update TB_SupplierInfo set IsUse=1 where " + where;
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }

            if (expWhere != " ID  in (")
            {
                expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                var sql = "update TB_SupplierInfo set IsUse=0 where " + expWhere;
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
        }

        protected void btnSpecial_Click(object sender, EventArgs e)
        {
            string where = " ID  in (";
            string expWhere = " ID  in (";
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("cbIsSpecial")) as CheckBox;
                if (cb.Checked)
                {
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    where += "'" + lblIds.Text + "',";
                }
                else
                {
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    expWhere += "'" + lblIds.Text + "',";
                }
            }

            if (where != " ID  in (")
            {
                where = where.Substring(0, where.Length - 1) + ")";
                var sql = "update TB_SupplierInfo set IsSpecial=1 where " + where;
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }

            if (expWhere != " ID  in (")
            {
                expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                var sql = "update TB_SupplierInfo set IsSpecial=0 where " + expWhere;
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
        }
    }
}
