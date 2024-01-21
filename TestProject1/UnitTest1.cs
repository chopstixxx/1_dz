using Npgsql;
using NpgsqlTypes;
using NUnit.Framework;
using System;
using System.Data;

namespace TestProject1
{
    public class Tests
    {
        private NpgsqlConnection _connection;
        [SetUp]
        public void Setup()
        {
            string conn_string = "Server=localhost;Port=5432;Database=contacts_db;User Id=postgres;Password=123;";

            _connection = new NpgsqlConnection(conn_string);
        }

        [Test]
        public void test_con_open_true()
        {
            _connection.Open();
            Assert.IsTrue(_connection.State == ConnectionState.Open);
        }
        [Test]
        public void test_insert_true()
        {
            using (_connection)
            {
                _connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO Òontacts (name, phone_number) VALUES (@name, @phone_num)", _connection))
                {
                    cmd.Parameters.Add("@name", NpgsqlDbType.Text).Value = "ÕËÍÓÎ‡È";
                    cmd.Parameters.Add("@phone_num", NpgsqlDbType.Text).Value = "89205674394";


                    try
                    {
                        cmd.ExecuteNonQuery();
                        Assert.IsTrue(true);

                    }
                    catch (Exception ex)
                    {
                        Assert.IsTrue(false);

                    }





                }
            }
        }
        [Test]
        public void test_insert_false()
        {
            using (_connection)
            {
                _connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO Òontacts (name, phone_number) VALUES (@name, @phone_num)", _connection))
                {
                    cmd.Parameters.Add("@name", NpgsqlDbType.Text).Value = 123;
                    cmd.Parameters.Add("@phone_num", NpgsqlDbType.Text).Value = "89205674394";


                    try
                    {
                        cmd.ExecuteNonQuery();
                        Assert.IsFalse(true);

                    }
                    catch (Exception ex)
                    {
                        Assert.IsFalse(false);

                    }





                }
            }
        }


    }
}