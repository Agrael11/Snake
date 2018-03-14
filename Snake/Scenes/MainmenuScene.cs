using System;

namespace Snake.Scenes
{
    public class MainMenuScene
    {
        private readonly Game _parent;
        private bool _asked;
        private bool _drawed;
        private bool _played;
        private int _selectedItem;


        public MainMenuScene(Game parent)
        {
            _parent = parent;
        }

        public void Update()
        {
            if (!_played && _drawed)
            {
                _played = true;
                Console.Beep(200, 1000);
                Console.Beep(310, 500);
                Console.Beep(250, 250);
                Console.Beep(220, 250);
                Console.Beep(330, 500);
                Console.Beep(310, 1000);
            }

            if (_asked)
            {
                _asked = false;
                if (_parent.Question.Result) Program.Running = false;
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (_selectedItem == 0) _selectedItem = 3;
                        else _selectedItem--;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (_selectedItem == 3) _selectedItem = 0;
                        else _selectedItem++;
                        break;
                    case ConsoleKey.Enter:
                        switch (_selectedItem)
                        {
                            case 0:
                                _parent.Level = 1;
                                _parent.Score = 0;
                                _parent.Lives = 5;
                                _parent.CurrentScene = Game.Scenes.SaveGame;
                                _parent.SaveQuestion.Last = Game.Scenes.MainMenu;
                                _parent.SaveQuestion.Mp = false;
                                break;
                            case 1:
                                _selectedItem = 0;
                                _parent.Level = 1;
                                _parent.Score = 0;
                                _parent.Lives = 5;
                                _parent.CurrentScene = Game.Scenes.SaveGame;
                                _parent.SaveQuestion.Last = Game.Scenes.MainMenu;
                                _parent.SaveQuestion.Mp = true;
                                break;
                            case 2:
                                _selectedItem = 0;
                                _parent.CurrentScene = Game.Scenes.LoadGame;
                                _parent.SaveQuestion.Last = Game.Scenes.MainMenu;
                                break;
                            case 3:
                                _asked = true;
                                _parent.Question.Text = "REALLY WANT TO EXIT TO WINDOWS?";
                                _parent.Question.Last = Game.Scenes.MainMenu;
                                _parent.CurrentScene = Game.Scenes.Question;
                                break;
                        }
                        break;
                }
            }
        }

        public void Draw()
        {
            if (!_drawed) _drawed = true;

            var logoPosX = (Renderer.WindowSize.X - 48)/2;
            //S
            Renderer.DrawRectangle(new Rectangle(logoPosX, 2, 47, 1), ConsoleColor.Yellow);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 47, 2, 1, 1), ConsoleColor.DarkYellow);
            Renderer.DrawString(":", new Vector2(logoPosX + 47, 2), ConsoleColor.DarkRed);
            Renderer.DrawRectangle(new Rectangle(logoPosX, 3, 1, 3), ConsoleColor.Yellow);
            Renderer.DrawRectangle(new Rectangle(logoPosX, 6, 8, 1), ConsoleColor.Yellow);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 7, 7, 1, 4), ConsoleColor.Yellow);
            Renderer.DrawRectangle(new Rectangle(logoPosX, 11, 8, 1), ConsoleColor.Yellow);
            //N
            Renderer.DrawRectangle(new Rectangle(logoPosX + 10, 4, 1, 8), ConsoleColor.Green);
            Renderer.DrawPoint(new Vector2(logoPosX + 11, 5), ConsoleColor.Green);
            Renderer.DrawPoint(new Vector2(logoPosX + 12, 6), ConsoleColor.Green);
            Renderer.DrawPoint(new Vector2(logoPosX + 13, 7), ConsoleColor.Green);
            Renderer.DrawPoint(new Vector2(logoPosX + 14, 8), ConsoleColor.Green);
            Renderer.DrawPoint(new Vector2(logoPosX + 15, 9), ConsoleColor.Green);
            Renderer.DrawPoint(new Vector2(logoPosX + 16, 10), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 17, 4, 1, 8), ConsoleColor.Green);
            //A
            Renderer.DrawRectangle(new Rectangle(logoPosX + 20, 6, 1, 6), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 21, 5, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 23, 4, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 21, 8, 6, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 25, 5, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 27, 6, 1, 6), ConsoleColor.Green);
            //K
            Renderer.DrawRectangle(new Rectangle(logoPosX + 30, 4, 1, 8), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 31, 7, 1, 2), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 32, 6, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 34, 5, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 36, 4, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 32, 9, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 34, 10, 2, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 36, 11, 2, 1), ConsoleColor.Green);
            //E
            Renderer.DrawRectangle(new Rectangle(logoPosX + 40, 4, 1, 8), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 41, 4, 7, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 41, 7, 6, 1), ConsoleColor.Green);
            Renderer.DrawRectangle(new Rectangle(logoPosX + 41, 11, 7, 1), ConsoleColor.Green);

            #region Menu

            if (_selectedItem == 0)
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 14, 35, 3), ConsoleColor.Green);
                Renderer.DrawString("New One Player Game",
                    new Vector2((Renderer.WindowSize.X - "New One Player Game".Length)/2, 15), ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 14, 35, 3), ConsoleColor.Blue);
                Renderer.DrawString("New One Player Game",
                    new Vector2((Renderer.WindowSize.X - "New One Player Game".Length)/2, 15), ConsoleColor.White);
            }
            if (_selectedItem == 1)
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 18, 35, 3), ConsoleColor.Green);
                Renderer.DrawString("New Two Player Game",
                    new Vector2((Renderer.WindowSize.X - "New Two Player Game".Length)/2, 19), ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 18, 35, 3), ConsoleColor.Blue);
                Renderer.DrawString("New Two Player Game",
                    new Vector2((Renderer.WindowSize.X - "New Two Player Game".Length)/2, 19), ConsoleColor.White);
            }
            if (_selectedItem == 2)
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 22, 35, 3), ConsoleColor.Green);
                Renderer.DrawString("Load Game", new Vector2((Renderer.WindowSize.X - "Load Game".Length)/2, 23),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 22, 35, 3), ConsoleColor.Blue);
                Renderer.DrawString("Load Game", new Vector2((Renderer.WindowSize.X - "Load Game".Length)/2, 23),
                    ConsoleColor.White);
            }
            if (_selectedItem == 3)
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 26, 35, 3), ConsoleColor.Green);
                Renderer.DrawString("Exit", new Vector2((Renderer.WindowSize.X - "Exit".Length)/2, 27),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(new Rectangle((Renderer.WindowSize.X - 35)/2, 26, 35, 3), ConsoleColor.Blue);
                Renderer.DrawString("Exit", new Vector2((Renderer.WindowSize.X - "Exit".Length)/2, 27),
                    ConsoleColor.White);
            }

            #endregion
        }
    }
}