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
using Newtonsoft.Json;

namespace VAN_OA.EFrom
{
    public partial class QPInvDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var requestString = HttpUtility.UrlDecode(CommHelp.DES3DecryptUtf8(Request["m"],"123"), System.Text.Encoding.GetEncoding("utf-8"));
                
                //var requestString = CommHelp.DES3Decrypt(HttpUtility.UrlDecode(Request["m"]), "AAA");

                if (Request["index"] != null)
                {
                    int index = Convert.ToInt32(Request["index"]);
                    if (Session["DataInvDetails"] != null)
                    {                       
                        List<tb_QuotePrice_InvDetails> invDetails = Session["DataInvDetails"] as List<tb_QuotePrice_InvDetails>;
                        tb_QuotePrice_InvDetails m = invDetails[index];

                        //var m = JsonConvert.DeserializeObject<tb_QuotePrice_InvDetails>(requestString);
                        setValue(m);
                        ViewState["m"] = m;
                    }
                    
                }
            }
        }

        private void setValue(tb_QuotePrice_InvDetails model)
        {          
            txtCostPrice.Text = Convert.ToDecimal(model.InvPrice).ToString();           
            txtInvName.Text = model.InvName;
            txtNum.Text = model.InvNum.ToString();          
            txtUnit.Text = model.InvUnit;           
            txtCostTotal.Text = string.Format("{0:n2}", model.InvNum * model.InvPrice);          
            lblSpec.Text = model.InvModel;  
            txtGoodNo.Text = model.GoodNo;
            lblGoodSmTypeName.Text = model.GoodTypeSmName;
            txtProduct.Text = model.Product;
            txtGoodBrand.Text = model.GoodBrand;
            txtRemark.Text = model.InvRemark;
        }
        private void clear()
        { 
            txtUnit.Text = "";           
            txtNum.Text = "";
            txtInvName.Text = "";
            txtCostPrice.Text = "";            
            txtModel.Text = "";
            txtCostTotal.Text = "";
            lblSpec.Text = "";
            txtRemark.Text = "";
        }
        Dal.BaseInfo.TB_GoodService goodsSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNum.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                    return;
                }
               
            }
            if (txtCostPrice.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtCostPrice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('单价 格式错误！');</script>");
                    return;
                }

            }
            //int goodId = goodsSer.GetGoodId(txtInvName.Text, lblSpec.Text, txtModel.Text,lblGoodSmTypeName.Text,txtUnit.Text);
            //if (goodId == 0)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的商品不存在！');</script>");

            //    return;
            //}
            try
            {
               
                Convert.ToDecimal(txtCostPrice.Text);
            }
            catch (Exception)
            {
                
                   base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的单价格式有误！');</script>");
                   return;
            }
           //var good= goodsSer.GetModel(goodId);
            tb_QuotePrice_InvDetails s = new tb_QuotePrice_InvDetails();
            s.InvUnit = txtUnit.Text;
            s.InvPrice = Convert.ToDecimal(txtCostPrice.Text);
            s.InvGoodId = 0;
            s.InvName = txtInvName.Text;
            s.InvNum = Convert.ToDecimal(txtNum.Text);
            s.InvModel = lblSpec.Text;
            s.GoodBrand =txtGoodBrand.Text;
            s.Total = s.InvNum * s.InvPrice;
            s.Product =txtProduct.Text;
            s.GoodNo = txtGoodNo.Text;
            s.GoodTypeSmName = lblGoodSmTypeName.Text;
            s.InvRemark = txtRemark.Text;
            //修改
            if (Request["index"] != null)
            {
                int index = Convert.ToInt32(Request["index"]);

                tb_QuotePrice_InvDetails model = ViewState["m"] as tb_QuotePrice_InvDetails;
                s.Id = model.Id;
                s.IfUpdate = true;
                s.UpdateIndex = Convert.ToInt32(Request["index"]);
                Session["m"] = s;

                
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");
            }
            else
            {

                if (Session["InvDetails"] == null)
                {
                    List<tb_QuotePrice_InvDetails> invDetails = new List<tb_QuotePrice_InvDetails>();
                    invDetails.Insert(invDetails.Count, s);
                    Session["InvDetails"] = invDetails;
                }
                else
                {                   
                    List<tb_QuotePrice_InvDetails> invDetails = Session["InvDetails"] as List<tb_QuotePrice_InvDetails>;
                    invDetails.Insert(invDetails.Count, s);
                    Session["InvDetails"] = invDetails;
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

           
        }

        protected void txtSellTotal_TextChanged(object sender, EventArgs e)
        {

          
        }

        protected void txtSellPrice_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
