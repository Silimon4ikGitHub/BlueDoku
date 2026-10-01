using _Bludoku.Scripts.Boards;
using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class ScoreMediator : MonoBehaviour
    {
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private Board board;
        [SerializeField] private ScoreBoosterView boosterView;
        
        private readonly ScoreBoostSystem _scoreBoostSystem = new();

        private void Awake()
        {
            board.OnFigurePlaced += FigurePlaced;
        }

        private void Start()
        {
            ScoreSystem.LoadScore();
            _scoreBoostSystem.SetBoosterData(ScoreSystem.CurrentBooster);
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
            ScoreSystem.AddSetScore(result.ClearedCount);
            AddCombo(result);
            scoreView.UpdateScore();
        }

        private void UpdateView()
        {
            boosterView.UpdateBoosterView(_scoreBoostSystem.GetBoosterData());
            _scoreBoostSystem.IsBoosted = false;
            scoreView.UpdateScore(false);
        }

        private void AddCombo(ClearResult result)
        {
            _scoreBoostSystem.FigurePlaced(result.ClearedCount);
            var boostData = _scoreBoostSystem.GetBoosterData();
            ScoreSystem.UpdateBoosterData(boostData);
            boosterView.UpdateBoosterView(boostData);
        }
    }
}