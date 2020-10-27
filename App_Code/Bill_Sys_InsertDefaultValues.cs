using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;

public class Bill_Sys_InsertDefaultValues
{

    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    string szProcedureName = "";
    ArrayList elementArrList;// = new ArrayList();

    public Bill_Sys_InsertDefaultValues()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void InsertDefaultValues(string XMLFilePath,string companyID,string billId,string patientID)
    {
        elementArrList = new ArrayList();

        elementArrList =ParseXML(XMLFilePath);

        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(szProcedureName, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            if (patientID != null) { sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientID); }
            if (billId != null) { sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", billId); }
            if (companyID != null) { sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID); }
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD"); 

            for (int i = 0; i <= elementArrList.Count - 1; i++)
            {

                sqlCmd.Parameters.AddWithValue("@" + ((DAO_Bill_Sys_XMLOBJECT)elementArrList[i]).FieldName, ((DAO_Bill_Sys_XMLOBJECT)elementArrList[i]).FieldValue);
            }
            sqlCmd.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }

    private ArrayList ParseXML(string p_szPath)
    {
        try
        {
            ArrayList objRArrList = new ArrayList();
           // objDiagnosisGrid = new ArrayList();
           // objServiceGrid = new ArrayList();
            XmlDocument doc1 = new XmlDocument();

            doc1.Load(p_szPath);
            XmlNodeList xmlNodeList;
            xmlNodeList = doc1.GetElementsByTagName("field");
            XmlNodeList xmlProcNameList;
            xmlProcNameList = doc1.GetElementsByTagName("document");

            foreach (XmlNode x in xmlProcNameList)
            {
                szProcedureName = x.Attributes["procedure_name"].Value;
            }

            DAO_Bill_Sys_XMLOBJECT objParameters = null;
            foreach (XmlNode xm in xmlNodeList)
            {
                objParameters = new DAO_Bill_Sys_XMLOBJECT();

                // IsTransactionDiagnosis is an optional attribute and will be applicable only to fields with diagnosis codes
                try
                {
                    objParameters.FieldName = xm.Attributes["sz_field_name"].Value;
                }
               catch (Exception ex)
                {
                    objParameters.FieldName= "0";
                }

                try
                {
                    // this is an optional value and exists only if the form has a service table
                    objParameters.FieldValue = xm.Attributes["sz_field_value"].Value;
                }
               catch (Exception ex)
                {
                    objParameters.FieldValue = "0";
                }
                objRArrList.Add(objParameters);


            }
            return objRArrList;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        return null;
    }

    private class DAO_Bill_Sys_XMLOBJECT
    {
        private string szDBFieldName;
        private string szDBFieldValue;

        public string FieldName
        {
            get
            {
                return szDBFieldName;
            }
            set
            {
                szDBFieldName = value;
            }
        }

        public string FieldValue
        {
            get
            {
                return szDBFieldValue;
            }
            set
            {
                szDBFieldValue = value;
            }
        }

    }

}
