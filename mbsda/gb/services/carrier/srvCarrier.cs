using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

using gb.mbs.da.service.util;
using gb.mbs.da.model.common;
using System.Configuration;
using System.IO;
using gb.mbs.da.dbconstant;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.service.carrier
{
    public class SrvCarrier
    {
        public List<gbmodel.carrier.Carrier> Select(gbmodel.user.User oUser, gbmodel.carrier.Carrier oCarrier)
        {
            SqlConnection sqlConnection = null;
            List<gbmodel.carrier.Carrier> oList = new List<gbmodel.carrier.Carrier>();
            try
            {
                sqlConnection = new SqlConnection(DBUtil.ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                sqlCommand = new SqlCommand(Procedures.PR_CARRIER_SELECT, sqlConnection);//TODO Add procedure name               
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@id", oUser.Account.ID));
                sqlCommand.Parameters.Add(new SqlParameter("@FLAG", "INSURANCE_LIST"));
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        gbmodel.carrier.Carrier oElement = new gbmodel.carrier.Carrier();
                        oElement.Name = dr["DESCRIPTION"].ToString();//to do
                        oElement.Id = dr["CODE"].ToString();//TODO

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
