using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

  

namespace TextureCreate
{      
    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsWindowEnabled(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Launch a texture create form
            // NOTE: Only allowing a single instance of the process
            bool firstInstance;
            Mutex m = new Mutex(true, "texture_create", out firstInstance);
            if (firstInstance)
            {
                Application.Run(new mainForm());
            }
            else
            {  
                // Set focus to the previous instance of the specified program.
        
                // Look for previous instance of this program.
                IntPtr hWnd = FindWindow(null, "Texture Create");

                // If a previous instance of this program was found...
                if (hWnd != null)
                {
                    // Is it displaying a popup window?
                    IntPtr hPopupWnd = GetLastActivePopup(hWnd);

                    // If so, set focus to the popup window. Otherwise set focus
                    // to the program's main window.
                    if (hPopupWnd != null && IsWindowEnabled(hPopupWnd))
                    {
                        hWnd = hPopupWnd;
                    }

                    SetForegroundWindow(hWnd);

                    // If program is minimized, restore it.
                    if (IsIconic(hWnd))
                    {
                        ShowWindow(hWnd, SW_RESTORE);
                    }
                }
            }
        }
    }
}
