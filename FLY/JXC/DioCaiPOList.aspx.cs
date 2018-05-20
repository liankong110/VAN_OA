using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;

using VAN_OA.Model.ReportForms;
using VAN_OA.Model.JXC;


namespace VAN_OA.JXC
{
    public partial class DioCaiPOList : BasePage
    {
        private CAI_POCaiService POSer = new CAI_POCaiService();


        private void Show()
        {
            string sql = " 1=1 and lastSupplier is not null ";



            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (ddlBusType.Text == "0")
            {
                sql += string.Format(" and lastSupplier<>'库存'");
                gvList.Columns[0].Visible = true;
            }

            if (ddlBusType.Text == "1")
            {
                sql += string.Format(" and lastSupplier='库存'");
                gvList.Columns[0].Visible = false;
            }
            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and AE='{0}'",ddlUser.SelectedItem.Text);
            }
            if (!string.IsNullOrEmpty(txtSupplier.Text))
            {
                sql += string.Format(" and LastSupplier like '%{0}%'", txtSupplier.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtGoodNo1.Text))
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo1.Text);
            }
            if (!string.IsNullOrEmpty(txtNameOrTypeOrSpec1.Text))
            {
                sql += string.Format(" and (GoodSpec like '%{0}%' or GoodName like '%{0}%' or GoodTypeSmName like '%{0}%')", txtNameOrTypeOrSpec1.Text);
            }
            if (!string.IsNullOrEmpty(txtGuestName.Text))
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text);
            }
            if (!string.IsNullOrEmpty(txtCaiProNo.Text))
            {
                sql += string.Format(" and ProNo like '%{0}%'", txtCaiProNo.Text);
            }
            List<CAI_POCaiView> cars = this.POSer.GetListViewCai_POOrders_Cai_POOrderChecks_View(sql);
            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
            this.gvList.DataBind();
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
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var model=e.Row.DataItem as CAI_POCaiView;
                if (model != null && model.Supplier=="库存")
                {
                    CheckBox cb = (e.Row.FindControl("chkSelect")) as CheckBox;
                    cb.Visible = false;
                }

                //CheckBox cb = (e.Row.FindControl("chkSelect")) as CheckBox;
                //if (ddlBusType.Text == "0")
                //{
                //    cb.Enabled = true;
                //}
                //else
                //{
                //    cb.Enabled = false;
                //}
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["CGPOList"] = null;
                List<CAI_POCaiView> cars = new List<CAI_POCaiView>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                user = userSer.getAllUserByLoginName("");
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });

                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {



        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                Session["CGPONo"] = e.CommandArgument;
                Response.Write("<script>window.close();window.opener=null;</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Model.JXC.CAI_POCaiView> modelList = new List<CAI_POCaiView>();
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("chkSelect")) as CheckBox;
                if (cb.Checked == true)
                {
                    Model.JXC.CAI_POCaiView model = new CAI_POCaiView();

                    Label ProNo = (gvList.Rows[i].FindControl("ProNo")) as Label;
                    model.ProNo = ProNo.Text;


                    Label PONo = (gvList.Rows[i].FindControl("PONo")) as Label;
                    model.PONo = PONo.Text;


                    Label POName = (gvList.Rows[i].FindControl("POName")) as Label;
                    model.POName = POName.Text;

                    Label GuestName = (gvList.Rows[i].FindControl("GuestName")) as Label;
                    model.GuestName = GuestName.Text;

                    Label GuestNo = (gvList.Rows[i].FindControl("GuestNo")) as Label;
                    model.GuestNo = GuestNo.Text;

                    Label AE = (gvList.Rows[i].FindControl("AE")) as Label;
                    model.AE = AE.Text;

                    Label INSIDE = (gvList.Rows[i].FindControl("INSIDE")) as Label;
                    model.INSIDE = INSIDE.Text;

                    Label Supplier = (gvList.Rows[i].FindControl("Supplier")) as Label;
                    model.Supplier = Supplier.Text;

                    Label GoodName = (gvList.Rows[i].FindControl("GoodName")) as Label;
                    model.GoodName = GoodName.Text;

                    Label GoodTypeSmName = (gvList.Rows[i].FindControl("GoodTypeSmName")) as Label;
                    model.GoodTypeSmName = GoodTypeSmName.Text;

                    Label GoodSpec = (gvList.Rows[i].FindControl("GoodSpec")) as Label;
                    model.GoodSpec = GoodSpec.Text;

                    Label Good_Model = (gvList.Rows[i].FindControl("Good_Model")) as Label;
                    model.Good_Model = Good_Model.Text;

                    Label GoodUnit = (gvList.Rows[i].FindControl("GoodUnit")) as Label;
                    model.GoodUnit = GoodUnit.Text;

                    Label lblNum = (gvList.Rows[i].FindControl("ResultNum")) as Label;

                    if (lblNum.Text != "")
                        model.Num = Convert.ToDecimal(lblNum.Text);


                    Label Price = (gvList.Rows[i].FindControl("Price")) as Label;
                    if (Price.Text != "")
                        model.Price = Convert.ToDecimal(Price.Text);

                    Label LastTruePrice = (gvList.Rows[i].FindControl("LastTruePrice")) as Label;
                    if (LastTruePrice.Text != "")
                        model.LastTruePrice = Convert.ToDecimal(LastTruePrice.Text);


                    Label Total = (gvList.Rows[i].FindControl("Total")) as Label;

                    if (Total.Text != "")
                        model.Total = Convert.ToDecimal(Total.Text);


                    Label GoodId = (gvList.Rows[i].FindControl("GoodId")) as Label;

                    if (GoodId.Text != "")
                        model.GoodId = Convert.ToInt32(GoodId.Text);

                    Label GoodAreaNumber = (gvList.Rows[i].FindControl("GoodAreaNumber")) as Label;

                    if (GoodAreaNumber.Text != "")
                        model.GoodAreaNumber = GoodAreaNumber.Text;


                    
                    Label POCaiID = (gvList.Rows[i].FindControl("POCaiID")) as Label;

                    if (POCaiID.Text != "")
                        model.POCaiId = Convert.ToInt32(POCaiID.Text);


                    Label CaiGou = (gvList.Rows[i].FindControl("CaiGou")) as Label;

                    if (CaiGou.Text != "")
                        model.CaiGou = CaiGou.Text;

                    Label loginName = (gvList.Rows[i].FindControl("loginName")) as Label;

                    if (loginName.Text != "")
                        model.loginName = loginName.Text;

                    Label GoodNo = (gvList.Rows[i].FindControl("GoodNo")) as Label;

                    if (GoodNo.Text != "")
                        model.GoodNo = GoodNo.Text;



                    modelList.Add(model);

                }

                if (modelList.Count > 0)
                    Session["CGPOList"] = modelList;

                Response.Write("<script>window.close();window.opener=null;</script>");

            }
        }

        protected void ddlBusType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}