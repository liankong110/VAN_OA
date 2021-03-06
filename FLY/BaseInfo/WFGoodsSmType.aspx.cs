﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

 
 
namespace VAN_OA.BaseInfo
{
    public partial class WFGoodsSmType : System.Web.UI.Page
    {
        private TB_GoodsSmTypeService invSer = new TB_GoodsSmTypeService();


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_GoodsSmType where GoodTypeSmName='{0}'", txtInvName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('小类[{0}],已经存在！');</script>", txtInvName.Text));
                        return;
                    }
                    TB_GoodsSmType per = getModel();
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


        public TB_GoodsSmType getModel()
        {
            string InvName = this.txtInvName.Text;
            VAN_OA.Model.BaseInfo.TB_GoodsSmType model = new VAN_OA.Model.BaseInfo.TB_GoodsSmType();
            model.GoodTypeSmName = InvName;
            model.GoodTypeName = ddlGoodType.SelectedItem.Value;
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
                    string sqlCheck = string.Format("select count(*) from TB_GoodsSmType where GoodTypeSmName='{0}' and Id<>{1}", txtInvName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('小类[{0}],已经存在！');</script>", txtInvName.Text));
                        return;
                    }

                    TB_GoodsSmType per = getModel();
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


            if (this.ddlGoodType.Text.Trim().Length == 0)
            {
                strErr = "请选择商品类别！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.ddlGoodType.Focus();
                return false;
            }

            if (this.txtInvName.Text.Trim().Length == 0)
            {
                strErr = "商品小类不能为空！\\n";
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
                TB_GoodsTypeService typeSer=new TB_GoodsTypeService ();
                List<TB_GoodsType> allType = typeSer.GetListArray("");
                ddlGoodType.DataSource = allType;
                ddlGoodType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeName";
                ddlGoodType.DataValueField = "GoodTypeName";

                if (base.Request["Id"] != null)
                {


                    this.btnAdd.Visible = false;
                    TB_GoodsSmType model = this.invSer.GetModel(Convert.ToInt32(base.Request["Id"]));


                    this.txtInvName.Text = model.GoodTypeSmName;
                    if (model.GoodTypeName != null)
                        ddlGoodType.SelectedValue = model.GoodTypeName.ToString();
                }
                else
                {

                    
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
