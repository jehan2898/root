using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gb.mbs.da.model.procedure.type
{
    public class TypeProcedure
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_procedure_id");
            table.Columns.Add("sz_name");
            table.Columns.Add("sz_company_id");

            return table;
        }
        public static DataTable FillDBType(List<procedure.Procedure> p_lstProcedure)
        {
            DataTable table = TypeProcedure.GetDBType();
            foreach (procedure.Procedure p in p_lstProcedure)
            {
                DataRow row = table.NewRow();
                row["sz_procedure_id"] = p.ID;
                row["sz_name"] = p.Name;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }
        public static string GetTypeName()
        {
            return "dbo.typ_procedure";
        }
    }
}