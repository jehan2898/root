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

namespace gb.mbs.da.services.procedure
{
    public class SrvProcedure
    {
        public List<gbmodel.procedure.Procedure> Select(gbmodel.account.Account oAccount, List<model.specialty.Specialty> p_oSpecialty)
        {
            SqlConnection sqlConnection = null;
            DataTable oDTSpecialty = null;
            if (p_oSpecialty != null && p_oSpecialty.Count > 0)
            {
                oDTSpecialty = gbmodel.specialty.type.TypeSpecialty.FillDBType(p_oSpecialty);
            }
            List<gbmodel.procedure.Procedure> oList = new List<gbmodel.procedure.Procedure>();
            try
            {

                sqlConnection = new SqlConnection(DBUtil.ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                sqlCommand = new SqlCommand("sp_select_procedure_codes_for_specialty", sqlConnection);//TODO Add procedure name               
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@sz_company_id", oAccount.ID));
                if (oDTSpecialty != null && oDTSpecialty.Rows.Count > 0)
                {
                    SqlParameter tvpParamSpecialty = sqlCommand.Parameters.AddWithValue(
                        "@tvp_specialty", oDTSpecialty);
                    tvpParamSpecialty.SqlDbType = SqlDbType.Structured;
                    tvpParamSpecialty.TypeName = gbmodel.specialty.type.TypeSpecialty.GetTypeName();
                }
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        gbmodel.procedure.Procedure oElement = new gbmodel.procedure.Procedure();
                        oElement.Code = dr["SZ_PROCEDURE_CODE"].ToString();
                        oElement.Name = dr["SZ_CODE_DESCRIPTION"].ToString();
                        oElement.ID = dr["SZ_TYPE_CODE_ID"].ToString();
                        oElement.Specialty = new model.specialty.Specialty();
                        oElement.Specialty.ID = dr["sz_specialty_id"].ToString();
                        oElement.Specialty.Name = dr["sz_specialty_name"].ToString();
                        oList.Add(oElement);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
