using System;
using UnityEngine;
using _Bludoku.Scripts.Boards;

namespace _Bludoku.Scripts.Score
{
    public class ScoreMediator : MonoBehaviour
    {
        public Action<int> OnReceiveBonus;
        public Action<ClearResult, BoosterData> OnComboIncreased;

        [SerializeField] private ScoreView scoreView;
        [SerializeField] private Board board;
        [SerializeField] private ScoreBoosterView boosterView;
        
        private readonly ScoreBoostSystem _scoreBoostSystem = new();

        private void OnEnable()
        {
            board.OnFigurePlaced += FigurePlaced;
        }

        private void OnDisable()
        {
            if(board != null)
                board.OnFigurePlaced -= FigurePlaced;
        }

        private void Start()
        {
            ScoreSystem.LoadScore();
            _scoreBoostSystem.Load();
            boosterView.UpdateBoosterView(_scoreBoostSystem.GetBoosterData());
            scoreView.UpdateScore(false);
        }

        public void ResetScore()
        {
            ScoreSystem.ResetScore();
            UpdateView();
        }

        private void FigurePlaced(ClearResult result)
        {
            AddCombo(result);
            AddScore(result);
            scoreView.UpdateScore();

            ScoreSystem.SaveScore();
            _scoreBoostSystem.Save();
        }

        private void UpdateView()
        {
            _scoreBoostSystem.SetBoosterData(new BoosterData(false, 0, 0));
            boosterView.UpdateBoosterView(_scoreBoostSystem.GetBoosterData());
            _scoreBoostSystem.Save();
            scoreView.UpdateScore(false);
        }

        private void AddScore(ClearResult result)
        {
            int multiplier = _scoreBoostSystem.IsBoosted ? _scoreBoostSystem.CurrentComboCount : 1;
            ScoreSystem.AddSetScore(result.ClearedCount, multiplier);

            if (multiplier > 1 && result.ClearedCount > 0)
            {
                int baseScore = ScoreSystem.CalculateScoreBySetsCount(result.ClearedCount);
                OnReceiveBonus?.Invoke(baseScore * multiplier - baseScore);
            }
        }

        private void AddCombo(ClearResult result)
        {
            _scoreBoostSystem.FigurePlaced(result.ClearedCount);
            var boostData = _scoreBoostSystem.GetBoosterData();
            boosterView.UpdateBoosterView(boostData);

            if (_scoreBoostSystem.IsComboIncreased())
            {
                OnComboIncreased?.Invoke(result, boostData);
            }
        }
    }
}