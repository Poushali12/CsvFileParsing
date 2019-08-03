using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvParser.Utils
{
    internal static class DataTypeUtilityMethods
    {
        internal static Type ExtractColumnDataType(List<string> cellValues)
        {
            DateTime dateTimeVariable = new DateTime();
            Double number;
            Boolean boolVariable;
            Byte byteVariable;
            if (cellValues.All(item => DateTime.TryParse(item, out dateTimeVariable)))
            {
                return typeof(System.DateTime);
            }
            else if (cellValues.All(item => Double.TryParse(item, out number)))
            {
                return typeof(System.Double);
            }
            else if (cellValues.All(item => Byte.TryParse(item, out byteVariable)))
            {
                return typeof(System.Byte);
            }
            else if (cellValues.All(item => Boolean.TryParse(item, out boolVariable)))
            {
                return typeof(System.Boolean);
            }
            return typeof(System.String);
        }
    }
}
