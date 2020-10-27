using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gb.mbs.da.model.carrier.type
{
    public class TypeCarrier
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_insurance_id");
            table.Columns.Add("sz_name");
            table.Columns.Add("sz_company_id");

            return table;
        }

        public static DataTable FillDBType(List<carrier.Carrier> p_LstCarrier)
        {
            DataTable table = TypeCarrier.GetDBType();
            foreach (carrier.Carrier c in p_LstCarrier)
            {
                DataRow row = table.NewRow();
                row["sz_insurance_id"] = c.Id ;
                row["sz_name"] = c.Name;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }

        public static string GetTypeName()
        {
            return "dbo.typ_insurance_company";
        }
    }
}