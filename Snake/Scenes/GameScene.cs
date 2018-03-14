using System;
using System.Collections.Generic;
using System.IO;

namespace Snake.Scenes
{
    public class GameScene
    {
        private readonly Dictionary<Snake.Side, ConsoleKey> _controls1 = new Dictionary<Snake.Side, ConsoleKey>();
        private readonly Dictionary<Snake.Side, ConsoleKey> _controls2 = new Dictionary<Snake.Side, ConsoleKey>();
        private readonly List<Rectangle> _obstacles = new List<Rectangle>();
        private readonly Game _parent;
        private readonly Random _r = new Random();

        private bool _isThereLive;
        private bool _isTherePoison;
        private List<Vector2> _lives = new List<Vector2>();
        private int _maxLives = 1;

        private int _maxPoints = 3;
        private int _maxPoisons = 1;
        private int _needSize;
        private List<Vector2> _points = new List<Vector2>();
        private List<Vector2> _poisons = new List<Vector2>();

        private Snake _snake1;
        private Snake _snake2;
        private int _speed;
        private Snake.Side _startingDir;

        private Vector2 _startingPos1;
        private Vector2 _startingPos2;
        private int _startingSize;
        public bool MultiPlayer = false;

        public int SaveFile = 0;

        public GameScene(Game parent)
        {
            _parent = parent;
            ResetScene();
        }

        public Vector2 GeneratePoint()
        {
            var point = new Vector2(0, 0)
            {
                X = _r.Next(0, Renderer.WindowSize.X),
                Y = _r.Next(2, Renderer.WindowSize.Y)
            };
            var collision = _snake1.CheckCollisionFull(point) || _snake1.CheckCollisionFull(point);
            if (MultiPlayer)
                collision |= _snake2.CheckCollisionFull(point) || _snake2.CheckCollisionFull(point);
            foreach (var pnt in _lives)
            {
                collision |= pnt == point;
            }

            foreach (var pnt in _points)
            {
                collision |= pnt == point;
            }

            foreach (var pnt in _poisons)
            {
                collision |= pnt == point;
            }

            foreach (var pnt in _obstacles)
            {
                collision |= pnt.Intersects(pnt);
            }
            while (collision)
            {
                point.X = _r.Next(0, Renderer.WindowSize.X);
                point.Y = _r.Next(2, Renderer.WindowSize.Y);

                collision = _snake1.CheckCollisionFull(point);
                if (MultiPlayer)
                    collision |= _snake2.CheckCollisionFull(point);

                foreach (var pnt in _lives)
                {
                    collision |= pnt == point;
                }

                foreach (var pnt in _points)
                {
                    collision |= pnt == point;
                }

                foreach (var pnt in _poisons)
                {
                    collision |= pnt == point;
                }

                foreach (var pnt in _obstacles)
                {
                    collision |= pnt.Intersects(pnt);
                }
            }
            return point;
        }


