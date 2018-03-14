using System;

namespace Snake
{
    [Serializable]
    public class SaveState
    {
        public int Level = 0;
        public int Lives = 0;
        public int Score = 0;
    }
}