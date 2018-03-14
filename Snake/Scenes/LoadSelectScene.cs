using System;

namespace Snake.Scenes
{
    public class LoadSelectScene
    {
        private readonly Game _parent;
        private bool _mp;
        private int _selectedItem;
        public Game.Scenes Last;

        public LoadSelectScene(Game parent)
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
                    case ConsoleKey.Escape:
                        _selectedItem = 0;
                        _parent.CurrentScene = Game.Scenes.MainMenu;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.A:
                    case ConsoleKey.D:
                        _mp = !_mp;
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
                        if (FileController.Exist("Saves\\Save_" + (!_mp ? "SP_" : "MP_") + (_selectedItem + 1) + ".xml"))
                        {
                            var state =
                                FileController.Load<SaveState>("Saves\\Save_" + (!_mp ? "SP_" : "MP_") +
                                                               (_selectedItem + 1) + ".xml");
                            _parent.Level = state.Level;
                            _parent.Lives = state.Lives;
                            _parent.Score = state.Score;
                            _parent.CurrentScene = Game.Scenes.InGame;
                            _parent.InGame.MultiPlayer = _mp;
                            _parent.InGame.ResetScene();
                            _parent.InGame.SaveFile = _selectedItem + 1;
                        }
                        break;
                }
            }
        }

        public void Draw()
        {
            Renderer.DrawRectangle(
                new Rectangle((Renderer.WindowSize.X - 33)/2, (Renderer.WindowSize.Y - 28)/2, 33, 28),
                ConsoleColor.DarkYellow);
            Renderer.DrawRectangle(
                new Rectangle((Renderer.WindowSize.X - 31)/2, (Renderer.WindowSize.Y - 26)/2, 31, 26),
                ConsoleColor.Yellow);

            Renderer.DrawString("SELECT SAVE",
                new Vector2((Renderer.WindowSize.X - "SELECT SAVE".Length)/2, (Renderer.WindowSize.Y - 28)/2),
                ConsoleColor.White);

            if (!_mp)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 1, 14, 3),
                    ConsoleColor.Green);
                Renderer.DrawString("SINGLEPLAYER",
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 2),
                    ConsoleColor.Black);
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 17, (Renderer.WindowSize.Y - 26)/2 + 1, 14, 3),
                    ConsoleColor.Blue);
                Renderer.DrawString("MULTIPLAYER",
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 18, (Renderer.WindowSize.Y - 26)/2 + 2),
                    ConsoleColor.White);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 1, 14, 3),
                    ConsoleColor.Blue);
                Renderer.DrawString("SINGLEPLAYER",
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 2),
                    ConsoleColor.White);
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 17, (Renderer.WindowSize.Y - 26)/2 + 1, 14, 3),
                    ConsoleColor.Green);
                Renderer.DrawString("MULTIPLAYER",
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 18, (Renderer.WindowSize.Y - 26)/2 + 2),
                    ConsoleColor.Black);
            }


            var save1 = "           SAVE 1";
            var save2 = "┌─────────────────────────┐";
            var save3 = "│           FREE          │";
            var save4 = "└─────────────────────────┘";
            if (FileController.Exist("Saves\\Save_" + (!_mp ? "SP_" : "MP_") + 1 + ".xml"))
            {
                var state = FileController.Load<SaveState>("Saves\\Save_" + (!_mp ? "SP_" : "MP_") + 1 + ".xml");
                save2 = "SCORE: " + state.Score;
                save3 = "LEVEL: " + state.Level;
                save4 = "LIVES: " + state.Lives;
            }
            if (_selectedItem == 0)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 5, 29, 6),
                    ConsoleColor.Green);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 6),
                    ConsoleColor.Black);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 7),
                    ConsoleColor.Black);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 8),
                    ConsoleColor.Black);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 9),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 5, 29, 6),
                    ConsoleColor.Blue);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 6),
                    ConsoleColor.White);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 7),
                    ConsoleColor.White);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 8),
                    ConsoleColor.White);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 9),
                    ConsoleColor.White);
            }

            save1 = "           SAVE 2";
            save2 = "┌─────────────────────────┐";
            save3 = "│           FREE          │";
            save4 = "└─────────────────────────┘";
            if (FileController.Exist("Saves\\Save_" + (!_mp ? "SP_" : "MP_") + 2 + ".xml"))
            {
                var state = FileController.Load<SaveState>("Saves\\Save_" + (!_mp ? "SP_" : "MP_") + 2 + ".xml");
                save2 = "SCORE: " + state.Score;
                save3 = "LEVEL: " + state.Level;
                save4 = "LIVES: " + state.Lives;
            }
            if (_selectedItem == 1)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 12, 29, 6),
                    ConsoleColor.Green);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 13),
                    ConsoleColor.Black);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 14),
                    ConsoleColor.Black);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 15),
                    ConsoleColor.Black);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 16),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 12, 29, 6),
                    ConsoleColor.Blue);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 13),
                    ConsoleColor.White);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 14),
                    ConsoleColor.White);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 15),
                    ConsoleColor.White);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 16),
                    ConsoleColor.White);
            }

            save1 = "           SAVE 3";
            save2 = "┌─────────────────────────┐";
            save3 = "│           FREE          │";
            save4 = "└─────────────────────────┘";
            if (FileController.Exist("Saves\\Save_" + (!_mp ? "SP_" : "MP_") + 3 + ".xml"))
            {
                var state = FileController.Load<SaveState>("Saves\\Save_" + (!_mp ? "SP_" : "MP_") + 3 + ".xml");
                save2 = "SCORE: " + state.Score;
                save3 = "LEVEL: " + state.Level;
                save4 = "LIVES: " + state.Lives;
            }
            if (_selectedItem == 2)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 19, 29, 6),
                    ConsoleColor.Green);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 20),
                    ConsoleColor.Black);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 21),
                    ConsoleColor.Black);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 22),
                    ConsoleColor.Black);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 23),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 33)/2 + 2, (Renderer.WindowSize.Y - 26)/2 + 19, 29, 6),
                    ConsoleColor.Blue);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 20),
                    ConsoleColor.White);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 21),
                    ConsoleColor.White);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 22),
                    ConsoleColor.White);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 33)/2 + 3, (Renderer.WindowSize.Y - 26)/2 + 23),
                    ConsoleColor.White);
            }
        }
    }
}