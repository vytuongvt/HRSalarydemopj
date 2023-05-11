using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp1
{
    public partial class ListEmployees : Form
    {
        public ListEmployees()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConString = "Data Source=XE;User Id=hr;Password=hr";
            using (OracleConnection objConn = new OracleConnection(ConString))
            {
                dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "Mypackage.P_DEMO2"; 
                // tên Procedure thuộc gói Mypackage(Oracle)
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("v_in_job_id", OracleDbType.Varchar2).Value = txtInput.Text;
                objCmd.Parameters.Add("OutputList", OracleDbType.RefCursor).Direction =
                ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    OracleDataAdapter oda = new OracleDataAdapter(objCmd);
                    DataSet ds = new DataSet();
                    oda.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                objConn.Close();
            }
        }
    }
}
