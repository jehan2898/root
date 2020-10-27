using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using dataaccess = gb.mbs.da.dataaccess;

namespace gb.mbs.da.model.common
{
    public  class ApplicationSettings
    {
       private SqlDataReader sqlDr;
       private SqlConnection sqlCon;
       private string strsqlCon = string.Empty;

       public ApplicationSettings()
        {

            strsqlCon = dataaccess.ConnectionManager.GetConnectionString(null);
        }
      public  string  GetParameterValue(string ParameterName)
       {
           string ParameterValue = string.Empty;
           sqlCon = new SqlConnection(strsqlCon);
           try
           {
               sqlCon.Open();
               sqlDr = new SqlCommand("SELECT "+ParameterName+" FROM tblBasePath where BasePathId=( select ParameterValue from tblapplicationsettings where parametername = 'BasePathId')", this.sqlCon).ExecuteReader();
               while (sqlDr.Read())
               {
                   ParameterValue = sqlDr[ParameterName].ToString();
               }
               sqlDr.Close();

           }
           catch (Exception)
           {
               
               throw;
           }finally
           {
               if(sqlCon.State==ConnectionState.Open)
               {
                   sqlCon.Close();
               }
           }
           return ParameterValue;
       }
    }
}
