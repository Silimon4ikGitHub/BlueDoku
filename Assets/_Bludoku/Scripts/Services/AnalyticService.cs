using _Bludoku.Scripts;
using _Bludoku.Scripts.Blocks;
using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Core;
using _Bludoku.Scripts.Score;
using System.Collections.Generic;
using UnityEngine;

namespace _Bludoku.Services
{
    public sealed class AnalyticService : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private Board boardController;
        [SerializeField] private FiguresController figuresController;
        [SerializeField] private ScoreMediator scoreMediator;

        private readonly IAnalyticsProvider[] _providers =
        {
            new DebugAnalyticsProvider(),
        };

        private void Awake()
        {
            foreach (var provider in _providers)
            {
                provider.Initialize();
            }
        }

        private void OnEnable()
        {
            gameController.OnGameStart += OnGameStart;
            gameController.OnNewGame += OnNewGame;
            boardController.OnFigurePlaced += OnFigurePlaced;
            figuresController.OnGameOver += OnGameOver;
            figuresController.OnFiguresPicked += OnFigurePicked;
            figuresController.OnFigureDragged += OnFigureDragged;
            figuresController.OnPlaceFigure += OnPlaceFigure;
            scoreMediator.OnReceiveBonus += OnReceiveBonus;
        }

        private void OnDisable()
        {
            if (gameController != null)
            {
                gameController.OnGameStart -= OnGameStart;
                gameController.OnNewGame -= OnNewGame;
            }

            if (boardController != null)
            {
                boardController.OnFigurePlaced -= OnFigurePlaced;
            }

            if (figuresController != null)
            {
                figuresController.OnGameOver -= OnGameOver;
                figuresController.OnFiguresPicked -= OnFigurePicked;
                figuresController.OnFigureDragged -= OnFigureDragged;
                figuresController.OnPlaceFigure -= OnPlaceFigure;
            }

            if (scoreMediator != null)
            {
                scoreMediator.OnReceiveBonus -= OnReceiveBonus;
            }
        }

        public void SendAnalyticEvent(string eventName)
        {
            SendAnalyticEvent(eventName, null);
        }

        public void SendAnalyticEvent(string eventName, string parameter, string value)
        {
            SendAnalyticEvent(eventName, new Dictionary<string, string> { { parameter, value } });
        }

        public void SendAnalyticEvent(string eventName, IReadOnlyDictionary<string, string> parameters)
        {
            foreach (var provider in _providers)
            {
                provider.SendEvent(eventName, parameters);
            }
        }

        private void OnGameStart()
        {
            SendAnalyticEvent("game_start");
        }

        private void OnNewGame()
        {
            SendAnalyticEvent("new_game");
        }

        private void OnFigurePlaced(ClearResult result)
        {
            SendAnalyticEvent("figures_cleared", "cleared_count", result.FiguresRemovedCount.ToString());
        }

        private void OnGameOver()
        {
            SendAnalyticEvent("game_over");
        }

        private void OnFigurePicked(Figure figure)
        {
            SendAnalyticEvent("figures_picked", "id", figure.ID.ToString());
        }

        private void OnFigureDragged(Figure figure)
        {
            SendAnalyticEvent("figures_dragged", "id", figure.ID.ToString());
        }

        private void OnPlaceFigure(Figure figure)
        {
            SendAnalyticEvent("place_figure", "id", figure.ID.ToString());
        }

        private void OnReceiveBonus(int bonus)
        {
            SendAnalyticEvent("receive_bonus", "value", bonus.ToString());
        }
    }
}
