using System;

namespace RLIRL_Simulator_OpenGL
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new RLIRL_Simulator.Sim())
                game.Run();
        }
    }
}
