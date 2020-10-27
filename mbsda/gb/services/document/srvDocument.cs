using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using gb.mbs.da.service.util;
using gb.mbs.da.model.common;
using gb.mbs.da.model.document;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.service.document
{
    public class SrvDocument
    {
        public List<IntakeProviderDocument> Select(gbmodel.user.User oUser, gbmodel.intakeprovider.IntakeProvider oProvider, gbmodel.casetype.CaseType  oCasetype)
        {
            List<SqlParameter> oParams = new List<SqlParameter>();
            List<IntakeProviderDocument> oList = new List<IntakeProviderDocument>();
            oParams.Add(new SqlParameter("@i_provider_id", "" + oProvider.Id));
            oParams.Add(new SqlParameter("@sz_case_type_id", oCasetype.ID));
            oParams.Add(new SqlParameter("@sz_company_id", oUser.Account.ID));
            DataSet ds = null;
            ds = DBUtil.DataSet(dbconstant.Procedures.PR_INTAKE_DOCUMENT, oParams);
            if (ds != null)
            {
                IntakeProviderDocument oType;
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    oType = new IntakeProviderDocument();
                    DataRow dr = dt.Rows[i];
                    oType.Id = "" + dr["i_document_id"];
                    oType.Name = "" + dr["sz_name"];
                    //oType.CreatedDate = Convert.ToDateTime(dr["dt_created"]);
                    //oType.CreatedBy = oUser;
                    //oType.Comapany = oUser.Account;
                    gbmodel.casetype.CaseType obj = new gbmodel.casetype.CaseType();
                    obj.ID = "" + dr["sz_case_type_id"];
                    obj.Name = "" + dr["sz_case_type_name"];
                    oType.CaseType = obj;

                    oList.Add(oType);
                }
            }
            return oList;
        }
    }
}
