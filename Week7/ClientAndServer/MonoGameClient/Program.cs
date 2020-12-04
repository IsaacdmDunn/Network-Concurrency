using System;

namespace MonoGameClient
{
#if WINDOWS || LINUX
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
            Client client = new Client();
            client.Connect("127.0.0.1", 4444);
            client.Run();
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
