namespace _Bludoku.Scripts.Score
{
    public class ScoreBoostSystem
    {
        public int CurrentComboCount => _comboCount;
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

        private int _movesCount;
        private int _comboCount;
        private bool _isComboIncreased;

        private const int MovesThreshold = 3;
        private const int MinComboToMultiply = 2;

        private readonly SaveLoadBooster _saveLoad = new();

        public void FigurePlaced(int removes)
        {
            if (removes == 0)
            {
                _movesCount++;
                _isComboIncreased = false;
            }
            else
            {
                _movesCount = 0;
                _comboCount++;
                _isComboIncreased = true;
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
            _isComboIncreased = false;
        }

        public void Load()
        {
            SetBoosterData(_saveLoad.LoadBooster());
        }

        public void Save()
        {
            _saveLoad.SaveBooster(GetBoosterData());
        }

        public bool IsComboIncreased()
        {
            return IsBoosted && _isComboIncreased;
        }
    }
}