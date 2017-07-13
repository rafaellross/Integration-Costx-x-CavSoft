using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace LibCostXCavSoft
{
    public class DbPostgres: IDisposable
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Server { get; set; }
        public string DataBaseName { get; set; }
        public string Port { get; set; }
        public NpgsqlConnection Connection { get; set; }

        public DbPostgres(string serverParam, string portParam, string dataBaseParam, string userParam, string passwordParam)
        {
            Server = serverParam;
            Port = portParam;
            DataBaseName = dataBaseParam;
            UserName = userParam;
            Password = passwordParam;

            string ConnectionString = "Server=" + Server + "; Port=" + Port + "; User Id=" + UserName + "; Password=" + Password + "; Database=" + DataBaseName;
            
            Connection = new NpgsqlConnection(ConnectionString);
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            
            }                        
        }

        public void Execute(string queryParam)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(queryParam, this.Connection);
            cmd.ExecuteNonQuery();
        }

        public NpgsqlDataReader Query(string query)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(query, this.Connection);
            return cmd.ExecuteReader();
        }

        public List<Dictionary<string, string>> queryListToDic(string query)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(query, this.Connection);



                NpgsqlDataReader dr = cmd.ExecuteReader();
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
