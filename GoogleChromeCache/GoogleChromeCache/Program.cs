using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace GoogleChromeCache
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = path + @"\Google\Chrome\User Data\Default\Cache";

            foreach(string filename in Directory.GetFiles(path))
            {
                string name = Path.GetFileName(filename);
                char[] nameinchar = name.ToCharArray();
                if (nameinchar[0] == 'f' && nameinchar[1] == '_')
                {
                    File.Delete(filename);
                }
            }
        }

        #region BrowserDetection
        public static List<Browser> GetBrowsers()
        {
            RegistryKey browserKeys;
            browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
            if (browserKeys == null)
                browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            string[] browserNames = browserKeys.GetSubKeyNames();
            var browsers = new List<Browser>();
            for (int i = 0; i < browserNames.Length; i++)
            {
                Browser browser = new Browser();
                RegistryKey browserKey = browserKeys.OpenSubKey(browserNames[i]);
                browser.Name = (string)browserKey.GetValue(null);
                RegistryKey browserKeyPath = browserKey.OpenSubKey(@"shell\open\command");
                browser.Path = (string)browserKeyPath.GetValue(null).ToString().StripQuotes();
                RegistryKey browserIconPath = browserKey.OpenSubKey(@"DefaultIcon");
                browser.IconPath = (string)browserIconPath.GetValue(null).ToString().StripQuotes();
                browsers.Add(browser);
                if (browser.Path != null)
                    browser.Version = FileVersionInfo.GetVersionInfo(browser.Path).FileVersion;
                else
                    browser.Version = "unknown";
            }
            return browsers;
        }
    }

    class Browser
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string IconPath { get; set; }
        public string Version { get; set; }
    }


    internal static class Extensions
    {
        internal static String StripQuotes(this String s)
        {
            if (s.EndsWith("\"") && s.StartsWith("\""))
            {
                return s.Substring(1, s.Length - 2);
            }
            else
            {
                return s;
            }
        }
    }
}

#endregion
