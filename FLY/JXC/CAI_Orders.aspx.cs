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

namespace VAN_OA.JXC
{
    public partial class CAI_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtInvName.Attributes.Add("onkeydown", "if(event.keyCode==13){event.keyCode=9;}");  
                if (Request["index"] != null)
                {
                    int index = Convert.ToInt32(Request["index"]);
                    if (Session["Orders"] != null)
                    {
                        List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;
                        CAI_POOrders model = POOrders[index];
                        setValue(model);
                    }
                }
            }
        }

        private void setValue(CAI_POOrders model)
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

            txtCostTotal.Text = string.Format("{0:n2}", model.Num*model.CostPrice);
            txtSellTotal.Text =string.Format("{0:n2}",  model.SellPrice*model.Num);
            txtGoodNo.Text = model.GoodNo;

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
            txtInvName.Focus();
        }

        Dal.BaseInfo.TB_GoodService goodsSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();
        protected void btnSave_Click(object sender, EventArgs e)
        {

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

            var goodModel = goodsSer.GetModel(goodId);
            CAI_POOrders s = new CAI_POOrders();
            s.GoodAreaNumber = goodModel.GoodAreaNumber;
            s.GoodNo = txtGoodNo.Text;// goodModel.GoodNo;
            s.GoodId = goodId;
            s.GoodUnit =lblUnit.Text;
            s.GoodName = txtInvName.Text;
            s.GoodSpec = lblSpec.Text;
            s.Good_Model = lblModel.Text;
            s.GoodTypeSmName = lblGoodSmTypeName.Text;
           
            s.CostPrice = Convert.ToDecimal(txtCostPrice.Text);
            
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
                if (Session["Orders"] != null)
                {
                    List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;

                    CAI_POOrders model = POOrders[index];
                    CAI_POOrders newSche = s;
                    s.Ids = model.Ids;
                    s.CG_POOrdersId = model.CG_POOrdersId;

                    newSche.IfUpdate = true;
                    POOrders[index] = newSche;
                    Session["Orders"] = POOrders;

                }
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");
            }
            else
            {

                if (Session["Orders"] == null)
                {
                    List<CAI_POOrders> POOrders = new List<CAI_POOrders>();
                    POOrders.Insert(POOrders.Count, s);
                    Session["Orders"] = POOrders;
                }
                else
                {
                    List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;
                    POOrders.Insert(POOrders.Count , s);
                    Session["Orders"] = POOrders;
                }
                clear();
            }
           // this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           
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
                txtGoodNo.Text = "";
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
