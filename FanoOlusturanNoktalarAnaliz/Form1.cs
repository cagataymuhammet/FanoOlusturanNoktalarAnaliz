using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FanoOlusturanNoktalarAnaliz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                StreamReader SR = new StreamReader(f.OpenFile());
                listBox1.Items.Clear();
                string metin = SR.ReadLine();
                while (metin != null)
                {
                    listBox1.Items.Add(metin);
                    metin = SR.ReadLine();
                }
                SR.Close();

            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
           
            string satir;
            progressBar1.Value = 0;
            for (int i = 0; i <listBox1.Items.Count-1; i++)
            {
             
                satir = listBox1.Items[i].ToString();
                if (sayi_ile_mi_basliyor(satir))
                {
                    satir = satir.Replace("      ", "     "); // 6 >> 5
                    satir = satir.Replace("     ", "    ");  // 5 >> 4
                    satir = satir.Replace("    ", "   ");  // 4 >> 3
                    satir = satir.Replace("   ", "  ");  // 3 >> 2
                    satir = satir.Replace("  ", " ");  // 2 >> 1
                    satir = satir.Replace(" ", "-");  // 1 >>-
                    if (satir.EndsWith("-"))
                    {
                        satir = satir.Remove(satir.Length - 1);
                    }

                    string[] dizi = satir.Split('-');
                    int[] sayi_dizisi = Array.ConvertAll(dizi, int.Parse);
                    Array.Sort(sayi_dizisi);
                    satir = "";
                    for (int j = 0; j <sayi_dizisi.Length; j++)
                    {
                        satir = satir + sayi_dizisi[j] + "-";
                    }

                    if (satir.EndsWith("-"))
                    {
                        satir = satir.Remove(satir.Length - 1);
                    }

                    if(!kayit_var_mi(satir))
                    {
                         veritabanina_yaz(sayi_dizisi,satir);
                    }

                    if (progressBar1.Value < 100)
                    {
                        progressBar1.Value += 1;
                    }
                    else
                    {
                        progressBar1.Value = 100;
                        progressBar1.Value = 0;
                    }
                }
            }
            progressBar1.Value = 100;
            MessageBox.Show("tamamlandı");
        }

        bool sayi_ile_mi_basliyor(string satir)
        {
            bool durum = false;
            try
            {
                string ilk_karakter = satir.Substring(0, 1);
                Convert.ToInt16(ilk_karakter);
                durum = true;
            }
            catch (Exception)
            {
                durum = false;
            }
            return durum;
        }

        static string sql_cumlesi;
        static string yol = @"server=ASUS; database=db_fano;Integrated Security=True";
        static SqlConnection baglanti = new SqlConnection(yol);
        static SqlCommand komut;
        static SqlDataAdapter verial;
        static DataSet ds;


        public static void baglantiyi_ac()
        {
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();
        }

        public static void baglantiyi_kapat()
        {
            if (baglanti.State == ConnectionState.Open) baglanti.Close();
        }


        bool veritabanina_yaz(int[] dizi, string kiyas_cumlesi)
        {
            baglantiyi_ac();
            bool durum = false;
            try
            {
                if (dizi.Length == 5)
                {
                    sql_cumlesi = "insert into tbl_fano_olmayan(BOYUT,KIYAS,N1,N2,N3,N4,N5)  values (@B,@K,@N1,@N2,@N3,@N4,@N5)";
                    komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = sql_cumlesi;
                    komut.Parameters.AddWithValue("@B", dizi.Length);
                    komut.Parameters.AddWithValue("@K", kiyas_cumlesi);
                    komut.Parameters.AddWithValue("@N1", dizi[0]);
                    komut.Parameters.AddWithValue("@N2", dizi[1]);
                    komut.Parameters.AddWithValue("@N3", dizi[2]);
                    komut.Parameters.AddWithValue("@N4", dizi[3]);
                    komut.Parameters.AddWithValue("@N5", dizi[4]);
                    komut.ExecuteNonQuery();
                    baglantiyi_kapat();
                    durum = true;
                }
                else if (dizi.Length == 6)
                {
                    sql_cumlesi = "insert into tbl_fano_olmayan(BOYUT,KIYAS,N1,N2,N3,N4,N5,N6)  values (@B,@K,@N1,@N2,@N3,@N4,@N5,@N6)";
                    komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = sql_cumlesi;
                    komut.Parameters.AddWithValue("@B", dizi.Length);
                    komut.Parameters.AddWithValue("@K", kiyas_cumlesi);
                    komut.Parameters.AddWithValue("@N1", dizi[0]);
                    komut.Parameters.AddWithValue("@N2", dizi[1]);
                    komut.Parameters.AddWithValue("@N3", dizi[2]);
                    komut.Parameters.AddWithValue("@N4", dizi[3]);
                    komut.Parameters.AddWithValue("@N5", dizi[4]);
                    komut.Parameters.AddWithValue("@N6", dizi[5]);
                    komut.ExecuteNonQuery();
                    baglantiyi_kapat();
                    durum = true;
                }
                else if (dizi.Length == 7)
                {
                    sql_cumlesi = "insert into tbl_fano_olmayan(BOYUT,KIYAS,N1,N2,N3,N4,N5,N6,N7)  values (@B,@K,@N1,@N2,@N3,@N4,@N5,@N6,@N7)";
                    komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = sql_cumlesi;
                    komut.Parameters.AddWithValue("@B", dizi.Length);
                    komut.Parameters.AddWithValue("@K", kiyas_cumlesi);
                    komut.Parameters.AddWithValue("@N1", dizi[0]);
                    komut.Parameters.AddWithValue("@N2", dizi[1]);
                    komut.Parameters.AddWithValue("@N3", dizi[2]);
                    komut.Parameters.AddWithValue("@N4", dizi[3]);
                    komut.Parameters.AddWithValue("@N5", dizi[4]);
                    komut.Parameters.AddWithValue("@N6", dizi[5]);
                    komut.Parameters.AddWithValue("@N7", dizi[6]);
                    komut.ExecuteNonQuery();
                    baglantiyi_kapat();
                    durum = true;
                }
                else if (dizi.Length == 8)
                {
                    sql_cumlesi = "insert into tbl_fano_olmayan(BOYUT,KIYAS,N1,N2,N3,N4,N5,N6,N7,N8)  values (@B,@K,@N1,@N2,@N3,@N4,@N5,@N6,@N7,@N8)";
                    komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = sql_cumlesi;
                    komut.Parameters.AddWithValue("@B", dizi.Length);
                    komut.Parameters.AddWithValue("@K", kiyas_cumlesi);
                    komut.Parameters.AddWithValue("@N1", dizi[0]);
                    komut.Parameters.AddWithValue("@N2", dizi[1]);
                    komut.Parameters.AddWithValue("@N3", dizi[2]);
                    komut.Parameters.AddWithValue("@N4", dizi[3]);
                    komut.Parameters.AddWithValue("@N5", dizi[4]);
                    komut.Parameters.AddWithValue("@N6", dizi[5]);
                    komut.Parameters.AddWithValue("@N7", dizi[6]);
                    komut.Parameters.AddWithValue("@N8", dizi[7]);
                    komut.ExecuteNonQuery();
                    baglantiyi_kapat();
                    durum = true;
                }
                else if (dizi.Length == 9)
                {
                    sql_cumlesi = "insert into tbl_fano_olmayan(BOYUT,KIYAS,N1,N2,N3,N4,N5,N6,N7,N8,N9)  values (@B,@K,@N1,@N2,@N3,@N4,@N5,@N6,@N7,@N8,@N9)";
                    komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = sql_cumlesi;
                    komut.Parameters.AddWithValue("@B", dizi.Length);
                    komut.Parameters.AddWithValue("@K", kiyas_cumlesi);
                    komut.Parameters.AddWithValue("@N1", dizi[0]);
                    komut.Parameters.AddWithValue("@N2", dizi[1]);
                    komut.Parameters.AddWithValue("@N3", dizi[2]);
                    komut.Parameters.AddWithValue("@N4", dizi[3]);
                    komut.Parameters.AddWithValue("@N5", dizi[4]);
                    komut.Parameters.AddWithValue("@N6", dizi[5]);
                    komut.Parameters.AddWithValue("@N7", dizi[6]);
                    komut.Parameters.AddWithValue("@N8", dizi[7]);
                    komut.Parameters.AddWithValue("@N9", dizi[8]);
                    komut.ExecuteNonQuery();
                    baglantiyi_kapat();
                    durum = true;

                }
                else if (dizi.Length == 10)
                {
                    sql_cumlesi = "insert into tbl_fano_olmayan(BOYUT,KIYAS,N1,N2,N3,N4,N5,N6,N7,N8,N9,N10)  values (@B,@K,@N1,@N2,@N3,@N4,@N5,@N6,@N7,@N8,@N9,@N10)";
                    komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = sql_cumlesi;
                    komut.Parameters.AddWithValue("@B", dizi.Length);
                    komut.Parameters.AddWithValue("@K", kiyas_cumlesi);
                    komut.Parameters.AddWithValue("@N1", dizi[0]);
                    komut.Parameters.AddWithValue("@N2", dizi[1]);
                    komut.Parameters.AddWithValue("@N3", dizi[2]);
                    komut.Parameters.AddWithValue("@N4", dizi[3]);
                    komut.Parameters.AddWithValue("@N5", dizi[4]);
                    komut.Parameters.AddWithValue("@N6", dizi[5]);
                    komut.Parameters.AddWithValue("@N7", dizi[6]);
                    komut.Parameters.AddWithValue("@N8", dizi[7]);
                    komut.Parameters.AddWithValue("@N9", dizi[8]);
                    komut.Parameters.AddWithValue("@N10", dizi[9]);
                    komut.ExecuteNonQuery();
                    baglantiyi_kapat();
                    durum = true;
                }
                else if (dizi.Length == 11)
                {
                    sql_cumlesi = "insert into tbl_fano_olmayan(BOYUT,KIYAS,N1,N2,N3,N4,N5,N6,N7,N8,N9,N10,N11)  values (@B,@K,@N1,@N2,@N3,@N4,@N5,@N6,@N7,@N8,@N9,@N10,@N11)";
                    komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = sql_cumlesi;
                    komut.Parameters.AddWithValue("@B", dizi.Length);
                    komut.Parameters.AddWithValue("@K", kiyas_cumlesi);
                    komut.Parameters.AddWithValue("@N1", dizi[0]);
                    komut.Parameters.AddWithValue("@N2", dizi[1]);
                    komut.Parameters.AddWithValue("@N3", dizi[2]);
                    komut.Parameters.AddWithValue("@N4", dizi[3]);
                    komut.Parameters.AddWithValue("@N5", dizi[4]);
                    komut.Parameters.AddWithValue("@N6", dizi[5]);
                    komut.Parameters.AddWithValue("@N7", dizi[6]);
                    komut.Parameters.AddWithValue("@N8", dizi[7]);
                    komut.Parameters.AddWithValue("@N9", dizi[8]);
                    komut.Parameters.AddWithValue("@N10", dizi[9]);
                    komut.Parameters.AddWithValue("@N11", dizi[10]);
                    komut.ExecuteNonQuery();
                    baglantiyi_kapat();
                    durum = true;
                }
                baglantiyi_kapat();
            }
            catch (Exception ex)
            {
                listBox2.Items.Add("hata " + ex.ToString());
                durum = false;
            }
            return durum;
        }


        bool kayit_var_mi(string kiyas)
        {

            bool durum = false;
            try
            {
                baglantiyi_ac();
                sql_cumlesi = "Select * From tbl_fano_olmayan where KIYAS='" + kiyas + "'";
                verial = new SqlDataAdapter(sql_cumlesi, baglanti);
                ds = new DataSet();
                verial.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    durum = true;
                }
                baglantiyi_kapat();
            }
            catch (Exception)
            {
                durum = false;
            }
            return durum;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 F3 = new Form3();
            F3.Show();
            this.Hide();

        }
    }
}
