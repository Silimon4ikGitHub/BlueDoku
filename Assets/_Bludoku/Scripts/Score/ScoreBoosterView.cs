using System;
using DG.Tweening;
using UnityEngine;
using TMPro;

namespace _Bludoku.Scripts.Score
{
    public class ScoreBoosterView : MonoBehaviour
    {
        [SerializeField] private Transform booster;
        [SerializeField] private TextMeshProUGUI _comboCount;
        [SerializeField] private BoostEffects _effects;
        
        private bool _isBoosterEnabled;
        private Tween _pulseTween;

        private void Awake()
        {
            booster.localScale = Vector3.zero;
        }

        public void UpdateBoosterView(BoosterData data)
        {
            Debug.Log("[ScoreBoosterView] SetBoosterEnabled " + data.IsBusted.ToString() + data.CurrentComboCount.ToString() + data.MovesToClearCombo.ToString());
            _comboCount.text = "X " + data.CurrentComboCount.ToString();
            _effects.UpdateBoostEffects(data);
            if (_isBoosterEnabled == data.IsBusted)
                return;

            booster.DOKill();
            _pulseTween?.Kill();
            _pulseTween = null;

            if (data.IsBusted)
            {
                booster.transform.DOScale(Vector3.one, 0.8f)
                    .SetEase(Ease.OutElastic)
                    .OnComplete(StartPulse);
            }
            else
            {
                booster.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            }

            _isBoosterEnabled = data.IsBusted;
        }

        private void StartPulse()
        {
            if (!_isBoosterEnabled)
                return;

            _pulseTween = booster.transform.DOScale(1.08f, 0.45f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            booster.DOKill();
            _pulseTween?.Kill();
            _pulseTween = null;
        }
    }
}