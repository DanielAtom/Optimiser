using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prefetch
{
    class Program
    {
        static void Main(string[] args)
        {
                string prefetchdirectory = @"C:\Windows\Prefetch";
                string[] prefetchdata = Directory.GetFiles(prefetchdirectory);

                foreach (string file in prefetchdata)
                {
                    if (file.EndsWith(".pf"))
                    {
                        File.Delete(file);
                    }
                }
        }
    }
}
