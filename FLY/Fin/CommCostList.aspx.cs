using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.Fin;
using VAN_OA.Dal.JXC;



namespace VAN_OA.Fin
{
    public partial class CommCostList :BasePage
    {
        protected List<FIN_Property> propertyList = new List<FIN_Property>();
        FIN_PropertyService FIN_PropertyService = new FIN_PropertyService();
        protected void btnAdd_Click(object sender, EventArgs e)
        { 
            
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Show();
            btnSave.Enabled = true;
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        private void Show()
        {
            var comModel = new Dal.BaseInfo.TB_CompanyService().GetModel(Convert.ToInt32(ddlCompany.Text));
            lblCompanyName.Text = comModel.ComName;
            lblSimpName.Text = comModel.ComSimpName;
            lblDate.Text = ddlYear.Text + ddlMonth.Text;
            propertyList = this.FIN_PropertyService.GetListArray_CommCost(ddlYear.Text + ddlMonth.Text, ddlCompany.Text);
            string poName = string.Format("{0}年{1}行政经费", ddlYear.Text, comModel.ComSimpName);
            //显示系统值
            #region 邮寄费
            var sql = string.Format(@"---项目中预期报销单的邮寄费合计
select isnull(SUM(Tb_DispatchList.PostTotal),0) from tb_Post left join Tb_DispatchList ON tb_Post.ID=Tb_DispatchList.Post_Id where  AppName in (select id from tb_User where CompanyCode='{0}') and tb_Post.poName='{1}'
and tb_Post.id in (select allE_id from tb_EForm where state='通过' and proid in (select pro_Id from A_ProInfo where pro_Type='邮寄文档快递表') )", comModel.ComCode, poName);

            var DispatchList = DBHelp.ExeScalar(sql);
            if (ddlMonth.Text != "12")
            {
                sql = string.Format(@"---项目中预期报销单的邮寄费合计
select isnull(SUM(Tb_DispatchList.PostTotal),0) from tb_Post left join Tb_DispatchList ON tb_Post.ID=Tb_DispatchList.Post_Id  where  year(AppTime)={0} 
and MONTH(AppTime)={1} and AppName in (select id from tb_User where CompanyCode='{2}') and tb_Post.poName='{3}'
and tb_Post.id in (select allE_id from tb_EForm where state='通过' and proid in (select pro_Id from A_ProInfo where pro_Type='邮寄文档快递表') )", ddlYear.Text, ddlMonth.Text, comModel.ComCode, poName);
            }
            else
            {
                sql = string.Format(@"---项目中预期报销单的邮寄费合计
select isnull(SUM(Tb_DispatchList.PostTotal),0) from tb_Post left join Tb_DispatchList ON tb_Post.ID=Tb_DispatchList.Post_Id  where   AppTime>='{0} 00:00:00'
 and AppName in (select id from tb_User where CompanyCode='{1}') and tb_Post.poName='{2}'
and tb_Post.id in (select allE_id from tb_EForm where state='通过' and proid in (select pro_Id from A_ProInfo where pro_Type='邮寄文档快递表') )", ddlYear.Text + "-12-1", comModel.ComCode, poName);

            }
            var DispatchList_Month = DBHelp.ExeScalar(sql); 
            #endregion

            #region 办公用品费
            sql = string.Format(@"--小类办公用品费 中的所有合计
select isnull(sum(goodTotal),0)  FROM JXC_REPORT 
 left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 and CG_POOrder.Status='通过' 
 where GoodTypeSmName ='办公用品费' and GoodTypeName ='杂费' 
and CG_POOrder.AppName in (select id from tb_User where CompanyCode='{0}') and CG_POOrder.poName='{1}'", comModel.ComCode, poName);
            var JXCREPORT = DBHelp.ExeScalar(sql);

            if (ddlMonth.Text != "12")
            {
                sql = string.Format(@"--小类办公用品费 中的所有合计
select isnull(sum(goodTotal),0)  FROM JXC_REPORT 
 left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 and CG_POOrder.Status='通过' 
 where GoodTypeSmName ='办公用品费' and GoodTypeName ='杂费' and year(RuTime)={0} and MONTH(RuTime)={1} 
and CG_POOrder.AppName in (select id from tb_User where CompanyCode='{2}') and CG_POOrder.poName='{3}'", ddlYear.Text, ddlMonth.Text, comModel.ComCode, poName);
            }
            else
            {
                sql = string.Format(@"--小类办公用品费 中的所有合计
select isnull(sum(goodTotal),0)  FROM JXC_REPORT 
 left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 and CG_POOrder.Status='通过' 
 where GoodTypeSmName ='办公用品费' and GoodTypeName ='杂费' and RuTime>='{0} 00:00:00'
and CG_POOrder.AppName in (select id from tb_User where CompanyCode='{1}') and CG_POOrder.poName='{2}'", ddlYear.Text + "-12-1", comModel.ComCode, poName);
            }
            var JXCREPORT_Month = DBHelp.ExeScalar(sql); 
            #endregion


            sql = string.Format(@"--项目中除去黄色框的内容的其他成本合计（该项目销售明细报表中除去黄色框的其他总成本+总损失差额）
select isnull(sum(goodTotal),0) as totoal
 FROM JXC_REPORT  left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 
 and CG_POOrder.Status='通过' where  CG_POOrder.AppName in (select id from tb_User where CompanyCode='{0}') and CG_POOrder.poName='{1}'
",  comModel.ComCode,poName);
            var JXCREPORT_Cha = (decimal)DBHelp.ExeScalar(sql);

            if (ddlMonth.Text != "12")
            {
                sql = string.Format(@"--行政经费的总额 扣除如下 黄色部分的总额 
select isnull(sum(goodTotal),0) as totoal
 FROM JXC_REPORT  left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 
 and CG_POOrder.Status='通过' where  year(RuTime)={0} and MONTH(RuTime)<={1} 
 and CG_POOrder.AppName in (select id from tb_User where CompanyCode='{2}') and CG_POOrder.poName='{3}'
", ddlYear.Text, ddlMonth.Text, comModel.ComCode, poName);
            }
            else
            {
                sql = string.Format(@"--行政经费的总额 扣除如下 黄色部分的总额 
select isnull(sum(goodTotal),0) as totoal
 FROM JXC_REPORT  left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 
 and CG_POOrder.Status='通过' where  RuTime>='{0} 00:00:00'
 and CG_POOrder.AppName in (select id from tb_User where CompanyCode='{1}') and CG_POOrder.poName='{2}'
", ddlYear.Text + "-12-1", comModel.ComCode, poName);
            }
            var JXCREPORT_Cha_Month = (decimal)DBHelp.ExeScalar(sql);

            Sell_OrderOutHouseService sellOutSer = new Sell_OrderOutHouseService();
            string where = string.Format(" and Sell_OrderOutHouse.CreateUserId in (select id from tb_User where CompanyCode='{0}') and poName='{1}'", comModel.ComCode, poName);

            Dictionary<string, decimal> GetAllTotal = sellOutSer.GetAllTotal_ChengBen(where);

            if (ddlMonth.Text != "12")
            {
                where = string.Format(" and year(RuTime)={0} and MONTH(RuTime)={1} and Sell_OrderOutHouse.CreateUserId in (select id from tb_User where CompanyCode='{2}') and poName='{3}'", ddlYear.Text, ddlMonth.Text, comModel.ComCode, poName);
            }
            else
            {
                where = string.Format(" and RuTime>='{0} 00:00:00' and Sell_OrderOutHouse.CreateUserId in (select id from tb_User where CompanyCode='{1}') and poName='{2}'",
                    ddlYear.Text+"-12-1", comModel.ComCode, poName);
            }
            Dictionary<string, decimal> GetAllTotal_Month = sellOutSer.GetAllTotal_ChengBen(where);
            
            foreach (var p in propertyList)
            {
                if (p.CostType == "均摊邮费")
                {
                    p.XiShu_Value = Convert.ToDecimal(DispatchList);
                    p.CurrentMonth_Value = Convert.ToDecimal(DispatchList_Month);
                }
                if (p.CostType == "办公用品费")
                {
                    p.XiShu_Value = Convert.ToDecimal(JXCREPORT);
                    p.CurrentMonth_Value = Convert.ToDecimal(JXCREPORT_Month);
                }
                #region 系统值

                if (p.CostType == "资质费用" && GetAllTotal.ContainsKey("17110"))
                {
                    p.XiShu_Value = GetAllTotal["17110"];
                }
                else if (p.CostType == "各类证照" && GetAllTotal.ContainsKey("17112"))
                {
                    p.XiShu_Value = GetAllTotal["17112"];
                }
                else if (p.CostType == "汽车维修费" && GetAllTotal.ContainsKey("16616"))
                {
                    p.XiShu_Value = GetAllTotal["16616"];
                }
                else if (p.CostType == "汽车保险" && GetAllTotal.ContainsKey("17109"))
                {
                    p.XiShu_Value = GetAllTotal["17109"];
                }
                else if (p.CostType == "公务汽油费" && GetAllTotal.ContainsKey("14346"))
                {
                    p.XiShu_Value = GetAllTotal["14346"];
                }
                else if (p.CostType == "律师费" && GetAllTotal.ContainsKey("17111"))
                {
                    p.XiShu_Value = GetAllTotal["17111"];
                }
                else if (p.CostType == "内部装修改建费" && GetAllTotal.ContainsKey("14350"))
                {
                    p.XiShu_Value = GetAllTotal["14350"];
                }
                else if (p.CostType == "软件开发费" && GetAllTotal.ContainsKey("14396"))
                {
                    p.XiShu_Value = GetAllTotal["14396"];
                }
                else if (p.CostType == "公务费" && GetAllTotal.ContainsKey("14423"))
                {
                    p.XiShu_Value = GetAllTotal["14423"];
                }
                else if (p.CostType == "交通费" && GetAllTotal.ContainsKey("15317"))
                {
                    p.XiShu_Value = GetAllTotal["15317"];
                }
                else if (p.CostType == "水费" && GetAllTotal.ContainsKey("14348"))
                {
                    p.XiShu_Value = GetAllTotal["14348"];
                }
                else if (p.CostType == "电费" && GetAllTotal.ContainsKey("14349"))
                {
                    p.XiShu_Value = GetAllTotal["14349"];
                }
                else if (p.CostType == "物业管理费" && GetAllTotal.ContainsKey("17108"))
                {
                    p.XiShu_Value = GetAllTotal["17108"];
                }
                else if (p.CostType == "设备投资费" && GetAllTotal.ContainsKey("17158"))
                {
                    p.XiShu_Value = GetAllTotal["17158"];
                }
                else if (p.CostType == "电信费用" && GetAllTotal.ContainsKey("17167"))
                {
                    p.XiShu_Value = GetAllTotal["17167"];                   
                } 
                #endregion

                #region 当月值

                if (p.CostType == "资质费用" && GetAllTotal_Month.ContainsKey("17110"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["17110"];
                }
                else if (p.CostType == "各类证照" && GetAllTotal_Month.ContainsKey("17112"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["17112"];
                }
                else if (p.CostType == "汽车维修费" && GetAllTotal_Month.ContainsKey("16616"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["16616"];
                }
                else if (p.CostType == "汽车保险" && GetAllTotal_Month.ContainsKey("17109"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["17109"];
                }
                else if (p.CostType == "公务汽油费" && GetAllTotal_Month.ContainsKey("14346"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["14346"];
                }
                else if (p.CostType == "律师费" && GetAllTotal_Month.ContainsKey("17111"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["17111"];
                }
                else if (p.CostType == "内部装修改建费" && GetAllTotal_Month.ContainsKey("14350"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["14350"];
                }
                else if (p.CostType == "软件开发费" && GetAllTotal_Month.ContainsKey("14396"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["14396"];
                }
                else if (p.CostType == "公务费" && GetAllTotal_Month.ContainsKey("14423"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["14423"];
                }
                else if (p.CostType == "交通费" && GetAllTotal_Month.ContainsKey("15317"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["15317"];
                }
                else if (p.CostType == "水费" && GetAllTotal_Month.ContainsKey("14348"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["14348"];
                }
                else if (p.CostType == "电费" && GetAllTotal_Month.ContainsKey("14349"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["14349"];
                }
                else if (p.CostType == "物业管理费" && GetAllTotal_Month.ContainsKey("17108"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["17108"];
                }
                else if (p.CostType == "设备投资费" && GetAllTotal_Month.ContainsKey("17158"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["17158"];
                }
                else if (p.CostType == "电信费用" && GetAllTotal_Month.ContainsKey("17167"))
                {
                    p.CurrentMonth_Value = GetAllTotal_Month["17167"];                  
                }
                #endregion

                #region 商品编码

                if (p.CostType == "资质费用")
                {
                    p.Code_Value = "17110";
                }
                else if (p.CostType == "各类证照")
                {                  
                    p.Code_Value = "17112";
                }
                else if (p.CostType == "汽车维修费")
                {                  
                    p.Code_Value = "16616";
                }
                else if (p.CostType == "汽车保险")
                {                  
                    p.Code_Value = "17109";
                }
                else if (p.CostType == "公务汽油费")
                {                  
                    p.Code_Value = "14346";
                }
                else if (p.CostType == "律师费")
                {                  
                    p.Code_Value = "17111";
                }
                else if (p.CostType == "内部装修改建费")
                {                  
                    p.Code_Value = "14350";
                }
                else if (p.CostType == "软件开发费")
                {                  
                    p.Code_Value = "14396";
                }
                else if (p.CostType == "公务费")
                {                  
                    p.Code_Value = "14423";
                }
                else if (p.CostType == "交通费")
                {                  
                    p.Code_Value = "15317";
                }
                else if (p.CostType == "水费")
                {                  
                    p.Code_Value = "14348";
                }
                else if (p.CostType == "电费")
                {                  
                    p.Code_Value = "14349";
                }
                else if (p.CostType == "物业管理费")
                {                  
                    p.Code_Value = "17108";
                }
                else if (p.CostType == "设备投资费")
                {                   
                    p.Code_Value = "17158";
                }
                else if (p.CostType == "电信费用")
                {
                    p.Code_Value = "17167";
                }
                #endregion
            }
            var other_M = propertyList.Find(t => t.CostType == "均摊杂费");
            if (other_M != null)
            {
                var others =JXCREPORT_Cha- propertyList.Sum(t => t.XiShu_Value) ;
                other_M.XiShu_Value = others;
                other_M.CurrentMonth_Value = JXCREPORT_Cha_Month-propertyList.Sum(t => t.CurrentMonth_Value);
            }
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                for (int i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
                TB_CompanyService comSer = new TB_CompanyService();

                var comList = comSer.GetListArray("");
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='公共费用-可修改保存'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "公共费用-可修改保存")==false)
                {
                    btnSave.Visible = false;                    
                }


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           var propertyList = this.FIN_PropertyService.GetListArray(" MyProperty='公共'");
           List<FIN_CommCost> commCostList = new List<FIN_CommCost>();
           foreach (var m in propertyList)
           {
               if (!string.IsNullOrEmpty(Request["pro_" + m.Id]))
               {
                   var newM = new FIN_CommCost();
                   newM.CaiYear = ddlYear.Text + ddlMonth.Text;
                   newM.CompId = Convert.ToInt32(ddlCompany.Text);
                   newM.CostTypeId = m.Id;

                    if (CommHelp.VerifesToNum(Request["pro_" + m.Id].ToString()) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数值 格式错误！');</script>");
                        return;
                    }

                    newM.Total = Convert.ToDecimal(Request["pro_" + m.Id]);
                   commCostList.Add(newM);
               }
           }
            FIN_CommCostService commSer=new FIN_CommCostService ();
            commSer.AddList(commCostList);

            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

        }
    }
}
