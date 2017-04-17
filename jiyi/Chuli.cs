using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Collections;
using System.IO;

namespace jiyi
{
    class Chuli
    {
        public string [,] mem=new string [1000,4];
        double mul=2;
        double lim=0.8;
        double inia = 0.02;
        public int tn;//总词数
        public int fxcs = 0;//需要复习词数
        double tc = 10000000;//转化为秒
        public Chuli()
        {
            tn = 0;
            ini();
        }
        public void ini()
        {
            MyData ac = new MyData();
            string sql = "select * from words";
            OleDbDataReader dr = ac.select(sql);
            int i = 0;
            while (dr.Read()) {
                mem[i,0] = dr.GetString(0).ToString();
                mem[i,1] = dr.GetString(1).ToString();
                mem[i,2] = dr.GetString(2).ToString();
                mem[i,3] = dr.GetString(3).ToString();
                i++;
            }
            tn = i;
        }
        public int next()
        {
            DateTime dt = DateTime.Now;
            double a = dt.Ticks;
            double ma = 1;
            int mn = 0;
            int i;
            fxcs = 0;
            for (i = 0; i < tn; i++) {
                double t = (a - Convert.ToDouble(mem[i, 3]))/tc;
                double a2 = Convert.ToDouble(mem[i, 2]); 
                double tmp = Math.Exp(-a2 * t);
                if (tmp < ma) {
                    ma = tmp;
                    mn = i;
                }
                if (tmp < lim) fxcs++;
            }
            if (ma >= lim) return -1;
            return mn;
        }
        public void update(int n)
        {
            MyData ac = new MyData();
            string sql = string.Format("update words set a='{0}',t='{1}' where key='{2}' ",mem[n, 2],mem[n,3], mem[n, 0]);
            ac.Update(sql);
        }
        public void yes(int n)
        {
            double a =Convert.ToDouble(mem[n, 2]);
            double t = (DateTime.Now.Ticks - Convert.ToDouble(mem[n, 3])) / tc;
            double a2 = -Math.Log(0.8) / t;
            if (a2 < a) a = a2;
            a = a / mul;
            mem[n, 2] = a.ToString();
            mem[n, 3] = DateTime.Now.Ticks.ToString();
            update(n);
        }
        public void no(int n)
        {
            double a = Convert.ToDouble(mem[n, 2]);
            double t = (DateTime.Now.Ticks - Convert.ToDouble(mem[n, 3])) / tc;
            double a2 = -Math.Log(0.8) / t;
            if (a2 > a) a = a2;
            a = a * mul*4;
            mem[n, 2] = a.ToString();
            mem[n, 3] = DateTime.Now.Ticks.ToString();
            update(n);
        }
        public void add(string key,string val)
        {
            DateTime dt = DateTime.Now;
            mem[tn,3] =dt.Ticks.ToString();
            mem[tn,2] = inia.ToString();
            mem[tn, 0] = key;
            mem[tn, 1] = val;
            MyData ac = new MyData();
            string sql = string.Format("insert into words values('{0}','{1}','{2}','{3}')", key, val,mem[tn,2],mem[tn,3]);
            ac.Update(sql);
            tn++;
        }
        public void alter(string key,string val,int n)
        {
            mem[n, 0] = key;
            mem[n, 1] = val;
            MyData ac = new MyData();
            string sql = string.Format("update words set val='{0}' where key='{1}' ", mem[n, 1], mem[n, 0]);
            ac.Update(sql);
        }
    }
}
