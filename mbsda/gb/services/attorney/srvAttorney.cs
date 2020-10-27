using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Configuration;
using System.IO;

using gb.mbs.da.service.util;
using gb.mbs.da.model.common;
using gb.mbs.da.dbconstant;

using gbmodel = gb.mbs.da.model;


namespace gb.mbs.da.service.attorney
{
   public class SrvAttorney
    {
       public List<gbmodel.attorney.Attorney> Select(gbmodel.user.User oUser, gbmodel.attorney.Attorney oAttorney)
       {
            SqlConnection sqlConnection = null;
            List<gbmodel.attorney.Attorney> oList = new List<gbmodel.attorney.Attorney>();
            try
            {
                sqlConnection = new SqlConnection(DBUtil.ConnectionString);
                sqlConnection.Open();              
                SqlCommand sqlCommand = null;
                sqlCommand = new SqlCommand(Procedures.PR_ATTORNEY_SELECT, sqlConnection);//TODO : add procedure name
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@id", oUser.Account.ID));
                sqlCommand.Parameters.Add(new SqlParameter("@FLAG", "ATTORNEY_LIST"));             
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        gbmodel.attorney.Attorney oElement = new gbmodel.attorney.Attorney();
                        oElement.Name = dr["DESCRIPTION"].ToString(); //TODO
                        oElement.Id = dr["CODE"].ToString(); //TODO:Addd db field inside []
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
