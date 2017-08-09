using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace AddYamdiFlv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // 设置根在桌面
            folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.Desktop;

            // 设置当前选择的路径
            folderBrowserDialog1.SelectedPath = "C:";

            // 允许在对话框中包括一个新建目录的按钮
            folderBrowserDialog1.ShowNewFolderButton = true;

            // 设置对话框的说明信息
            folderBrowserDialog1.Description = "请选择输出目录";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtIn.Text = folderBrowserDialog1.SelectedPath;
                getFile(txtIn.Text);
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            // 设置根在桌面
            folderBrowserDialog2.RootFolder = System.Environment.SpecialFolder.Desktop;

            // 设置当前选择的路径
            folderBrowserDialog2.SelectedPath = "C:";

            // 允许在对话框中包括一个新建目录的按钮
            folderBrowserDialog2.ShowNewFolderButton = true;

            // 设置对话框的说明信息
            folderBrowserDialog2.Description = "请选择输出目录";

            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                txtOut.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        protected void getFile(string paths)
        {
            listView.Items.Clear();            
            DirectoryInfo dirInfo = new DirectoryInfo(paths);
            int nIndex = 0;
            foreach (FileSystemInfo fsi in dirInfo.GetFileSystemInfos())
            {
                FileInfo fi = (FileInfo)fsi;
                ListViewItem item = new ListViewItem();
                item.Text = (nIndex + 1).ToString();
                item.SubItems.Add(fi.Name);
                item.SubItems.Add(fi.Length.ToString());
                item.SubItems.Add("未成功");

                //if (nIndex % 2 == 0)
                //    item.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                //else
                //    item.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);

                listView.Items.Add(item);
                nIndex++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string paths = System.Windows.Forms.Application.StartupPath + "\\" + "yamdi.exe";
            for (int i = 0; i < listView.Items.Count; i++)
            {
                string arguments = "-i " + txtIn.Text + "\\" + listView.Items[i].SubItems[1].Text + " -o " + txtOut.Text + "\\" + listView.Items[i].SubItems[1].Text + "";
                if (startapp(paths, arguments))
                {
                    listView.Items[i].SubItems[3].Text = "成功";
                    listView.Items[i].BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
                }
                Application.DoEvents();
            }
            MessageBox.Show("关键帧添加结束!");
        }

        /// <summary>  
        /// 启动外部应用程序，隐藏程序界面  
        /// </summary>  
        /// <param name="appname">应用程序路径名称</param>  
        /// <param name="arguments">启动参数</param>  
        /// <returns>true表示成功，false表示失败</returns>  
        public bool startapp(string appname, string arguments)
        {
            return startapp(appname, arguments, ProcessWindowStyle.Hidden);
        }  

        /// <summary>  
        /// 启动外部应用程序  
        /// </summary>  
        /// <param name="appname">应用程序路径名称</param>  
        /// <param name="arguments">启动参数</param>  
        /// <param name="style">进程窗口模式</param>  
        /// <returns>true表示成功，false表示失败</returns>  
        public bool startapp(string appname, string arguments, ProcessWindowStyle style)
        {
            bool blnrst = false;
            Process p = new Process();
            p.StartInfo.FileName = appname;//exe,bat and so on  
            p.StartInfo.WindowStyle = style;
            p.StartInfo.Arguments = arguments;
            p.StartInfo.UseShellExecute = true;
            try
            {
                p.Start();
                p.WaitForExit();
                p.Close();
                blnrst = true;
            }
            catch
            {
            }
            return blnrst;
        }  
    }
}
