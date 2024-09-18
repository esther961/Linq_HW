using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        NorthwindEntities dbContext = new NorthwindEntities();

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        List<Student> students_scores = new List<Student>()
                  {
                  new Student { Name = "Aaron", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                  new Student { Name = "Ben", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                  new Student { Name = "Ashley", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                  new Student { Name = "Daniel", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Male" },
                  new Student { Name = "Eric", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                  new Student { Name = "Jessica", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                  new Student { Name = "Leo", Class = "CS_101", Chi = 80, Eng = 75, Math = 80, Gender = "Male" },
                  new Student { Name = "Anna", Class = "CS_102", Chi = 90, Eng = 95, Math = 95, Gender = "Female" },
                  new Student { Name = "Sophia", Class = "CS_101", Chi = 75, Eng = 80, Math = 85, Gender = "Female" },
                  new Student { Name = "Emily", Class = "CS_102", Chi = 90, Eng = 85, Math = 75, Gender = "Female" }
                  };

        public Frm作業_3()
        {
            InitializeComponent();

            //hint
            //List<> students_scores = new List<Student>()
            //      {
            //      new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
            //      new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
            //      new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
            //      new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
            //      new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
            //      new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
            //      };
        }
    
        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 共幾個 學員成績 ?						
            var q = from scores in students_scores
                    select scores;
            this.dataGridView1.DataSource = students_scores;
            MessageBox.Show($"共{q.Count().ToString()}個學員成績");

            // 找出 前面三個 的學員所有科目成績

            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績

            // 數學不及格 ... 是誰 
            #endregion
        }

        private void button37_Click(object sender, EventArgs e)
        {
            // 個人 sum, min, max, avg
            var studentscores1 = from s in students_scores
                                 select new
                                 {
                                     s.Name,
                                     s.Class,
                                     Sum = s.Chi + s.Eng + s.Math,         // 總和
                                     Min = new[] { s.Chi, s.Eng, s.Math }.Min(),  // 最小值
                                     Max = new[] { s.Chi, s.Eng, s.Math }.Max(),  // 最大值
                                     Avg = (s.Chi + s.Eng + s.Math) / 3.0      // 平均值
                                 };

            // 統計 每個學生個人成績 並排序
            //var studentscores2 = studentscores1.OrderByDescending(s => s.Sum).ToList();
            //this.dataGridView1.DataSource = studentscores2;

            this.dataGridView1.DataSource = studentscores1.OrderByDescending(s => s.Sum).ToList();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            var groups = from s in students_scores
                         let avgScore = (s.Chi + s.Eng + s.Math) / 3.0 // 計算平均分數
                         group s by avgScore >= 90 ? "優良" :
                                    avgScore >= 70 ? "佳" : "待加強" into g
                         select new
                         {
                             GroupName = g.Key,
                             Students = g.OrderByDescending(s => (s.Chi + s.Eng + s.Math) / 3.0) // 按平均分數降序排列
                         };

            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            var score = groups.SelectMany(g => g.Students.Select(s => new
            {
                Group = g.GroupName,
                s.Name,
                AvgScore = (s.Chi + s.Eng + s.Math) / 3.0 // 計算平均分數
            })).ToList();

            this.dataGridView1.DataSource = score;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var f = from file in files
                    group file by file.Length > 1000000 ? "超過 1,000,000 bytes" :
                                  file.Length >= 100000 ? "100,000 - 1,000,000 bytes" :
                                  "小於 100,000 bytes" into sizeGroup
                    orderby sizeGroup.Key descending
                    select new
                    {
                        SizeGroup = sizeGroup.Key, // 分組標籤
                        Files = sizeGroup.OrderByDescending(file => file.Length) // 每組內依檔案大小降序排列
                    };

            var displayData = f.SelectMany(g => g.Files.Select(file => new
            {
                Group = g.SizeGroup,
                FileName = file.Name,
                Size = file.Length
            })).ToList();

            this.dataGridView1.DataSource = displayData;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var f = from file in files
                    group file by file.CreationTime.Year < 2018 ? "2018年以前" :
                                  file.CreationTime.Year >= 2018 && file.CreationTime.Year <= 2022 ? "2018 ~ 2022年" :
                                    "大於2022年" into yearGroup
                    orderby yearGroup.Key descending
                    select new
                    {
                        YearGroup = yearGroup.Key, // 分組標籤
                        Files = yearGroup.OrderByDescending(file => file.CreationTime) // 每組內依檔案建立時間降序排列
                    };

            var displayData = f.SelectMany(g => g.Files.Select(file => new
            {
                Group = g.YearGroup,
                FileName = file.Name,
                CreationDate = file.CreationTime
            })).ToList();

            this.dataGridView1.DataSource = displayData;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    group p by p.UnitPrice < 10 ? 0 : // 低價產品
                               p.UnitPrice <= 100 ? 1 : // 中價產品
                               2 into priceGroup // 高價產品
                    orderby priceGroup.Key
                    select new
                    {
                        PriceGroup = priceGroup.Key == 0 ? "低價產品" :
                                     priceGroup.Key == 1 ? "中價產品" : "高價產品",
                        Products = priceGroup.OrderByDescending(p => p.UnitPrice) // 每組內按價格降序排列
                    };

            var displayData = q.SelectMany(g => g.Products.Select(p => new
            {
                Group = g.PriceGroup,
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice
            })).ToList();

            this.dataGridView1.DataSource = displayData;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year into yearGroup
                    orderby yearGroup.Key descending
                    select new
                    {
                        Year = yearGroup.Key,
                        OrdersCount = yearGroup.Count(), // 每年訂單數量
                        Orders = yearGroup.OrderBy(o => o.OrderDate) // 每年內按訂單日期排序
                    };

            var displayData = q.SelectMany(g => g.Orders.Select(o => new
            {
                Year = g.Year,
                OrderID = o.OrderID,
                OrderDate = o.OrderDate,
                CustomerID = o.CustomerID,
            })).ToList();

            this.dataGridView1.DataSource = displayData;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var q = from o in this.dbContext.Orders
                    group o by new { Year = o.OrderDate.Value.Year, Month = o.OrderDate.Value.Month } into yearMonthGroup
                    orderby yearMonthGroup.Key.Year descending, yearMonthGroup.Key.Month descending
                    select new
                    {
                        Year = yearMonthGroup.Key.Year,
                        Month = yearMonthGroup.Key.Month,
                        OrdersCount = yearMonthGroup.Count(),
                        Orders = yearMonthGroup.OrderBy(o => o.OrderDate)
                    };

            var displayData = q.SelectMany(g => g.Orders.Select(o => new
            {
                Year = g.Year,
                Month = g.Month,
                OrderID = o.OrderID,
                OrderDate = o.OrderDate,
                CustomerID = o.CustomerID,
            })).ToList();

            this.dataGridView1.DataSource = displayData;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var totalSales = this.dbContext.Order_Details
                                .Sum(od => od.Quantity * od.UnitPrice);

            MessageBox.Show($"總銷售額: {totalSales:C}", "總銷售額");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var topSalesPeople = from od in this.dbContext.Order_Details
                                 join o in this.dbContext.Orders on od.OrderID equals o.OrderID
                                 group new { od.Quantity, o.EmployeeID } by o.EmployeeID into employeeGroup
                                 orderby employeeGroup.Sum(g => g.Quantity) descending // 按總銷售量降序排序
                                 select new
                                 {
                                     EmployeeID = employeeGroup.Key,
                                     TotalQuantity = employeeGroup.Sum(g => g.Quantity)
                                 };

            var topFiveEmployees = topSalesPeople.Take(5).ToList();

            this.dataGridView1.DataSource = topFiveEmployees;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var topFiveProducts = from p in this.dbContext.Products
                                  join c in this.dbContext.Categories on p.CategoryID equals c.CategoryID
                                  orderby p.UnitPrice descending // 按單價降序排序
                                  select new
                                  {
                                      p.ProductID,
                                      p.ProductName,
                                      p.UnitPrice,
                                      c.CategoryName // 類別名稱
                                  };

            var topFiveProductsList = topFiveProducts.Take(5).ToList();

            this.dataGridView1.DataSource = topFiveProductsList;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var price = from p in this.dbContext.Products
                                    where p.UnitPrice > 300
                                    select new
                                    {
                                        p.ProductID,
                                        p.ProductName,
                                        p.UnitPrice
                                    };

            this.dataGridView1.DataSource = price.ToList();
        }
    }
}
