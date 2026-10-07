using UnityEngine;

namespace _Bludoku.Scripts.Score
{
    public class SaveLoadBooster
    {
        private const string BoosterKey = "Booster";
        private const string BoosterDataKey = "BoosterData";

        public void SaveBooster(BoosterData boosterData)
        {
            PlayerPrefs.DeleteKey(BoosterKey);
            PlayerPrefs.SetString(BoosterDataKey, JsonUtility.ToJson(boosterData));
            PlayerPrefs.Save();
        }

        public BoosterData LoadBooster()
        {
            //*** "BoosterKey" is deprecated, use only for reverse optimization ***

            if (PlayerPrefs.GetInt(BoosterKey) == 1)
                return new BoosterData(true, 2, 3);

            return ParseBoosterData(PlayerPrefs.GetString(BoosterDataKey, ""));
        }

        private static BoosterData ParseBoosterData(string saveData)
        {
            if (string.IsNullOrEmpty(saveData))
                return new BoosterData(false, 0, 0);

            try
            {
                BoosterData data = JsonUtility.FromJson<BoosterData>(saveData);
                return data ?? new BoosterData(false, 0, 0);
            }
            catch
            {
                return new BoosterData(false, 0, 0);
            }
        }
    }
}
