using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gb.mbs.da.model.carriergroup.type
{
    public class TypeCarrierGroup
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_insurance_group_id");
            table.Columns.Add("sz_name");
            table.Columns.Add("sz_company_id");

            return table;
        }

        public static DataTable FillDBType(List<carriergroup.CarrierGroup> p_LstCarrierGroup)
        {
            DataTable table =TypeCarrierGroup.GetDBType();
            foreach (carriergroup.CarrierGroup c in p_LstCarrierGroup)
            {
                DataRow row = table.NewRow();
                row["sz_insurance_group_id"] = c.Id;
                row["sz_name"] = c.Name;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }

        public static string GetTypeName()
        {
            return "dbo.typ_insurance_group";
        }
    }
}
