using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };

            //分群組統計資料
            //IEnumerable<IGrouping<int,int>> q = from n in nums //第一個int代表key 第二個代表群內每個都是int
                                                //group n by (n % 2); //小括號內的是key (可以不用小括號
            IEnumerable<IGrouping<string, int>> q = from n in nums
                                                    group n by n % 2 == 0 ? "偶數" : "奇數";

            this.dataGridView1.DataSource = q.ToList();
            //========================

            //treeView
            foreach (var group in q)
            {
                TreeNode node = this.treeView1.Nodes.Add(group.Key.ToString()); //父節點

                foreach (var item in group)
                {
                    node.Nodes.Add(item.ToString()); //子節點
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //split切割=> Apply => Combine
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };

            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g //給一個g變數表示那一群
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g }; //匿名型別

            this.dataGridView1.DataSource = q.ToList();
            //=============================
            
            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

            //==========
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 121 };

            var q = from n in nums
                    group n by MyKey(n) into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g };

            this.dataGridView1.DataSource = q.ToList();

            //========================

            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //=====================
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        string MyKey(int n)
        {
            if (n < 5)
            {
                return "Small";
            }
            else if (n < 10)
            {
                return "Medium";
            }
            else
            {
                return "Large";
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //來源
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            //step 2
            var q = from f in files
                    group f by f.Extension into g //統計資料要暫存到變數: into g
                    orderby g.Count() descending
                    select new { g.Key, Count = g.Count() }; //屬性要給名字: Count = g.Count()

            //step 3
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.dataGridView1.DataSource= this.nwDataSet1.Orders;

            var q = from o in this.nwDataSet1.Orders
                    group o by o.OrderDate.Year into yearGroup
                    orderby yearGroup.Key descending
                    select new {Year = yearGroup.Key, Count = yearGroup.Count(), MyGroup = yearGroup };

            this.dataGridView2.DataSource = q.ToList();

            //========================

            foreach (var group in q)
            {
                string s = $"{group.Year} ({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var item in group.MyGroup)
                {
                    //node.Nodes.Add(item.OrderDate + " - " + item.OrderID);
                    node.Nodes.Add($"{item.OrderDate:yyyy/MM/dd} - {item.OrderID}");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //StreamReader...
            string s = "This is a pen. this is an apple. this is a book.";
            char[] chs = { ' ', '?', '.' };

            //來源物件=字串陣列(string[]) 透過來源物件去切割字
            string[] words = s.Split(chs);

            //每個字給一個key
            var q = from w in words
                    where w !="" //忽略空白  w:字串 不等於 "":空字串
                    group w by w into g
                    //group w by w.ToUpper() into g //全部轉成大寫計算
                    select new { g.Key, Count = g.Count() };

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    let s = f.Extension //變數如果很長可用暫存
                    where s == ".log"
                    select f;

            MessageBox.Show("Count = " + q.Count());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //求交集
            int[] nums1 = { 1, 2, 4, 5, 6, 7 };
            int[] nums2 = { 2, 4, 5, 11, 123 };

            IEnumerable<int> q; //q宣告一次即可
            q = nums1.Intersect(nums2);
            q = nums1.Union(nums2);
            q = nums1.Distinct(); //求唯一值

            q = q.Take(2);

            bool result;
            result = nums1.Any(n => n > 4); //任一元素大於4會回傳true -> true
            result = nums1.All(n => n > 4); //所有元素大於4會回傳true -> false

            int N = nums1.First();
            N = nums1.Last();
            N = nums1.ElementAt(3); //這邊下中斷點 再按F10 可看到int=5
            //N = nums1.ElementAt(32); //Exception
            N = nums1.ElementAtOrDefault(32); //strong 超過也不會產生Exception

            //RangeTest();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q = from p in this.nwDataSet1.Products
                    group p by p.CategoryID into g //求每一群的平均單價
                    select new { CategoryID = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };

            this.dataGridView1.DataSource = q.ToList();

            //===============
            var q2 = from c in this.nwDataSet1.Categories
                    join p in this.nwDataSet1.Products
                    on c.CategoryID equals p.CategoryID
                    //select new { c.CategoryID,c.CategoryName, p.ProductID, p.ProductName, p.UnitPrice };
                    group p by c.CategoryName into g //p.CategoryID沒有意義
                    select new { CategoryName = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };

            //要記得寫Fill
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
