using System;

namespace MidtermGame
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(1920, 1080))
            {
                game.Run();
            }
        }
    }
}
