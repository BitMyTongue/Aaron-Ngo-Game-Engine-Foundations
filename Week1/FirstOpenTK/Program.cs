using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace FirstOpenTK
{
    class Program
    {
        static void Main(String[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            } 
        }
    }
}