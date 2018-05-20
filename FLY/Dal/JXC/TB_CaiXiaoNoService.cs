using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace VAN_OA.Dal.JXC
{
    public class TB_CaiXiaoNoService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.TB_CaiXiaoNo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("'" + model.Type + "',");
            }
            if (model.lblNo != null)
            {
                strSql1.Append("lblNo,");
                strSql2.Append("'" + model.lblNo + "',");
            }
            strSql.Append("insert into TB_CaiXiaoNo(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");

            return Convert.ToInt32(DBHelp.ExeScalar(strSql.ToString()));
            
        }

    }
}
