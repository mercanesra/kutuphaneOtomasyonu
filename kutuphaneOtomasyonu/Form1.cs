using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kutuphaneOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-SINNOVD9;Initial Catalog=kutuphane;Integrated Security=True");
        private void verilerigörüntüle()
        {
            listView1.Items.Clear();
            baglan.Open();//yukarıdaki bağlantıyı açmak için
            SqlCommand komut = new SqlCommand("Select *From kitaplar", baglan);//istediğimiz tablodan istediğimiz alanları çekmek için komut oluşturuyoruz
            SqlDataReader oku = komut.ExecuteReader(); //veri tabanını okutuyoruz

            while (oku.Read())
            {
                ListViewItem ekle= new ListViewItem();//ekle adında değişken oluşturuldu
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add (oku["kitapad"].ToString());
                ekle.SubItems.Add(oku["yazar"].ToString());
                ekle.SubItems.Add(oku["yayınevi"].ToString());
                ekle.SubItems.Add(oku["sayfa"].ToString());
                listView1.Items.Add(ekle); //ekle adında oluşturduğumuz değeri çağıracağız

            }
            baglan.Close();//Yaptıgımız her işlemden sonra baglanntıyı kapatmak veritabınını yormuyor ve programın daha verimli çalışmasını sağlıyor
        }
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigörüntüle();//yukarıdaki verileri butona tıklanınca görüntüleriz
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into kitaplar (id,kitapad,yazar,yayınevi,sayfa) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString() + "')",baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigörüntüle();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

        }

        int id = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Delete From kitaplar where id=(" + id +")", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigörüntüle();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();

            SqlCommand komut = new SqlCommand("Update kitaplar set id='" + textBox1.Text.ToString() + "',kitapad='"+textBox2.Text.ToString()+"',yazar='"+textBox3.Text.ToString()+"',yayınevi='"+textBox4.Text.ToString()+"',sayfa='"+textBox5.Text.ToString()+"'where id=" + id + "", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigörüntüle();
        }
    }
}
