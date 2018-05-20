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
    public partial class WFGoodsType : System.Web.UI.Page
    {
        private TB_GoodsTypeService invSer = new TB_GoodsTypeService();


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_GoodsType where GoodTypeName='{0}'", txtInvName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('类别[{0}],已经存在！');</script>", txtInvName.Text));
                        return;
                    }
                    TB_GoodsType per = getModel();
                    if (this.invSer.Add(per) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        Clear();
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFGoodsTypeList.aspx");
        }

        private void Clear()
        {
            
            txtInvName.Text = "";
            
            txtInvName.Focus();
        }


        public TB_GoodsType getModel()
        {
            string InvName = this.txtInvName.Text;
            VAN_OA.Model.BaseInfo.TB_GoodsType model = new VAN_OA.Model.BaseInfo.TB_GoodsType();
            model.GoodTypeName = InvName;          

            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_GoodsType where GoodTypeName='{0}' and Id<>{1}", txtInvName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('类别[{0}],已经存在！');</script>", txtInvName.Text));
                        return;
                    }

                    TB_GoodsType per = getModel();
                    if (this.invSer.Update(per))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {

            string strErr = "";
            if (this.txtInvName.Text.Trim().Length == 0)
            {
                strErr = "商品类别不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtInvName.Focus();
                return false;
            }

            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Id"] != null)
                {


                    this.btnAdd.Visible = false;
                    TB_GoodsType model = this.invSer.GetModel(Convert.ToInt32(base.Request["Id"]));


                    this.txtInvName.Text = model.GoodTypeName;
                   
                }
                else
                {

                    
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
