using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace atomiki_ergasia2
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection;
        private OleDbConnectionStringBuilder strb;
        public Form1()
        {
            InitializeComponent();
        }
        private DataTable FTD(String sql)
        {
            DataTable tb = new DataTable();
            using (OleDbDataAdapter adapt = new OleDbDataAdapter(sql, connection))
            {
                adapt.Fill(tb);
            }
            return tb;
        }
        private void RD()
        {
            strb = new OleDbConnectionStringBuilder();
            strb.Provider = "Microsoft.Jet.OLEDB.4.0";
            strb.DataSource = "Database1.mdb";
            connection = new OleDbConnection(strb.ConnectionString);
            dataGridView1.DataSource = FTD("SELECT * FROM database1");
            dataGridView1.ReadOnly = true;
        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from database1 where name='" + comboBox2.Text + "'";
            command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                richTextBox2.Text = reader["name"].ToString()
                + Environment.NewLine
                + reader["address"].ToString()
                + Environment.NewLine
                + reader["email"].ToString()
                + Environment.NewLine
                + reader["phone"].ToString()
                + Environment.NewLine
                + reader["birthday"].ToString()
                + Environment.NewLine
                + reader["city"].ToString()
                + Environment.NewLine
                + reader["country"].ToString();

                //Κώδικας για να εμφανίζεται η εικόνα 
                /* byte[] imgg = (byte[])(reader["Path"]);
                if (imgg == null)
                    pictureBox1.Image = null;
                else
                {
                    MemoryStream mstream = new MemoryStream(imgg);
                    pictureBox1.Image = System.Drawing.Image.FromStream(mstream);
                } */
            }
            connection.Close(); 
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RD(); 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string address = textBox5.Text;
            string email = textBox2.Text;
            string phone = textBox3.Text;
            string photoPath = label9.Text;
            string musicPath = label10.Text;
            string birthday = dateTimePicker1.Text;
            string city = textBox4.Text;
            string country = textBox6.Text;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO database1 (Name,Address,Email,Phone,Path,MusicPath,Birthday,City,Country) VALUES ('"+ name +"','"+ address +"','"+ email +"','"+ phone +"','"+ photoPath +"','"+ musicPath +"','"+ birthday +"', '"+ city +"', '"+ country +"')";
            command.ExecuteNonQuery();
            MessageBox.Show("Contact added successfully");
            connection.Close();
            RD();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string address = textBox5.Text;
            string email = textBox2.Text;
            string phone = textBox3.Text;
            string photoPath = label9.Text;;
            string musicPath = label10.Text; ;
            string birthday = dateTimePicker1.Text;
            string city = textBox4.Text;
            string country = textBox6.Text;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "update database1 set Name='" + name + "',Address='" + address + "',Email='" + email + "',Phone='" + phone + "',Path='" + photoPath + "',MusicPath='" + musicPath + "',Birthday='" + birthday + "',City='" + city + "',Country='" + country + "' where name='" + name + "'";
            command.CommandText = query;
            command.ExecuteNonQuery();
            MessageBox.Show("Contact changed succssefully");
            connection.Close();
            RD();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string address = textBox5.Text;
            string email = textBox2.Text;
            string phone = textBox3.Text;
            string photoPath = label9.Text;
            string musicPath = label10.Text;
            string birthday = dateTimePicker1.Text;
            string city = textBox4.Text;
            string country = textBox6.Text;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "delete from database1 where Name='" + name + "'";
            command.CommandText = query;
            command.ExecuteNonQuery();
            MessageBox.Show("Contact deleted successfully");
            connection.Close();
            RD();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                string imageloction = "";
                imageloction = opf.FileName;
                string directoryPath;
                directoryPath = Path.GetFileName(imageloction);
                label9.Text = directoryPath;
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                string audioloction = "";
                audioloction= opf.FileName;
                string directoryPath;
                directoryPath = Path.GetFileName(audioloction);
                label10.Text = directoryPath;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from database1";
            command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader["name"].ToString());
            }
            connection.Close();
        }
    }
}