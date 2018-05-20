using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;


namespace VAN_OA.BaseInfo
{
    public partial class WFGoodsTypeList : BasePage
    {

        private TB_GoodsTypeService invSer = new TB_GoodsTypeService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFGoodsType.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            show();
        }

        private void show()
        {
            string sql = "";
            if (txtName.Text != "")
            {
                sql += string.Format("  GoodTypeName like '%{0}%'", txtName.Text);
            }
            List<TB_GoodsType> pers = this.invSer.GetListArray(sql);
            this.gvList.DataSource = pers;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            show();
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
            //检测是否有子类信息
            string check = string.Format("select count(*) from TB_GoodsSmType where GoodTypeName in (select GoodTypeName from TB_GoodsType where ID={0})", this.gvList.DataKeys[e.RowIndex].Value.ToString());
            if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无法删除,该信息已经被其他数据使用！');</script>");
                return;
            }
            this.invSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));


            List<TB_GoodsType> pers = this.invSer.GetListArray("");
            this.gvList.DataSource = pers;
            this.gvList.DataBind();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFGoodsType.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<TB_GoodsType> pers = new List<TB_GoodsType>();
                this.gvList.DataSource = pers;
                this.gvList.DataBind();

                TB_GoodsTypeService typeSer = new TB_GoodsTypeService();
                List<TB_GoodsType> allType = typeSer.GetListArray("");
                allType.Insert(0, new TB_GoodsType());
                ddlGoodType.DataSource = allType;
                ddlGoodType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeName";
                ddlGoodType.DataValueField = "GoodTypeName";


                List<TB_GoodsSmType> goodsTypeList = new List<TB_GoodsSmType>();
                this.gvGoodSmType.DataSource = goodsTypeList;
                this.gvGoodSmType.DataBind();

            }
        }




        private TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
        protected void btnAddGoodSmType_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFGoodsSmType.aspx");
        }

        protected void btnSelectGoodSmType_Click(object sender, EventArgs e)
        {
            showGoodSmType();
        }


        private void showGoodSmType()
        {
            string sql = " 1=1 ";
            if (txtGoodSmType.Text != "")
            {
                sql += string.Format(" and GoodTypeSmName like '%{0}%'", txtGoodSmType.Text);
            }

            if (ddlGoodType.Text != "" && ddlGoodType.Text != "0")
            {
                sql += string.Format("  and GoodTypeName='{0}'", ddlGoodType.SelectedItem.Value);
            }
            List<TB_GoodsSmType> pers = this.goodsSmTypeSer.GetListArray(sql);
            this.gvGoodSmType.DataSource = pers;
            this.gvGoodSmType.DataBind();
        }
        protected void gvGoodSmType_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvGoodSmType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGoodSmType.PageIndex = e.NewPageIndex;
            showGoodSmType();
        }

        protected void gvGoodSmType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvGoodSmType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            this.goodsSmTypeSer.Delete(Convert.ToInt32(this.gvGoodSmType.DataKeys[e.RowIndex].Value.ToString()));


            List<TB_GoodsSmType> pers = this.goodsSmTypeSer.GetListArray("");
            this.gvGoodSmType.DataSource = pers;
            this.gvGoodSmType.DataBind();

        }

        protected void gvGoodSmType_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFGoodsSmType.aspx?Id=" + this.gvGoodSmType.DataKeys[e.NewEditIndex].Value.ToString());

        }

    }
}
