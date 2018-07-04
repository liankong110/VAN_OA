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
    public partial class WFContractList : BasePage
    {

        ContractService contractSer = new ContractService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFContract.aspx");
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
            string sql = "1=1";
            if (!string.IsNullOrEmpty(txtContract_No.Text.Trim()))
            {
                sql += string.Format(" and Contract_No like '%{0}%'", txtContract_No.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtContract_Unit.Text.Trim()))
            {
                sql += string.Format(" and Contract_Unit like '%{0}%'", txtContract_Unit.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtContract_Name.Text.Trim()))
            {
                sql += string.Format(" and Contract_Name like '%{0}%'", txtContract_Name.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtContract_Summary.Text.Trim()))
            {
                sql += string.Format(" and Contract_Summary like '%{0}%'", txtContract_Summary.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPONo.Text.Trim()))
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtContract_ProNo.Text))
            {
                sql += string.Format(" and Contract_ProNo like '%{0}%'", txtContract_ProNo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtContract_Remark.Text))
            {
                sql += string.Format(" and Contract_Remark like '%{0}%'", txtContract_Remark.Text.Trim());
            }
            if (ddlContract_Type.Text!="-1")
            {
                sql += string.Format(" and Contract_Type={0}", ddlContract_Type.Text);
            }
            if (ddlContract_Use.Text != "-1")
            {
                sql += string.Format(" and Contract_Use='{0}'", ddlContract_Use.Text);
            }
            if (!string.IsNullOrEmpty(txtContract_Total.Text))
            {
                if (CommHelp.VerifesToNum(txtContract_Total.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('总金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Contract_Total{0}{1}",ddlContract_Total.Text, txtContract_Total.Text);
            }
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签订时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Contract_Date>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签订时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Contract_Date<='{0} 23:59:59'", txtTo.Text);
            }

            if (!string.IsNullOrEmpty(txtContract_PageCount.Text))
            {
                if (CommHelp.VerifesToNum(txtContract_PageCount.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('合同总页数 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Contract_PageCount{0}{1}", ddlContract_PageCount.Text, txtContract_PageCount.Text);
            }
            if (!string.IsNullOrEmpty(txtContract_AllCount.Text))
            {
                if (CommHelp.VerifesToNum(txtContract_AllCount.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('合同总份数 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Contract_AllCount{0}{1}", ddlContract_AllCount.Text, txtContract_AllCount.Text);
            }
            if (!string.IsNullOrEmpty(txtContract_BCount.Text))
            {
                if (CommHelp.VerifesToNum(txtContract_BCount.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('己方份数 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Contract_BCount{0}{1}", ddlContract_BCount.Text, txtContract_BCount.Text);
            }
            if (ddlAE.Text != "全部")
            {
                sql += string.Format(" and AE='{0}'", ddlAE.Text);
            }
            if (ddlContract_Brokerage.Text != "全部")
            {
                sql += string.Format(" and Contract_Brokerage='{0}'", ddlContract_Brokerage.Text);
            }
            if (ddlContract_IsSign.Text != "-1")
            {
                sql += string.Format(" and Contract_IsSign={0}", ddlContract_IsSign.Text);
            }
            if (ddlContract_Local.Text != "-1")
            {
                sql += string.Format(" and Contract_Local='{0}'", ddlContract_Local.Text);
            }
            if (ddlContract_Year.Text != "-1")
            {
                sql += string.Format(" and Contract_Year={0}", ddlContract_Year.Text);
            }
            if (ddlContract_Month.Text != "-1")
            {
                sql += string.Format(" and Contract_Month={0}", ddlContract_Month.Text);
            }
            if (ddlContract_IsRequire.Text != "-1")
            {
                sql += string.Format(" and Contract_IsRequire={0}", ddlContract_IsRequire.Text);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where Model='{0}' AND PONO=Contract.PONO) ", ddlModel.Text);
            }

            List<Contract> caiList = this.contractSer.GetListArray(sql);
            lblTotal.Text = caiList.Sum(t => t.Contract_Total).ToString();
            AspNetPager1.RecordCount = caiList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = caiList;
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
            this.contractSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFContract.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                if (!NewShowAll_textName("合同档案管理", "编辑"))
                {
                    gvList.Columns[0].Visible = false;
                }
                if (!NewShowAll_textName("合同档案管理", "删除"))
                {
                    gvList.Columns[1].Visible = false;
                }

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                user = userSer.getAllUserByLoginName("");
                user.Insert(0, new Model.User { LoginName="全部" });
                ddlContract_Brokerage.DataSource = user;
                ddlContract_Brokerage.DataBind();
                ddlContract_Brokerage.DataTextField = "LoginName";
                ddlContract_Brokerage.DataValueField = "LoginName";

                var aeList = userSer.getAllUserByPOList("");
                aeList.Insert(0, new Model.User { LoginName = "全部" });
                ddlAE.DataSource = aeList;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "LoginName";

                List<Contract> contractList = new List<Contract>();
                this.gvList.DataSource = contractList;
                this.gvList.DataBind();

                if (Request["Contract_No"] != null)
                {
                    txtContract_No.Text = Request["Contract_No"].ToString();
                    Show();
                }
            }
        }
    }
}
