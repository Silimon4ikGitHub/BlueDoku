using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Score;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private Board board;
        [SerializeField] private ScoreMediator scoreMediator;
        [SerializeField] private ParticleSystem figureParticles;
        [SerializeField] private ParticleSystem splashParticles;

        private ParticleEffect _figureParticleEffect;
        private ParticleEffect _splashParticleEffect;
        private VibrationEffect _vibrationEffect;

        private void Awake()
        {
            _figureParticleEffect = new ParticleEffect(figureParticles);
            _splashParticleEffect = new ParticleEffect(splashParticles);
            _vibrationEffect = new VibrationEffect();
        }

        private void OnEnable()
        {
            board.OnFigurePlaced += OnFigurePlaced;
            scoreMediator.OnComboIncreased += AnimateComboChange;
        }

        private void OnDisable()
        {
            if (board != null)
                board.OnFigurePlaced -= OnFigurePlaced;
            if (scoreMediator != null)
                scoreMediator.OnComboIncreased -= AnimateComboChange;
        }

        private void OnFigurePlaced(ClearResult result)
        {
            _vibrationEffect.Play(result);
            _figureParticleEffect.Play(result);
        }

        private void AnimateComboChange(ClearResult result, BoosterData data)
        {
            _splashParticleEffect.Play(result);
        }
    }
}
