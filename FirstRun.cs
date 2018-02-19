namespace Start
{
    using System;
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class FirstRun
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            using (var game = new Start())
            {
                game.Run();
            }
        }
    }
#endif
}
