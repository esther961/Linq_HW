using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.aW2019DataSet1.ProductPhoto);

            var q = from p in this.aW2019DataSet1.ProductPhoto
                    select p.ModifiedDate.Year;
            this.comboBox3.DataSource = q.Distinct().ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.aW2019DataSet1.ProductPhoto.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int year;
            int.TryParse(this.comboBox3.Text, out year);

            var q2 = from p in this.aW2019DataSet1.ProductPhoto
                     where p.ModifiedDate.Year == year
                     select p;

            this.dataGridView1.DataSource = q2.ToList();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = 2008;
            int.TryParse(this.comboBox3.Text, out year);

            var q = from p in this.aW2019DataSet1.ProductPhoto
                    where p.ModifiedDate.Year == year
                    select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            var q2 = from p in this.aW2019DataSet1.ProductPhoto
                     where p.ModifiedDate >= startDate && p.ModifiedDate <= endDate
                     select p;

            this.dataGridView1.DataSource = q2.ToList();
        }

        //圖片點擊
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                byte[] imageData1 = (byte[])row.Cells["ThumbNailPhoto"].Value;
                byte[] imageData2 = (byte[])row.Cells["LargePhoto"].Value;

                using (MemoryStream ms = new MemoryStream(imageData1))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
                using (MemoryStream ms = new MemoryStream(imageData2))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string s = comboBox2.SelectedItem.ToString();

            int startMonth = 1;
            int endMonth = 3;

            switch (s)
            {
                case "第一季":
                    startMonth = 1;
                    endMonth = 3;
                    break;
                case "第二季":
                    startMonth = 4;
                    endMonth = 6;
                    break;
                case "第三季":
                    startMonth = 7;
                    endMonth = 9;
                    break;
                case "第四季":
                    startMonth = 10;
                    endMonth = 12;
                    break;
                default:
                    MessageBox.Show("請選擇正確的季度");
                    return;
            }

            var q2 = from p in this.aW2019DataSet1.ProductPhoto
                     where p.ModifiedDate.Month >= startMonth && p.ModifiedDate.Month <= endMonth
                     select p;

            this.dataGridView1.DataSource = q2.ToList();

            int count = q2.Count();
            MessageBox.Show($"選出的資料有 {count} 筆。");
        }
    }
}
