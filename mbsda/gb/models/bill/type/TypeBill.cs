using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using gb.mbs.da.model.bill;

namespace gb.mbs.da.model.bill.type
{
    public class TypeBill
    {
        public static DataTable GetDBType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sz_bill_id");
            table.Columns.Add("sz_assigned_lawfirm_id");
            table.Columns.Add("sz_company_id");
            return table;
        }
        public static DataTable FillDBType(List<bill.Bill> p_lstBill)
        {
            DataTable table = GetDBType();
            foreach (bill.Bill b in p_lstBill)
            {
                DataRow row = table.NewRow();
                row["sz_bill_id"] = b.Number;
                table.Rows.Add(row);
                table.AcceptChanges();
            }
            return table;
        }
        public static string GetTypeName()
        {
            return "dbo.typ_bill";
        }
    }
}
