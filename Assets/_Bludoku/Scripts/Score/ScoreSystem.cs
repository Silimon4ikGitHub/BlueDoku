namespace _Bludoku.Scripts.Score
{
    public static class ScoreSystem
    {
        private static readonly SaveLoadScore _saveLoad = new();

        private static int _score;
        private static int _highScore;
        private static BoosterData _boosterData = new(false, 0, 0);

        private const int ScoreForSet = 1;
        private const float BoosterMultiplier = 1f;

        public static int Score => _score;
        public static int HighScore => _highScore;
        public static bool IsBoosterEnabled => _boosterData.IsBusted;
        public static BoosterData CurrentBooster => _boosterData;

        public static void UpdateBoosterData(BoosterData data)
        {
            _boosterData = data;
        }

        public static void LoadScore()
        {
            SaveLoadScore.LoadedScore saved = _saveLoad.LoadScore();
            _score = saved.Score;
            _highScore = saved.HighScore;
            _boosterData = saved.BoosterData;
        }
        
        public static void AddSetScore(int setsCount)
        {
            int scoreToAdd = setsCount * ScoreForSet;
            scoreToAdd = (int)(scoreToAdd * (IsBoosterEnabled ? _boosterData.CurrentComboCount * BoosterMultiplier : 1));
            
            AddScore(scoreToAdd);
        }
        
        public static void AddScore(int score)
        {
            _score = Score + score;
            if (HighScore < Score)
            {
                _highScore = Score;
            }
            
            SaveScore();
        }

        public static void ResetScore()
        {
            _score = 0;
            SaveScore();
        }

        private static void SaveScore()
        {
            _saveLoad.SaveScore(Score, HighScore, _boosterData);
        }
    }
}