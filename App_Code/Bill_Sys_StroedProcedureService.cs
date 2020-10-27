using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.Sql;

/// <summary>
/// Summary description for Bill_Sys_StroedProcedureService
/// </summary>
public class Bill_Sys_StroedProcedureService
{
	public string getReplacedString(string p_htmlstring,string p_szCaseID,string p_szCompanyID,string p_szBillID)
    {
        string szReplaceString = "";
        try 
    	{
         
            SqlConnection objSqlConn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
            objSqlConn.Open();
            SqlCommand objSqlComm = new SqlCommand("SP_NF3_TEMPLATE", objSqlConn);
            objSqlComm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader rsHTMLString;
            objSqlComm.CommandType = CommandType.StoredProcedure;
            objSqlComm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            objSqlComm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            objSqlComm.Parameters.AddWithValue("@SZ_BILL_ID", p_szBillID);
            rsHTMLString = objSqlComm.ExecuteReader();
            szReplaceString = ReplaceAll(p_htmlstring, rsHTMLString);

            szReplaceString = szReplaceString.Replace("TABLE_FOR_SERVICE_RECORD", getSeviceRecord(p_szBillID));

	    }
	    catch (Exception ex)
	    {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        return szReplaceString;
    }


    private ArrayList ParseXML(string p_szPath )
    {
        ArrayList objRArrList = new ArrayList();
        try
        {
            Bill_Sys_KeyValue objKeyValue;

            XmlDocument doc1 = new XmlDocument();
            doc1.Load(p_szPath);
            XmlNodeList xmlNodeList;
            xmlNodeList = doc1.GetElementsByTagName("key");

            foreach ( XmlNode xm in xmlNodeList )
            {
                objKeyValue = new Bill_Sys_KeyValue();
                objKeyValue.Key = xm.Attributes["name"].Value ;
                objKeyValue.Value = xm.Attributes["value"].Value;
                objRArrList.Add(objKeyValue);
            }

        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return objRArrList;
    }

    public String getSeviceRecord(string p_szbillid)
    {
        string szTableString = "";
        try
        {

            SqlConnection objSqlConn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
            objSqlConn.Open();
            SqlCommand objSqlComm = new SqlCommand("SP_GET_BILL_RECORD", objSqlConn);
            objSqlComm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader rd;
            objSqlComm.CommandType = CommandType.StoredProcedure;
            objSqlComm.Parameters.AddWithValue("@SZ_BILL_ID", p_szbillid);
            rd = objSqlComm.ExecuteReader();
            szTableString = "<table border='1'><tr><td>IC Code</td><td>Description</td><td>Unit</td><td>Amount</td></tr>";
            Double dbTotal = 0.0;
            while (rd.Read())
            {
                dbTotal = dbTotal + Convert.ToDouble(rd["FLT_AMOUNT"].ToString());
                szTableString += "<tr><td>" + rd["SZ_IC9_ID"] + "</td><td>" + rd["SZ_DESCRIPTION"] + "</td><td>" + rd["I_UNIT"] + "</td><td align='right'>" + rd["FLT_AMOUNT"] + "</td></tr>";
            }
            szTableString += "<tr><td colspan='3' align='right'><b>Total</b></td><td align='right'><b>" + dbTotal.ToString() + "</b></td></tr>";
            szTableString +="</table>";

            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szTableString;
    }

    public string ReplaceAll(string HtmString,SqlDataReader rs)
    {
        try
        {
             if (rs.Read())
             {
                 ArrayList objAL = new ArrayList();
                 Bill_Sys_KeyValue objKeyValue;
                 Int32 count;
                 objAL = ParseXML(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE"]);
                // HtmString.Replace("NOWDT", FormatDateTime(Now(),2));
               //  HtmString.Replace("NOWDATE", FormatDateTime(Now(),2));

                 for (count = 0 ; count <= objAL.Count-1;count++)
                 {
                     objKeyValue = (Bill_Sys_KeyValue)objAL[count];
                     HtmString = HtmString.Replace(objKeyValue.Key.ToString(), rs[objKeyValue.Value].ToString());

                //      = HtmString.Replace("RV_SZ_INSURANCE_NAME", "---------");
                      



                 }
             }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return HtmString;
    }
}
