using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.EFrom;

namespace VAN_OA.BaseInfo
{
    public partial class GoodTemp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["GoodNo"] != null)
                {
                    var goodList=new TB_GoodService().GetListArray(string.Format(" GoodNo='{0}'", Request["GoodNo"]));
                    if (goodList.Count != 1)
                    {
                        return;
                    }
                    var goodId = goodList[0].GoodId;
                    string sql = "select pro_Id from A_ProInfo where pro_Type='商品档案申请'";

                    string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='商品档案申请')"
                        , goodId);
                    object eformIdObj = DBHelp.ExeScalar(efromId);

                    object proId = DBHelp.ExeScalar(sql);
                    if ((eformIdObj is DBNull) || eformIdObj == null)
                    {
                        sql = "select ProNo from TB_Good where GoodId=" + goodId;
                        var proNo = DBHelp.ExeScalar(sql);
                        string strProNo = "";
                        if (proNo is DBNull || proNo == null || proNo.ToString() == "")
                        {
                            strProNo = new tb_EFormService().GetAllE_No("TB_Good");
                            DBHelp.ExeCommand(string.Format(" update TB_Good set GoodProNo='{0}',GoodStatus='通过' where GoodId={1}", strProNo, goodId));
                        }
                        else
                        {
                            strProNo = proNo.ToString();
                        }
                        string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',GETDATE())", proId,
                           goodId, strProNo);
                        DBHelp.ExeCommand(insertEform);
                        efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='商品档案申请')", goodId);
                        eformIdObj = DBHelp.ExeScalar(efromId);
                    }

                    string url = "~/BaseInfo/WFGoods.aspx?ProId=" + proId + "&allE_id=" + goodId + "&EForm_Id=" + eformIdObj + "&&ReAudit=true";
                    Session["backurl1"] = "/BaseInfo/WFGoodsList.aspx";
                    Response.Redirect(url);
                }
            }
        }
    }
}