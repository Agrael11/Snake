using System;
using System.Xml;
using Snake.Properties;

namespace Snake.Scenes
{
    public class SaveSelectScene
    {
        private readonly Game _parent;

        private bool _asked;
        private int _selectedItem;
        public Game.Scenes Last;
        public bool Mp;

        public SaveSelectScene(Game parent)
        {
            _parent = parent;
        }

        public void Update()
        {
            if (_asked)
            {
                if (_parent.Question.Result)
                {
                    _parent.CurrentScene = Game.Scenes.InGame;
                    _parent.InGame.MultiPlayer = Mp;
                    _parent.InGame.ResetScene();
                    _parent.InGame.SaveFile = _selectedItem + 1;
                    _selectedItem = 0;
                }
            }
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        _selectedItem = 0;
                        _parent.CurrentScene = Game.Scenes.MainMenu;
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
                        if (FileController.Exist("Saves\\Save_" + (!Mp ? "SP_" : "MP_") + _selectedItem + 1 + ".xml"))
                        {
                            _asked = true;
                            _parent.Question.Text = Resources.SaveOverwite;
                            _parent.Question.Last = Game.Scenes.SaveGame;
                            _parent.CurrentScene = Game.Scenes.Question;
                        }
                        else
                        {
                            _parent.CurrentScene = Game.Scenes.InGame;
                            _parent.InGame.MultiPlayer = Mp;
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
                new Rectangle((Renderer.WindowSize.X - 26)/2, (Renderer.WindowSize.Y - 26)/2, 26, 26),
                ConsoleColor.DarkYellow);
            Renderer.DrawRectangle(
                new Rectangle((Renderer.WindowSize.X - 24)/2, (Renderer.WindowSize.Y - 24)/2, 24, 24),
                ConsoleColor.Yellow);

            Renderer.DrawString(Resources.SaveSelect ,
                new Vector2((Renderer.WindowSize.X - Resources.SaveSelect.Length)/2, (Renderer.WindowSize.Y - 26)/2),
                ConsoleColor.White);

            var save1 = "      SAVE 1";
            var save2 = "┌────────────────┐";
            var save3 = "│      FREE      │";
            var save4 = "└────────────────┘";
            if (FileController.Exist("Saves\\Save_" + (!Mp ? "SP_" : "MP_") + 1 + ".xml"))
            {
                var state = FileController.Load<SaveState>("Saves\\Save_" + (!Mp ? "SP_" : "MP_") + 1 + ".xml");
                save2 = "SCORE: " + state.Score;
                save3 = "LEVEL: " + state.Level;
                save4 = "LIVES: " + state.Lives;
            }
            if (_selectedItem == 0)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 24)/2 + 1, 20, 6),
                    ConsoleColor.Green);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 2),
                    ConsoleColor.Black);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 3),
                    ConsoleColor.Black);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 4),
                    ConsoleColor.Black);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 5),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 24)/2 + 1, 20, 6),
                    ConsoleColor.Blue);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 2),
                    ConsoleColor.White);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 3),
                    ConsoleColor.White);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 4),
                    ConsoleColor.White);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 5),
                    ConsoleColor.White);
            }

            save1 = "      SAVE 2";
            save2 = "┌────────────────┐";
            save3 = "│      FREE      │";
            save4 = "└────────────────┘";
            if (FileController.Exist("Saves\\Save_" + (!Mp ? "SP_" : "MP_") + 2 + ".xml"))
            {
                var state = FileController.Load<SaveState>("Saves\\Save_" + (!Mp ? "SP_" : "MP_") + 2 + ".xml");
                save2 = "SCORE: " + state.Score;
                save3 = "LEVEL: " + state.Level;
                save4 = "LIVES: " + state.Lives;
            }
            if (_selectedItem == 1)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 24)/2 + 8, 20, 6),
                    ConsoleColor.Green);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 9),
                    ConsoleColor.Black);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 10),
                    ConsoleColor.Black);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 11),
                    ConsoleColor.Black);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 12),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 24)/2 + 8, 20, 6),
                    ConsoleColor.Blue);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 9),
                    ConsoleColor.White);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 10),
                    ConsoleColor.White);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 11),
                    ConsoleColor.White);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 12),
                    ConsoleColor.White);
            }

            save1 = "      SAVE 3";
            save2 = "┌────────────────┐";
            save3 = "│      FREE      │";
            save4 = "└────────────────┘";
            if (FileController.Exist("Saves\\Save_" + (!Mp ? "SP_" : "MP_") + 3 + ".xml"))
            {
                var state = FileController.Load<SaveState>("Saves\\Save_" + (!Mp ? "SP_" : "MP_") + 3 + ".xml");
                save2 = "SCORE: " + state.Score;
                save3 = "LEVEL: " + state.Level;
                save4 = "LIVES: " + state.Lives;
            }
            if (_selectedItem == 2)
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 24)/2 + 15, 20, 6),
                    ConsoleColor.Green);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 16),
                    ConsoleColor.Black);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 17),
                    ConsoleColor.Black);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 18),
                    ConsoleColor.Black);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 19),
                    ConsoleColor.Black);
            }
            else
            {
                Renderer.DrawRectangle(
                    new Rectangle((Renderer.WindowSize.X - 24)/2 + 2, (Renderer.WindowSize.Y - 24)/2 + 15, 20, 6),
                    ConsoleColor.Blue);
                Renderer.DrawString(save1,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 16),
                    ConsoleColor.White);
                Renderer.DrawString(save2,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 17),
                    ConsoleColor.White);
                Renderer.DrawString(save3,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 18),
                    ConsoleColor.White);
                Renderer.DrawString(save4,
                    new Vector2((Renderer.WindowSize.X - 24)/2 + 3, (Renderer.WindowSize.Y - 24)/2 + 19),
                    ConsoleColor.White);
            }
        }
    }
}