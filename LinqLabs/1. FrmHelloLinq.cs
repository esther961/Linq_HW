using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //陣列集合是一個iterator(迭代器)物件 可列舉的
            //public interface INumerable<T>
            
            //    public interface IEnumerable
            //    System.Collections 的成員
            //摘要:
            //公開能逐一查看非泛型集合內容一次的列舉程式。
            int[] nums = { 1, 2, 3, 4, 5, 6, 7 };

            //syntax sugar 語法糖
            foreach (int number in nums)
            {
                this.listBox1.Items.Add(number);
            }

            this.listBox1.SelectedIndex = 0;

            //C#轉譯(interface寫法
            this.listBox1.Items.Add("====================");
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int> { 1,2,3,4,5,6,7,8 };
            foreach (int number in list)
            {
                this.listBox1.Items.Add(number);
                //可以用變數接: int result = this.listBox1.Items.Add(number);
            }

            //C#轉譯
            this.listBox1.Items.Add("====================");
            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //int w = 100;
            //錯誤CS1579 因為 'int' 不包含 'GetEnumerator' 的公用執行個體或延伸模組定義
            //所以 foreach 陳述式無法在型別 'int' 的變數上運作

            //foreach (int n in w)
            //{
            //    this.listBox1.Items.Add(n);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Step 1:define Data Source object
            //Step 2:define Query
            //Step 3:execute Query

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            //Step 2 (來源物件nums where接條件
            IEnumerable<int> q = from n in nums
                                 //where n >5
                                 //where n>5 && n<=10
                                 where n%2 == 0
                                 //where n<3 || n>10
                                 select n;
            //Step 3
            this.listBox1.Items.Clear();
            foreach(int n in q)
            {
                this.listBox1.Items.Add(n);
            }


            //note: DataGridView Binding繫節 找屬性
            IEnumerable<string>q1 = from n in nums
                                    where n%2 == 0
                                    select n.ToString();
            this.dataGridView1.DataSource = q1.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            //IEnumerable<int> q = from n in nums
            //                     //where n % 2 == 0
            //                     where IsEven(n)
            //                     select n;
            IEnumerable<string> q = from n in nums
                                    where IsEven(n)
                                    select "Hello" + n.ToString();

            this.listBox1.Items.Clear();
            foreach (string n in q) //int改成string
            {
                this.listBox1.Items.Add(n);
            }
        }

        //方法 回傳布林值
        bool IsEven(int n)
        {
            //if (n%2==0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return n % 2 == 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            IEnumerable<Point> q = from n in nums
                                    where n >5
                                    select new Point(n, n * n);

            this.listBox1.Items.Clear();

            //execute query => foreach (... in q..)
            foreach (Point p in q)
            {
                this.listBox1.Items.Add(p.X + "," + p.Y);
            }
            //execute query => ToXXX()
            List<Point> list = q.ToList();  //ToList是方法會執行迴圈 = foreach (...in q..) {......}

            this.dataGridView1.DataSource = list;

            //==================
            //Chart
            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X";  //Point X屬性
            this.chart1.Series[0].YValueMembers = "Y"; //Point Y屬性
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; //ChartType可自行更換 例:Line->Column

            //也可點選圖->屬性表去修改
            this.chart1.Series[0].Color = Color.LightSalmon;
            this.chart1.Series[0].BorderWidth = 4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int n = 100;
            //var n1 = 200; var儘量在寫不出型別時才用 因為可讀性不佳

            string[] names = { "aaa", "Apple", "xxxApple", "pineapple", "bbb", "ccccccc" };

            IEnumerable<string> q = from s in names
                                    where s.Length > 4 && s.ToUpper().Contains("APPLE") //.ToLower().Contains("apple");
                                    orderby s
                                    select s;
            foreach (string s in q)
            {
                this.listBox1.Items.Add(s);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //找到來源物件 this.nwDataSet1.Products 並fill到記憶體
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            //linq運算式
            var q = from p in this.nwDataSet1.Products
                    where p.UnitPrice > 30 && p.ProductName.StartsWith("M")
                    select p;

            //ToXXX
            this.dataGridView1.DataSource = q.ToList();

            //new class1.class2() 巢狀
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);

            IEnumerable<NWDataSet.OrdersRow> q = from o in this.nwDataSet1.Orders
                                                 where o.OrderDate.Year == 1997  //判斷是否一樣要寫兩個=
                                                 orderby o.OrderDate.Month descending
                                                 select o;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            IEnumerable<int> q = from n in nums
                                 where n % 2 == 0
                                 select n;
            //nums.Where<>(...);也是泛用的方法?
            //public static System.Collections.Generic.IEnumerable<TSource> Where<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, bool> predicate) 要做delegate委派
            //System.Linq.Enumerable 的成員


        }
    }
}
