using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeCommunicationWithPlc
{
    public partial class gas_history_value : Form
    {
        public gas_history_value()
        {
            InitializeComponent();
        }
        Basic_DataBase_Operate operate = new Basic_DataBase_Operate();

        private void gas_history_value_Load(object sender, EventArgs e)
        {
            string str_sqlstr = "select DA,VT126,VT127,VT218,VT226,VT227,VT257,VT357 from GAS_OVEN_DAILY ";

            string str_sqlTable = "GAS_OVEN_DAILY";


            DataSet myds = operate.OrcGetDs(str_sqlstr, str_sqlTable);

            dataGridView1.DataSource = myds.Tables[str_sqlTable];
        }
    }
}
