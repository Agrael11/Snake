using System;
using System.Collections.Generic;

namespace Snake
{
    [Serializable]
    public class Level
    {
        public int MaxLives;

        public int MaxPoints;
        public int MaxPoisons;
        public int NeededSize;

        public List<Rectangle> Obstacles;
        public Vector2 Player1StartPosition;
        public Vector2 Player2StartPosition;

        public int SpeedModifier;
        public Snake.Side StartDirection;

        public int StartingSize;
    }
}