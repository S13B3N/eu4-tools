using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EUProvinceEditor
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault ( false );
            Application.Run (new Gui.MainWnd());
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDPIAware();
    }
}
