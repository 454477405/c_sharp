using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
namespace 画矩形
{
    public class filesort
    {
        public static void sort(string str)
        {
            FileInfo[] filearray = new DirectoryInfo(str).GetFiles();
            Comparison<FileInfo> comparison = new Comparison<FileInfo>
    ((FileInfo x, FileInfo y) =>
    {
        if (Int32.Parse(x.Name.Split('.')[0]) < Int32.Parse(y.Name.Split('.')[0]))
            return -1;
        else if (Int32.Parse(x.Name.Split('.')[0]) == Int32.Parse(y.Name.Split('.')[0]))
            return 0;
        else
            return 1;
    });
            List<FileInfo> list = new List<FileInfo>(filearray);
            list.Sort(comparison);
            int index = 0;
            foreach (FileInfo a in list)
            {
                index++;
                a.MoveTo(str + index.ToString() + ".png");
            }
        }
    }
}
