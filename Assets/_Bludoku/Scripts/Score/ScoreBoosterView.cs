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
        private int _cashedComboCount;
        private Tween _pulseTween;
        private Tween _comboCountTween;

        private void Awake()
        {
            booster.localScale = Vector3.zero;
        }

        public void UpdateBoosterView(BoosterData data)
        {
            _comboCount.text = "X " + data.CurrentComboCount.ToString();

            _effects.UpdateBoostEffects(data);

            if (data.IsBusted && _cashedComboCount != data.CurrentComboCount)
            {
                PlayComboCountTextAnimation();
                _cashedComboCount = data.CurrentComboCount;
            }

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

        private void PlayComboCountTextAnimation()
        {
            _comboCountTween?.Kill();

            _comboCount.rectTransform.localScale = Vector3.one;
            _comboCount.rectTransform.localRotation = Quaternion.identity;

            _comboCountTween = DOTween.Sequence()
                .Append(
                    _comboCount.rectTransform.DOScale(
                        Vector3.one * 1.25f,
                        0.15f)
                    .SetEase(Ease.OutBack))
                .Join(
                    _comboCount.rectTransform.DORotate(
                        new Vector3(0, 0, 12f),
                        0.15f))
                .Append(
                    _comboCount.rectTransform.DORotate(
                        new Vector3(0, 0, -8f),
                        0.1f))
                .Append(
                    _comboCount.rectTransform.DORotate(
                        Vector3.zero,
                        0.1f))
                .Join(
                    _comboCount.rectTransform.DOScale(
                        Vector3.one,
                        0.2f)
                    .SetEase(Ease.OutQuad));
        }

        private void OnDisable()
        {
            booster.DOKill();
            _pulseTween?.Kill();
            _pulseTween = null;
        }
    }
}