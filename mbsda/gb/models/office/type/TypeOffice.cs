using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace gb.mbs.da.model.office.type
{
   public class TypeOffice
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_office_id");
            table.Columns.Add("sz_name");
            table.Columns.Add("sz_company_id");

            return table;
        }

        public static DataTable FillDBType(List<office.Office> p_LstOffice)
        {
            DataTable table = TypeOffice.GetDBType();
            foreach (office.Office o in p_LstOffice)
            {
                DataRow row = table.NewRow();
                row["sz_office_id"] = o.ID;
                row["sz_name"] = o.Name;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }

        public static string GetTypeName()
        {
            return "dbo.typ_office";
        }
    }
}
