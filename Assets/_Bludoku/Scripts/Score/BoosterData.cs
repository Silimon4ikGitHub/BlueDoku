namespace _Bludoku.Scripts.Score
{
    public class BoosterData
    {
        public bool IsBusted;
        public int CurrentComboCount;
        public int MovesToClearCombo;

        public BoosterData(bool isBusted, int currentComboCount, int movesToClearCombo)
        {
            IsBusted = isBusted;
            CurrentComboCount = currentComboCount;
            MovesToClearCombo = movesToClearCombo;
        }
    }
}
