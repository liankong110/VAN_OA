using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;

namespace VAN_OA
{
    /// <summary>
    /// 清除多余的查询Session
    /// </summary>
    public class WebQuerySessin : System.Web.UI.Page
    {          

       List<string> SavaAllQuerys = new List<string>();

        public WebQuerySessin()
        {
            
        }

        public WebQuerySessin(Hashtable SaveSession)
        {
            ////需要审批的列表
            //SavaAllQuerys.Add("QueryEFormToDo");

            ////所有审批的列表
            //SavaAllQuerys.Add("QueryEForms");

            ClearOtherQuerySession(SaveSession);
        }


        public WebQuerySessin(string SaveSession)
        {
            //我所申请的列表
            SavaAllQuerys.Add("QueryRequestEForms");

            //所有审批的列表
            SavaAllQuerys.Add("QueryEForms");

            //所有审批的列表
            SavaAllQuerys.Add("QueryEFormToDo");

            //所有已经审批的列表
            SavaAllQuerys.Add("QueryMyAppEForms");

            //被委托记录
            SavaAllQuerys.Add("QueryConsignoring");

            //已经被委托记录
            SavaAllQuerys.Add("QueryHadConsignors");

            //公共平台
            SavaAllQuerys.Add("QueryWFComm");

            //用车申请表-修改 
            SavaAllQuerys.Add("QueryUseCarDetail");

            //客户联系跟踪表管理 
            SavaAllQuerys.Add("QueryGuestTrack");


            //个人 --客户联系跟踪表管理 
            SavaAllQuerys.Add("QueryMyGuestTrack");

            ClearOtherQuerySession(SaveSession);
        }



        /// <summary>
        ///清除其他所有查询Session,排除SaveSession 中的数据
        /// </summary>
        /// <param name="SaveSession"></param>
        public void ClearOtherQuerySession(Hashtable SaveSession)
        {
            for (int i = 0; i < SavaAllQuerys.Count; i++)
            {
                if (SaveSession != null)
                {
                    if (!SaveSession.Contains(SavaAllQuerys[i]))
                    {
                        if (Session[SavaAllQuerys[i]] != null)
                        {
                            Session[SavaAllQuerys[i]] = null;
                        }
                    }
                }
                else
                {
                    Session[SavaAllQuerys[i]] = null;
                }
            }
        }

        /// <summary>
        ///清除其他所有查询Session,排除SaveSession 中的数据
        /// </summary>
        /// <param name="SaveSession"></param>
        public void ClearOtherQuerySession(string SaveSession)
        {
            for (int i = 0; i < SavaAllQuerys.Count; i++)
            {
                if (SaveSession != null)
                {
                    if (SaveSession!=SavaAllQuerys[i])
                    {
                        if (Session[SavaAllQuerys[i]] != null)
                        {
                            Session[SavaAllQuerys[i]] = null;
                        }
                    }
                }
                else
                {
                    Session[SavaAllQuerys[i]] = null;
                }
            }
        }
         
    }
}
