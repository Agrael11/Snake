using System;

namespace Snake.Scenes
{
    public class PauseScene
    {
        private readonly Game _parent;
        private int _asked;
        private int _selectedItem;

        public PauseScene(Game parent)
        {
            _parent = parent;
        }

        public void Update()
        {
            if (_asked != 0)
            {
                if (_parent.Question.Result)
                {
                    if (_asked == 1)
                    {
                        _selectedItem = 0;
                        _parent.CurrentScene = Game.Scenes.MainMenu;
                    }
                    else if (_asked == 2)
                    {
                        Program.Running = false;
                    }
                }
                _asked = 0;
            }
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        _selectedItem = 0;
                        _parent.CurrentScene = Game.Scenes.InGame;
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (_selectedItem == 0) _selectedItem = 2;
                        else _selectedItem--;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (_selectedItem == 2) _selectedItem = 0;
                        else _selectedItem++;
                        break;
                    case ConsoleKey.Enter:
                        if (_selectedItem == 0)
                        {
                            _parent.CurrentScene = Game.Scenes.InGame;
                        }
                        else if (_selectedItem == 1)
                        {
                            _asked = 1;
                            _parent.Question.Text = "REALLY WANT TO RETURN TO MENU?";
                            _parent.Question.Last = Game.Scenes.Pause;
                            _parent.CurrentScene = Game.Scenes.Question;
                        }
                        else if (_selectedItem == 2)
                        {
                            _asked = 2;
                            _parent.Question.Text = "REALLY WANT TO EXIT TO WINDOWS?";
                            _parent.Question.Last = Game.Scenes.Pause;
                            _parent.CurrentScene = Game.Scenes.Question;
                        }
                        break;
                }
            }
        }

        public void Draw()
        {
            Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 26)/2, 8, 26, 15), ConsoleColor.DarkYellow);
            Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 24)/2, 9, 24, 13), ConsoleColor.Yellow);
            Renderer.DrawString("PAUSE", new Vector2((Renderer.WindowSize.X - "PAUSE".Length)/2, 8), ConsoleColor.White);

            if (_selectedItem == 0)
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 20)/2, 10, 20, 3), ConsoleColor.Green);
                Renderer.DrawString("Return to Game",
                    new Vector2((Renderer.WindowSize.X - "Return to Game".Length)/2, 11), ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 20)/2, 10, 20, 3), ConsoleColor.Blue);
                Renderer.DrawString("Return to Game",
                    new Vector2((Renderer.WindowSize.X - "Return to Game".Length)/2, 11), ConsoleColor.White);
            }
            if (_selectedItem == 1)
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 20)/2, 14, 20, 3), ConsoleColor.Green);
                Renderer.DrawString("Quit to Menu", new Vector2((Renderer.WindowSize.X - "Quit to Menu".Length)/2, 15),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 20)/2, 14, 20, 3), ConsoleColor.Blue);
                Renderer.DrawString("Quit to Menu", new Vector2((Renderer.WindowSize.X - "Quit to Menu".Length)/2, 15),
                    ConsoleColor.White);
            }
            if (_selectedItem == 2)
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 20)/2, 18, 20, 3), ConsoleColor.Green);
                Renderer.DrawString("Exit to Desktop",
                    new Vector2((Renderer.WindowSize.X - "Exit to Desktop".Length)/2, 19), ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 20)/2, 18, 20, 3), ConsoleColor.Blue);
                Renderer.DrawString("Exit to Desktop",
                    new Vector2((Renderer.WindowSize.X - "Exit to Desktop".Length)/2, 19), ConsoleColor.White);
            }
        }
    }
}