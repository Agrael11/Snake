using System.IO;
using Snake.Scenes;

namespace Snake
{
    public class Game
    {
        public enum Scenes
        {
            MainMenu,
            InGame,
            HighScore,
            SaveGame,
            LoadGame,
            Pause,
            Question
        }

        public Scenes CurrentScene = Scenes.MainMenu;
        public HighscoresScene HighscoreScn;
        public GameScene InGame;
        public int Level = 1;

        public int Lives = 5;
        public LoadSelectScene LoadQuestion;
        public MainMenuScene MainMenu;
        public PauseScene Pause;
        public ExitQuestion Question;
        public SaveSelectScene SaveQuestion;
        public int Score = 0;

        public void Init()
        {
            if (!Directory.Exists("Levels")) Directory.CreateDirectory("Levels");
            if (!Directory.Exists("Saves")) Directory.CreateDirectory("Saves");
            if (!File.Exists("Saves\\Highscores_MP.xml"))
            {
                var scores = new Highscores();
                FileController.Save(scores, "Saves\\Highscores_MP.xml");
            }
            if (!File.Exists("Saves\\Highscores_SP.xml"))
            {
                var scores = new Highscores();
                FileController.Save(scores, "Saves\\Highscores_SP.xml");
            }
            Pause = new PauseScene(this);
            MainMenu = new MainMenuScene(this);
            InGame = new GameScene(this);
            Question = new ExitQuestion(this);
            SaveQuestion = new SaveSelectScene(this);
            LoadQuestion = new LoadSelectScene(this);
            HighscoreScn = new HighscoresScene(this);
        }

        public void Update()
        {
            switch (CurrentScene)
            {
                case Scenes.MainMenu:
                    MainMenu.Update();
                    break;
                case Scenes.InGame:
                    InGame.Update();
                    break;
                case Scenes.Pause:
                    Pause.Update();
                    break;
                case Scenes.Question:
                    Question.Update();
                    break;
                case Scenes.SaveGame:
                    SaveQuestion.Update();
                    break;
                case Scenes.LoadGame:
                    LoadQuestion.Update();
                    break;
                case Scenes.HighScore:
                    HighscoreScn.Update();
                    break;
            }
        }

        public void Draw()
        {
            Renderer.Clean();

            DrawScene(CurrentScene);
        }

        public void DrawScene(Scenes scene)
        {
            switch (scene)
            {
                case Scenes.MainMenu:
                    MainMenu.Draw();
                    break;
                case Scenes.InGame:
                    InGame.Draw();
                    break;
                case Scenes.Pause:
                    InGame.Draw();
                    Pause.Draw();
                    break;
                case Scenes.Question:
                    DrawScene(Question.Last);
                    Question.Draw();
                    break;
                case Scenes.SaveGame:
                    DrawScene(SaveQuestion.Last);
                    SaveQuestion.Draw();
                    break;
                case Scenes.LoadGame:
                    DrawScene(LoadQuestion.Last);
                    LoadQuestion.Draw();
                    break;
                case Scenes.HighScore:
                    HighscoreScn.Draw();
                    break;
            }
        }
    }
}