using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Core;
using _Bludoku.Scripts.Score;
using _Bludoku.Scripts.UI;
using _Bludoku.Services;
using System;
using UnityEngine;

namespace _Bludoku.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public event Action OnGameStart;
        public event Action OnNewGame;

        [SerializeField] private ScoreMediator scoreMediator;
        [SerializeField] private UIMediator uiMediator;
        [SerializeField] private Board board;
        [SerializeField] private FiguresController figuresController;
        [SerializeField] private AnalyticService analytics;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            figuresController.OnGameOver += HandleGameOver;

            board.LoadGrid();
            figuresController.LoadFigures();
            analytics.SubscribeAnalyticEvenst();
            OnGameStart?.Invoke();
        }

        public void NewGame()
        {
            BoardSaveLoad.Delete();
            board.ResetBoard();
            figuresController.ResetFigures();
            uiMediator.HideGameOver();
            scoreMediator.ResetScore();
            OnNewGame?.Invoke();
        }

        public void SecondChance()
        {
            uiMediator.HideGameOver();
            figuresController.UpdateToEasyFigures();
        }

        private void HandleGameOver()
        {
            uiMediator.ShowGameOver();
        }

        public void Update()
        {
            if( Input.GetKeyUp(KeyCode.E))
            {
                NewGame();
            }
        }
    }
}