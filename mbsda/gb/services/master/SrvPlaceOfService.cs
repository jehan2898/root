using System;
using System.Collections.Generic;
using System.Text;
using model = gb.mbs.da;
using dataaccess = gb.mbs.da.dataaccess;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Collections;

namespace gb.mbs.da.services.master
{
   public class SrvPlaceOfService
    {
       private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);

        public void Create(model.master.PlaceOfService p_oPlaceofService, model.user.User p_oUser, model.account.Account p_oAccount)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();

            try
            {
                   
                    sqlCmd = new SqlCommand("sp_create_placeofservice", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTran;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@i_id", p_oPlaceofService.Id);
                    sqlCmd.Parameters.AddWithValue("@s_address_type",p_oPlaceofService.AddressType);
                    sqlCmd.Parameters.AddWithValue("@sz_address1", p_oPlaceofService.Address1);
                    sqlCmd.Parameters.AddWithValue("@sz_address2", p_oPlaceofService.Address2);
                    sqlCmd.Parameters.AddWithValue("@sz_city", p_oPlaceofService.City);
                    sqlCmd.Parameters.AddWithValue("@sz_state", p_oPlaceofService.State);
                    sqlCmd.Parameters.AddWithValue("@sz_zipcode", p_oPlaceofService.Zipcode);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_code", p_oPlaceofService.Code);
                    sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_created_by", p_oUser.ID);
                    sqlCmd.Parameters.AddWithValue("@sz_modified_by", p_oUser.ID);
                    
                   
                    sqlCmd.ExecuteNonQuery();
                
                    sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public void Update(model.master.PlaceOfService p_oPlaceofService, model.user.User p_oUser, model.account.Account p_oAccount)
        {
            SqlCommand sqlCmd;
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            sqlCon.Open();
            SqlTransaction sqlTran;
            sqlTran = sqlCon.BeginTransaction();
            try
            {
                sqlCmd = new SqlCommand("sp_create_placeofservice", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = sqlTran;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@i_id", p_oPlaceofService.Id);
                sqlCmd.Parameters.AddWithValue("@s_address_type", p_oPlaceofService.AddressType);
                sqlCmd.Parameters.AddWithValue("@sz_address1", p_oPlaceofService.Address1);
                sqlCmd.Parameters.AddWithValue("@sz_address2", p_oPlaceofService.Address2);
                sqlCmd.Parameters.AddWithValue("@sz_city", p_oPlaceofService.City);
                sqlCmd.Parameters.AddWithValue("@sz_state", p_oPlaceofService.State);
                sqlCmd.Parameters.AddWithValue("@sz_zipcode", p_oPlaceofService.Zipcode);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                sqlCmd.Parameters.AddWithValue("@sz_code", p_oPlaceofService.Code);
                sqlCmd.Parameters.AddWithValue("@sz_user_id", p_oUser.ID);
                sqlCmd.Parameters.AddWithValue("@sz_created_by", p_oUser.ID);
                sqlCmd.Parameters.AddWithValue("@sz_modified_by", p_oUser.ID);
                sqlCmd.Parameters.AddWithValue("@bt_is_active", p_oPlaceofService.bt_is_active);

                sqlCmd.ExecuteNonQuery();

                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public List<model.master.PlaceOfService> Select(model.account.Account p_oAccount)
        {
            SqlConnection sqlConnection = null;
            List<model.master.PlaceOfService> oList = new List<model.master.PlaceOfService>();
            List<model.master.PlaceOfService> oList2 = new List<model.master.PlaceOfService>();
            try
            {
                sqlConnection = new SqlConnection(sSQLCon);
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                sqlCommand = new SqlCommand("sp_select_placeofservice", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@sz_company_id", p_oAccount.ID));
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        model.master.PlaceOfService oElement = new model.master.PlaceOfService();
                        oElement.Id = Convert.ToInt32(dr["i_id"].ToString());
                        oElement.AddressType = dr["s_address_type"].ToString();
                        oElement.Code = dr["sz_code"].ToString();
                        oElement.Address1 = dr["sz_address1"].ToString();
                        oElement.Address2 = dr["sz_address2"].ToString();
                        oElement.City = dr["sz_city"].ToString();
                        oElement.State = dr["sz_state"].ToString();
                        oElement.Zipcode = dr["sz_zipcode"].ToString();
                        
                        oList.Add(oElement);
                    }

                    for (var i = 0; i < oList.Count; i++)
                    {
                        model.master.PlaceOfService oElement2 = new model.master.PlaceOfService();

                        oElement2.Id = oList[i].Id;
                        string Address_Type_Code = oList[i].AddressType.ToString() + "-" + oList[i].Code.ToString() ;
                        oElement2.Address = oList[i].AddressType.ToString() + "-" + oList[i].Code.ToString() +","+ oList[i].Address1.ToString() + "," + oList[i].Address2.ToString() + "," + oList[i].City.ToString() + "," + oList[i].State.ToString() + "," + oList[i].Zipcode.ToString();
                        oElement2.AddressType = Address_Type_Code;

                        if (oElement2.Address != null)
                        {
                            oElement2.Address.Replace(",,", ",");

                            if (oElement2.Address.StartsWith(",").Equals(true))
                            {
                                //oElement2.Address.Replace(",,", "");
                                //oElement2.Address.Replace(",", "");
                            }

                            if (oElement2.Address.EndsWith(","))
                            {
                                oElement2.Address = oElement2.Address.TrimEnd(',');
                            }
                        }
                        
                        oList2.Add(oElement2);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return oList2;
        }

        //public DataSet select(model.master.PlaceOfService p_oPlaceofService)
        //{
        //    DataSet ds = null;
        //    SqlConnection connection = new SqlConnection(sSQLCon);
        //    SqlTransaction sqlTran = null;
        //    try
        //    {
        //        connection.Open();
        //        sqlTran = connection.BeginTransaction();
        //        ds = new DataSet();

        //        SqlCommand sqlCommand = new SqlCommand("sp_select_placeofservice", connection);
        //        sqlCommand.CommandType = CommandType.StoredProcedure;
        //        sqlCommand.Transaction = sqlTran;
               
        //        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
        //        da.Fill(ds);
        //        sqlTran.Commit();
        //    }

        //    finally
        //    {
        //        if (connection != null)
        //        {
        //            if (connection.State == ConnectionState.Open)
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //    return ds;
        //}

        public int deletePlaceOfService(int i_id, string sz_company_id,int IsActive)
        {            
            SqlConnection conn = new SqlConnection(sSQLCon);
            conn.Open();
            int iRowsAffected = 0;
            try
            {
                SqlCommand comm = new SqlCommand("sp_delete_place_of_service", conn);
                comm.Parameters.AddWithValue("@i_id", i_id);
                comm.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                comm.Parameters.AddWithValue("@bt_is_active", IsActive);
                comm.CommandType = CommandType.StoredProcedure;
                iRowsAffected = comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return iRowsAffected;
        }

        public DataSet Get_PlaceOfService_Detail(string i_id, string sz_company_id)
        {
            try
            {
                DataSet ds = new DataSet();
                decimal _value = new decimal();
                SqlConnection conn = new SqlConnection(sSQLCon);
                conn.Open();
                SqlCommand comm = new SqlCommand("sp_get_PlaceOfService_Detail", conn);                
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@i_id", i_id);
                comm.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                SqlDataAdapter sqlda = new SqlDataAdapter(comm);
                ds = new DataSet();
                sqlda.Fill(ds);
                return ds;              
            }
            catch
            {
                return null;
            }
        }       
   }
}