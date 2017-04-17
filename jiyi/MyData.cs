using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace jiyi
{
    class MyData
    {
        private const string _ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=abcd.mdb";
        OleDbConnection con;
        public MyData()
        {
            try {
                con = new OleDbConnection(_ConString);
                con.Open();
            }
            catch (Exception) {
            }
        }
        public void close()
        {
            con.Close();
        }
        public OleDbDataReader select(string cmdTxt)
        {            
            OleDbCommand cmd = new OleDbCommand(cmdTxt, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            cmd.Dispose();
            return dr;
        }
        public OleDbDataAdapter selectAdapter(string cmdTxt)
        {           
            OleDbDataAdapter da = new OleDbDataAdapter(cmdTxt, con);
            return da;
        }
        public bool Update(string cmdTxt)
        {
            try {
                OleDbCommand cmd = new OleDbCommand(cmdTxt, con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception) {
                return false;
            }
            finally {
                con.Close();
            }
        }
    }
}
