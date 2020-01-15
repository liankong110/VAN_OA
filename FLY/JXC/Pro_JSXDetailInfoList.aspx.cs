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
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class Pro_JSXDetailInfoList : BasePage
    {
        Pro_JSXDetailInfoService jxcDetailSer = new Pro_JSXDetailInfoService();

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                ddlHouse.DataSource = houseList;
                ddlHouse.DataBind();
                ddlHouse.DataTextField = "houseName";
                ddlHouse.DataValueField = "id";

                //主单
                List<Pro_JSXDetailInfo> pOOrderList = new List<Pro_JSXDetailInfo>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();
                if (Request["goodNo"] != null)
                {
                    //txtFrom.Text = (DateTime.Now.Year - 1) + "-1-1";
                    txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtGoodNo.Text = Request["goodNo"];
                    ddlHouse.SelectedIndex = 0;
                    Show();
                }
            }
        }
        Dal.BaseInfo.TB_GoodService goodsSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();

        private void Show()
        {

            if ((txtInvName.Text == "" && txtGoodNo.Text == "") || ddlHouse.SelectedValue == "0")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('查询条件必选全部填写！');</script>");
                txtFrom.Focus();
                return;
            }
            if (txtFrom.Text == "" && txtTo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期至少选一个！');</script>");
                txtFrom.Focus();
                return;
                
            }
            try
            {
                if (txtFrom.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期时间 格式错误！');</script>");
                        return;
                    }
                    Convert.ToDateTime(txtFrom.Text);
                }
                if (txtTo.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期时间 格式错误！');</script>");
                        return;
                    }
                    Convert.ToDateTime(txtTo.Text);
                }
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期格式错误！');</script>");

                return;
            }
            //if (txtGoodNo.Text != "" && txtInvName.Text != "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('查询条件必选全部填写！');</script>");
            //    txtFrom.Focus();
            //    return;
            //}
            int goodId = 0;

            if (txtInvName.Text != "")
            {
                //string goodName = txtInvName.Text.Replace(@"\", ",");
                string[] allList = txtInvName.Text.Split('\\');
                if (allList.Length != 7)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品格式不正确！');</script>");
                    return ;
                }
                goodId = goodsSer.GetGoodId(allList[1], allList[3], allList[4], allList[2], allList[5]);
                if (goodId == 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的商品不存在！');</script>");

                    return;
                }
            }

            if (txtGoodNo.Text != "")
            {
                var allModels = goodsSer.GetListArray(string.Format(" 1=1 and GoodNo='{0}'", txtGoodNo.Text));

                if (allModels.Count != 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的商品不存在！');</script>");

                    return;
                }
                goodId = allModels[0].GoodId;
            }

            var models=goodsSer.GetListArray(" GoodId=" + goodId);
            var nums=goodsSer.GetGoodNum(goodId);
            lblGoodNum.Text = nums[0].ToString();
            lblCaiKuNum.Text= nums[1].ToString();

            List<TB_Good> goodList = new TB_GoodService().GetListArray_New(string.Format(" Temp.GoodId={0}", goodId));
            decimal ZhiLiuKuCun = 0;
            if (goodList.Count > 0)
            {
                ZhiLiuKuCun = goodList[0].ZhiLiuKuCun;
            }

            lblZhiLiuNum.Text = ZhiLiuKuCun.ToString();
            
            if (models.Count == 1)
            {
                lblGoodAreaNumber.Text = models[0].GoodAreaNumber;
                //名称\小类\规格
                lblGoodInfo.Text = models[0].GoodName + @"\" + models[0].GoodTypeSmName + @"\" + models[0].GoodSpec;
            }
            //显示当前库存：XX,采库需 出: YY ，滞留库存：XX - YY

            DateTime fromDate=string.IsNullOrEmpty(txtFrom.Text)?Convert.ToDateTime("2000-1-1"):Convert.ToDateTime(txtFrom.Text);
            DateTime toDate = string.IsNullOrEmpty(txtTo.Text) ? Convert.ToDateTime("2036-1-1") : Convert.ToDateTime(txtTo.Text);

            List<Pro_JSXDetailInfo> pOOrderList = this.jxcDetailSer.GetListArray(Convert.ToInt32(ddlHouse.SelectedValue), goodId, fromDate, toDate);
            pOOrderList.Sort(delegate(Pro_JSXDetailInfo a, Pro_JSXDetailInfo b) { return a.RuTime.CompareTo(b.RuTime); });

            jxcDetailSer.ReSetPro_JSXDetailInfo(pOOrderList);
            decimal iniNum = 0;
            for (int i = 0; i < pOOrderList.Count; i++)
            {
                var model = pOOrderList[i];
                if (i == 0)
                {
                    iniNum = model.GoodInNum - model.GoodOutNum;
                    //i++;
                }
                else
                {
                    iniNum = iniNum + model.GoodInNum - model.GoodOutNum;
                }
                pOOrderList[i].GoodResultNum = iniNum;
            }

            


            foreach (var m in pOOrderList)
            {
                m.KuCunTotal = m.GoodResultNum * m.TempHousePrice;
            }
            lblHadInvoice.Text = string.Format("{0:n2}", pOOrderList.Sum(t=>t.HadInvoice));
            lblNoInvoice.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.NoInvoice));

            lblGoodInNum.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.GoodInNum));
            lbllblGoodInNumTotal.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.GoodInNum*t.Price));
            lblGoodOutNum.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.GoodOutNum));
            lblGoodOutNumTotal.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.GoodOutTotal));
            if (pOOrderList.Count > 0)
            {
                var lastModel = pOOrderList[pOOrderList.Count - 1];
                lblGoodResultNum.Text = string.Format("{0:n2}", lastModel.GoodResultNum);
                LBLHouseTotal.Text = string.Format("{0:n2}", lastModel.KuCunTotal);
            }
            else
            {
                lblGoodResultNum.Text = "0";
            }
          


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



        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                Pro_JSXDetailInfo model = e.Row.DataItem as Pro_JSXDetailInfo;
                if (txtProNO.Text.Trim()==""&& txtPONO.Text.Trim()!=""&&model.PONO.Contains(txtPONO.Text.Trim()))
                {                    
                    e.Row.BackColor = System.Drawing.Color.YellowGreen;
                }
                if (txtProNO.Text.Trim() != "" && txtPONO.Text.Trim() == "" && model.ProNo.Contains(txtProNO.Text.Trim()))
                {
                    e.Row.BackColor = System.Drawing.Color.Pink;
                }

                if (txtProNO.Text.Trim() != "" && txtPONO.Text.Trim() != "" && model.ProNo.Contains(txtProNO.Text.Trim()) && model.PONO.Contains(txtPONO.Text.Trim()))
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }
            }
        }



    }
}
