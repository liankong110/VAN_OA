using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VAN_OA.Dal.BaseInfo
{
    public class Province_CityService
    {
        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        public List<string> ProvinceList()
        {
            List<string> provinceList = new List<string>();
            string sql = "select ProvinceName from TB_Province";
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        provinceList.Add(objReader[0].ToString());
                    }
                }
            }
            return provinceList;
        }

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        public List<string> CityList(string ProvinceName)
        {
            List<string> cityList = new List<string>();
            string sql =string.Format( @"select CityName from [dbo].[TB_City] where ProvinceId in (select id from TB_Province where ProvinceName='{0}')
union all
select DistrictName as CityName from[dbo].[TB_District] where CityID in (
select id from[dbo].[TB_City]
        where ProvinceId in (select id from TB_Province where ProvinceName = '{0}')
)", ProvinceName);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        cityList.Add(objReader[0].ToString());
                    }
                }
            }
            return cityList;
        }
    }
}