namespace _Bludoku.Scripts.Score
{
    public class ScoreBoostSystem
    {
        private readonly SaveLoadBooster _saveLoad = new();

        private int _movesCount;
        private int _comboCount;

        private const int MovesThreshold = 3;
        private const int MinComboToMultiply = 2;
        
        public bool IsBoosted
        {
            get => _comboCount >= MinComboToMultiply;
            set
            {
                if (value)
                {
                    _comboCount = MinComboToMultiply;
                }
                else
                {
                    _comboCount = 0;
                }
            }
        }

        public void FigurePlaced(int removes)
        {
            if (removes == 0)
            {
                _movesCount++;
                //_comboCount--;
            }
            else
            {
                _movesCount = 0;
                _comboCount++;
            }
            
            if (_movesCount >= MovesThreshold)
            {
                _comboCount = 0;
            }
        }

        public BoosterData GetBoosterData()
        {
            return new BoosterData(IsBoosted, _comboCount, _movesCount);
        }

        public void SetBoosterData(BoosterData data)
        {
            _comboCount = data.CurrentComboCount;
            _movesCount = data.MovesToClearCombo;
        }

        public void Load()
        {
            SetBoosterData(_saveLoad.LoadBooster());
        }

        public void Save()
        {
            _saveLoad.SaveBooster(GetBoosterData());
        }
    }
}