using System;

namespace Snake.Scenes
{
    public class ExitQuestion
    {
        private readonly Game _parent;
        private int _selectedItem;
        public Game.Scenes Last;
        public bool Result;
        public string Text = "REALLY WANT TO EXIT TO WINDOWS?";

        public ExitQuestion(Game parent)
        {
            _parent = parent;
        }

        public void Update()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if (_selectedItem == 0) _selectedItem = 1;
                        else _selectedItem--;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        if (_selectedItem == 1) _selectedItem = 0;
                        else _selectedItem++;
                        break;
                    case ConsoleKey.Enter:
                        if (_selectedItem == 0)
                        {
                            Result = false;
                            _parent.CurrentScene = Last;
                        }
                        else if (_selectedItem == 1)
                        {
                            _selectedItem = 0;
                            Result = true;
                            _parent.CurrentScene = Last;
                        }
                        break;
                }
            }
        }

        public void Draw()
        {
            if (Text.Length < 20)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 26)/2, (Renderer.WindowSize.Y - 9)/2, 26, 9),
                    ConsoleColor.DarkYellow);
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2, (Renderer.WindowSize.Y - 7)/2, 24, 7),
                    ConsoleColor.Yellow);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - (Text.Length + 4))/2, (Renderer.WindowSize.Y - 9)/2,
                        Text.Length + 4, 9), ConsoleColor.DarkYellow);
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - (Text.Length + 2))/2, (Renderer.WindowSize.Y - 7)/2,
                        Text.Length + 2, 7), ConsoleColor.Yellow);
            }

            Renderer.DrawString("QUESTION",
                new Vector2((Renderer.WindowSize.X - "QUESTION".Length)/2, (Renderer.WindowSize.Y - 9)/2),
                ConsoleColor.White);
            Renderer.DrawString(Text,
                new Vector2((Renderer.WindowSize.X - Text.Length)/2, (Renderer.WindowSize.Y - 7)/2 + 1),
                ConsoleColor.Black);

            if (_selectedItem == 0)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 7)/2 + 3, 8, 3),
                    ConsoleColor.Green);
                Renderer.DrawString("NO",
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 5, (Renderer.WindowSize.Y - 7)/2 + 4),
                    ConsoleColor.Black);
                Renderer.DrawRectangle(
                    new Rectangle(Renderer.WindowSize.X - 24 - 15, (Renderer.WindowSize.Y - 7)/2 + 3, 9, 3),
                    ConsoleColor.Blue);
                Renderer.DrawString("YES",
                    new Vector2(Renderer.WindowSize.X - 24 - 12, (Renderer.WindowSize.Y - 7)/2 + 4), ConsoleColor.Black);
            }
            else if (_selectedItem == 1)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 7)/2 + 3, 8, 3),
                    ConsoleColor.Blue);
                Renderer.DrawString("NO",
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 5, (Renderer.WindowSize.Y - 7)/2 + 4),
                    ConsoleColor.White);
                Renderer.DrawRectangle(
                    new Rectangle(Renderer.WindowSize.X - 24 - 15, (Renderer.WindowSize.Y - 7)/2 + 3, 9, 3),
                    ConsoleColor.Green);
                Renderer.DrawString("YES",
                    new Vector2(Renderer.WindowSize.X - 24 - 12, (Renderer.WindowSize.Y - 7)/2 + 4), ConsoleColor.White);
            }
        }
    }
}