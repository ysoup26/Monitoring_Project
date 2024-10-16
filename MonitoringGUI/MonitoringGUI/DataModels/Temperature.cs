using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringGUI.DataModels
{
    public static class Temperature
    {
        public static DataTable Data
        {
            get
            {
                DataTable table = new DataTable();
                table.Columns.AddRange(new DataColumn[] { new DataColumn("Time", typeof(DateTime)), new DataColumn("Temp",typeof(double))});
                table.Rows.Add(new DateTime(24, 5, 6, 7, 0, 0),30);
                table.Rows.Add(new DateTime(24, 5, 6, 8, 0, 0), 30);
                table.Rows.Add(new DateTime(24, 5, 6, 9, 0, 0), 43);
                table.Rows.Add(new DateTime(24, 5, 6, 10, 0, 0), 30);
                table.Rows.Add(new DateTime(24, 5, 6, 11, 0, 0), 31);
                table.Rows.Add(new DateTime(24, 5, 6, 12, 0, 0), 38);
                table.Rows.Add(new DateTime(24, 5, 6, 13, 0, 0), 38);
                table.Rows.Add(new DateTime(24, 5, 6, 14, 0, 0), 39);
                table.Rows.Add(new DateTime(24, 5, 6, 15, 0, 0), 37);
                table.Rows.Add(new DateTime(24, 5, 6, 16, 0, 0), 22);
                table.Rows.Add(new DateTime(24, 5, 6, 17, 0, 0), 32);
                table.Rows.Add(new DateTime(24, 5, 6, 18, 0, 0), 34);
                table.Rows.Add(new DateTime(24, 5, 6, 19, 0, 0), 36);
                table.Rows.Add(new DateTime(24, 5, 6, 20, 0, 0), 55);
                table.Rows.Add(new DateTime(24, 5, 6, 21, 0, 0), 52);
                table.Rows.Add(new DateTime(24, 5, 6, 22, 0, 0), 50);
                table.Rows.Add(new DateTime(24, 5, 6, 23, 0, 0), 44);

                return table;
            }
        }
    }
}
