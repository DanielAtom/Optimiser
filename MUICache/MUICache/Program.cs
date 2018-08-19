using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Win32;

namespace MUICache
{
    class Program
    {
        static void Main(string[] args)
        {
            const string MuiCacheKeyPath = @"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\MuiCache";

            using (RegistryKey muiCacheKey = Registry.CurrentUser.OpenSubKey(MuiCacheKeyPath, writable: true))
            {
                string[] keys = muiCacheKey.GetValueNames();

                foreach(string key in keys)
                {
                    if (key.Contains(@"\"))
                    {
                        muiCacheKey.DeleteValue(key);
                    }
                }
            }
        }
    }
}
