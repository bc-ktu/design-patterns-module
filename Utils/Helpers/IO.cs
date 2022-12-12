using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public static class IO
    {
        public static void WriteToFile(string filepath, string text)
        {
            StreamWriter sw = new StreamWriter(filepath, true);
            sw.WriteLine(text);
            sw.Close();
        }

        public static void ClearFile(string filepath)
        {
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine(String.Empty);
            sw.Close();
        }
    }
}
