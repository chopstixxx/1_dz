using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_dz.Classes
{
    public class DB : IDisposable
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=contacts_db;User Id=postgres;Password=123;");

        public void OpenConn()
        {
            conn.Open();
        }
        public void CloseConn()
        {
            conn.Close();
        }
        public NpgsqlConnection GetConn()
        {
            return conn;
        }

        public void Dispose() {

            conn.Dispose();
        }
        
    }
}
