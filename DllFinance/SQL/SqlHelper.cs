using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllFinance.SQL
{
    public class SqlHelper
    {
        private string connectionString;

        public SqlHelper(string ip, string databaseName, string username, string password)
        {
            // IP, database, user ve password bilgilerini dışarıdan alarak bağlantı stringini oluşturuyoruz
            connectionString = $"Server={ip}; Database={databaseName}; User Id={username}; Password={password};";
        }
        private static readonly Lazy<SqlHelper> instance = new Lazy<SqlHelper>(() => new SqlHelper());
      
        private Dictionary<string, DataTable> dataTables;

        private SqlHelper()
        {
            // Initialize connection string here
        }

        public static SqlHelper Instance => instance.Value;

        public void Initialize(string ip, string databaseName, string username, string password)
        {
            connectionString = $"Server={ip}; Database={databaseName}; User Id={username}; Password={password};";
            dataTables = new Dictionary<string, DataTable>();
        }

        public void LoadDataFromViews(IEnumerable<string> viewNames)
        {
            foreach (var viewName in viewNames)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM {viewName}";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataTables[viewName] = dataTable; // Store in dictionary
                }
            }
        }

        public DataTable GetDataTable(string viewName)
        {
            return dataTables.ContainsKey(viewName) ? dataTables[viewName] : null;
        }

        public DataTable GetCombinedDataTable()
        {
            try
            {
                if (dataTables == null || dataTables.Count == 0)
                    return null;

                // Birleştirilmiş veri için yeni bir DataTable oluştur
                DataTable combinedTable = new DataTable("CombinedData");

                // İlk tabloyu baz al ve yapısını kopyala
                bool firstTable = true;
                foreach (var kvp in dataTables)
                {
                    DataTable sourceTable = kvp.Value;

                    if (sourceTable == null || sourceTable.Rows.Count == 0)
                        continue;

                    if (firstTable)
                    {
                        // İlk tablo için kolonları oluştur
                        combinedTable = sourceTable.Clone();

                        // Kaynak bilgisi için yeni bir kolon ekle
                        if (!combinedTable.Columns.Contains("ViewSource"))
                        {
                            combinedTable.Columns.Add("ViewSource", typeof(string));
                        }
                        firstTable = false;
                    }

                    // Her tablonun satırlarını kombineli tabloya ekle
                    foreach (DataRow row in sourceTable.Rows)
                    {
                        DataRow newRow = combinedTable.NewRow();

                        // Mevcut kolonları kopyala
                        foreach (DataColumn col in sourceTable.Columns)
                        {
                            if (combinedTable.Columns.Contains(col.ColumnName))
                            {
                                newRow[col.ColumnName] = row[col.ColumnName];
                            }
                        }

                        // Kaynak view bilgisini ekle
                        newRow["ViewSource"] = kvp.Key;

                        combinedTable.Rows.Add(newRow);
                    }
                }

                return combinedTable;
            }
            catch (Exception ex)
            {
                throw new Exception($"Veriler birleştirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
