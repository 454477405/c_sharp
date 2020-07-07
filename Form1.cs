using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;//延时函数要用
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
namespace 画矩形
{

    public partial class Form1 : Form
    {
        public static string lujing1;
        public static string lujing;
        string str_exe = @"D:\c#代码\考研背诵机1\bin\Debug\考研背诵机.exe";
        bool hua=true;
        bool bDrawStart = false;
        Point pointStart = Point.Empty;
        Point pointContinue = Point.Empty;
        Dictionary<Point, Point> dicPoints;
        public static Bitmap bit;
        Control con = new Control();
        Color color1;
        string comboxindex;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboxindex=File.ReadAllText(Application.StartupPath + "\\comboboxindex.txt");
            comboBox1.SelectedIndex = Int32.Parse(comboxindex);
            lujing = @"C:\Users\yc\Desktop\考研\" + comboBox1.SelectedItem.ToString()+ "\\";
            lujing1 = @"C:\Users\yc\Desktop\考研\" + comboBox1.SelectedItem.ToString() + "1\\";
            color1 = TransparencyKey;
            SetProcessDPIAware();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            dicPoints = new Dictionary<Point, Point>();
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.MouseDown += new MouseEventHandler(frmMain_MouseDown);
            this.MouseMove += new MouseEventHandler(frmMain_MouseMove);
            this.MouseUp += new MouseEventHandler(frmMain_MouseUp);
            this.MouseClick += new MouseEventHandler(frmMain_MouseClick);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
            this.Paint += new PaintEventHandler(frmMain_Paint);
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
           Color Mycolor = System.Drawing.Color.FromArgb(255, Color.Black);
            Pen pen = new Pen(Mycolor, 2f);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;           
            if (bDrawStart)
            {
                //实时的画矩形
                 int w = pointContinue.X - pointStart.X;
                int h = pointContinue.Y - pointStart.Y;
                Rectangle rect = new Rectangle(pointStart, new Size(w, h));
                e.Graphics.DrawRectangle(pen, rect);
            }
            pen.Dispose();
        }

        void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            hua = true;
            bDrawStart = true;
            pointStart = e.Location;
        }
        void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDrawStart&&hua)
            {
                pointContinue = e.Location;
                //Refresh();
                Invalidate();
            }
        }
        void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                return;
                hua = false;
            if (bDrawStart)
                dicPoints.Add(pointStart, pointContinue);
            button1.Show();
            button2.Show();
            int a = pointContinue.X - pointStart.X;
            int b = pointContinue.Y - pointStart.Y;
            if (a > 0 && b > 0)
                bit = new Bitmap(a, b);
            Graphics g;
            if (bit != null)
            {
                g = Graphics.FromImage(bit);
                g.CopyFromScreen(pointStart, new Point(0, 0), bit.Size);
                g.Dispose();
            }
            if (checkBox1.CheckState == CheckState.Checked)
            {
                Form myForm = new Form2();
                myForm.ShowDialog();
            }
            else
            {
                Button5_Click(sender, e);
            }

        }
        
        private void ReadyToCaptrue()
        {
            Cursor = Cursors.Cross;
            this.Opacity = 0.1;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            button1.Hide();
            button2.Hide();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            ReadyToCaptrue();
        }
    [DllImport("user32.dll")]
    public static extern bool SetProcessDPIAware();

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Form2.dakai)
                System.Diagnostics.Process.Start("explorer",lujing1 ); 
            else
                System.Diagnostics.Process.Start("explorer", lujing);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Unchecked)
                Clipboard.SetImage(bit);
            this.WindowState = FormWindowState.Normal;
            this.Opacity = 1;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            button1.Show();
            button2.Show();

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start(str_exe, $"{lujing} {lujing1} {comboBox1.SelectedItem.ToString()}");
        }
        private void Form1_MouseWheel(object sender, EventArgs e)
        {
            // TransparencyKey = BackColor;
            WindowState = FormWindowState.Minimized;
            Thread.Sleep(1000);
            // TransparencyKey =color1;
            WindowState = FormWindowState.Maximized;
        }

          private void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
               
                Button5_Click(sender, e);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            string str = "";
            if (checkBox2.CheckState==CheckState.Unchecked)
                 str = lujing;
            else
                str= lujing1;
            filesort.sort(str);
            File.WriteAllText(@"D:\c#代码\考研背诵机1\bin\Debug\最近.txt", "");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\comboboxindex.txt", comboBox1.SelectedIndex.ToString());
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lujing = @"C:\Users\yc\Desktop\考研\" + comboBox1.SelectedItem.ToString() + "\\";
            lujing1 = @"C:\Users\yc\Desktop\考研\" + comboBox1.SelectedItem.ToString() + "1\\";
        }
    }
}