using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class SaveLoadScore
    {
        private const string ScoreKey = "CurrentScore";
        private const string HighScoreKey = "HighScore";

        public void SaveScore(int score, int highScore)
        {
            PlayerPrefs.SetInt(ScoreKey, score);
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        public LoadedScore LoadScore()
        {
            return new LoadedScore(
                PlayerPrefs.GetInt(ScoreKey, 0),
                PlayerPrefs.GetInt(HighScoreKey, 0));
        }

        public readonly struct LoadedScore
        {
            public readonly int Score;
            public readonly int HighScore;

            public LoadedScore(int score, int highScore)
            {
                Score = score;
                HighScore = highScore;
            }
        }
    }
}
