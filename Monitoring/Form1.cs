using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Monitoring
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            StartPro();
        }

        /// <summary>
        /// 进程是否在运行
        /// </summary>
        /// <returns></returns>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName("PDFConverter");
            foreach (Process process in processes)
            {
                process.Kill();
            }
            return null;
        }

        /// <summary>
        /// 监控 主程序是否启动起来
        /// </summary>
        private void StartPro()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "PDFConverter.exe";
            //var path = "D:\\项目\\OA\\万邦OA\\PDFConverter\\bin\\Debug\\PDFConverter.exe";
            if (File.Exists(path))
            {
                try
                {
                    RunningInstance();
                    var p = new Process { StartInfo = { FileName = path } };
                    p.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(@"TC.SelfServiceTicketMachine文件不存在");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartPro();
        }
    }
}
