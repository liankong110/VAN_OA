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
using System.Text;
using System.Drawing;

namespace VAN_OA.BaseInfo
{
    public partial class WFNEWGoodsList : BasePage
    {
        Dal.BaseInfo.TB_GoodService goodsSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();

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

        private string GetSql()
        {
            string sql = " 1=1 ";
            if (txtGoodNum.Text != "")
            {
                if (CommHelp.VerifesToNum(txtGoodNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('库存数量格式出错！');</script>");
                    return "";
                }
                sql += string.Format("and isnull(GoodNum,0){0}{1}",ddlGoodNum.Text,txtGoodNum.Text);
            }

            if (txtCaiKuNum.Text != "")
            {
                if (CommHelp.VerifesToNum(txtCaiKuNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购需出格式出错！');</script>");
                    return "";
                }
                sql += string.Format("and isnull(SumKuXuCai,0){0}{1}", ddlCaiKuNum.Text, txtCaiKuNum.Text);
            }
            
            if (txtZhiLiuNum.Text != "")
            {
                if (CommHelp.VerifesToNum(txtZhiLiuNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('滞留库存格式出错！');</script>");
                    return "";
                }
                sql += string.Format("and (isnull(GoodNum,0)-isnull(SumKuXuCai,0)+isnull(CaiNotCheckNum,0)){0}{1}", ddlZhiLiuNum.Text, txtZhiLiuNum.Text);
            }

            if (ddlSpecial.Text != "-1")
            {
                sql += " and IfSpec=" + ddlSpecial.Text;
            }
            if (!string.IsNullOrEmpty(txtGoodBrand.Text))
            {
                sql += string.Format(" and GoodBrand like '%{0}%'", txtGoodBrand.Text);
            }
            if (txtGoodAvgPrice.Text != "")
            {
                try
                {
                    if (Convert.ToDecimal(txtGoodAvgPrice.Text) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品均价必须大于0！');</script>");

                        return "";
                    }
                    sql += string.Format(" and GoodAvgPrice {0} {1}", ddlPrice.Text, txtGoodAvgPrice.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品均价格式有误！');</script>");

                    return "";
                }
            }


            if (txtZhuJi.Text != "")
            {
                //string goodName = txtZhuJi.Text.Replace(@"\", ",");

                string[] allList = txtZhuJi.Text.Split('\\');
                if (allList.Length != 7)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品格式不正确！');</script>");
                    return "";
                }
                int goodId = goodsSer.GetGoodId(allList[1], allList[3], allList[4], allList[2], allList[5]);
                if (goodId == 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的商品不存在！');</script>");

                    return "";
                }

                sql += string.Format(" and Temp.GoodId={0}", goodId);

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
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
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


			if (txtNameOrTypeOrSpec2.Text != "" || txtNameOrTypeOrSpecTwo2.Text != "")
			{
				if (txtNameOrTypeOrSpec2.Text != "" && txtNameOrTypeOrSpecTwo2.Text != "")
				{
					sql += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') and (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
					   txtNameOrTypeOrSpec2.Text, txtNameOrTypeOrSpecTwo2.Text);
				}
				else if (txtNameOrTypeOrSpec2.Text != "" || txtNameOrTypeOrSpecTwo2.Text != "")
				{
					var NameOrTypeOrSpec = "";
					if (txtNameOrTypeOrSpec2.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec2.Text;
					if (txtNameOrTypeOrSpecTwo2.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo2.Text;

					sql += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
					   NameOrTypeOrSpec);
				}
			}
			//sql += string.Format(@" and GoodStatus='通过' ");

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
            if (ddlStatus.Text != "-1")
            {
                sql += string.Format(" and GoodStatus='{0}'", ddlStatus.Text);
            }
            return sql;
        }
        private void Show()
        {
            if (!string.IsNullOrEmpty(txtGoodAvgPrice.Text))
            {
                if (CommHelp.VerifesToNum(txtGoodAvgPrice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品均价 格式错误！');</script>");
                    return;
                }
            }

            List<TB_Good> gooQGooddList = this.goodSer.GetListArray_New(GetSql());

            lblHadInvoice.Text = string.Format("{0:n2}", gooQGooddList.Sum(t => t.HadInvoice));
            lblNoInvoice.Text = string.Format("{0:n2}", gooQGooddList.Sum(t => t.NoInvoice));
            lblHouseTotal.Text = string.Format("{0:n2}", gooQGooddList.Sum(t => t.GoodTotal));

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
                TB_Good model = e.Row.DataItem as TB_Good;
                if (model.GoodNum > model.SumKuXuCai)
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#FFF0F5");
                }
            }
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

                List<TB_Good> goodList = new List<TB_Good>();
                this.gvList.DataSource = goodList;
                this.gvList.DataBind();
                
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

        public string xlfile = "商品列表.xls";
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(@" IfSpec as '特殊',GoodProNo as '单据号',GoodNo as '编码',GoodName as '名称',GoodBrand as '品牌',ZhuJi as '助记词',GoodTypeName as '类别',GoodTypeSmName as '小类',
GoodSpec as '规格',GoodModel as '型号',GoodUnit as '单位',GoodNum as '库存数量', GoodAvgPrice as '均价',GoodNum*GoodAvgPrice as '金额',CreateTime as '创建时间',loginName as '创建人' ");

           
            strSql.Append(@" from (select TB_HouseGoods.HouseId, GoodAvgPrice,GoodNum,GoodProNo,GoodStatus,GoodBrand,TB_Good.GoodId,GoodNo,GoodName,ZhuJi,GoodSpec,GoodModel,GoodUnit,CreateUserId,CreateTime,tb_User.loginName,GoodTypeSmName,GoodTypeName,IfSpec
from TB_Good left join tb_User on tb_User.ID=TB_Good.CreateUserId
 left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId where TB_HouseGoods.id is not null
 union all
 select TB_HouseGoods.HouseId,GoodPrice as GoodAvgPrice,GoodNum,GoodProNo,GoodStatus,GoodBrand,TB_Good.GoodId,GoodNo,GoodName,ZhuJi,GoodSpec,GoodModel,GoodUnit,CreateUserId,CreateTime,tb_User.loginName,GoodTypeSmName,GoodTypeName,IfSpec
from TB_Good left join tb_User on tb_User.ID=TB_Good.CreateUserId
 left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId
 left join 
 ( select * from (select GooId,GoodPrice,ROW_NUMBER() over (partition  by GooId order by id desc ) as RowNum from CAI_OrderInHouses ) A WHERE RowNum=1
 ) as B on B.GooId=TB_Good.GoodId  where TB_HouseGoods.id is null) as Temp ");            
                strSql.Append(" where " + GetSql());
            
            strSql.Append(" order by GoodNum desc,GoodProNo desc ");


            System.Data.DataTable dt = DBHelp.getDataTable(strSql.ToString());



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
