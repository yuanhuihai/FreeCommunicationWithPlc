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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GetPlcDBW operatePlc = new GetPlcDBW();
        Basic_DataBase_Operate operateDatabase = new Basic_DataBase_Operate();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            
        }

        public void updateTodayGasValue()
        {
            gased1.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.46", 0, 3, 294, 0));

            gased2.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.54", 0, 3, 294, 0));

            gaspvc.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.98", 0, 3, 294, 0));

            gasp1.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.38", 0, 3, 294, 0));

            gasp2.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.46", 0, 3, 294, 0));

            gastc1.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.82", 0, 3, 294, 0));

            gastc2.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.126", 0, 3, 294, 0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 60000;
            updateTodayGasValue();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 1000;//执行间隔时间,单位为毫秒;此时时间间隔为1秒 
            insert_data_to_database();//定点数据插入数据库
            
        }

        //帮助页面-软件简介
        private void 软件简介ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help helpform = new help();
            helpform.TopLevel = false;
            //清空panel控件的内容
            this.panel1.Controls.Clear();
            //向panel控件中添加修改密码的窗体
            this.panel1.Controls.Add(helpform);
            //panel控件内显示窗体
            helpform.Show();
        }

        //操作数据库
        public void operate_database()
        {
            string riqi = System.DateTime.Now.ToString("yyyy-MM-dd");//定义日期格式
            string shijian = DateTime.Now.ToLongTimeString().ToString();//定义时间格式
            string str_sqlstr = "insert into GAS_OVEN_DAILY values('" + riqi + "','" + shijian + "','" + gased1.Text + "','" + gased2.Text + "','" + gaspvc.Text + "','" + gasp1.Text + "','" + gasp2.Text + "','" + gastc1.Text + "','" + gastc2.Text + "') ";
            operateDatabase.OrcGetCom(str_sqlstr);
            listInfo.Items.Add(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "数据插入成功");

        }

        //定点数据插入数据库
        public void insert_data_to_database()
        {
            if (DateTime.Now.Hour == Convert.ToInt32(23) && DateTime.Now.Minute == Convert.ToInt32(58) && DateTime.Now.Second == Convert.ToInt32(00))
            {
                operate_database();         
                operatePlc.resetPlcDbwValue("10.228.140.46", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.140.54", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.140.98", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.38", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.46", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.82", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.126", 0, 3, 294, 0);
            }
        }

        //显示历史值窗口
        private void 历史值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gas_history_value gas_History_Value = new gas_history_value();
            gas_History_Value.TopLevel = false;
            //清空panel控件的内容
            this.panel1.Controls.Clear();
            //向panel控件中添加修改密码的窗体
            this.panel1.Controls.Add(gas_History_Value);
            //panel控件内显示窗体
            gas_History_Value.Show();
        }
    }
}
