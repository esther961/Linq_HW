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
    //public static System.Collections.Generic.IEnumerable<TSource> Where<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, bool> predicate)
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }
        //Notes: LINQ 主要參考 
        //組件 System.Core.dll,
        //namespace {}  System.Linq
        //public static class Enumerable

        //public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

        //1. 泛型 (泛用方法)                                          (ex.  void SwapAnyType<T>(ref T a, ref T b)
        //2. 委派參數 Lambda Expression (匿名方法簡潔版)               (ex.  MyWhere(nums, n => n %2==0);
        //3. Iterator                                                (ex.  MyIterator)
        //4. 擴充方法                                                 (ex. WordCount() Chars(2)
        //
        private void button4_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);
            
            Swap(ref n1,ref n2); //reference傳址

            MessageBox.Show(n1 + "," + n2);

            //=============================

            string s1 ="aaa", s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);

            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }

        //只有依賴參數所以寫靜態 靜態的東西用類別去點 例如:FrmLangForLINQ.Swap(ref n1,ref n2);
        //因為overload(多載)所以可以兩個都叫Swap
        private static void Swap(ref int n1 ,ref int n2)
        {
            int temp = n2;
            n2 = n1;
            n1 = temp;
        }
        private static void Swap(ref string n1, ref string n2)
        {
            string temp = n2;
            n2 = n1;
            n1 = temp;
        }

        //寫object 如要轉型會耗效能
        private static void Swapxxx(ref object n1, ref object n2)
        {
            object temp = n2;
            n2 = n1;
            n1 = temp;
        }

        //型別當參數傳 T為型別參數
        private static void SwapAnyType<T>(ref T n1, ref T n2)
        {
            T temp = n2;
            n2 = n1;
            n1 = temp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);

            SwapAnyType<int>(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);
            //=============================
            string s1 = "aaa", s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);

            // SwapAnyType<string>(ref s1, ref s2)
            SwapAnyType(ref s1, ref s2); //角括號不寫也可推斷型別 如果推斷不出會顯示錯誤
            MessageBox.Show(s1 + "," + s2);

        }

        //用委派演進按鈕去控制buttonX(執行時先按一次委派演進按鈕 如果直接按buttonX會是null
        private void button2_Click(object sender, EventArgs e)
        {
            //具名方法
            //this.buttonX.Click += new EventHandler(ButtonX_Click); 完整寫法
            this.buttonX.Click += ButtonX_Click;

            //錯誤 CS0123 : 'aaa' 沒有任何多載符合委派 'EventHandler'
            //沒有給(object sender, EventArgs e)參數就會跑出以上錯誤
            this.buttonX.Click += aaa;

            //=======================
            //C# 2.0 匿名方法(其他地方沒有要用到 又很簡短時使用
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
            {
                MessageBox.Show("匿名方法");
            };

            //=======================
            //匿名方法 C# 3.0 lambda =>(goes to的符號
            this.buttonX.Click += (object sender1, EventArgs e1) =>
            {
                MessageBox.Show("匿名方法 簡潔版");
            };
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX click");
        }

        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }



        bool Test (int n)
        {
            return n > 5;
        }
        //判斷偶數
        bool IsEven(int n)
        {
            return n %2 == 0;
        }
        //判斷奇數
        bool IsOdd(int n)
        {
            return n % 2 == 1;
        }

        //Step 1: create delegate class 建型別
        //Step 2: create delegate object 建物件 指向方法
        //Step 3: Invoke method / call method 呼叫方法

        //Step 1
        delegate bool MyDelegate(int n);

        private void button9_Click(object sender, EventArgs e)
        {
            bool result;
            result = Test(10);

            //======================

            //Step 2
            MyDelegate delegateObj = new MyDelegate(Test); //完整寫法
            //Step 3
            result = delegateObj(7);

            //======================

            delegateObj = IsEven;
            //result = delegateObj(7);
            result = delegateObj.Invoke(7);

            MessageBox.Show("result = " + result);

            //======================
            delegateObj = IsOdd;
            result = delegateObj.Invoke(7);

            MessageBox.Show("result = " + result);
            //=====以上為具名方法=====

            //=====以下為匿名方法=====
            //C# 2.0
            delegateObj = delegate (int n)
            {
                return n > 5;
            };

            result = delegateObj(3);
            MessageBox.Show("result = " + result);

            //======================
            //C# 3.0 lambda =>(goes to的符號
            //lambda運算式是建立委派最簡單的方法 (參數型別也沒寫/return也沒寫 -> 非常高階的抽象
            delegateObj = n => n > 5;
            result = delegateObj(8); //寫完要記得call方法

            MessageBox.Show("result = " + result);
        }

        //MyWhere方法中第一個參數int陣列 第二個參數委派
        List<int> MyWhere(int[] nums, MyDelegate delegateObj)
        {
            List<int> list = new List<int>();

            foreach (int n in nums)
            {
                //判斷 傳進來的委派 int的n要不要放進來
                if (delegateObj(n))
                {
                    list.Add(n);
                }
            }
            return list;

        }

        int Add(int n1, int n2)
        {
            return n1+ n2;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 101 };
            MyWhere(nums, Test);
            //MyWhere(nums, Test2); 因為第二個是委派所以不能再寫Test

            List<int> largeList = MyWhere(nums, n => n > 5);
            List<int> EvenList = MyWhere(nums, n => n%2==0);
            List<int> OddList = MyWhere(nums, n => n%2==1);

            //呈現資料 跑迴圈
            foreach (int n in EvenList)
            {
                this.listBox1.Items.Add(n);
            }
            foreach (int n in OddList)
            {
                this.listBox2.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            //IEnumerable<int> q = from n in nums
            //                     where n > 5
            //                     select n;

            //nums.Where<int>(n => n > 5); <int>可省略 因為是泛用方法
            IEnumerable<int> q = nums.Where(n => n > 5); //Where也是要傳委派 判斷nums(來源物件)中的n有沒有大於5

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

            //======================

            string[] words = { "aaa", "bbbb", "ccccc" };

            IEnumerable<string> q2 = words.Where<string>(w => w.Length > 3);

            foreach (string s in q2)
            {
                this.listBox2.Items.Add(s);
            }
        }


        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    yield return n;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            IEnumerable<int> q = MyIterator(nums, n => n>5);

            foreach(int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bool b;
            b = true;
            b = false;

            //======================
            bool? result; //可為空值 可設null
            result = true;
            result = false;
            result = null;

            //if(result.HasValue) 判斷是否為空值
        }

        //var只能出現在區域變數(?
        //var x = 100;
        private void button45_Click(object sender, EventArgs e)
        {
            //var 型別難寫
            //var for 匿名型別
            int n = 100;
            var n1 = 200;
            var s = "abc";
            s=s.ToUpper();
            MessageBox.Show(s);

            var p = new Point(100, 100);
            MessageBox.Show(p.X + "," + p.Y);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            //Point pt1 = new Point(1, 1);
            Font f = new Font("Arial", 14);
            //======================

            MyPoint pt1 = new MyPoint(9,9);
            
            //MyPoint pt1 = new MyPoint();
            //pt1.P1 = 100; //呼叫set
            //pt1.P2 = 200; //呼叫set

            List<MyPoint> list = new List<MyPoint>();
            list.Add(pt1);
            list.Add(new MyPoint(11));   //constructor() 建構子方法
            list.Add(new MyPoint(22,22));

            //物件初始化的方式
            list.Add(new MyPoint { P1 = 33, P2 = 33, Field1 = 33 }); //用的是{}大括號
            list.Add(new MyPoint { P1 = 1111 });

            //dataGridView會找有屬性的 沒有屬性會繫結不上 因此Field1.Field2不會顯示
            this.dataGridView1.DataSource = list;

            //======================
            List<MyPoint> list2 = new List<MyPoint>
                                    {   //大括號集合
                                        //大括號物件初始化
                                        new MyPoint{P1=3,P2=3},
                                        new MyPoint{P1=31,P2=3},
                                        new MyPoint{P1=33,P2=3},
                                        new MyPoint{P1=3444,P2=3}
                                    };
            this.dataGridView2.DataSource = list2;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //想要幾個P屬性可自己設定
            var pt1 = new { P1 = 100, P2 = 200, P3=999 }; //匿名型別 有三個屬性
            var pt2 = new { P1 = 100, P2 = 200, P3 = 999 };
            var pt3 = new { X = 7, Y = 8 };

            MessageBox.Show(pt1.P2.ToString());
            MessageBox.Show(pt3.X.ToString());

            //pt1.P2 = 999;
            //SystemInformation.ComputerName = "xxx";

            //======================

            this.listBox1.Items.Add(pt1.GetType());
            this.listBox1.Items.Add(pt2.GetType());
            this.listBox1.Items.Add(pt3.GetType());

            //======================

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            //匿名型別寫var
            //var q = from n in nums
            //        where n > 5
            //        select new { N = n, Square = n * n, Cube = n * n * n };
            //上面改寫成方法 Where委派 select投射
            var q = nums.Where(n => n > 5).Select(n => new { N = n, Square = n * n, Cube = n * n * n });

            this.dataGridView1.DataSource = q.ToList(); //ToList:每個物件列舉出來並放到list裡

            //======================
            //北風資料庫產品為例 先拉工具箱 準備來源 投射
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q2 = from p in this.nwDataSet1.Products
                     where p.UnitPrice > 30
                     select new
                     {
                         ID = p.ProductID,
                         名稱 = p.ProductName,
                         p.UnitPrice,
                         p.UnitsInStock,
                         //格式化小數點
                         TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
                     };

            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            //Point[] pts = new Point[]
            //{

            //}
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string s = "abcde";
            int count = s.WordCount();

            MessageBox.Show("count = " + count);

            //======================
            string s1 = "123456789";
            MessageBox.Show("s1 count = " + s1.WordCount());
            //======================

            char ch = s1.Chars(2);
            MessageBox.Show("ch = " + ch);

            //擴充方法 靜態
            //s1.Chars(2)
            ch = MyStringExtend.Chars(s1, 2);
            //?? 嚴重性上面有一段去複製
            MessageBox.Show("ch =" + ch);
        }
    }
}

public static class MyStringExtend
{
    public static int WordCount(this string s) //this表某個物件點進來 例s1.s2...
    {
        return s.Length;
    }

    public static char Chars(this string s, int index)
    {
        return s[index]; //索引 中括號
    }
}

public class MyPoint
{
    public MyPoint()
    {

    }
    public MyPoint(int p1)
    {
        //this代表MyPoint物件(看是誰call的
        this.P1 = p1;
    }
    public MyPoint(int p1,int p2)
    {
        this.P1 = p1;
        this.P2 = p2;
    }


    private int m_p1;
    public int P1
    {
        get
        {
            //logic...(一般運算邏輯
            return m_p1;
        }
        set
        {
            //logic...value...
            //設進來的值通常用私有變數來儲存
            m_p1 = value;
        }
    }

    public int P2 { get; set; }

    //class Variable(類別變數 非屬性
    public int Field1 = 999;
    public int Field2 = 888;
}
