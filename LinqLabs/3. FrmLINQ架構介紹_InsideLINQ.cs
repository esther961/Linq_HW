﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();

            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //非泛用(不能當成來源物件 要轉型
            System.Collections.ArrayList arrlist = new System.Collections.ArrayList();
            arrlist.Add(12);
            arrlist.Add(3);

            var q = from n in arrlist.Cast<int>() //角括號不能省略 因為無法自動推斷型別
                    where n > 3
                    select new { N = n };

            this.dataGridView1.DataSource = q.ToList(); //看不到結果是因為沒有屬性 所以投射匿名型別select new { n };

            //DataSet
            //    Datatable
            //int[] nums = { 1, 2, 3 };
            //nums.Where

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.productsTableAdapter1.Fill(this.nwDataSet1.Products); 在上面建構子用過就好了

            var q = from p in this.nwDataSet1.Products
                    where p.UnitPrice > 20
                    orderby p.UnitsInStock descending
                    select p;
                    //select p.Take(5);

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = {1,2,3,4,5,6,7,8,9,10};

            var q = nums.Where(n => n > 5);

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //When execute query
            //1. foreach
            //2. ToXXX()
            //3. Aggregation Sum()...

            int[] nums = {1,2,3,4,5,6,7,8,9,10,11};

            this.listBox1.Items.Add("Sum = " + nums.Sum());
            this.listBox1.Items.Add("Max = " + nums.Max());
            this.listBox1.Items.Add("Min = " + nums.Min());
            this.listBox1.Items.Add("Avg = " + nums.Average());
            this.listBox1.Items.Add("Count = " + nums.Count());

            //nums.Median() <-沒有這個 但python有Median跟Mean

            //=======================
            this.listBox1.Items.Add("Avg UnitInStock = "+this.nwDataSet1.Products.Average(p => p.UnitsInStock));
            this.listBox1.Items.Add("Max UnitPrice = " + this.nwDataSet1.Products.Max(p => p.UnitPrice));
            //this.listBox1.Items.Add("Max UnitPrice = " + this.nwDataSet1.Products.Max(p => $"{p.UnitPrice:c2}")); //數字出來不一樣
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //
            int[] nums = new int[10] {1,2,3,4,5,6,7,8,9,10};

            int i = 0;
            var q = from n in nums
                    select ++i;
            //foreach 執行query 未執行前i=0
            foreach(var v in q)
            {
                listBox1.Items.Add(string.Format("v={0}, i ={1}", v, i));
            }
            listBox1.Items.Add("====================");

            //=======
            i= 0;
            var q1 = (from n in nums
                      select ++i).ToList();  //因為ToList會執行所以i已經是10

        }
    }
}

//public static class MyLinqExtention
//{
//    public static IEnumerable<T> MyWhere <T>(this IEnumerable<T> source, Func<T, bool> predicate)
//    {
//        return source.Where(predicate);
//    }
//}