        public void MakeRandom()
        {
            _maxPoints = _r.Next(1, 7);
            _maxLives = _r.Next(1, 3);
            _maxPoisons = _r.Next(1, 3);

            var spdr = _r.Next(1, 3);
            _speed = 10 - _parent.Level/spdr;
            if (_speed <= 0) _speed = 1;
            _needSize = _r.Next(10, 100);
            _startingSize = _r.Next(0, 9);
            _startingDir = (Snake.Side) _r.Next(0, 4);

            _startingPos1 = new Vector2(_r.Next(10, Renderer.WindowSize.X - 10), _r.Next(10, Renderer.WindowSize.Y - 12));
            _startingPos2 = new Vector2(Renderer.WindowSize.X - _startingPos1.X, Renderer.WindowSize.Y - _startingPos1.Y);

            _obstacles.Clear();

            _snake1 = new Snake(this, true, _startingPos1, _needSize, _speed, _startingSize, _startingDir, _controls1);
            Snake.Side dir2;
            if (_startingDir == Snake.Side.Down) dir2 = Snake.Side.Up;
            else if (_startingDir == Snake.Side.Up) dir2 = Snake.Side.Down;
            else if (_startingDir == Snake.Side.Left) dir2 = Snake.Side.Right;
            else dir2 = Snake.Side.Left;
            if (MultiPlayer)
                _snake2 = new Snake(this, false, _startingPos2, _needSize, _speed, _startingSize, dir2, _controls2);

            var sides = _r.Next(0, 10);
            if ((sides > 3) && (sides < 6))
            {
                _obstacles.Add(new Rectangle(1, 2, Renderer.WindowSize.X - 2, 1));
                _obstacles.Add(new Rectangle(1, Renderer.WindowSize.Y - 1, Renderer.WindowSize.X - 2, 1));
                _obstacles.Add(new Rectangle(0, 3, 1, Renderer.WindowSize.Y - 4));
                _obstacles.Add(new Rectangle(Renderer.WindowSize.X - 1, 3, 1, Renderer.WindowSize.Y - 4));
            }
            else if (sides >= 6)
            {
                _obstacles.Add(new Rectangle(1, 2, (Renderer.WindowSize.X - 2)/2 - 10, 1));
                _obstacles.Add(new Rectangle((Renderer.WindowSize.X - 2)/2 + 11, 2, (Renderer.WindowSize.X - 2)/2 - 10,
                    1));
                _obstacles.Add(new Rectangle(1, Renderer.WindowSize.Y - 1, (Renderer.WindowSize.X - 2)/2 - 10, 1));
                _obstacles.Add(new Rectangle((Renderer.WindowSize.X - 2)/2 + 11, Renderer.WindowSize.Y - 1,
                    (Renderer.WindowSize.X - 2)/2 - 10, 1));
                _obstacles.Add(new Rectangle(0, 3, 1, (Renderer.WindowSize.Y - 4)/2 - 5));
                _obstacles.Add(new Rectangle(0, (Renderer.WindowSize.Y - 4)/2 + 8, 1, (Renderer.WindowSize.Y - 4)/2 - 5));
                _obstacles.Add(new Rectangle(Renderer.WindowSize.X - 1, 3, 1, (Renderer.WindowSize.Y - 4)/2 - 5));
                _obstacles.Add(new Rectangle(Renderer.WindowSize.X - 1, (Renderer.WindowSize.Y - 4)/2 + 8, 1,
                    (Renderer.WindowSize.Y - 4)/2 - 5));
            }

            for (var i = 0; i < _r.Next(0, 5); i++)
            {
                var rec = new Rectangle(_r.Next(10, Renderer.WindowSize.X - 10), _r.Next(10, Renderer.WindowSize.Y - 10),
                    _r.Next(1, 20), _r.Next(1, 10));

                if (!MultiPlayer)
                    while (_snake1.CheckCollisionFull(rec))
                    {
                        rec = new Rectangle(_r.Next(10, Renderer.WindowSize.X - 10),
                            _r.Next(10, Renderer.WindowSize.Y - 10), _r.Next(1, 20), _r.Next(1, 10));
                    }
                else
                    while (_snake1.CheckCollisionFull(rec) || _snake2.CheckCollisionFull(rec))
                    {
                        rec = new Rectangle(_r.Next(10, Renderer.WindowSize.X - 10),
                            _r.Next(10, Renderer.WindowSize.Y - 10), _r.Next(1, 20), _r.Next(1, 10));
                    }
                _obstacles.Add(rec);
            }
        }

        public void LoadFile(string file)
        {
            var l = FileController.Load<Level>(file);

            _maxPoints = l.MaxPoints;
            _maxLives = l.MaxLives;
            _maxPoisons = l.MaxPoisons;

            _speed = 10 - _parent.Level/l.SpeedModifier;
            if (_speed <= 0) _speed = 1;
            _needSize = l.NeededSize;
            _startingSize = l.StartingSize;
            _startingDir = l.StartDirection;

            _startingPos1 = l.Player1StartPosition;
            _startingPos2 = l.Player2StartPosition;

            _obstacles.Clear();

            _snake1 = new Snake(this, true, _startingPos1, _needSize, _speed, _startingSize, _startingDir, _controls1);
            Snake.Side dir2;
            if (_startingDir == Snake.Side.Down) dir2 = Snake.Side.Up;
            else if (_startingDir == Snake.Side.Up) dir2 = Snake.Side.Down;
            else if (_startingDir == Snake.Side.Left) dir2 = Snake.Side.Right;
            else dir2 = Snake.Side.Left;
            if (MultiPlayer)
                _snake2 = new Snake(this, false, _startingPos2, _needSize, _speed, _startingSize, dir2, _controls2);

            foreach (var rec in l.Obstacles)
            {
                _obstacles.Add(rec);
            }
        }

