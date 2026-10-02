using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class SaveLoadScore
    {
        private const string ScoreKey = "CurrentScore";
        private const string HighScoreKey = "HighScore";
        private const string BoosterKey = "Booster";
        private const string BoosterDataKey = "BoosterData";

        public void SaveScore(int score, int highScore, BoosterData boosterData)
        {
            PlayerPrefs.DeleteKey(BoosterKey);
            PlayerPrefs.SetString(BoosterDataKey, JsonUtility.ToJson(boosterData));
            PlayerPrefs.SetInt(ScoreKey, score);
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        public LoadedScore LoadScore()
        {
            return new LoadedScore(
                PlayerPrefs.GetInt(ScoreKey, 0),
                PlayerPrefs.GetInt(HighScoreKey, 0),
                LoadBoosterData());
        }

        private BoosterData LoadBoosterData()
        {
            //*** "BoosterKey" is deprecated, use only for reverse optimization ***

            if (PlayerPrefs.GetInt(BoosterKey) == 1)
                return new BoosterData(true, 2, 3);

            return ParseBoosterData(PlayerPrefs.GetString(BoosterDataKey, ""));
        }

        private static BoosterData ParseBoosterData(string saveData)
        {
            if (string.IsNullOrEmpty(saveData))
                return new BoosterData(false, 0, 0);

            try
            {
                BoosterData data = JsonUtility.FromJson<BoosterData>(saveData);
                return data ?? new BoosterData(false, 0, 0);
            }
            catch
            {
                return new BoosterData(false, 0, 0);
            }
        }

        public readonly struct LoadedScore
        {
            public readonly int Score;
            public readonly int HighScore;
            public readonly BoosterData BoosterData;

            public LoadedScore(int score, int highScore, BoosterData boosterData)
            {
                Score = score;
                HighScore = highScore;
                BoosterData = boosterData;
            }
        }
    }
}
