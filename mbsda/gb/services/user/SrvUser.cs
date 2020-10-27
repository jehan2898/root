using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using gb.mbs.da;
using gb.mbs.da.dbconstant;
using gb.mbs.da.service.util;
using gb.mbs.da.user.exception;
using gb.mbs.da.common.exception;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.services.user
{
    public class SrvUser
    {
        private string sSQLCon = string.Empty;
        public gbmodel.user.User Authenticate(gbmodel.user.User p_oUser)
        {
            List<SqlParameter> oParams = new List<SqlParameter>();

            oParams.Add(new SqlParameter("@username", p_oUser.UserName));
            oParams.Add(new SqlParameter("@password", GetEncryptedString(p_oUser.Password)));

            DataSet ds = null;
            string sUserName = null;
            string sUserID = null;
            gbmodel.user.User oUser = null;

            ds = DBUtil.DataSet(Procedures.PR_AUTHENTICATION, oParams);

            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new AuthenticationException("Invalid username or password");
            }

            sUserName = ds.Tables[0].Rows[0]["username"].ToString();
            sUserID = ds.Tables[0].Rows[0]["ID"].ToString();

            if (sUserID != null && sUserName != null && sUserID.Trim().Length != 0 && sUserName.Trim().Length != 0)
            {
                oParams = new List<SqlParameter>();
                oParams.Add(new SqlParameter("@sz_user_name", p_oUser.UserName));
                oParams.Add(new SqlParameter("@flag", "CHECKLOGIN"));

                // get the user object
                ds = DBUtil.DataSet(Procedures.PR_DOCTOR_LOGIN_PARAMETERS, oParams);

                oUser = new gbmodel.user.User();
                oUser.ID = sUserID;
                oUser.UserName = sUserName;
                oUser.Domain = ds.Tables[0].Rows[0]["DomainName"].ToString();
                oUser.Email = ds.Tables[0].Rows[0]["user_email_id"].ToString();

                gbmodel.account.Account oAccount = new gbmodel.account.Account();

                try
                {
                    oAccount.ID = ds.Tables[0].Rows[0]["sz_company_id"].ToString();
                    oAccount.Name = ds.Tables[0].Rows[0]["sz_company_name"].ToString();
                }
                catch (IndexOutOfRangeException _x)
                {
                    throw new IncompleteDataException("Account data not found for your user account");
                }

                if (oAccount.ID == null || oAccount.Name == null || oAccount.ID == "" || oAccount.Name == "")
                {
                    throw new IncompleteDataException("Account data not set for your user account");
                }

                gbmodel.user.Role oRole = new gbmodel.user.Role();
                oRole.ID = ds.Tables[0].Rows[0]["sz_user_role"].ToString();
                oRole.Name = ds.Tables[0].Rows[0]["sz_user_role_name"].ToString();

                if (oRole.ID == null || oRole.Name == null || oRole.ID == "" || oRole.Name == "")
                {
                    throw new IncompleteDataException("User role data not found");
                }

                oUser.Account = oAccount;
                oUser.Role = oRole;
                oUser.Token = GenerateUserToken(oUser.UserName, oUser.Domain);
            }
            else
            {
                throw new AuthenticationException("Invalid username or password");
            }
            return oUser;
        }
        private string GenerateUserToken(string sKey1, string sKey2)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();

            //Added by YC : refactor
            byte[] z = new byte[time.Length + key.Length];
            time.CopyTo(z, 0);
            key.CopyTo(z, time.Length);

            string token = Convert.ToBase64String(z);
            return GetEncryptedString((sKey1 + sKey2 + token));
        }
        public List<gbmodel.user.User> Select(gbmodel.account.Account p_Account)
        {
            //TODO: write logic to execute procedure to load all users under an account
            //create object of type gbmodel.user.User and add to list.
            //return the list
            List<gbmodel.user.User> oUserList = new List<gbmodel.user.User>();
            gbmodel.user.User oUser = new gbmodel.user.User();

            DataSet ds = null;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            ArrayList list = new ArrayList();

            try
            {
                connection.Open();
                ds = new DataSet();

                SqlCommand sqlCommand = new SqlCommand("sp_select_user_multiple", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    oUser = new gbmodel.user.User();
                    oUser.Role = new gbmodel.user.Role();
                    oUser.Provider = new gbmodel.provider.Provider();
                    oUser.Office = new gbmodel.office.Office();

                    oUser.ID = dr["sz_user_id"].ToString();
                    oUser.UserName = dr["sz_user_name"].ToString();
                    oUser.Password = dr["sz_password"].ToString();
                    oUser.Role.ID = dr["sz_user_role_id"].ToString();
                    oUser.Role.Name = dr["sz_user_role"].ToString();
                    oUser.Provider.Id = dr["sz_provider_id"].ToString();
                    oUser.Email = dr["sz_email"].ToString();
                    p_Account.ID = dr["sz_billing_company"].ToString();
                    oUser.Created = Convert.ToDateTime(dr["dt_created"].ToString());
                    oUser.LastLogin = Convert.ToDateTime(dr["dt_last_login"].ToString());
                    oUser.Office.ID = dr["sz_office_id"].ToString();
                    oUser.ID = dr["sz_created_by"].ToString();
                    oUser.UserName = dr["sz_created_user_name"].ToString();
                    oUserList.Add(oUser);
                }
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
            }
            return oUserList;
        }
        /***
            Required input values - gbmodel.account.Account.ID and gbmodel.user.User.ID and UserName
        ***/
        public gbmodel.user.User Select(gbmodel.account.Account p_Account, gbmodel.user.User p_oUser)
        {
            //TODO: write logic to execute procedure to load specific user that is selected
            //create object of type gbmodel.user.User and return

            gbmodel.user.User oUser = new gbmodel.user.User();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            DataSet ds = new DataSet();

            SqlCommand sqlCommand = new SqlCommand("sp_select_user_single", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
            sqlCommand.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
            sqlCommand.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                oUser = new gbmodel.user.User();
                oUser.Role = new gbmodel.user.Role();
                oUser.Provider = new gbmodel.provider.Provider();
                oUser.Office = new gbmodel.office.Office();

                oUser.ID = dr["sz_user_id"].ToString();
                oUser.UserName = dr["sz_user_name"].ToString();
                oUser.Password = dr["sz_password"].ToString();
                oUser.Role.ID = dr["sz_user_role_id"].ToString();
                oUser.Role.Name = dr["sz_user_role"].ToString();
                oUser.Provider.Id = dr["sz_provider_id"].ToString();
                oUser.Email = dr["sz_email"].ToString();
                p_Account.ID = dr["sz_billing_company"].ToString();
                oUser.Created = Convert.ToDateTime(dr["dt_created"].ToString());
                oUser.LastLogin = Convert.ToDateTime(dr["dt_last_login"].ToString());
                oUser.Office.ID = dr["sz_office_id"].ToString();
                oUser.ID = dr["sz_created_by"].ToString();
                oUser.UserName = dr["sz_created_user_name"].ToString();
            }

            return oUser;
        }
        /***
            Required input values - gbmodel.account.Account.ID and gbmodel.user.User.ID and UserName
        ***/
        public int Delete(string sz_user_id, string sz_company_id, int IsActive)
        {
            //TODO: write logic to execute procedure to delete (update with inactive flag) specific user that is selected
            //return the rows affected by executing the function

            int iResult = 0;
            gbmodel.user.User oUser = new gbmodel.user.User();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);

            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_disable_users", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                sqlCommand.Parameters.AddWithValue("@sz_user_id", sz_user_id);
                sqlCommand.Parameters.AddWithValue("@bt_is_disabled", IsActive);
                iResult = Convert.ToInt16(sqlCommand.ExecuteNonQuery());
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
            }
            return iResult;
        }
        public int Create(gbmodel.account.Account p_Account,
            gbmodel.user.User p_oUser,
            gbmodel.user.User p_oLoginUser, // this is the user who is adding a new user
            List<gbmodel.physician.Physician> p_lstDoctor,
            List<gbmodel.provider.Provider> p_lstProvider,
            List<gbmodel.provider.Provider> p_lstReferringProvider,
            gbmodel.office.Office o_lstOffice)
        {
            int iResult = 0;
            gbmodel.user.User oUser = new gbmodel.user.User();
            //SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            this.sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);
            SqlConnection connection = new SqlConnection(sSQLCon);
            connection.Open();
            SqlTransaction oTransaction = connection.BeginTransaction();

            try
            {
                bool result = false;
                result = Convert.ToBoolean(Exists(p_oUser));
                if (result == false)
                {
                    throw new Exception("The username is already taken. Please try with a different username");
                }
                if (result == true)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_create_user", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Transaction = oTransaction;
                    sqlCommand.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCommand.Parameters.AddWithValue("@sz_password", GetEncryptedString(p_oUser.Password));
                    sqlCommand.Parameters.AddWithValue("@sz_user_role", p_oUser.Role.ID);
                    sqlCommand.Parameters.AddWithValue("@sz_email", p_oUser.Email);
                    sqlCommand.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                    sqlCommand.Parameters.AddWithValue("@sz_created_id", p_oLoginUser.ID);
                    sqlCommand.Parameters.AddWithValue("@sz_domain_name", p_oUser.Domain);
                    if (p_lstDoctor != null && p_lstDoctor.Count > 0)
                        sqlCommand.Parameters.AddWithValue("@bt_validate_and_show", p_oUser.IsShowPreviousVisitByDefault);

                    // If the user has selected role referring office - the add the below parameter
                    if (o_lstOffice != null && o_lstOffice.ID != null && o_lstOffice.ID != "")
                    {
                        sqlCommand.Parameters.AddWithValue("@sz_office_id", o_lstOffice.ID);
                    }

                    SqlParameter oParamUserID = new SqlParameter("@sz_user_id", SqlDbType.VarChar, 50);
                    oParamUserID.Direction = ParameterDirection.Output;

                    sqlCommand.Parameters.Add(oParamUserID);
                    iResult = sqlCommand.ExecuteNonQuery();
                    string sUserID = oParamUserID.Value.ToString();

                    if (sUserID == null || sUserID == "")
                    {
                        throw new Exception("Invalid user sequence number generated. User could not be created");
                    }


                    // If the user has selected role as doctor - the below block will be executed
                    if (p_lstDoctor != null && p_lstDoctor.Count > 0)
                    {
                        // remove the existing doctor-user mapping if any
                        SqlCommand sqlCmdDeMap = new SqlCommand("sp_doctor_userid", connection);
                        sqlCmdDeMap.Transaction = oTransaction;
                        sqlCmdDeMap.CommandType = CommandType.StoredProcedure;
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_user_id", sUserID);
                        sqlCmdDeMap.Parameters.AddWithValue("@FLAG", "DELETE");
                        sqlCmdDeMap.ExecuteNonQuery();

                        SqlCommand sqlCmd = new SqlCommand("sp_doctor_userid", connection);
                        sqlCmd.Transaction = oTransaction;
                        foreach (gbmodel.physician.Physician p in p_lstDoctor)
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                            sqlCmd.Parameters.AddWithValue("@sz_user_ID", sUserID);
                            sqlCmd.Parameters.AddWithValue("@sz_doctor_ID", p.ID);
                            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                    // If the user has selected role as provider - the below block will be executed
                    if (p_lstProvider != null && p_lstProvider.Count > 0)
                    {
                        // Delete the existing providers mapped to this user
                        // sp_txn_user_provider - delete flag. pass user id and company id
                        foreach (gbmodel.provider.Provider p in p_lstProvider)
                        {
                            SqlCommand cmdProvider = new SqlCommand("sp_txn_user_provider", connection);
                            cmdProvider.CommandType = CommandType.StoredProcedure;
                            cmdProvider.Transaction = oTransaction;
                            cmdProvider.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                            cmdProvider.Parameters.AddWithValue("@sz_user_id", sUserID);
                            cmdProvider.Parameters.AddWithValue("@sz_user_provider_name_id", p.Id);
                            cmdProvider.Parameters.AddWithValue("@sz_user_provider_name", p.Name);
                            cmdProvider.Parameters.AddWithValue("@FLAG", "ADD");
                            cmdProvider.ExecuteNonQuery();
                        }
                    }

                    // If the user has selected role as referring provider - the below block will be executed
                    if (p_lstReferringProvider != null && p_lstReferringProvider.Count > 0)
                    {
                        foreach (gbmodel.provider.Provider p in p_lstReferringProvider)
                        {
                            SqlCommand sqlCmdReferringProvider = new SqlCommand("sp_save_user_provider_connection", connection);
                            sqlCmdReferringProvider.CommandType = CommandType.StoredProcedure;
                            sqlCmdReferringProvider.Transaction = oTransaction;
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_latest_user_id", sUserID);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_reffering_provider_id", p.Id);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_user_id", p_oLoginUser.ID);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@flag", "ADD");
                            sqlCmdReferringProvider.ExecuteNonQuery();
                        }
                    }
                    oTransaction.Commit();
                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                oTransaction.Rollback();
                //log and throw   
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();

                    }
                    connection = null;
                }
            }
            return iResult;
        }

        public int Update(gbmodel.account.Account p_Account,
                            gbmodel.user.User p_oUser,
                            gbmodel.user.User p_oLoginUser, // this is the user who is adding a new user
                            List<gbmodel.physician.Physician> p_lstDoctor,
                            List<gbmodel.provider.Provider> p_lstProvider,
                            List<gbmodel.provider.Provider> p_lstReferringProvider,
                            gbmodel.office.Office o_lstOffice)
        {
            int iResult = 0;
            gbmodel.user.User oUser = new gbmodel.user.User();
            this.sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);
            SqlConnection connection = new SqlConnection(sSQLCon);
            connection.Open();
            SqlTransaction oTransaction = connection.BeginTransaction();

            try
            {
                bool result = false;
                result = Convert.ToBoolean(Exists(p_oUser));
                //if (result == false)
                //{
                //    throw new Exception("The username is already taken. Please try with a different username");
                //}

                if (result == false)
                {
                    SqlCommand sqlCommand = new SqlCommand("SP_MST_USERS_UPDATE", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Transaction = oTransaction;
                    sqlCommand.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
                    sqlCommand.Parameters.AddWithValue("@sz_password", GetEncryptedString(p_oUser.Password));
                    sqlCommand.Parameters.AddWithValue("@sz_user_role", p_oUser.Role.ID);
                    sqlCommand.Parameters.AddWithValue("@sz_email", p_oUser.Email);
                    sqlCommand.Parameters.AddWithValue("@SZ_BILLING_COMPANY", p_Account.ID);
                    sqlCommand.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    if (p_lstDoctor != null && p_lstDoctor.Count > 0)
                        sqlCommand.Parameters.AddWithValue("@bt_validate_and_show", p_oUser.IsShowPreviousVisitByDefault);
                    // If the user has selected role referring office - the add the below parameter
                    if (o_lstOffice != null && o_lstOffice.ID != null && o_lstOffice.ID != "")
                    {
                        sqlCommand.Parameters.AddWithValue("@sz_office_id", o_lstOffice.ID);
                    }

                    //SqlParameter oParamUserID = new SqlParameter("@sz_user_id", SqlDbType.VarChar, 50);
                    //oParamUserID.Direction = ParameterDirection.Output;

                    //sqlCommand.Parameters.Add(oParamUserID);
                    //iResult = sqlCommand.ExecuteNonQuery();
                    //string sUserID = oParamUserID.Value.ToString();

                    //if (sUserID == null || sUserID == "")
                    //{
                    //    throw new Exception("Invalid user sequence number generated. User could not be created");
                    //}
                    iResult = sqlCommand.ExecuteNonQuery();

                    // If the user has selected role as doctor - the below block will be executed
                    if (p_lstDoctor != null && p_lstDoctor.Count > 0)
                    {
                        // remove the existing doctor-user mapping if any
                        SqlCommand sqlCmdDeMap = new SqlCommand("sp_doctor_userid", connection);
                        sqlCmdDeMap.Transaction = oTransaction;
                        sqlCmdDeMap.CommandType = CommandType.StoredProcedure;
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                        sqlCmdDeMap.Parameters.AddWithValue("@FLAG", "DELETE");
                        sqlCmdDeMap.ExecuteNonQuery();

                        foreach (gbmodel.physician.Physician p in p_lstDoctor)
                        {
                            SqlCommand sqlCmd = new SqlCommand("sp_doctor_userid", connection);
                            sqlCmd.Transaction = oTransaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                            sqlCmd.Parameters.AddWithValue("@sz_user_ID", p_oUser.ID);
                            sqlCmd.Parameters.AddWithValue("@sz_doctor_ID", p.ID);
                            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                    // If the user has selected role as provider - the below block will be executed
                    if (p_lstProvider != null && p_lstProvider.Count > 0)
                    {
                        SqlCommand sqlCmdDeMap = new SqlCommand("sp_txn_user_provider", connection);
                        sqlCmdDeMap.Transaction = oTransaction;
                        sqlCmdDeMap.CommandType = CommandType.StoredProcedure;
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                        sqlCmdDeMap.Parameters.AddWithValue("@FLAG", "DELETE");
                        sqlCmdDeMap.ExecuteNonQuery();


                        foreach (gbmodel.provider.Provider p in p_lstProvider)
                        {
                            SqlCommand cmdProvider = new SqlCommand("sp_txn_user_provider", connection);
                            cmdProvider.Transaction = oTransaction;
                            cmdProvider.CommandType = CommandType.StoredProcedure;
                            cmdProvider.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                            cmdProvider.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                            cmdProvider.Parameters.AddWithValue("@sz_user_provider_name_id", p.Id);
                            cmdProvider.Parameters.AddWithValue("@sz_user_provider_name", p.Name);
                            cmdProvider.Parameters.AddWithValue("@FLAG", "ADD");
                            cmdProvider.ExecuteNonQuery();
                        }
                    }
                    // If the user has selected role as referring provider - the below block will be executed
                    if (p_lstReferringProvider != null && p_lstReferringProvider.Count > 0)
                    {
                        SqlCommand sqlCmdDeMap = new SqlCommand("sp_save_user_provider_connection", connection);
                        sqlCmdDeMap.Transaction = oTransaction;
                        sqlCmdDeMap.CommandType = CommandType.StoredProcedure;
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_latest_user_id", p_oUser.ID);
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_reffering_provider_id", "");
                        sqlCmdDeMap.Parameters.AddWithValue("@sz_user_id", "");
                        sqlCmdDeMap.Parameters.AddWithValue("@FLAG", "DELETE");
                        sqlCmdDeMap.ExecuteNonQuery();

                        foreach (gbmodel.provider.Provider p in p_lstReferringProvider)
                        {
                            SqlCommand sqlCmdReferringProvider = new SqlCommand("sp_save_user_provider_connection", connection);
                            sqlCmdReferringProvider.CommandType = CommandType.StoredProcedure;
                            sqlCmdReferringProvider.Transaction = oTransaction;
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_latest_user_id", p_oUser.ID);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_reffering_provider_id", p.Id);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_company_id", p_Account.ID);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@sz_user_id", p_oLoginUser.ID);
                            sqlCmdReferringProvider.Parameters.AddWithValue("@flag", "UPDATE");
                            sqlCmdReferringProvider.ExecuteNonQuery();
                        }
                    }
                    oTransaction.Commit();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                oTransaction.Rollback();
                //log and throw   
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();

                    }
                    connection = null;
                }
            }
            return iResult;

        }
        private string GetEncryptedString(string p_PlainText)
        {
            string sPassPhrase = "Pas5pr@se";
            string sSaltValue = "s@1tValue";
            string sAlgorithm = "SHA1";
            int iIteration = 2;
            string sKey = "@1B2c3D4e5F6g7H8";
            int iBit = 256;
            string str4 = EncryDecryUtil.Encrypt(p_PlainText, sPassPhrase, sSaltValue, sAlgorithm, iIteration, sKey, iBit);
            return str4;
        }

        public bool Exists(gbmodel.user.User p_oUser)
        {
            //TODO: Return true if user exists (result set returns 1 record)
            // else return false    

            gbmodel.user.User oUser = new gbmodel.user.User();
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            DataSet ds = new DataSet();

            SqlCommand sqlCommand = new SqlCommand("sp_exists_user", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Parameters.AddWithValue("@sz_user_name", p_oUser.UserName);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(ds);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

/*
 * Database objects used in this class
 *
 * @Authenticate
 *      Procedures.PR_AUTHENTICATION - ValidateLogin
 *      Procedures.PR_DOCTOR_LOGIN_PARAMETERS - sp_user_login
 * 
 * @Create
 *      sp_create_user - Adds a new user to user_master table
 *      sp_doctor_userid - Maps a user login with a doctor
 *      sp_save_user_provider_connection - Maps referring provider to user login
 *      
 * @select 
 * sp_select_user_multiple - to load all users under an account
 * 
 * @Select 
 * sp_select_user_single - to load specific user that is selected
 * 
 * @Delete
 * sp_delete_user_single - logic to execute procedure to delete (update with inactive flag) specific user that is selected
 * 
 * @Create
 * sp_create_user - for creating a new user
 * sp_doctor_userid  - If the user has selected role as doctor - remove the existing doctor-user mapping if any      
 * sp_txn_user_provider - If the user has selected role as provider - Delete the existing providers mapped to this user
 * sp_save_user_provider_connection - If the user has selected role as referring provider
 * 
 * @Update
 * SP_MST_USERS_UPDATE  - for update() to update existing users information
 * 
 * @Exists
 * sp_exists_user   - to check is there same user is exists or not
 **/
