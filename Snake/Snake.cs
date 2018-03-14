using System;
using System.Collections.Generic;
using Snake.Scenes;

namespace Snake
{
    public class Snake
    {
        public enum DeathCause
        {
            CollisionSelf,
            CollisionOppenent,
            CollisionWall,
            TooShort
        }

        public enum Side
        {
            Left,
            Right,
            Up,
            Down
        }

        private readonly GameScene _parent;
        private readonly bool _playerOne;
        private readonly Vector2 _position;

        private readonly List<Vector2> _tail = new List<Vector2>();
        private Side _direction;
        private bool _leaveTail;
        private Side _nextDirection;
        private int _timer;

        public Dictionary<Side, ConsoleKey> Controls;
        public int NeedToComplete;

        public int Speed;

        public Snake(GameScene parent, bool playerOne, Vector2 startPosition, int neededSize, int startSpeed,
            int startSize, Side startDirection, Dictionary<Side, ConsoleKey> controls)
        {
            _parent = parent;

            Controls = controls;

            _playerOne = playerOne;
            _position = new Vector2(startPosition.X, startPosition.Y);
            Speed = startSpeed;
            _direction = startDirection;
            _nextDirection = startDirection;
            NeedToComplete = neededSize;

            for (var i = startSize; i > 1; i--)
            {
                var pos = new Vector2(startPosition.X, startPosition.Y);
                switch (startDirection)
                {
                    case Side.Left:
                        pos.X += i - 1;
                        break;
                    case Side.Right:
                        pos.X -= i - 1;
                        break;
                    case Side.Up:
                        pos.Y += i - 1;
                        break;
                    case Side.Down:
                        pos.Y -= i - 1;
                        break;
                }
                _tail.Add(pos);
            }
        }

        public int Size
        {
            get { return _tail.Count; }
            set
            {
                while (value < _tail.Count)
                {
                    _tail.RemoveAt(0);
                }
                while (value > _tail.Count + 1)
                {
                    var pos = new Vector2(_position.X, _position.Y);
                    switch (_nextDirection)
                    {
                        case Side.Down:
                            pos.Y--;
                            break;
                        case Side.Up:
                            pos.Y++;
                            break;
                        case Side.Left:
                            pos.X++;
                            break;
                        case Side.Right:
                            pos.X--;
                            break;
                    }
                    _tail.Add(pos);
                }
            }
        }

        public event EventHandler<DeathCause> Died;
        public event EventHandler GotLive;
        public event EventHandler<int> GotScore;
        public event EventHandler Complete;

        public event EventHandler<Vector2> AtePoint;
        public event EventHandler<Vector2> AtePoision;
        public event EventHandler<Vector2> AteLive;

        public void Control(ConsoleKeyInfo pressed)
        {
            if ((_direction != Side.Up) && (pressed.Key == Controls[Side.Down]))
                _nextDirection = Side.Down;
            else if ((_direction != Side.Down) && (pressed.Key == Controls[Side.Up]))
                _nextDirection = Side.Up;
            else if ((_direction != Side.Left) && (pressed.Key == Controls[Side.Right]))
                _nextDirection = Side.Right;
            else if ((_direction != Side.Right) && (pressed.Key == Controls[Side.Left]))
                _nextDirection = Side.Left;
        }

        public void Update(List<Vector2> lives, List<Vector2> points, List<Vector2> poisons, List<Rectangle> colliders,
            Snake oponnent)
        {
            _timer++;
            if (_timer == Speed)
            {
                _timer = 0;
                var pos = new Vector2(_position.X, _position.Y);
                _tail.Add(pos);

                _direction = _nextDirection;

                #region Movement

                switch (_direction)
                {
                    case Side.Left:
                        _position.X -= 1;
                        break;
                    case Side.Right:
                        _position.X += 1;
                        break;
                    case Side.Up:
                        _position.Y -= 1;
                        break;
                    case Side.Down:
                        _position.Y += 1;
                        break;
                }

                #endregion

                Rectangle head = _position;

                if (!_leaveTail)
                {
                    if (_tail.Count > 0) _tail.RemoveAt(0);
                }
                else _leaveTail = false;

                #region Points collisions

                foreach (var live in lives)
                {
                    if (head.Intersects(live))
                    {
                        AteLive?.Invoke(this, live);

                        GotLive?.Invoke(this, null);
                    }
                }

                foreach (var point in points)
                {
                    if (head.Intersects(point))
                    {
                        _leaveTail = true;

                        AtePoint?.Invoke(this, point);

                        GotScore?.Invoke(this, 100);
                    }
                }

                foreach (var poison in poisons)
                {
                    if (head.Intersects(poison))
                    {
                        AtePoision?.Invoke(this, poison);

                        GotScore?.Invoke(this, -100);
                        if (_tail.Count > 0) _tail.RemoveAt(0);
                        else
                        {
                            Died?.Invoke(this, DeathCause.TooShort);
                        }
                    }
                }

                #endregion

                #region Collision wall

                foreach (var collider in colliders)
                {
                    if (head.Intersects(collider))
                    {
                        Died?.Invoke(this, DeathCause.CollisionWall);
                    }
                }

                #endregion

                #region SelfCollsion

                foreach (var tail in _tail)
                {
                    if (head.Intersects(tail))
                    {
                        Died?.Invoke(this, DeathCause.CollisionSelf);
                        break;
                    }
                }

                #endregion

                #region Multiplayer Collision

                if (_parent.MultiPlayer && _playerOne && oponnent.CheckCollisionFull(_position))
                {
                    Died?.Invoke(this, DeathCause.CollisionOppenent);
                }

                #endregion

                if (_position.X >= Renderer.WindowSize.X) _position.X = 0;
                else if (_position.X < 0) _position.X = Renderer.WindowSize.X;

                if (_position.Y >= Renderer.WindowSize.Y) _position.Y = 2;
                else if (_position.Y < 2) _position.Y = Renderer.WindowSize.Y;

                if (_playerOne)
                {
                    if (!_parent.MultiPlayer)
                    {
                        if (_tail.Count >= NeedToComplete)
                        {
                            Complete?.Invoke(this, null);
                        }
                    }
                    else
                    {
                        if (_tail.Count + oponnent._tail.Count >= NeedToComplete*2)
                        {
                            Complete?.Invoke(this, null);
                        }
                    }
                }
            }
        }

        public bool CheckCollisionFull(Vector2 pos)
        {
            if (pos == _position) return true;
            foreach (var position in _tail)
            {
                if (position == pos) return true;
            }

            return false;
        }

        public bool CheckCollisionFull(Rectangle rec)
        {
            if (rec.Intersects(_position)) return true;
            foreach (var position in _tail)
            {
                if (rec.Intersects(position)) return true;
            }

            return false;
        }

        public void Draw()
        {
            foreach (var pos in _tail)
            {
                Renderer.DrawPoint(pos, _playerOne ? ConsoleColor.Yellow : ConsoleColor.Cyan);
            }

            Renderer.DrawPoint(_position, _playerOne ? ConsoleColor.DarkYellow : ConsoleColor.Blue);

            if (_direction == Side.Up) Renderer.DrawString("▲", _position, ConsoleColor.DarkRed);
            else if (_direction == Side.Down) Renderer.DrawString("▼", _position, ConsoleColor.DarkRed);
            else if (_direction == Side.Left) Renderer.DrawString(":", _position, ConsoleColor.DarkRed);
            else if (_direction == Side.Right) Renderer.DrawString(":", _position, ConsoleColor.DarkRed);
        }
    }
}