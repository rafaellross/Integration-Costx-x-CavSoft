using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCostXCavSoft
{
    public class DB : IDisposable
    {
        public string DatabaseName { get; set; }
        public string Password { internal get; set; }
        public string User { internal get; set; }
        public string Server { get; set; }
        public SqlConnection Connection;

        public DB(bool TrustedConnection = true, string Server = "", string DatabaseName = "", string User = "", string Password = "")
        {
            Connection = new SqlConnection();

            this.Server = Server;
            this.DatabaseName = DatabaseName;
            this.Password = Password;
            this.User = User;

            if (TrustedConnection)
            {
                Connection.ConnectionString =
                "Data Source=" + Server + ";" +
                "Initial Catalog=" + DatabaseName + ";" +
                "Integrated Security=SSPI;";
            }
            else
            {
                Connection.ConnectionString =
                "Data Source=" + Server + ";" +
                "Initial Catalog=" + DatabaseName + ";" +
                "User id=" + User + ";" +
                "Password=" + Password + ";MultipleActiveResultSets=True";
            }
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);                
            }
            
            
        }

        public void Execute(string queryParam)
        {
            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                this.Connection.Open();
            }
            var cmd = new SqlCommand
            {
                CommandText = queryParam,
                CommandType = System.Data.CommandType.Text,
                Connection = Connection
            };

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public SqlDataReader Query(string queryParam)
        {
            var cmd = new SqlCommand
            {
                CommandText = queryParam,
                CommandType = System.Data.CommandType.Text,
                Connection = Connection
            };

            return cmd.ExecuteReader();
        }
      
        public List<Dictionary<string, string>> queryListToDic(string query)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.Connection;                
                cmd.CommandText = query;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var dict = new Dictionary<string, string>();
                    for (int i = 0; i <= dr.FieldCount - 1; i++)
                    {
                        dict.Add(dr.GetName(i), dr.GetValue(i).ToString());
                    }
                    result.Add(dict);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public void Dispose()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
    }
}

