using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            string tempdirectory = Path.GetTempPath();
            string[] tempdata = Directory.GetFiles(tempdirectory);

            foreach (string file in tempdata)
            {
                if (file.EndsWith(".tmp") || file.EndsWith(".dat") || file.EndsWith(".log"))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
