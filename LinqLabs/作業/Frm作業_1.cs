using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        int page = -1;
        //int countPerPage = 10;

        public Frm作業_1()
        {
            InitializeComponent();

            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q = from o in this.nwDataSet1.Orders
                    select o.OrderDate.Year;
            this.comboBox1.DataSource = q.Distinct().ToList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            //files[0].CreationTime
            var q = from f in files
                    where f.Extension == ".log"
                    select f;
            this.dataGridView1.DataSource = q.ToList();

            //this.dataGridView1.DataSource = files;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.CreationTime.Year == 2017
                    select f;
            this.dataGridView1.DataSource = q.ToList();
        }

        //自己設大小
        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Length > 100000
                    orderby f.Length descending
                    select f;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet1.Orders.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int year;
            int.TryParse(this.comboBox1.Text, out year);

            var q2 = from o in this.nwDataSet1.Orders
                     where o.OrderDate.Year == year
                     select o;

            this.dataGridView1.DataSource = q2.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
            page -= 1;

            this.dataGridView1.DataSource =
                this.nwDataSet1.Products.Skip(10 * page).Take(10).ToList();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)
            //Distinct()

            //int.TryParse(this.textBox1.Text, out countPerPage);
            //page += 1;
            //this.dataGridView1.DataSource =
            //    this.nwDataSet1.Products.Skip(countPerPage * page).Take(countPerPage).ToList();

            page += 1;

            this.dataGridView1.DataSource =
                this.nwDataSet1.Products.Skip(10 * page).Take(10).ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = 1996;
            int.TryParse(this.comboBox1.Text, out year);

            var q = from o in this.nwDataSet1.Orders
                    where o.OrderDate.Year == year
                    select o;

            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
