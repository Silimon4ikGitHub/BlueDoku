
namespace _Bludoku.Scripts.Score
{
    public static class ScoreSystem
    {
        private static readonly SaveLoadScore _saveLoad = new();

        private static int _score;
        private static int _highScore;

        private const int ScoreForSet = 1;

        public static int Score => _score;
        public static int HighScore => _highScore;

        public static void LoadScore()
        {
            SaveLoadScore.LoadedScore saved = _saveLoad.LoadScore();
            _score = saved.Score;
            _highScore = saved.HighScore;
        }
        
        public static void AddSetScore(int setsCount, int multiplier = 1)
        {
            int scoreForSets = CalculateScoreBySetsCount(setsCount);
            int scoreToAdd = (scoreForSets * multiplier);
            
            AddScore(scoreToAdd);
        }
        
        public static void AddScore(int score)
        {
            _score = Score + score;
            if (HighScore < Score)
            {
                _highScore = Score;
            }
        }

        public static void ResetScore()
        {
            _score = 0;
            SaveScore();
        }

        public static void SaveScore()
        {
            _saveLoad.SaveScore(Score, HighScore);
        }

        public static int CalculateScoreBySetsCount(int setsCount)
        {
            return setsCount * ScoreForSet;
        }
    }
}