using System;
using Snake.Properties;

namespace Snake.Scenes
{
    public class HighscoresScene
    {
        private readonly Game _parent;
        private bool _firstDraw;
        private Highscores _highscores;
        private string _name = "";
        private bool _played;

        private int _selectedItem = -1;

        public bool Mp;

        public HighscoresScene(Game parent)
        {
            _parent = parent;
        }

        public void LoadScores()
        {
            _highscores = FileController.Load<Highscores>("Saves\\Highscores_" + (Mp ? "MP" : "SP") + ".xml");
            for (var i = 0; i < 10; i++)
                if (_parent.Score > _highscores.Scores[i].Points)
                {
                    _selectedItem = i;
                    break;
                }
            if (_selectedItem != -1)
                for (var i = 8; i >= _selectedItem; i--)
                {
                    _highscores.Scores[i + 1] = new Score
                    {
                        Name = _highscores.Scores[i].Name,
                        Points = _highscores.Scores[i].Points
                    };
                }
        }

        public void Update()
        {
            if (_firstDraw)
            {
                if (_selectedItem != -1)
                {
                    if (!_played)
                    {
                        _played = true;
                        Console.Beep(200, 500);
                        Console.Beep(300, 500);
                        Console.Beep(400, 500);
                        Console.Beep(500, 500);
                        Console.Beep(600, 1500);
                    }
                    Console.CursorVisible = true;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.CursorLeft = (Renderer.WindowSize.X - 42)/2 + 2;
                    Console.CursorTop = (Renderer.WindowSize.Y - 23)/2 + (_selectedItem + 1)*2;
                    _name = Console.ReadLine();
                    Console.CursorVisible = false;
                    _highscores.Scores[_selectedItem].Name = _name;
                    _highscores.Scores[_selectedItem].Points = _parent.Score;
                    FileController.Save(_highscores, "Saves\\Highscores_" + (Mp ? "MP" : "SP") + ".xml");
                    _selectedItem = -1;
                }
                else
                {
                    if (!_played)
                    {
                        _played = true;
                        Console.Beep(200, 500);
                        Console.Beep(400, 500);
                        Console.Beep(600, 1500);
                    }
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Enter)
                    {
                        _parent.CurrentScene = Game.Scenes.MainMenu;
                        _parent.Score = 0;
                        _parent.Lives = 5;
                        _parent.Level = 1;
                    }
                }
            }
        }

        public void Draw()
        {
            if (!_firstDraw) _firstDraw = true;
            Renderer.DrawRectangle(
                new Rectangle((Renderer.WindowSize.X - 42)/2, (Renderer.WindowSize.Y - 23)/2, 42, 23),
                ConsoleColor.DarkGreen);
            Renderer.DrawRectangle(
                new Rectangle((Renderer.WindowSize.X - 40)/2, (Renderer.WindowSize.Y - 21)/2, 40, 21),
                ConsoleColor.Green);
            //Renderer.DrawString("HIGH SCORES TABLE",
            Renderer.DrawString(Resources.HighscoresTable,
                new Vector2((Renderer.WindowSize.X - Resources.HighscoresTable.Length)/2, (Renderer.WindowSize.Y - 21)/2 - 1),
                ConsoleColor.White);
            for (var i = 0; i < 10; i++)
            {
                if (_selectedItem == i)
                {
                    Renderer.DrawRectangle(
                        new Rectangle((Renderer.WindowSize.X - 42)/2 + 2, (Renderer.WindowSize.Y - 23)/2 + (i + 1)*2, 38,
                            1), ConsoleColor.DarkYellow);
                    Renderer.DrawString(_name.PadRight(38 - _parent.Score.ToString().Length, ' ') + _parent.Score,
                        new Vector2((Renderer.WindowSize.X - 42)/2 + 2, (Renderer.WindowSize.Y - 23)/2 + (i + 1)*2),
                        ConsoleColor.White);
                }
                else
                {
                    Renderer.DrawRectangle(
                        new Rectangle((Renderer.WindowSize.X - 42)/2 + 2, (Renderer.WindowSize.Y - 23)/2 + (i + 1)*2, 38,
                            1), ConsoleColor.Blue);
                    Renderer.DrawString(
                        _highscores.Scores[i].Name.PadRight(38 - _highscores.Scores[i].Points.ToString().Length, ' ') +
                        _highscores.Scores[i].Points,
                        new Vector2((Renderer.WindowSize.X - 42)/2 + 2, (Renderer.WindowSize.Y - 23)/2 + (i + 1)*2),
                        ConsoleColor.White);
                }
            }
        }
    }
}