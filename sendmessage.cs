using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace 画矩形
{
    class sendmessage
    {
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        public static void find1()
        {
            const int BM_CLICK = 0xF5;
            IntPtr maindHwnd = FindWindow(null, "QQ用户登录"); //获得QQ登陆框的句柄 hovertree.com
            if (maindHwnd != IntPtr.Zero)
            {
                IntPtr childHwnd = FindWindowEx(maindHwnd, IntPtr.Zero, null, "登录");   //获得按钮的句柄
                if (childHwnd != IntPtr.Zero)
                {
                    SendMessage(childHwnd, BM_CLICK, 0, 0);     //发送点击按钮的消息
                }
                else
                {
                    MessageBox.Show("没有找到子窗口");
                } 
            }
            else
            {
                MessageBox.Show("没有找到窗口");
            }

        }
    }
}
