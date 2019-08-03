using CsvParser.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CsvParser.Utils
{
    internal static class DataTableHelper
    {
        
        internal static bool HasDuplicateColumnName(DataTable table)
        {
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataColumn column in table.Columns)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (!column.Equals(table.Columns[i]) && column.ColumnName.Equals(table.Columns[i].ColumnName, StringComparison.OrdinalIgnoreCase))
                        {
                            throw new DuplicateColumnException(column.ColumnName +" and " + table.Columns[i].ColumnName + " are duplicate columns");
                        }
                    }
                }
                return false;
            }
            return true;
        }
    }
    
}
