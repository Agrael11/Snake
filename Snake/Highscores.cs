using System;

namespace Snake
{
    [Serializable]
    public class Highscores
    {
        public Score[] Scores =
        {
            new Score(), new Score(), new Score(), new Score(), new Score(), new Score(),
            new Score(), new Score(), new Score(), new Score()
        };
    }
}