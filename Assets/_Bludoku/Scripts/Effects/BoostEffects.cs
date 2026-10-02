using _Bludoku.Scripts.Score;
using UnityEngine;

public class BoostEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _snowParticles;

    public void UpdateBoostEffects(BoosterData data)
    {
        float combo = data.CurrentComboCount;
        var main = _snowParticles.main;
        main.startSize = new ParticleSystem.MinMaxCurve(5 * combo, 10 * combo);

        var velocity = _snowParticles.velocityOverLifetime;
        velocity.y = new ParticleSystem.MinMaxCurve(50 * combo, 100 * combo);
    }
}
