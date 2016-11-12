using System;
using SSVEP_Overlay.BCI_Logic.Device_Emulation;
using System.Windows.Forms;
using SSVEP_Overlay.Forms;

namespace SSVEP_Overlay
{
#if WINDOWS || XBOX
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Start main application GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppSelectionGUI gui = new AppSelectionGUI();
            Application.Run(gui);

            // Start XNA
            using (Game1 game = new Game1(gui.selectedApp))
            {
                InterceptKeys.Hook();
                game.Run();
                InterceptKeys.Unhook();
            }
        }
    }
#endif
}

