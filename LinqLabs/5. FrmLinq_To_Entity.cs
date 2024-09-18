using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();

            Console.Write("XXX"); //測試

            //this.dbContext.Database.Log = Console.WriteLine; //委派物件Log指向這個方法
        }

        //會重複使用所以拉到外面來變類別變數
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //NorthwindEntities dbContext = new NorthwindEntities();

            //var q = from p in dbContext.Products
            //        where p.UnitPrice > 30
            //        select p;
            IQueryable<Product> q = from p in dbContext.Products
                                    where p.UnitPrice > 30
                                    select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();

            MessageBox.Show(this.dbContext.Products.First().Category.CategoryName); //從導覽屬性Category去找CategoryName
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    orderby p.UnitsInStock descending ,p.ProductID descending
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //var q3 = dbContext.Products.AsEnumerable().OrderBy(p => p,new MyComparer()).ToList();
        }
        //class

        private void button2_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    select new { p.CategoryID, p.Category.CategoryName, p.ProductID, p.ProductName, p.UnitPrice };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var q = from c in this.dbContext.Categories
                    from p in c.Products //從類別Categories去找到相關產品Products
                    select new { c.CategoryID, c.CategoryName, p.ProductID, p.ProductName, p.UnitPrice };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products.ToList() //AsEnumerable()
                    group p by p.Category.CategoryName into g
                    select new { CategoryName = g.Key, AvgUnitPrice= $"{g.Average(p=>p.UnitPrice):c2}" };
            this.dataGridView1.DataSource= q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //bool? b =null ;
            //b.Value


            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year into g //要加value才抓得到年 因為Nullable
                    select new { g.Key, Count = g.Count() };

            this.dataGridView1.DataSource = q.ToList();
            //=======
            //note subquery

        }

        private void button55_Click(object sender, EventArgs e)
        {
            //Step 1
            Product product = new Product { ProductName="XXX", Discontinued=true};
            this.dbContext.Products.Add(product);

            //Step 2 db SaveChanges
            this.dbContext.SaveChanges();

            //this.Read_RefreshDataGridView();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            //var product = (from p in this.dbContext.Products
                           //where p.ProductName)
        }
    }
}
