using System.Data;
using System.Data.SqlClient;

namespace TTHDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query, params SqlParameterModel[] sqlParameters) { 
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query,connection);
            if (sqlParameters != null) {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            return dt;

        }
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public SqlParameterModel() { }

        public SqlParameterModel(string name, string value) {
            Name = name;
            Value = value;
        }
    }
}
