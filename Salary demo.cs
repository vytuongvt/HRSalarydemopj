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
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            string ConString = "Data Source=XE;User Id=hr;Password=hr";
            using (OracleConnection objConn = new OracleConnection(ConString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "P_DEMO1"; // tên Procedure (tạo từ Oracle)
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("v_in_department_id", OracleDbType.Int32).Value = Int32.Parse(txtIn.Text);
                objCmd.Parameters.Add("v_out_max_sal", OracleDbType.Int32).Direction =
                ParameterDirection.Output;
                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    txtOutput.Text = objCmd.Parameters["v_out_max_sal"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                objConn.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
