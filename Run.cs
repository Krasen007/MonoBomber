namespace MonoContra
{
    using System;
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Run
    {
        public static EntryPoint Game { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            using (var game = new EntryPoint())
            {
                Game = game;
                game.Run();
            }
        }
    }
#endif
}
