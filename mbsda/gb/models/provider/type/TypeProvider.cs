using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gb.mbs.da.model.provider.type
{
    public class TypeProvider
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_provider_id");
            table.Columns.Add("sz_name");
            table.Columns.Add("sz_company_id");

            return table;
        }
        public static DataTable FillDBType(List<provider.Provider> p_lstProvider)
        {
            DataTable table = TypeProvider.GetDBType();
            foreach (provider.Provider p in p_lstProvider)
            {
                DataRow row = table.NewRow();
                row["sz_provider_id"] = p.Id;
                row["sz_name"] = p.Name;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }
        public static string GetTypeName()
        {
            return "dbo.typ_provider";
        }
    }
}
