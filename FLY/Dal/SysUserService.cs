namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    [Serializable]
    public class SysUserService
    {
        public string GetUserNo(string CompanyCode)
        {
            string qianZhui="";
            if (CompanyCode == "001")
            {
                qianZhui = "wb";
            }
            if (CompanyCode == "002")
            {
                qianZhui = "wb";
            }
            if (CompanyCode == "003")
            {
                qianZhui = "ydww";
            }
            if (CompanyCode == "004")
            {
                qianZhui = "yjt";
            }
            string MaxCardNo = "";
            string sql = string.Format("select right('0000000000'+convert(varchar,(max(RIGHT(loginUserNo,3))+1)),3) from tb_User  where CompanyCode='{0}'",
                CompanyCode, DateTime.Now.Year);

            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = objMax.ToString();
            }
            else
            {
                MaxCardNo = "001";
            }

            return qianZhui+MaxCardNo;
        }
        public int addUser(User user)
        {
            user.LoginUserNO = GetUserNo(user.CompanyCode);
            string sql1 = "select max(ID) as id from tb_User";
            int id = 1;
            try
            {
                id = ((int) DBHelp.ExeScalar(sql1)) + 1;
            }
            catch (Exception)
            {
            }
            int LoginIFXCBM = 0;
            if (user.LoginIFXCBM)
            {
                LoginIFXCBM = 1;
            }
            DBHelp.ExeCommand(string.Format(@"insert into tb_User values({9},'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{10}','{11}','{12}',getdate(),{14},{15},'{16}','{17}',{18},
                '{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}')",
                new object[] { user.LoginId, user.LoginName, user.LoginPwd, user.LoginStatus, user.LoginCPosition, user.LoginMemo, user.LoginTmpPwd, user.LoginIPosition, user.LoginRemark, id, user.LoginUserNO, user.LoginPhone, user.LoginAddress, user.LoginCreateTime, 
                    LoginIFXCBM, user.ReportTo, user.Zhiwu, user.CompanyCode, (user.IsSpecialUser ? 1 : 0),
               user.Mobile,user.CardNO,user.CityNo,user.Education,user.School,user.SchoolDate,user.Title,user.Political,user.HomeAdd,user.WorkDate ,user.SheBaoCode}));
            return id;
        }

        public User checkExist(User user)
        {
            string sql = string.Format("select * from tb_User where loginId='{0}'", user.LoginId);
            return this.getUserBySQL(sql);
        }

        public User checkExist_1(User user)
        {
            string sql = string.Format("select * from tb_User where loginName='{0}'", user.LoginName);
            return this.getUserBySQL(sql);
        }

        public bool deleteUserByUserId(int userId)
        {
            return DBHelp.ExeCommand(string.Format("delete from  tb_User where ID={0}", userId));
            //return false;
        }

        private User getUserBySQL(string sql)
        {
            User User = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User = new User();
                        User.Id = objReader.GetInt32(0);
                        User.LoginId = objReader.GetString(1);
                        User.LoginName = objReader.GetString(2);
                        User.LoginPwd = objReader.GetString(3);
                        User.LoginStatus = objReader.GetString(4);
                        User.LoginCPosition = objReader.GetString(5);
                        User.LoginMemo = objReader.GetString(6);
                        User.LoginTmpPwd = objReader.GetString(7);
                        User.LoginIPosition = objReader.GetString(8);
                        User.LoginUserNO = objReader["loginUserNO"].ToString();
                        User.LoginPhone = objReader["LoginPhone"].ToString();
                        User.LoginAddress = objReader["LoginAddress"].ToString();
                        User.LoginCreateTime = Convert.ToDateTime(objReader["LoginCreateTime"].ToString());
                        User.LoginIFXCBM = Convert.ToBoolean(objReader["LoginIFXCBM"]);
                        User.LoginRemark = Convert.ToString(objReader["LoginRemark"]);
                        User.CompanyCode = Convert.ToString(objReader["CompanyCode"]);

                        if (!(objReader["ReportTo"] is DBNull))
                        {
                            User.ReportTo = Convert.ToInt32(objReader["ReportTo"]);
                        }

                        if (!(objReader["zhiwu"] is DBNull))
                        {
                            User.Zhiwu = Convert.ToString(objReader["zhiwu"]);
                        }
                        User.IsSpecialUser = Convert.ToBoolean(objReader["IsSpecialUser"]);

                        User.Mobile = Convert.ToString(objReader["Mobile"]);
                        User.CardNO = Convert.ToString(objReader["CardNO1"]);
                        User.CityNo = Convert.ToString(objReader["CityNo"]);
                        User.Education = Convert.ToString(objReader["Education"]);
                        User.School = Convert.ToString(objReader["School"]);
                        User.SchoolDate = Convert.ToString(objReader["SchoolDate"]);
                        User.Title = Convert.ToString(objReader["Title"]);
                        User.Political = Convert.ToString(objReader["Political"]);
                        User.HomeAdd = Convert.ToString(objReader["HomeAdd"]);
                        User.WorkDate = Convert.ToString(objReader["WorkDate"]);
                        try
                        {
                            User.SheBaoCode = Convert.ToString(objReader["SheBaoCode"]);
                        }
                        catch (Exception)
                        {
                            
                          
                        }
                    }
                    objReader.Close();
                }
            }
            if (User == null)
            {
            }
            return User;
        }




        public List<User> getUserBySQL1(string sql)
        {
            List<User> list = new List<User>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();
                        User.Id = objReader.GetInt32(0);
                        User.LoginId = objReader.GetString(1);
                        User.LoginName = objReader.GetString(2);
                        User.LoginPwd = objReader.GetString(3);
                        User.LoginStatus = objReader.GetString(4);
                        User.LoginCPosition = objReader.GetString(5);
                        User.LoginMemo = objReader.GetString(6);
                        User.LoginTmpPwd = objReader.GetString(7);
                        User.LoginIPosition = objReader.GetString(8);
                        User.LoginUserNO = objReader["loginUserNO"].ToString();
                        User.LoginPhone = objReader["LoginPhone"].ToString();
                        User.LoginAddress = objReader["LoginAddress"].ToString();
                        User.LoginCreateTime = Convert.ToDateTime(objReader["LoginCreateTime"].ToString());
                        User.LoginIFXCBM = Convert.ToBoolean(objReader["LoginIFXCBM"]);
                        User.LoginRemark = objReader["LoginRemark"].ToString();

                        if (!(objReader["ReportTo"] is DBNull))
                        {
                            User.ReportTo = Convert.ToInt32(objReader["ReportTo"]);
                        }

                        try
                        {
                            if (!(objReader["ReportToName"] is DBNull))
                            {
                                User.ReportToName = Convert.ToString(objReader["ReportToName"]);
                            }
                        }
                        catch (Exception)
                        {
                            
                            
                        }

                        if (!(objReader["zhiwu"] is DBNull))
                        {
                            User.Zhiwu = Convert.ToString(objReader["zhiwu"]);
                        }
                        User.CompanyCode = Convert.ToString(objReader["CompanyCode"]);
                        User.IsSpecialUser = Convert.ToBoolean(objReader["IsSpecialUser"]);
                        User.SheBaoCode = Convert.ToString(objReader["SheBaoCode"]);
                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;
        }



        public List<User> getUserReportTos(string sql)
        {
            List<User> list = new List<User>();
            list.Add(new User());
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();
                       
                        User.LoginName = objReader.GetString(0);
                        User.Id = Convert.ToInt32(objReader["ID"]);
                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;
        }

        public User getUserByUserId(int userId)
        {
            string sql = string.Format("select * from tb_User where id={0}", userId);
            return this.getUserBySQL(sql);
        }

        public DataTable getUsers()
        {
            return DBHelp.getDataTable(string.Format("select [tb_User].id,[loginId],[loginName],[loginPwd],[loginStatus],[loginCPosition],[loginRemark], loginUserNO,loginPhone,loginAddress,loginCreateTime,loginIFXCBM ,loginMemo,loginTmpPwd,loginIPosition,zhiwu from [tb_User]", new object[0]));
        }

        public List<User> getAllUserByLoginName(string where)
        {
            string sql = "select ID,loginName,loginId from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' " + where + " order by loginName";
            List<User> list = new List<User>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();
                        User.Id = objReader.GetInt32(0);
                        
                        User.LoginName = objReader.GetString(1);
                        User.LoginId = objReader.GetString(2);
                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;

        }

        public List<User> getAllUserByPOList(string where="")
        {
            string sql = "select ID,loginName from tb_User where IsSpecialUser=1 " + where + " order by loginName";
            List<User> list = new List<User>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();
                        User.Id = objReader.GetInt32(0);

                        User.LoginName = objReader.GetString(1);

                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;

        }

        public List<User> getZaiZhiList(string where = "")
        {
            string sql = "select loginName from tb_User where 1=1 " + where + " order by loginName";
            List<User> list = new List<User>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();                    

                        User.LoginName = objReader.GetString(0);

                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;

        }

        public List<User> getAllUserByLoginName_Report(string where)
        {
            string sql = @"select tb_User.ID,loginName,TB_Company.Id as ComId from tb_User 
LEFT JOIN TB_Company ON tb_User.CompanyCode=TB_Company.ComCode
where 1=1 and loginName<>'admin' 
 " + where + " order by loginName";
            List<User> list = new List<User>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();
                        User.Id = objReader.GetInt32(0);

                        User.LoginName = objReader.GetString(1);
                        User.CompanyId = objReader.GetInt32(2);
                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;

        }

        public List<User> GetAllAEUserList(string where)
        {
            string sql = "select ID,loginName from AllAEUserList where 1=1  " + where + " order by loginName";
            List<User> list = new List<User>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();
                        User.Id = objReader.GetInt32(0);

                        User.LoginName = objReader.GetString(1);

                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;

        }

        public DataTable getUserTableByLoginName(string where)
        {
            return DBHelp.getDataTable("select ID,loginName from tb_User where loginName<>'admin' " + where + " order by loginName");
        }

        public DataTable getUserTableByLoginNameForPAForm(string PATemplateID,string ItemID)
        {
            return DBHelp.getDataTable("select U.ID,U.loginName,UP.[User_ID] from tb_User U left join A_PATemplateItemUser UP on U.ID=UP.[User_ID] and UP.A_PAItemID=" + ItemID + " and UP.A_PATemplateID=" + PATemplateID + " where U.loginName<>'admin' order by loginName");
        }
        public DataTable getUserTableByLoginNameForUserPAForm(string UserID, string ItemID)
        {
            return DBHelp.getDataTable("select U.ID,U.loginName,UP.UserID from tb_User U left join tb_UserPAFormUser UP on U.ID=UP.ReviewID and UP.ItemID=" + ItemID + " and UP.UserID=" + UserID + " where U.loginName<>'admin' and U.ID<>" + UserID + " order by loginName");
        }

        public bool modifyUser(User user)
        {
            int LoginIFXCBM = 0;
            if (user.LoginIFXCBM)
            {
                LoginIFXCBM = 1;
            }
            return DBHelp.ExeCommand(string.Format(@"UPDATE tb_User SET loginId ='{0}', loginName ='{1}', loginPwd = '{2}', loginStatus ='{3}', loginCPosition = '{4}',
loginMemo = '{5}', loginTmpPwd ='{6}', loginIPosition = '{7}', LoginRemark ='{8}', LoginUserNO ='{10}', LoginPhone ='{11}', LoginAddress ='{12}',
LoginIFXCBM ={13},reportTo={14},zhiwu='{15}',CompanyCode='{16}',IsSpecialUser={17} 
,Mobile='{18}',CardNO1='{19}',CityNo='{20}',Education='{21}',School='{22}',SchoolDate='{23}',Title='{24}',Political='{25}',HomeAdd='{26}',WorkDate='{27}',SheBaoCode='{28}'
WHERE ID = {9}",
                new object[] { user.LoginId, user.LoginName, user.LoginPwd, user.LoginStatus, user.LoginCPosition, user.LoginMemo,
                    user.LoginTmpPwd, user.LoginIPosition, user.LoginRemark, user.Id, user.LoginUserNO, user.LoginPhone, 
                    user.LoginAddress, LoginIFXCBM,user.ReportTo,user.Zhiwu ,user.CompanyCode,(user.IsSpecialUser?1:0),                
               user.Mobile,user.CardNO,user.CityNo,user.Education,user.School,user.SchoolDate,
               user.Title,user.Political,user.HomeAdd,user.WorkDate,user.SheBaoCode}));
        }
    }
}

