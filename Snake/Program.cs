using System;

namespace Snake
{
    internal class Program
    {
        public static bool Running = true;

        private static void Main()
        {
            Console.Clear();
            Renderer.Init();
            var game = new Game();
            game.Init();
            while (Running)
            {
                game.Update();
                game.Draw();
                Renderer.Draw();
            }
        }
    }
}