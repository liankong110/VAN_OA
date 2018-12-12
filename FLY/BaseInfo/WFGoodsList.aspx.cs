using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using System.Data;
using Microsoft.Office.Interop.Excel;
using VAN_OA.Dal.EFrom;



namespace VAN_OA.BaseInfo
{
    public partial class WFGoodsList : BasePage
    {
        public string Query = "QueryGood";
        private TB_GoodService goodSer = new TB_GoodService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFGoods.aspx?ProId=36");
        }
        protected void btnDo_Click(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void ShowAll()
        {
            List<TB_Good> pos = this.goodSer.GetListArray("");
            foreach (var m in pos)
            {
                string sql = "select pro_Id from A_ProInfo where pro_Type='商品档案申请'";

                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='商品档案申请')", m.GoodId);
                object eformIdObj = DBHelp.ExeScalar(efromId);

                object proId = DBHelp.ExeScalar(sql);
                if ((eformIdObj is DBNull) || eformIdObj == null)
                {
                    sql = "select ProNo from TB_Good where GoodId=" + m.GoodId;
                    var proNo = DBHelp.ExeScalar(sql);
                    string strProNo = "";
                    if (proNo is DBNull || proNo == null || proNo.ToString() == "")
                    {
                        strProNo = new tb_EFormService().GetAllE_No("TB_Good");
                        DBHelp.ExeCommand(string.Format(" update TB_Good set GoodProNo='{0}',GoodStatus='通过' where GoodId={1}", strProNo, m.GoodId));
                    }
                    else
                    {
                        strProNo = proNo.ToString();
                    }
                    string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',GETDATE())", proId,
                         m.GoodId, strProNo);
                    DBHelp.ExeCommand(insertEform);
                    efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='商品档案申请')", m.GoodId);
                    eformIdObj = DBHelp.ExeScalar(efromId);
                }

            }
        }

        protected string IfSepc(object obj)
        {
            if (obj != null && Convert.ToBoolean(obj))
            {
                return "是";
            }
            return "否";
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

        private void Show()
        {

            QuerySession.QueryGood QGood = new VAN_OA.QuerySession.QueryGood();
            string sql = " 1=1 ";

            if (txtGoodNo.Text != "")
            {
                sql += string.Format("  and GoodNo like '%{0}%'", txtGoodNo.Text);
                QGood.GoodNo = txtGoodNo.Text;
            }

            if (txtName.Text != "")
            {
                sql += string.Format(" and GoodName like '%{0}%'", txtName.Text);
                QGood.GoodName = txtName.Text;
            }

            if (ddlArea.Text != "")
            {
                sql += string.Format(" and GoodArea='{0}'", ddlArea.Text);
            }
            if (ddlNumber.Text != "")
            {
                sql += string.Format(" and GoodNumber='{0}'", ddlNumber.Text);
            }
            if (ddlRow.Text != "")
            {
                sql += string.Format(" and GoodRow='{0}'", ddlRow.Text);
            }
            if (ddlCol.Text != "")
            {
                sql += string.Format(" and GoodCol='{0}'", ddlCol.Text);
            }

            if (txtZhuJi.Text != "")
            {
                sql += string.Format(" and ZhuJi like '%{0}%'", txtZhuJi.Text);
                QGood.ZhuJi = txtZhuJi.Text;
            }

            if (txtGoodBrand.Text != "")
            {
                sql += string.Format(" and GoodBrand like '%{0}%'", txtGoodBrand.Text);              
            }
            //if (ddlGoodSmType.Text != "" && ddlGoodSmType.Text != "0")
            //{
            //    sql += string.Format(" and GoodTypeSmName ='{0}'", ddlGoodSmType.SelectedItem.Text);
            //    //QGood.GoodSmTypeId = Convert.ToInt32(ddlGoodSmType.SelectedItem.Value);
            //}

            if (ddlGoodSmType.Text != "" && ddlGoodType.Text != "")
            {
                sql += string.Format(" and GoodTypeSmName ='{0}' and GoodTypeName='{1}'", ddlGoodSmType.SelectedItem.Value, ddlGoodType.SelectedItem.Value);
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

            if (Request["IsMine"] != null)
            {
                sql += " and CreateUserId=" + Session["currentUserId"].ToString();
            }
            if (ddlSpecial.Text != "-1")
            {
                sql += " and ifSpec=" + ddlSpecial.Text;
            }

            if (ddlStatus.Text != "-1")
            {
                sql += string.Format(" and GoodStatus='{0}'", ddlStatus.Text);
            }

            //sql += string.Format(@" and GoodStatus='通过' ");
            Session[Query] = QGood;
            List<TB_Good> gooQGooddList = this.goodSer.GetListArray(sql);

            //处理下仓库信息
            //foreach (var m in gooQGooddList)
            //{
            //    if (!string.IsNullOrEmpty(m.GoodAreaNumber))
            //    {
            //        string[] tt = m.GoodAreaNumber.Split('-');
            //        sql = string.Format("update TB_Good set GoodArea='{0}',GoodNumber='{1}',GoodRow='{2}',GoodCol='{3}' where GoodId={4}",
            //            tt[0].Substring(0,1),tt[0].Substring(1),tt[1],tt[2],m.GoodId );
            //        DBHelp.ExeCommand(sql);
            //    }
            //}
            AspNetPager1.RecordCount = gooQGooddList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = gooQGooddList;
            this.gvList.DataBind();
        }

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
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //判断是否删除数据
            string sql = string.Format("select count(*) from CG_POOrders where goodId={0}", this.gvList.DataKeys[e.RowIndex].Value);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('该商品已经被其他信息引用，无法删除！');</script>"));
                return;
            }

            sql = string.Format("select count(*) from CAI_POOrders where goodId={0}", this.gvList.DataKeys[e.RowIndex].Value);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('该商品已经被其他信息引用，无法删除！');</script>"));
                return;
            }

            sql = string.Format("select * from TB_HouseGoods where goodId={0}", this.gvList.DataKeys[e.RowIndex].Value);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('该商品已经被其他信息引用，无法删除！');</script>"));
                return;
            }
            sql = string.Format("delete from tb_EForms where e_Id=( select id from tb_EForm where proId=36 and allE_id={0});delete from tb_EForm where proId=36 and allE_id={0};", gvList.DataKeys[e.RowIndex].Value);
            if (DBHelp.ExeCommand(sql))
            {
                this.goodSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                Show();

            }
          
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            //base.Response.Redirect("~/BaseInfo/WFGoods.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

            string sql = "select pro_Id from A_ProInfo where pro_Type='商品档案申请'";

            string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='商品档案申请')", this.gvList.DataKeys[e.NewEditIndex].Value);
            object eformIdObj = DBHelp.ExeScalar(efromId);

            object proId = DBHelp.ExeScalar(sql);
            if ((eformIdObj is DBNull) || eformIdObj == null)
            {
                sql = "select GoodProNo as ProNo from TB_Good where GoodId=" + gvList.DataKeys[e.NewEditIndex].Value;
                var proNo = DBHelp.ExeScalar(sql);
                string strProNo = "";
                if (proNo is DBNull || proNo == null || proNo.ToString() == "")
                {
                    strProNo = new tb_EFormService().GetAllE_NoByGoods("TB_Good");
                    DBHelp.ExeCommand(string.Format(" update TB_Good set GoodProNo='{0}',GoodStatus='通过' where GoodId={1}", strProNo, gvList.DataKeys[e.NewEditIndex].Value));
                }
                else
                {
                    strProNo = proNo.ToString();
                }
                string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',GETDATE())", proId,
                    gvList.DataKeys[e.NewEditIndex].Value, strProNo);
                DBHelp.ExeCommand(insertEform);
                efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='商品档案申请')", this.gvList.DataKeys[e.NewEditIndex].Value);
                eformIdObj = DBHelp.ExeScalar(efromId);
            }

            string url = "~/BaseInfo/WFGoods.aspx?ProId=" + proId + "&allE_id=" + this.gvList.DataKeys[e.NewEditIndex].Value + "&EForm_Id=" + eformIdObj + "&&ReAudit=true";
            Session["backurl"] = "/BaseInfo/WFGoodsList.aspx";
            Response.Redirect(url);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //加载基本信息
                ddlNumber.Items.Add(new ListItem { Text = "全部", Value = "" });
                ddlRow.Items.Add(new ListItem { Text = "全部", Value = "" });
                ddlCol.Items.Add(new ListItem { Text = "全部", Value = "" });
                //货架号：1.全部  缺省 2….51 1,..50 
                for (int i = 1; i < 51; i++)
                {
                    ddlNumber.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    //层数：1.全部  缺省 2….21 1,2,3…20 
                    //部位：1.全部  缺省 2….21 1,2,3…20
                    if (i <= 21)
                    {
                        ddlRow.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                        ddlCol.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                }

                if (Request["IsMine"] != null)
                {
                    gvList.Columns[0].Visible = false;
                }


                TB_GoodsTypeService typeSer = new TB_GoodsTypeService();
                List<TB_GoodsType> allType = typeSer.GetListArray("");
                allType.Insert(0, new TB_GoodsType());
                ddlGoodType.DataSource = allType;
                ddlGoodType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeName";
                ddlGoodType.DataValueField = "GoodTypeName";



                TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
                List<TB_GoodsSmType> goodsSmTypeList = goodsSmTypeSer.GetListArray("");
                goodsSmTypeList.Insert(0, new TB_GoodsSmType());
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodSmType.DataTextField = "GoodTypeSmName";
                ddlGoodSmType.DataValueField = "GoodTypeSmName";


                //加载SESSION中的数据
                if (Session[Query] != null)
                {
                    //QueryEForms
                    QuerySession.QueryGood QEForm = Session[Query] as QuerySession.QueryGood;
                    if (QEForm == null)
                    {
                        return;
                    }

                    if (QEForm.GoodSmTypeId != 0)
                    {
                        try
                        {
                            // ddlGoodSmType.Text = QEForm.GoodSmTypeId.ToString();
                        }
                        catch (Exception)
                        {


                        }

                    }
                    if (QEForm.GoodName != "")
                    {

                        txtName.Text = QEForm.GoodName;

                    }

                    if (QEForm.GoodNo != "")
                    {

                        txtGoodNo.Text = QEForm.GoodNo;
                    }


                    if (QEForm.ZhuJi != "")
                    {

                        txtZhuJi.Text = QEForm.ZhuJi;
                    }

                    Show();



                }
                else
                {
                    List<TB_Good> goodList = new List<TB_Good>();
                    this.gvList.DataSource = goodList;
                    this.gvList.DataBind();
                }
            }
        }

        protected void ddlGoodSmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

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
            Show();
        }

        public string xlfile = "库存列表.xls";
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string sql = @"select * from 
(SELECT [GoodNo] as '编码'
      ,[GoodName] as '名称'
	,GoodTypeName as '大类'
      ,[GoodTypeSmName] as '小类'
      ,[GoodSpec] as '规格'
      ,[GoodModel] as '型号'
      ,[GoodUnit] as '单位'
      ,0  as '数量'
  FROM TB_Good
 where GoodId not in  (
select GoodId from TB_HouseGoods)
union all
select GoodNo as '编码'
,GoodName  as '名称',GoodTypeName as '大类',GoodTypeSmName  as '小类',GoodSpec as '规格',GoodModel as '型号',GoodUnit as '单位',GoodNum as '数量'
FROM TB_HouseGoods left join TB_Good on TB_Good.GoodId=TB_HouseGoods.GoodId  
) as newtable
order by 小类";
            System.Data.DataTable dt = DBHelp.getDataTable(sql);



            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + xlfile);
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = dt;
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();

            //CreateExcel(dt, xlfile);
        }


    }
}
