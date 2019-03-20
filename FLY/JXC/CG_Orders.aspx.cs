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
using VAN_OA.Model.EFrom;
using System.Collections.Generic;
using VAN_OA.Model.JXC;
using Newtonsoft.Json;

namespace VAN_OA.JXC
{
    public partial class CG_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtInvName.Attributes.Add("onkeydown", "if(event.keyCode==13){event.keyCode=9;}");
                if (Request["index"] != null)
                {
                    var m = JsonConvert.DeserializeObject<CG_POOrders>(HttpUtility.UrlDecode(Request["m"]));
                    setValue(m);
                    ViewState["m"] = m;
                    //int index = Convert.ToInt32(Request["index"]);
                    //if (Session["Orders"] != null)
                    //{
                    //    List<CG_POOrders> POOrders = Session["Orders"] as List<CG_POOrders>;
                    //    CG_POOrders model = POOrders[index];
                    //    setValue(model);
                    //}
                   
                }
                
            }
        }

        private void setValue(CG_POOrders model)
        {          
            txtCostPrice.Text = Convert.ToDecimal(model.CostPrice).ToString();
            //txtGuestName.Text = model.GuestName;
            txtInvName.Text = model.GoodName;
            txtNum.Text = model.Num.ToString();
            txtOtherCost.Text = model.OtherCost.ToString();
            if (model.Profit!=null)
            txtProfit.Text = string.Format("{0:n2}", model.Profit.Value);
            txtSellPrice.Text = model.SellPrice.ToString();
            //txtTime.Text = model.Time.ToShortDateString();
            if(model.ToTime!=null)
            txtToTime.Text = model.ToTime.Value.ToShortDateString();
            lblUnit.Text = model.GoodUnit;
            lblSpec.Text = model.GoodSpec;
            lblModel.Text = model.Good_Model;
            lblGoodSmTypeName.Text = model.GoodTypeSmName;

            txtCostTotal.Text = string.Format("{0:n4}", model.Num*model.CostPrice);
            txtSellTotal.Text =string.Format("{0:n4}",  model.SellPrice*model.Num);
            txtGoodNo.Text = model.GoodNo;
            txtDetailRemark.Text = model.DetailRemark;

        }
        private void clear()
        {
            lblGoodSmTypeName.Text = "";
            lblModel.Text = "";
            lblSpec.Text = "";
            lblUnit.Text = "";


            txtGoodNo.Text = "";
            txtToTime.Text = "";
            txtSellPrice.Text = "";
            txtProfit.Text = "";
            txtOtherCost.Text = "";
            txtNum.Text = "";
            txtInvName.Text = "";
            txtCostPrice.Text = "";
           // txtTime.Focus();
            txtSellTotal.Text = "";
            txtCostTotal.Text = "";
            txtDetailRemark.Text = "";
            txtInvName.Focus();
        }

        Dal.BaseInfo.TB_GoodService goodsSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNum.Text)&&CommHelp.VerifesToNum(txtNum.Text)==false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式有误！');</script>");
                return;
            }
            if (!string.IsNullOrEmpty(txtOtherCost.Text) && CommHelp.VerifesToNum(txtOtherCost.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('管理费 格式有误！');</script>");
                return;
            }
            if (!string.IsNullOrEmpty(txtSellPrice.Text) && CommHelp.VerifesToNum(txtSellPrice.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售单价 格式有误！');</script>");
                return;
            }
            if (!string.IsNullOrEmpty(txtCostPrice.Text) && CommHelp.VerifesToNum(txtCostPrice.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成本单价 格式有误！');</script>");
                return;
            }

            if (txtToTime.Text != "")
            {
                try
                {
                    Convert.ToDateTime(txtToTime.Text);
                }
                catch (Exception)
                {
                    
                      base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到帐日期格式有误！');</script>");
                   return;
                }
            }
            if (CommHelp.GetByteLen(lblModel.Text) > 20 * 2)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('型号长度不能超过20个汉字符，请修正！');</script>");
                return;
            }
            if (CommHelp.GetByteLen(txtDetailRemark.Text) > 20 * 2)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('备注长度不能超过20个汉字符，请修正！');</script>");
                return;
            }

            int goodId = goodsSer.GetGoodId(txtInvName.Text, lblSpec.Text, lblModel.Text,lblGoodSmTypeName.Text,lblUnit.Text);
            if (goodId == 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的商品不存在！');</script>");
                 
                return;
            }

            try
            {
                Convert.ToDecimal(txtSellPrice.Text);
                Convert.ToDecimal(txtCostPrice.Text);
            }
            catch (Exception)
            {
                
                   base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的单价格式有误！');</script>");
                   return;
            } 
           // var goodModel = goodsSer.GetModel(goodId);
            CG_POOrders s = new CG_POOrders();
            s.GoodNo = txtGoodNo.Text;// goodModel.GoodNo;
            s.GoodId = goodId;
            s.GoodUnit =lblUnit.Text;
            s.GoodName = txtInvName.Text;
            s.GoodSpec = lblSpec.Text;
            s.Good_Model = lblModel.Text;
            s.GoodTypeSmName = lblGoodSmTypeName.Text;
           
            s.CostPrice = Convert.ToDecimal(txtCostPrice.Text);
            s.DetailRemark = txtDetailRemark.Text;
            s.InvName = txtInvName.Text;
            s.Num = Convert.ToDecimal(txtNum.Text);
            if (txtOtherCost.Text != "")
                s.OtherCost = Convert.ToDecimal(txtOtherCost.Text);
            if (txtProfit.Text != "")
                s.Profit = Convert.ToDecimal(txtProfit.Text);

            s.SellPrice = Convert.ToDecimal(txtSellPrice.Text);
          
           
            if (txtToTime.Text != "")
            {
                s.ToTime = Convert.ToDateTime(txtToTime.Text);
            }

            s.CostTotal = s.CostPrice * s.Num;
            s.SellTotal=s.SellPrice*s.Num;
            s.YiLiTotal = s.SellTotal - s.CostTotal - s.OtherCost;
            if (s.SellTotal != 0)
                s.Profit = s.YiLiTotal / s.SellTotal * 100;
            else if (s.YiLiTotal != 0)
            {
                s.Profit = -100;
            }
            else
            {
                s.Profit = 0;
            }
                //修改
            if (Request["index"] != null)
            {
                int index = Convert.ToInt32(Request["index"]);
               
                    //List<CG_POOrders> POOrders = Session["Orders"] as List<CG_POOrders>;

                CG_POOrders model = ViewState["m"] as CG_POOrders;                    
                    s.Ids = model.Ids;
                    s.IfUpdate = true;
                    s.UpdateIndex = Convert.ToInt32(Request["index"]);
                    Session["m"] = s;
                 
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");
            }
            else
            {

                if (ViewState["Orders"] == null)
                {
                    List<CG_POOrders> POOrders = new List<CG_POOrders>();
                    POOrders.Insert(POOrders.Count, s);
                    ViewState["Orders"] = POOrders;
                }
                else
                {
                    List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                    POOrders.Insert(POOrders.Count , s);
                    ViewState["Orders"] = POOrders;
                }
                clear();
            }
           // this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request["index"] == null)
            {
                List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                Session["Orders"] = POOrders;

            }
            //else
            //{
            //    Session["m"] = ViewState["m"] as CG_POOrders;
            //}
            Response.Write("<script>window.close();window.opener=null;</script>"); 
            
        }

        protected void txtCostPrice_TextChanged(object sender, EventArgs e)
        {

            if (txtNum.Text != "" && txtCostPrice.Text != "")
            {
                try
                {
                    txtCostTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtCostPrice.Text)).ToString();

                }
                catch (Exception)
                {
                    
                    
                }
            }
            
        }

        protected void txtNum_TextChanged(object sender, EventArgs e)
        {
            if (txtNum.Text != "" && txtCostPrice.Text != "")
            {
                try
                {
                    txtCostTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtCostPrice.Text)).ToString();
                }
                catch (Exception)
                {


                }
            }

            if (txtNum.Text != "" && txtSellPrice.Text != "")
            {
                try
                {

                    txtSellTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtSellPrice.Text)).ToString();
                }
                catch (Exception)
                {


                }
            }
        }

        protected void txtSellTotal_TextChanged(object sender, EventArgs e)
        {

          
        }

        protected void txtSellPrice_TextChanged(object sender, EventArgs e)
        {
            if (txtNum.Text != "" && txtSellPrice.Text != "")
            {
                try
                {

                    txtSellTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtSellPrice.Text)).ToString();
                }
                catch (Exception)
                {


                }
            }
        }

       
        protected void txtInvName_TextChanged(object sender, EventArgs e)
        {
            if (txtInvName.Text == "")
            {
                lblGoodSmTypeName.Text = "";
                lblModel.Text = "";
                lblSpec.Text = "";
                lblUnit.Text = "";    
            }
            else
            {

              

                string goodName=txtInvName.Text.Replace(@"\",",");
                string[] allList = goodName.Split(',');

               // string[] allList=new string[2];
                if (allList.Length == 6)
                {
                    lblGoodSmTypeName.Text = allList[2];
                    lblModel.Text = allList[3];
                    lblSpec.Text = allList[4];
                    lblUnit.Text = allList[5];
                    txtInvName.Text = allList[1];
                    txtGoodNo.Text = allList[0];

                }
                //List<Model.BaseInfo.TB_Good> goodsList=goodsSer.GetListArray(" 1=1 and GoodName='{0}' and GoodSpec='{1}' and GoodModel='{2}'");

                //if (goodsList.Count > 0)
                //{

                //    lblGoodSmTypeName.Text = goodsList[0].GoodTypeSmName;
                //    lblModel.Text = goodsList[0].GoodModel;
                //    lblSpec.Text = goodsList[0].GoodSpec;
                //    lblUnit.Text = goodsList[0].GoodUnit;
                //    txtInvName.Text = goodsList[0].GoodName;
                //}
            }
        }

      
    }
}
