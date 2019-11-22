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

        public string riqi = System.DateTime.Now.ToString("yyyy-MM-dd");//定义日期格式
        public string shijian = DateTime.Now.ToLongTimeString().ToString();//定义时间格式

        //窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        
            
        }
        //窗体关闭时执行，窗体后台运行
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }
        //桌面右小角图标
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;

                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }


        //读取DB294.DBW0的值
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

    
    
        //计时器-1 每1秒执行一次
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;//执行间隔时间,单位为毫秒;此时时间间隔为1秒 
            updateTodayGasValue();
            insert_data_to_database();//定点数据插入数据库
                    
        }

        //定点操作数据库
        public void operate_database()
        {          
            string str_sqlstr = "insert into GAS_OVEN_DAILY values('" + riqi + "','" + shijian + "','" + gased1.Text + "','" + gased2.Text + "','" + gaspvc.Text + "','" + gasp1.Text + "','" + gasp2.Text + "','" + gastc1.Text + "','" + gastc2.Text + "') ";
            operateDatabase.OrcGetCom(str_sqlstr);
            listInfo.Items.Add(riqi + shijian + "数据插入成功");
        }

    

        //天然气计数数据插入数据库
        public void insert_data_to_database()
        {
            if (DateTime.Now.Hour == Convert.ToInt32(23) && DateTime.Now.Minute == Convert.ToInt32(58) && DateTime.Now.Second == Convert.ToInt32(00))
            {
                operate_database();
                //将DB294.DBW0的值清空
                operatePlc.resetPlcDbwValue("10.228.140.46", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.140.54", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.140.98", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.38", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.46", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.82", 0, 3, 294, 0);
                operatePlc.resetPlcDbwValue("10.228.141.126", 0, 3, 294, 0);
            }
        }

    }
}
