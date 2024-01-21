using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dz_1_test
{
    [TestClass]
    public class UnitTest1
    {
        readonly string conn_string = "Server=localhost;Port=5432;Database=contacts_db;User Id=postgres;Password=123;";

        [TestMethod]
        public void conn_test()
        {
            NpgsqlConnection conn = new NpgsqlConnection(conn_string)
        }
    }
}