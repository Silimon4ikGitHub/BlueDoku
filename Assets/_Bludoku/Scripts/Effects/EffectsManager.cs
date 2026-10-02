using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Score;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private Board board;
        [SerializeField] private ParticleSystem figureParticles;
        [SerializeField] private ParticleSystem splashParticles;

        private ParticleEffect _figureParticleEffect;
        private ParticleEffect _splashParticleEffect;
        private VibrationEffect _vibrationEffect;

        private int _cashedComboCount;

        private void Awake()
        {
            _figureParticleEffect = new ParticleEffect(figureParticles);
            _splashParticleEffect = new ParticleEffect(splashParticles);
            _vibrationEffect = new VibrationEffect();
            
            board.OnFigurePlaced += OnFigurePlaced;
        }

        private void OnFigurePlaced(ClearResult result)
        {
            _vibrationEffect.Play(result);
            _figureParticleEffect.Play(result);

            if (ScoreSystem.CurrentBooster.IsBusted || _cashedComboCount != ScoreSystem.CurrentBooster.CurrentComboCount)
            {
                _cashedComboCount = ScoreSystem.CurrentBooster.CurrentComboCount;
                _splashParticleEffect.Play(result);
            }
        }
    }
}
