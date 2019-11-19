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

        private void Form1_Load(object sender, EventArgs e)
        {
            //timer1.Start();
            //timer2.Start();
            
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

        //读取烘干炉TNV出口温度 烟囱温度 车身计数
        public void updateOvenInfo()
        {
            ed1exit.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.46", 0, 3, 294, 0));
            ed1chimney.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.46", 0, 3, 294, 0));
            ed1body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.46", 0, 3, 294, 0));
            
            ed2exit.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.54", 0, 3, 294, 0));
            ed2chimney.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.54", 0, 3, 294, 0));
            ed2body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.54", 0, 3, 294, 0));

            pvcexit.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.98", 0, 3, 294, 0));
            pvcchimney.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.98", 0, 3, 294, 0));
            pvcbody.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.140.98", 0, 3, 294, 0));

            p1exit.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.38", 0, 3, 294, 2));
            p1chimney.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.38", 0, 3, 294, 4));
            p1body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.38", 0, 3, 294, 0));

            p2exit.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.46", 0, 3, 294, 0));
            p2chimney.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.46", 0, 3, 294, 0));
            p2body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.46", 0, 3, 294, 0));

            tc1exit.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.82", 0, 3, 294, 0));
            tc1chimney.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.82", 0, 3, 294, 0));
            tc1body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.82", 0, 3, 294, 0));

            tc2exit.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.126", 0, 3, 294, 0));
            tc2chimney.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.126", 0, 3, 294, 0));
            tc2body.Text = System.Convert.ToString(operatePlc.getPlcDbwValue("10.228.141.126", 0, 3, 294, 0));


        }

        //计时器-1 每10分钟采集一次数据
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 600000;
            updateTodayGasValue();
            ovenInfoToDatabase();
        }
        //计时器-2 每1秒执行一次
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 1000;//执行间隔时间,单位为毫秒;此时时间间隔为1秒 
            insert_data_to_database();//定点数据插入数据库
            
        }

        //定点操作数据库
        public void operate_database()
        {          
            string str_sqlstr = "insert into GAS_OVEN_DAILY values('" + riqi + "','" + shijian + "','" + gased1.Text + "','" + gased2.Text + "','" + gaspvc.Text + "','" + gasp1.Text + "','" + gasp2.Text + "','" + gastc1.Text + "','" + gastc2.Text + "') ";
            operateDatabase.OrcGetCom(str_sqlstr);         
        }

        //将日期时间 TNV出口温度 烟囱温度 记录到数据库中
        public void ovenInfoToDatabase()
        {
            string vt126_sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','VT126','" + ed1exit.Text + "','" + ed1chimney.Text + "','" + ed1body.Text + "') ";
            operateDatabase.OrcGetCom(vt126_sqlstr);
            string vt127_sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','VT127','" + ed2exit.Text + "','" + ed2chimney.Text + "','" + ed2body.Text + "') ";
            operateDatabase.OrcGetCom(vt127_sqlstr);
            string vt218_sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','VT218','" + pvcexit.Text + "','" + pvcchimney.Text + "','" + pvcbody.Text + "') ";
            operateDatabase.OrcGetCom(vt218_sqlstr);
            string vt226_sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','VT226','" + p1exit.Text + "','" + p1chimney.Text + "','" + p1body.Text + "') ";
            operateDatabase.OrcGetCom(vt226_sqlstr);
            string vt227_sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','VT227','" + p2exit.Text + "','" + p2chimney.Text + "','" + p2body.Text + "') ";
            operateDatabase.OrcGetCom(vt227_sqlstr);
            string vt257_sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','VT257','" + tc1exit.Text + "','" + tc1chimney.Text + "','" + tc1body.Text + "') ";
            operateDatabase.OrcGetCom(vt257_sqlstr);
            string vt357_sqlstr = "insert into OVEN_INFO values('','" + riqi + "','" + shijian + "','VT357','" + tc2exit.Text + "','" + tc2chimney.Text + "','" + tc2body.Text + "') ";
            operateDatabase.OrcGetCom(vt357_sqlstr);

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
