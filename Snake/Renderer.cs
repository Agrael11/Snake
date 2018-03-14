using System;

namespace Snake
{
    public static class Renderer
    {
        private static Pixel[,] _display = new Pixel[80, 30];
        private static Vector2 _size = new Vector2(80, 30);
        private static ConsoleColor _backgroundColor = ConsoleColor.Black;

        public static Vector2 WindowSize
        {
            get { return _size; }
            set
            {
                var displayNew = new Pixel[value.X, value.Y];
                for (var x = 0; x < value.X; x++)
                {
                    for (var y = 0; y < value.Y; y++)
                    {
                        if ((x >= _size.X) || (y >= _size.Y))
                        {
                            displayNew[x, y] = new Pixel {Color = _backgroundColor};
                        }
                        else
                        {
                            displayNew[x, y] = _display[x, y];
                        }
                    }
                }
                _display = displayNew;
                _size = value;
            }
        }


        public static void Init()
        {
            Console.SetWindowSize(_size.X, _size.Y);
            Console.CursorVisible = false;
            for (var x = 0; x < _size.X; x++)
            {
                for (var y = 0; y < _size.Y; y++)
                {
                    _display[x, y] = new Pixel {Color = _backgroundColor, Changed = false};
                }
            }
        }


        public static void Clean()
        {
            for (var x = 0; x < _size.X; x++)
            {
                for (var y = 0; y < _size.Y; y++)
                {
                    var p = new Pixel {Color = _backgroundColor};
                    p.Changed = p != _display[x, y] || _display[x, y].Changed;
                    _display[x, y] = p;
                }
            }
        }

        public static void Clean(ConsoleColor background)
        {
            _backgroundColor = background;
            Clean();
        }


        public static void DrawPoint(Vector2 point, ConsoleColor color)
        {
            if ((point.X >= 0) && (point.Y >= 0) && (point.X < _size.X) && (point.Y < _size.Y))
            {
                var p = new Pixel {Color = color};
                _display[point.X, point.Y] = p;
            }
        }

        public static void DrawRectangle(Rectangle rectangle, ConsoleColor color)
        {
            for (var x = rectangle.X; x < rectangle.X + rectangle.Width; x++)
            {
                for (var y = rectangle.Y; y < rectangle.Y + rectangle.Height; y++)
                {
                    if ((x < _size.X) && (x >= 0) && (y < _size.Y) && (y >= 0))
                    {
                        var p = new Pixel {Color = color};
                        _display[x, y] = p;
                    }
                }
            }
        }

        public static void DrawString(string str, Vector2 point, ConsoleColor fontColor)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if ((point.X + i >= 0) && (point.Y >= 0) && (point.X + i < _size.X) && (point.Y < _size.Y))
                {
                    _display[point.X + i, point.Y].Character = str[i];
                    _display[point.X + i, point.Y].CharColor = fontColor;
                    _display[point.X + i, point.Y].Changed = true;
                }
            }
        }

        public static void DrawString(string str, Vector2 point, ConsoleColor fontColor, ConsoleColor backColor)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if ((point.X + i >= 0) && (point.Y >= 0) && (point.X + i < _size.X) && (point.Y < _size.Y))
                {
                    _display[point.X + i, point.Y].Character = str[i];
                    _display[point.X + i, point.Y].CharColor = fontColor;
                    _display[point.X + i, point.Y].CharColor = backColor;
                    _display[point.X + i, point.Y].Changed = true;
                }
            }
        }


        public static void Draw()
        {
            Console.SetWindowSize(_size.X, _size.Y);
            Console.CursorTop = 0;
            Console.CursorLeft = 0;

            for (var x = 0; x < _size.X; x++)
            {
                for (var y = 0; y < _size.Y; y++)
                {
                    if (_display[x, y].Changed)
                    {
                        Console.BackgroundColor = _display[x, y].Color;
                        Console.ForegroundColor = _display[x, y].CharColor;
                        Console.CursorTop = y;
                        Console.CursorLeft = x;
                        Console.Write(_display[x, y].Character);
                        _display[x, y].Changed = false;
                    }
                }
            }

            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
    }
}