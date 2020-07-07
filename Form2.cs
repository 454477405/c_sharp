using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace 画矩形
{  public partial class Form2 : Form
    {
        public static bool dakai=false;
        bool isEdited = false;
        public Form2()
        {
            InitializeComponent();
            pictureBox1.Image = Form1.bit;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            dakai = true;
            DirectoryInfo TheFolder = new DirectoryInfo(Form1.lujing1);
            int len = TheFolder.GetFiles().Length;
            // string[] a=new string[len];
            //  string aa= "";
            int[] a = new int[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = Int32.Parse(TheFolder.GetFiles()[i].Name.Split('.')[0]);
            }
            for (int i = 0; i < len - 1; i++)
            {
                if (a[i] > a[i + 1])
                    a[i + 1] = a[i];
            }
            string savepath= "";
            if (len==0)
                savepath = Form1.lujing1 + "1" + ".png";
            else
            savepath=Form1.lujing1 +  (a[len - 1] + 1).ToString() + ".png";
            if (isEdited)
            {
                File.Move(Application.StartupPath + "\\buffer.png", savepath);
                isEdited = false;
            }
            
            else
                Form1.bit.Save(savepath, System.Drawing.Imaging.ImageFormat.Png);

            Graphics g = Graphics.FromHwnd(IntPtr.Zero);

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dakai =false;
            DirectoryInfo TheFolder = new DirectoryInfo(Form1.lujing);
            int len = TheFolder.GetFiles().Length;
            int[] a = new int[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = Int32.Parse(TheFolder.GetFiles()[i].Name.Split('.')[0]);
            }
            for (int i = 0; i < len - 1; i++)
            {
                if (a[i] > a[i + 1])
                    a[i + 1] = a[i];
            }
            string savepath = "";
            if (len == 0)
                savepath = Form1.lujing +  "1" + ".png";
            else
                savepath = Form1.lujing +    (a[len - 1] + 1).ToString() + ".png";
            if (isEdited)
            {
                File.Move(Application.StartupPath + "\\buffer.png", savepath);
                isEdited = false;
            }
            else
                Form1.bit.Save(savepath, System.Drawing.Imaging.ImageFormat.Png);

            Graphics g = Graphics.FromHwnd(IntPtr.Zero);

            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            isEdited = true;
            string savepath = Application.StartupPath+ "\\buffer.png";
            Form1.bit.Save(savepath, System.Drawing.Imaging.ImageFormat.Png);
            System.Diagnostics.Process.Start("MsPAint", "buffer.png");
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 按快捷键Ctrl+S执行按钮的点击事件方法
            if (keyData == Keys.A)
            {
                button2.PerformClick();
                return true;
            }
            if (keyData == Keys.F)
            {
                button1.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData); // 其他键按默认处理　
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
