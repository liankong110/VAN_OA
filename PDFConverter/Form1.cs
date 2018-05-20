using System;
using System.Drawing;
using System.Runtime.Remoting;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;

namespace PDFConverter
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

        

        }
        private void show()
        {
            try
            {
                //initialize remoting
                //RemotingConfiguration.Configure("PDFConverter.exe.config", false);
                //RemotingConfiguration.RegisterWellKnownServiceType(new RemoteConverter().GetType(), "RemoteConverter", WellKnownObjectMode.Singleton);


                //check if word 2007 is available
                try
                {
                    object paramMissing = Type.Missing;
                    ApplicationClass wordApplication = new ApplicationClass();
                    wordApplication.Visible = true;
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    lbl_wordinstalled.ForeColor = Color.Green;
                    lbl_wordinstalled.Text = "available";

                }
                catch
                {
                    lbl_wordinstalled.ForeColor = Color.Red;
                    lbl_wordinstalled.Text = "not available";
                }
            }
            catch (Exception ex)
            {
                lbl_wordinstalled.Text = ex.Message;
                //MessageBox.Show(ex.Message);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //var R_startPath = System.Windows.Forms.Application.ExecutablePath;
            //if (checkBox1.Checked)
            //{
            //    try
            //    {
            //        var R_local = Registry.LocalMachine;
            //        var R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            //        if (R_run != null)
            //        {
            //            R_run.SetValue("BirthdayTipF", R_startPath);
            //            R_run.Close();
            //        }
            //        R_local.Close();

            //    }
            //    catch (Exception)
            //    {


            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        var R_local = Registry.LocalMachine;
            //        var R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            //        if (R_run != null)
            //        {
            //            R_run.DeleteValue("BirthdayTipF", false);
            //            R_run.Close();
            //        }
            //        R_local.Close();
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("您需要管理员权限修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //show();
        }
    }
}
