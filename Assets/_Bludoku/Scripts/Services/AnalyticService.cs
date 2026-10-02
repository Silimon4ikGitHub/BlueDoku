using _Bludoku.Scripts;
using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Core;
using _Bludoku.Scripts.Score;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Bludoku.Services
{
    public sealed class AnalyticService : MonoBehaviour
    {
        [SerializeField] private Board boardController;
        [SerializeField] private FiguresController figuresController;

        public void SubscribeAnalyticEvenst()
        {
            GameController.Instance.OnGameStart += () => SendAnalyticEvent("game_start");
            GameController.Instance.OnNewGame += () => SendAnalyticEvent("new_game");
            boardController.OnFigurePlaced += (x) => SendAnalyticEvent("figures_cleared", "cleared_count", x.FiguresRemovedCount.ToString());
            figuresController.OnGameOver += () => SendAnalyticEvent("game_over");
            figuresController.OnFiguresPicked += (x) => SendAnalyticEvent("figures_picked", "id", x.ID.ToString());
            figuresController.OnFigureDragged += (x) => SendAnalyticEvent("figures_dragged", "id", x.ID.ToString());
            figuresController.OnPlaceFigure += (x) => SendAnalyticEvent("place_figure", "id", x.ID.ToString());
            ScoreSystem.OnReceiveBonus += (x) => SendAnalyticEvent("receive_bonus", "value", x.ToString());
        }

        public void SendAnalyticEvent(string eventName)
        {

        }
        public void SendAnalyticEvent(string eventName, string parameter, string value)
        {

        }
        public void SendAnalyticEvent(string eventName, Dictionary<string, string> data)
        {

        }

        private void UnsubscribeAnalyticEvenst()
        {
            GameController.Instance.OnGameStart -= () => SendAnalyticEvent("game_start");
            GameController.Instance.OnNewGame -= () => SendAnalyticEvent("new_game");
            boardController.OnFigurePlaced -= (x) => SendAnalyticEvent("figures_cleared", "cleared_count", x.FiguresRemovedCount.ToString());
            figuresController.OnGameOver -= () => SendAnalyticEvent("game_over");
            figuresController.OnFiguresPicked -= (x) => SendAnalyticEvent("figures_picked", "id", x.ID.ToString());
            figuresController.OnFigureDragged -= (x) => SendAnalyticEvent("figures_dragged", "id", x.ID.ToString());
            figuresController.OnPlaceFigure -= (x) => SendAnalyticEvent("place_figure", "id", x.ID.ToString());
            ScoreSystem.OnReceiveBonus -= (x) => SendAnalyticEvent("receive_bonus", "value", x.ToString());
        }

        public void OnDestroy()
        {
            UnsubscribeAnalyticEvenst();
        }
    }
}
