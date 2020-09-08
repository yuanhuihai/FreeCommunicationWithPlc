using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using comWithPlc;
using oracleDatabase;

namespace FreeCommunicationWithPlc
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

         getPlcValues operatePlc = new getPlcValues();
         oracleDatabaseOperate operateDatabase = new oracleDatabaseOperate();

    
        //窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
             timer1.Start();//1s定时器
             timer2.Start();//10分钟定时器

            toolStripStatusLabel3.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabel3.Text = "V" + Application.ProductVersion;
            toolStripStatusLabel4.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabel4.Text = "yuanhuihai@sina.com";
          

        }
        //窗体关闭时执行，窗体后台运行
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                notifyIcon1.Visible = false;
                this.Close();
                this.Dispose();
                Application.Exit();
            }

        }

        private void hideMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();

        }

        //桌面右小角图标
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//判断鼠标的按键
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
        }


     
    
    
        //计时器-1 每1秒执行一次
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;//执行间隔时间,单位为毫秒;此时时间间隔为1秒          

           string riqi = DateTime.Now.ToString("yyyy-MM-dd");
           string shijian = DateTime.Now.ToLongTimeString().ToString();

            insert_data_to_database();//定点数据插入数据库  
 
            toolStripStatusLabel5.Text = DateTime.Now.ToLocalTime().ToString();

        }


        //读取DB294.DBW0的值 今天的天然气消耗量
        public void updateTodayGasValue()
        {

            gased1.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.46", 0, 3, 294, 0));

            gased2.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.54", 0, 3, 294, 0));

            gaspvc.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.98", 0, 3, 294, 0));

            gasp1.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.38", 0, 3, 294, 0));

            gasp2.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.46", 0, 3, 294, 0));

            gastc1.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.82", 0, 3, 294, 0));

            gastc2.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.126", 0, 3, 294, 0));
        }



        //读取烘干炉TNV出口温度 烟囱温度 车身计数
        public void updateOvenInfo()
        {
            ed1exit.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.46", 0, 3, 294, 2));
            ed1chimney.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.46", 0, 3, 294, 4));
            //ed1body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.46", 0, 3, 294, 0));

            ed2exit.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.54", 0, 3, 294, 2));
            ed2chimney.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.54", 0, 3, 294, 4));
            //ed2body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.54", 0, 3, 294, 0));

            pvcexit.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.98", 0, 3, 294, 2));
            pvcchimney.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.98", 0, 3, 294, 4));
            //pvcbody.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.98", 0, 3, 294, 0));

            p1exit.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.38", 0, 3, 294, 2));
            p1chimney.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.38", 0, 3, 294, 4));
            //p1body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.38", 0, 3, 294, 0));

            p2exit.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.46", 0, 3, 294, 2));
            p2chimney.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.46", 0, 3, 294, 4));
            //p2body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.46", 0, 3, 294, 0));

            tc1exit.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.82", 0, 3, 294, 2));
            tc1chimney.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.82", 0, 3, 294, 4));
            //tc1body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.82", 0, 3, 294, 0));

            tc2exit.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.126", 0, 3, 294, 2));
            tc2chimney.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.126", 0, 3, 294, 4));
            //tc2body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.126", 0, 3, 294, 0));
        }

        //读取燃烧室温度 20200907

            public void getBurningTem (){

            label42.Text = System.Convert.ToString(operatePlc.readPlcDbdValue("10.228.140.46", 0, 3, 92, 140)+40);


            label39.Text = System.Convert.ToString(operatePlc.readPlcDbdValue("10.228.140.54", 0, 3, 92, 140)+40);


            label38.Text = System.Convert.ToString(operatePlc.readPlcDbdValue("10.228.140.98", 0, 3, 92, 112)+40);


            label37.Text = System.Convert.ToString(operatePlc.readPlcDbdValue("10.228.141.38", 0, 3, 92, 112)+40);


            label32.Text = System.Convert.ToString(operatePlc.readPlcDbdValue("10.228.141.46", 0, 3, 92, 112)+40);


            label31.Text = System.Convert.ToString(operatePlc.readPlcDbdValue("10.228.141.82", 0, 3, 92, 140)+40);


            label30.Text = System.Convert.ToString(operatePlc.readPlcDbdValue("10.228.141.126", 0, 3, 92, 140)+40);



        }

        //燃烧室温度进入数据库 20200907
        public void burnTmepToDatabase()
        {
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");
            string shijian = DateTime.Now.ToLongTimeString().ToString();
            string sqlstr = "insert into BURNTEMP values('" + label42.Text + "','" + label39.Text + "','" + label38.Text + "','" + label37.Text + "','" + label32.Text + "','" + label31.Text + "','" + label30.Text + "','" + riqi + "','" + shijian + "') ";
            operateDatabase.OrcGetCom(sqlstr);
        }



        //烘干炉预热天然气消耗量
        public void getOvenPreGasInfo()
        {
            preed.Text= System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.46", 0, 3, 294, 6));
            prepvc.Text = System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.140.98", 0, 3, 294, 6));
            pretcp1.Text= System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.38", 0, 3, 294, 6));
            pretcp2.Text= System.Convert.ToString(operatePlc.readPlcDbwValue("10.228.141.46", 0, 3, 294, 6));
        }

        //烘干炉预热天然气记录到数据库
        public void ovenPreInfoToDatabase()
        {
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");
            string shijian = DateTime.Now.ToLongTimeString().ToString();
            string sqlstr = "insert into GAS_OVEN_PRE_DAILY values('" + riqi + "','" + shijian + "','" + preed.Text + "','" + prepvc.Text + "','" + pretcp1.Text + "','" + pretcp2.Text + "') ";
            operateDatabase.OrcGetCom(sqlstr);
            listInfo.Items.Add(riqi + shijian + "烘干炉预热天然气消耗数据记录成功");

        }

        //将日期时间 TNV出口温度 烟囱温度 记录到数据库中
        public void ovenInfoToDatabase()
        {
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");
            string shijian = DateTime.Now.ToLongTimeString().ToString();
            string sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','" + ed1exit.Text + "','" + ed1chimney.Text + "','" + ed2exit.Text + "','" + ed2chimney.Text + "','" + pvcexit.Text + "','" + pvcchimney.Text + "','" + p1exit.Text + "','" + p1chimney.Text + "','" + p2exit.Text + "','" + p2chimney.Text + "','" + tc1exit.Text + "','" + tc1chimney.Text + "','" + tc2exit.Text + "','" + tc2chimney.Text + "') ";
            operateDatabase.OrcGetCom(sqlstr);
        }

        //TNV天然气消耗量记录到数据库中
        public void gasTnvInfoToDatabase()
        {
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");
            string shijian = DateTime.Now.ToLongTimeString().ToString();
            string str_sqlstr = "insert into GAS_OVEN_DAILY values('" + riqi + "','" + shijian + "','" + gased1.Text + "','" + gased2.Text + "','" + gaspvc.Text + "','" + gasp1.Text + "','" + gasp2.Text + "','" + gastc1.Text + "','" + gastc2.Text + "') ";
            operateDatabase.OrcGetCom(str_sqlstr);
            listInfo.Items.Add(riqi + shijian + "TNV天然气消耗数据记录成功");
        }


        //更新水量信息
        public void updateWaterInfo()
        {
            label27.Text = Convert.ToString(operatePlc.readPlcDbwValue("10.228.142.114", 0, 3, 294, 0));//原水流量
            label24.Text = Convert.ToString(Convert.ToInt32(operatePlc.readPlcDbdValue("10.228.142.114", 0, 3, 88, 88)));//RO水流量
            label22.Text = Convert.ToString(operatePlc.readPlcDbdValue("10.228.142.114", 0, 3, 88, 108));//VE水流量
            label7.Text = Convert.ToString(operatePlc.readPlcDbwValue("10.228.142.114", 0, 3, 1200, 2));//海得科RO水量


            label20.Text = Convert.ToString(Convert.ToInt32(operatePlc.readPlcDbdValue("10.228.142.114", 0, 3, 88, 28)));//前处理RO水用量
            label12.Text = Convert.ToString(Convert.ToInt32(operatePlc.readPlcDbdValue("10.228.142.114", 0, 3, 88, 48)));//电泳RO水用量
        }

        //用水信息消耗量记录到数据库中
        public void waterInfoToDatabase()
        {
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");
            string shijian = DateTime.Now.ToLongTimeString().ToString();
            string str_sqlstr = "insert into WATERPLANT values('" + riqi + "','" + shijian + "','" + label27.Text + "','" + label24.Text + "','" + label22.Text + "','" + label7.Text + "','" + label20.Text + "','" + label12.Text + "') ";
            operateDatabase.OrcGetCom(str_sqlstr);
            listInfo.Items.Add(riqi + shijian + "用水数据记录成功");
        }

      

        //天然气计数数据插入数据库
        public void insert_data_to_database()
        {
            if (DateTime.Now.Hour == Convert.ToInt32(23) && DateTime.Now.Minute == Convert.ToInt32(58) && DateTime.Now.Second == Convert.ToInt32(00))
            {
                gasTnvInfoToDatabase();//记录TNV天然气消耗量
                ovenPreInfoToDatabase();//记录预热天然气消耗量到数据库中
                waterInfoToDatabase();//记录水量信息
               

                //重置每天的TNV天然气消耗量
                operatePlc.writePlcDbwValue("10.228.140.46", 0, 3, 294, 0,0);
                operatePlc.writePlcDbwValue("10.228.140.54", 0, 3, 294, 0,0);
                operatePlc.writePlcDbwValue("10.228.140.98", 0, 3, 294, 0,0);
                operatePlc.writePlcDbwValue("10.228.141.38", 0, 3, 294, 0,0);
                operatePlc.writePlcDbwValue("10.228.141.46", 0, 3, 294, 0,0);
                operatePlc.writePlcDbwValue("10.228.141.82", 0, 3, 294, 0,0);
                operatePlc.writePlcDbwValue("10.228.141.126",0, 3, 294, 0,0);
                //重置每天的预热天然气消耗量
                operatePlc.writePlcDbwValue("10.228.140.46", 0, 3, 294, 6,0);
                operatePlc.writePlcDbwValue("10.228.140.98", 0, 3, 294, 6,0);
                operatePlc.writePlcDbwValue("10.228.141.38", 0, 3, 294, 6,0);
                operatePlc.writePlcDbwValue("10.228.141.46", 0, 3, 294, 6,0);
                
                //重置水量信息
                operatePlc.writePlcDbwValue("10.228.142.114", 0, 3, 294, 0, 0);//原水流量清0
                operatePlc.writePlcDbdValue("10.228.142.114", 0, 3, 88, 80, 0);//RO水流量清0
                operatePlc.writePlcDbdValue("10.228.142.114", 0, 3, 88, 24, 0);//前处理RO水流量清0
                operatePlc.writePlcDbdValue("10.228.142.114", 0, 3, 88, 44, 0);//电泳RO水流量清0
                operatePlc.writePlcDbdValue("10.228.142.114", 0, 3, 88, 108, 0);//VE水流量清0

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 600000;//执行间隔时间,单位为毫秒;此时时间间隔为10分钟 
            updateOvenInfo();//更新TNV相关信息
            updateTodayGasValue();//更新今天的天然气消耗量
            getOvenPreGasInfo();//更新烘干炉强冷区预热天然气消耗量
            ovenInfoToDatabase();//TNV出口温度等记录到数据库中
            updateWaterInfo();//更新水量信息
            getBurningTem();//获取燃烧室温度 20200907
            burnTmepToDatabase();//燃烧室温度进入数据库 20200907
        }

    }
}
