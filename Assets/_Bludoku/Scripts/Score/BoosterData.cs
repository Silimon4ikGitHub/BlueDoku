using System;

namespace _Bludoku.Scripts.Score
{
    [Serializable]
    public class BoosterData
    {
        public bool IsBoosted;
        public int CurrentComboCount;
        public int MovesToClearCombo;

        public BoosterData(bool isBoosted, int currentComboCount, int movesToClearCombo)
        {
            IsBoosted = isBoosted;
            CurrentComboCount = currentComboCount;
            MovesToClearCombo = movesToClearCombo;
        }
    }
}