        public void LoadLevel()
        {
            if (Directory.Exists("Levels") && File.Exists("Levels\\Level" + _parent.Level + ".lvl"))
                LoadFile("Levels\\Level" + _parent.Level + ".lvl");
            else MakeRandom();

            _snake1.GotScore += GotScore;
            _snake1.GotLive += GotLive;
            _snake1.Died += Died;
            _snake1.Complete += Completed;

            _snake1.AtePoint += AtePoint;
            _snake1.AteLive += AteLive;
            _snake1.AtePoision += AtePoison;

            if (MultiPlayer)
            {
                _snake2.GotScore += GotScore;
                _snake2.GotLive += GotLive;
                _snake2.Died += Died;
                _snake2.Complete += Completed;

                _snake2.AtePoint += AtePoint;
                _snake2.AteLive += AteLive;
                _snake2.AtePoision += AtePoison;
            }

            for (var i = 0; i < _maxPoints; i++)
            {
                _points.Add(GeneratePoint());
            }
            for (var i = 0; i < _maxPoisons; i++)
            {
                _poisons.Add(GeneratePoint());
            }
            for (var i = 0; i < _maxLives; i++)
            {
                _lives.Add(GeneratePoint());
            }
        }

        public void ResetScene()
        {
            _controls1.Clear();
            _controls2.Clear();
            _controls1.Add(Snake.Side.Up, ConsoleKey.W);
            _controls1.Add(Snake.Side.Down, ConsoleKey.S);
            _controls1.Add(Snake.Side.Left, ConsoleKey.A);
            _controls1.Add(Snake.Side.Right, ConsoleKey.D);
            _controls2.Add(Snake.Side.Up, ConsoleKey.UpArrow);
            _controls2.Add(Snake.Side.Down, ConsoleKey.DownArrow);
            _controls2.Add(Snake.Side.Left, ConsoleKey.LeftArrow);
            _controls2.Add(Snake.Side.Right, ConsoleKey.RightArrow);

            _points = new List<Vector2>();
            _lives = new List<Vector2>();
            _poisons = new List<Vector2>();

            LoadLevel();
        }

        public void ResetLevel()
        {
            _snake1 = new Snake(this, true, _startingPos1, _needSize, _speed, _startingSize, _startingDir, _controls1);
            _snake1.GotScore += GotScore;
            _snake1.GotLive += GotLive;
            _snake1.Died += Died;
            _snake1.Complete += Completed;

            _snake1.AtePoint += AtePoint;
            _snake1.AteLive += AteLive;
            _snake1.AtePoision += AtePoison;

            if (MultiPlayer)
            {
                Snake.Side dir2;
                if (_startingDir == Snake.Side.Down) dir2 = Snake.Side.Up;
                else if (_startingDir == Snake.Side.Up) dir2 = Snake.Side.Down;
                else if (_startingDir == Snake.Side.Left) dir2 = Snake.Side.Right;
                else dir2 = Snake.Side.Left;

                _snake2 = new Snake(this, false, _startingPos2, _needSize, _speed, _startingSize, dir2, _controls2);
                _snake2.GotScore += GotScore;
                _snake2.GotLive += GotLive;
                _snake2.Died += Died;
                _snake2.Complete += Completed;

                _snake2.AtePoint += AtePoint;
                _snake2.AteLive += AteLive;
                _snake2.AtePoision += AtePoison;
            }

            _points.Clear();
            _poisons.Clear();
            _lives.Clear();
            for (var i = 0; i < _maxPoints; i++)
            {
                _points.Add(GeneratePoint());
            }
            for (var i = 0; i < _maxPoisons; i++)
            {
                _poisons.Add(GeneratePoint());
            }
            for (var i = 0; i < _maxLives; i++)
            {
                _lives.Add(GeneratePoint());
            }
        }


        public void Update()
        {
            if (Console.KeyAvailable)
            {
                var pressed = Console.ReadKey(true);
                if (pressed.Key == ConsoleKey.Escape)
                {
                    _parent.CurrentScene = Game.Scenes.Pause;
                }
                _snake1.Control(pressed);
                if (MultiPlayer) _snake2.Control(pressed);
            }
            if (!_isThereLive)
                _isThereLive = _r.Next(0, 1000) == 1;
            if (!_isTherePoison)
                _isTherePoison = _r.Next(0, 100) == 1;
            _snake1.Update(_isThereLive ? _lives : new List<Vector2>(), _points,
                _isTherePoison ? _poisons : new List<Vector2>(), _obstacles, _snake2);
            if (MultiPlayer)
                _snake2.Update(_isThereLive ? _lives : new List<Vector2>(), _points,
                    _isTherePoison ? _poisons : new List<Vector2>(), _obstacles, _snake1);
        }

        public void Draw()
        {
            foreach (var point in _points)
            {
                Renderer.DrawPoint(point, ConsoleColor.Blue);
            }
            if (_isTherePoison)
                foreach (var poison in _poisons)
                {
                    Renderer.DrawPoint(poison, ConsoleColor.Red);
                }
            if (_isThereLive)
                foreach (var live in _lives)
                {
                    Renderer.DrawPoint(live, ConsoleColor.Green);
                }

            foreach (var r in _obstacles)
            {
                Renderer.DrawRectangle(r, ConsoleColor.DarkGray);
                if ((r.Width > 3) && (r.Height > 3))
                {
                    Renderer.DrawRectangle(new Rectangle(r.X + 1, r.Y + 1, r.Width - 2, r.Height - 2), ConsoleColor.Gray);
                }
            }

            _snake1.Draw();
            if (MultiPlayer) _snake2.Draw();

            DrawInfo();
        }

