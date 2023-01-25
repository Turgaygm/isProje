using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KO0HIRU\\SQLEXPRESS;Initial Catalog=Testdb;Integrated Security=True");
        TestdbEntities db = new TestdbEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.STK", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Malkodu"]);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int bitis = 0;
            int baslangıc = 0;

            if (maskedTextBox1 != null)
            {
                DateTime dt1 = Convert.ToDateTime(maskedTextBox1.Text);
                baslangıc = Convert.ToInt32(dt1.ToOADate());

            }
            else
            {
                MessageBox.Show("başlangıç tarihi giriniz");
            }
            if (maskedTextBox2 != null)
            {
                DateTime dt2 = Convert.ToDateTime(maskedTextBox2.Text);
                bitis = Convert.ToInt32(dt2.ToOADate());

            }
            else
            {
                MessageBox.Show("bitiş tarihi giriniz");
            }
            string malkodu = comboBox1.Text.ToString();


            SqlCommand cmd = new SqlCommand("pros7", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add("@Malkodu", SqlDbType.VarChar, 30).Value = malkodu;
            cmd.Parameters.Add("@BaslangicTarihi", SqlDbType.Int).Value = baslangıc;
            cmd.Parameters.Add("@BitisTarihi", SqlDbType.Int).Value = bitis;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
    }
}
