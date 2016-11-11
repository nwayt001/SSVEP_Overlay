using System;
using SSVEP_Overlay.BCI_Logic.Device_Emulation;

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
            using (Game1 game = new Game1())
            {
                InterceptKeys.Hook();
                game.Run();
                InterceptKeys.Unhook();
            }
        }
    }
#endif
}

