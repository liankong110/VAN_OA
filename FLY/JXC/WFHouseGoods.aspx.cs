using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.EFrom;
using System.Drawing;

namespace VAN_OA.JXC
{
    public partial class WFHouseGoods : BasePage
    {

        protected string GetValue(object value)
        {
            return string.Format("{0:n4}", Convert.ToDecimal(value));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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


                Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                houseList.Insert(0, new TB_HouseInfo());
                ddlHouse.DataSource = houseList;
                ddlHouse.DataBind();
                ddlHouse.DataTextField = "houseName";
                ddlHouse.DataValueField = "id";



                List<TB_HouseGoods> allGoods = new List<TB_HouseGoods>();
                gvList.DataSource = allGoods;
                gvList.DataBind();

                if (NewShowAll_textName("库存查询", "编码可链接"))
                {
                    ViewState["codeUpdate"] = true;
                }
                if (NewShowAll_textName("库存查询", "可导出"))
                {
                    btnExcel.Visible = true;
                }
                else
                {
                    btnExcel.Visible = false;
                }
            }

        }
        Dal.BaseInfo.TB_GoodService goodsSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();
        Dal.JXC.TB_HouseGoodsService houseSer = new VAN_OA.Dal.JXC.TB_HouseGoodsService();
        private void Show()
        {
            if (!string.IsNullOrEmpty(txtNum.Text))
            {
                try
                {
                    Convert.ToDecimal(txtNum.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量格式有误！');</script>");

                    return;
                }
            }
            string sql = " 1=1 ";
            if (txtGoodAvgPrice.Text != "")
            {
                try
                {
                    if (Convert.ToDecimal(txtGoodAvgPrice.Text) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品均价必须大于0！');</script>");

                        return;
                    }
                    sql += string.Format(" and GoodAvgPrice {0} {1}", ddlPrice.Text, txtGoodAvgPrice.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品均价格式有误！');</script>");

                    return;
                }
            }


            if (txtZhuJi.Text != "")
            {
                //string goodName = txtZhuJi.Text.Replace(@"\", ",");

                string[] allList = txtZhuJi.Text.Split('\\');
                if (allList.Length != 7)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品格式不正确！');</script>");
                    return;
                }

                int goodId = goodsSer.GetGoodId(allList[1], allList[3], allList[4], allList[2], allList[5]);
                if (goodId == 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的商品不存在！');</script>");

                    return;
                }

                sql += string.Format(" and TB_HouseGoods.GoodId={0}", goodId);

            }

            if (!string.IsNullOrEmpty(txtNum.Text))
            {
                sql += string.Format(" and GoodNum{0}{1}", ddlFuHao.SelectedItem.Text, txtNum.Text);
            }
            if (ddlGoodSmType.Text != "" && ddlGoodType.Text != "")
            {
                sql += string.Format(" and GoodTypeSmName ='{0}' ", ddlGoodSmType.SelectedItem.Value);
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

            if (ddlHouse.Text != "" && ddlHouse.Text != "0")
            {
                sql += string.Format(" and HouseId ={0}", ddlHouse.SelectedItem.Value);

            }


            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo.Text);
            }
            if (txtNameOrTypeOrSpec.Text != "" && txtNameOrTypeOrSpecTwo.Text != "")
            {
                sql += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                   txtNameOrTypeOrSpec.Text, txtNameOrTypeOrSpecTwo.Text);
            }
            else if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
            {
                var NameOrTypeOrSpec = "";
                if (txtNameOrTypeOrSpec.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec.Text;
                if (txtNameOrTypeOrSpecTwo.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo.Text;

                sql += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                   NameOrTypeOrSpec);
            }


            if (txtNameOrTypeOrSpec1.Text != "" && txtNameOrTypeOrSpecTwo1.Text != "")
            {
                sql += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') and (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                   txtNameOrTypeOrSpec1.Text, txtNameOrTypeOrSpecTwo1.Text);
            }
            else if (txtNameOrTypeOrSpec1.Text != "" || txtNameOrTypeOrSpecTwo1.Text != "")
            {
                var NameOrTypeOrSpec = "";
                if (txtNameOrTypeOrSpec1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec1.Text;
                if (txtNameOrTypeOrSpecTwo1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo1.Text;

                sql += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                   NameOrTypeOrSpec);
            }
            if (cbEmpty.Checked == false)
            {
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
            }
            else
            {
                sql += string.Format(" and GoodAreaNumber=''");
            }

            if (txtCaiKuNum.Text != "")
            {
                if (CommHelp.VerifesToNum(txtCaiKuNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购需出格式出错！');</script>");
                    return;
                }
                sql += string.Format("and isnull(SumKuXuCai,0){0}{1}", ddlCaiKuNum.Text, txtCaiKuNum.Text);
            }

            if (txtZhiLiuNum.Text != "")
            {
                if (CommHelp.VerifesToNum(txtZhiLiuNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('滞留库存格式出错！');</script>");
                    return;
                }
                sql += string.Format("and (isnull(GoodNum,0)-isnull(SumKuXuCai,0)+isnull(CaiNotCheckNum,0)){0}{1}", ddlZhiLiuNum.Text, txtZhiLiuNum.Text);
            }

            List<TB_HouseGoods> gooQGooddList = this.houseSer.GetListArrayToInvoice(sql);

            lblHadInvoice.Text = string.Format("{0:n2}", gooQGooddList.Sum(t => t.HadInvoice));
            lblNoInvoice.Text = string.Format("{0:n2}", gooQGooddList.Sum(t => t.NoInvoice));
            this.gvList.DataSource = gooQGooddList;
            this.gvList.DataBind();
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
                sql = "select ProNo from TB_Good where GoodId=" + gvList.DataKeys[e.NewEditIndex].Value;
                var proNo = DBHelp.ExeScalar(sql);
                string strProNo = "";
                if (proNo is DBNull || proNo == null || proNo.ToString() == "")
                {
                    strProNo = new tb_EFormService().GetAllE_No("TB_Good");
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
            Session["backurl"] = "/JXC/WFHouseGoods.aspx";
            Response.Redirect(url);
        }

        TB_HouseGoods SumOrders = new TB_HouseGoods();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                TB_HouseGoods model = e.Row.DataItem as TB_HouseGoods;
                SumOrders.GoodNum += model.GoodNum;
                SumOrders.Total += model.Total;

                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                if (btnEdit != null && ViewState["codeUpdate"] == null)
                {
                    btnEdit.Enabled = false;
                }

                if (model.GoodNum > model.SumKuXuCai)
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#FFF0F5");
                }
                //if (btnEdit != null)
                //{
                //    string val = string.Format("javascript:window.showModalDialog('CG_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                //    btnEdit.Attributes.Add("onclick", val);
                //}
            }


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.GoodNum.ToString());//数量                
                setValue(e.Row.FindControl("lblTotal") as Label, SumOrders.Total.ToString());//成本总额    
            }
        }


        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected void btnSelect_Click(object sender, EventArgs e)
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

        protected void ddlGoodSmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

        protected void cbEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEmpty.Checked)
            {
                ddlArea.Enabled = false;
                ddlCol.Enabled = false;
                ddlNumber.Enabled = false;
                ddlRow.Enabled = false;
            }
            else
            {
                ddlArea.Enabled = true;
                ddlCol.Enabled = true;
                ddlNumber.Enabled = true;
                ddlRow.Enabled = true;
            }
        }

        public string xlfile = "库存查询.xls";
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            gvList.AllowPaging = false;
            Show();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            //Response.AppendHeader("Content-Disposition", "attachment;filename=" + xlfile);
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(xlfile, System.Text.Encoding.UTF8) + "\"");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvList.RenderControl(hw);

            Response.Write(sw.ToString());
            Response.End();

            gvList.AllowPaging = true;




        }
        public override void VerifyRenderingInServerForm(Control control)
        { }
    }
}
