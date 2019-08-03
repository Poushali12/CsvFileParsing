using CsvParser.ExceptionHandler;
using CsvParser.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser
{
    public class CsvSource : IDisposable
    {
        #region properties
        public string FilePath {  get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        
        #endregion
        #region constructor
        public CsvSource(string filePath) : this(filePath,string.Empty,string.Empty)
        {
           
        }
        public CsvSource(string filePath, string networkUserName, string password)
        {
            FilePath = filePath;
            UserName = networkUserName;
            Password = password;
            
        }
        #endregion
        #region private methods
        public DataTable Read()
        {
            string csvText = string.Empty;
            if (string.IsNullOrEmpty(this.FilePath))
            {
                throw new IncorrectFilePathException("File path is null or empty");
            }
            try
            {

                if (this.FilePath.Substring(1, 2).Equals(":\\", StringComparison.OrdinalIgnoreCase))
                {
                    csvText = File.ReadAllText(this.FilePath);
                }

                else
                {
                    WebClient client = new WebClient();
                    if (!string.IsNullOrEmpty(this.UserName))
                    {

                        client.Credentials = new NetworkCredential(this.UserName, this.Password);

                    }
                    csvText = client.DownloadString(this.FilePath);
                    client.Dispose();
                }
               DataTable table = Parser(csvText);
                if (table != null)
                {
                    if (!DataTableHelper.HasDuplicateColumnName(table))
                    {
                        return table;
                    }
                }

            }
            catch (Exception e)
            {
                throw new IncompatibleFileDataException(e.Message);
            }

            return null;
        }
        private DataTable Parser(string text)
        {
            DataTable table = new DataTable();
            if (!string.IsNullOrEmpty(text))
            {
                string[] rows = text.Split('\n');
                char sepChar = ',';
                string[] columnHeaders = rows[0].Split(sepChar);
                Dictionary<int, List<object>> columnCellMapping = new Dictionary<int, List<object>>();
                List<string> cells = new List<string>();
                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    cells.Clear();
                    for (int j = 1; j < rows.Length - 1; j++)
                    {
                        cells.Add(rows[j].Split(sepChar).ElementAt(i));
                    }
                    cells.RemoveAll(item => string.IsNullOrEmpty(item));
                    table.Columns.Add(columnHeaders[i], DataTypeUtilityMethods.ExtractColumnDataType(cells)).AllowDBNull = true;
                }
                for (int i = 1; i < rows.Length - 1; i++)
                {
                    DataRow row = table.NewRow();
                    object[] cellValues = rows[i].Split(sepChar);
                    for (int j = 0; j < cellValues.Length; j++)
                    {
                        row[j] = cellValues[j];
                    }
                    table.Rows.Add(row);
                }
                return table;

            }
            return table;
        }
        #endregion
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }
        /// <summary>
        /// Destroy object
        /// </summary>
        ~CsvSource()
        {
            Dispose(false);
        }
        /// <summary>
        /// Dispose the object
        /// </summary>
        public  void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
