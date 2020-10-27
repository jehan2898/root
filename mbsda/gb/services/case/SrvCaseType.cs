using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.dbconstant;
using gbmodel = gb.mbs.da.model;
using gb.mbs.da.service.util;
using gb.mbs.da.model.common;
using System.Configuration;
using System.IO;

namespace gb.mbs.da.service.casetype
{
    public class SrvCaseType
    {
        public List<gbmodel.casetype.CaseType> Select(gbmodel.user.User oUser)
        {
            SqlConnection sqlConnection = null;
            List<gbmodel.casetype.CaseType> oList = new List<gbmodel.casetype.CaseType>();
            try
            {
                sqlConnection = new SqlConnection(DBUtil.ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                sqlCommand = new SqlCommand(Procedures.PR_SELECT_CASETYPE, sqlConnection); 
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@sz_company_id", oUser.Account.ID));                
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        gbmodel.casetype.CaseType oElement = new gbmodel.casetype.CaseType();
                        oElement.Name = dr["Name"].ToString();//to do
                        oElement.ID = dr["ID"].ToString();//TODO

                        oList.Add(oElement);
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
            return oList;
        }
    }
}
