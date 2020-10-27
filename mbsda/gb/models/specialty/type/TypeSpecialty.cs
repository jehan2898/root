using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using gb.mbs.da.model.specialty;

namespace gb.mbs.da.model.specialty.type
{
  public class TypeSpecialty
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_specialty_id");
            table.Columns.Add("sz_name");
            table.Columns.Add("sz_company_id");
            return table;
        }
        public static DataTable FillDBType(List<specialty.Specialty> p_lstSpecialty)
        {
            DataTable table = GetDBType();
            foreach (specialty.Specialty s in p_lstSpecialty)
            {
                DataRow row = table.NewRow();
                row["sz_specialty_id"] = s.ID;
                row["sz_name"] = s.Name;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }
        public static string GetTypeName()
        {
            return "dbo.typ_specialty";
        }
    }
}