        private void DrawInfo()
        {
            Renderer.DrawRectangle(new Rectangle(0, 0, Renderer.WindowSize.X, 2), ConsoleColor.Blue);
            var livString = "Lives : " + _parent.Lives;
            var levString = "Level : " + _parent.Level;
            var scoString = "Score : " + _parent.Score;
            Renderer.DrawString(livString, new Vector2(1, 0), ConsoleColor.White);
            Renderer.DrawString(levString, new Vector2((Renderer.WindowSize.X - levString.Length)/2, 0),
                ConsoleColor.White);
            Renderer.DrawString(scoString, new Vector2(Renderer.WindowSize.X - 1 - scoString.Length, 0),
                ConsoleColor.White);
            if (!MultiPlayer)
            {
                var sizString = "Current Size : " + _snake1.Size;
                var neeString = "Needed Size : " + _snake1.NeedToComplete;
                Renderer.DrawString(sizString, new Vector2(1, 1), ConsoleColor.White);
                Renderer.DrawString(neeString, new Vector2(Renderer.WindowSize.X - 1 - neeString.Length, 1),
                    ConsoleColor.White);
            }
            else
            {
                var siz1String = "Player 1 Size : " + _snake1.Size;
                var siz2String = "Player 2 Size : " + _snake2.Size;
                var neeString = "Needed Size : " + _snake1.NeedToComplete*2;
                Renderer.DrawString(siz1String, new Vector2(1, 1), ConsoleColor.White);
                Renderer.DrawString(siz2String, new Vector2(Renderer.WindowSize.X - 1 - siz2String.Length, 1),
                    ConsoleColor.White);
                Renderer.DrawString(neeString, new Vector2((Renderer.WindowSize.X - neeString.Length)/2, 1),
                    ConsoleColor.White);
            }
        }


        public void GotScore(object sender, int e)
        {
            _parent.Score += e;
        }

        public void GotLive(object sender, EventArgs e)
        {
            _parent.Lives++;
        }

        public void Died(object sender, Snake.DeathCause e)
        {
            _parent.Lives--;
            if (_parent.Lives < 0)
            {
                _parent.HighscoreScn.Mp = MultiPlayer;
                _parent.HighscoreScn.LoadScores();
                _parent.CurrentScene = Game.Scenes.HighScore;
            }
            else
            {
                Console.Beep(1000, 500);
                Console.Beep(500, 500);
                Console.Beep(250, 500);
                if (!MultiPlayer || (e == Snake.DeathCause.CollisionOppenent))
                {
                    ResetLevel();
                }
                else
                {
                    ((Snake) sender).Size = _startingSize;
                }
            }
        }

        public void Completed(object sender, EventArgs e)
        {
            Console.Beep(250, 500);
            Console.Beep(500, 1000);
            Console.Beep(1000, 500);
            Console.Beep(1000, 500);
            _parent.Level++;
            var state = new SaveState
            {
                Level = _parent.Level,
                Lives = _parent.Lives,
                Score = _parent.Score
            };
            FileController.Save(state, "Saves\\Save_" + (!MultiPlayer ? "SP_" : "MP_") + SaveFile + ".xml");
            ResetScene();
        }


        public void AtePoint(object sender, Vector2 e)
        {
            foreach (var point in _points)
            {
                if (point == e)
                {
                    var p = GeneratePoint();
                    point.X = p.X;
                    point.Y = p.Y;
                }
            }
            Console.Beep(1000, 10);
        }

        public void AteLive(object sender, Vector2 e)
        {
            foreach (var point in _lives)
            {
                if (point == e)
                {
                    var p = GeneratePoint();
                    point.X = p.X;
                    point.Y = p.Y;
                }
            }
            Console.Beep(500, 50);
            Console.Beep(1000, 50);
            Console.Beep(1500, 50);
            _isThereLive = false;
        }

        public void AtePoison(object sender, Vector2 e)
        {
            foreach (var point in _poisons)
            {
                if (point == e)
                {
                    var p = GeneratePoint();
                    point.X = p.X;
                    point.Y = p.Y;
                }
                Console.Beep(1000, 50);
                Console.Beep(500, 50);
                Console.Beep(250, 50);
                _isTherePoison = false;
            }
        }
    }
}