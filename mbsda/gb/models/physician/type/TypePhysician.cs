using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gb.mbs.da.model.physician.type
{
   public class TypePhysician
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_doctor_id");
            table.Columns.Add("sz_name");
            table.Columns.Add("sz_company_id");

            return table;
        }

        public static DataTable FillDBType(List<physician.Physician> p_LstPhysician)
        {
            DataTable table = TypePhysician.GetDBType();
            foreach (physician.Physician p in p_LstPhysician)
            {
                DataRow row = table.NewRow();
                row["sz_doctor_id"] = p.ID;
                row["sz_name"] = p.Name;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }

        public static string GetTypeName()
        {
            return "dbo.typ_doctor";
        }
    }
}
