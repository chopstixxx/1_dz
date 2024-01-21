using _1_dz.Classes;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Windows.Forms;

namespace _1_dz
{
    public partial class Contacts : Form
    {
        readonly string conn_string = "Server=localhost;Port=5432;Database=contacts_db;User Id=postgres;Password=123;";
        public Contacts()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;

            Load_data();


        }

        private void Load_data()
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter("SELECT * FROM сontacts ORDER BY id ASC", conn_string);

            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Номер телефона";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Поле с именем пустое!");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Поле с номером телефона пустое!");
                return;
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(conn_string))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO сontacts (name, phone_number) VALUES (@name, @phone_num)", conn))
                {
                    cmd.Parameters.Add("@name", NpgsqlDbType.Text).Value = textBox1.Text;
                    cmd.Parameters.Add("@phone_num", NpgsqlDbType.Text).Value = textBox2.Text;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Контакт успешно добавлен!");
                        Load_data();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Выберите вариант поиска!");
                return;
            }


            if (radioButton1.Checked)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Поле с именем пустое!");
                    return;
                }

                try
                {
                    string name = textBox1.Text;
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter($"SELECT id, name, phone_number FROM сontacts WHERE name = '{name}'", conn_string);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (radioButton2.Checked)
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Поле с номером телефона пустое!");
                    return;
                }

                try
                {
                    string phone_num = textBox2.Text;
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter($"SELECT id, name, phone_number FROM сontacts WHERE phone_number = '{phone_num}'", conn_string);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }




        }
        private void button4_Click(object sender, EventArgs e)
        {
            Load_data();
        }



        private void button5_Click(object sender, EventArgs e)
        {





            using (NpgsqlConnection conn = new NpgsqlConnection(conn_string))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE сontacts SET name = @name, phone_number = @phone_num WHERE id = @id", conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    cmd.Parameters.Add("@name", NpgsqlDbType.Text).Value = (string)dataGridView1.CurrentRow.Cells[1].Value;
                    cmd.Parameters.Add("@phone_num", NpgsqlDbType.Text).Value = (string)dataGridView1.CurrentRow.Cells[2].Value;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Контакт успешно обновлён!");
                        Load_data();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }

            }





        }

        private void button6_Click(object sender, EventArgs e)
        {



            using (NpgsqlConnection conn = new NpgsqlConnection(conn_string))
            {

                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM сontacts WHERE id = @id", conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = (int)dataGridView1.CurrentRow.Cells[0].Value;


                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Контакт успешно удалён!");
                        Load_data();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}