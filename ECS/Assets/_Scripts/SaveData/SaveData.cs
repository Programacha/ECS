using System;

namespace _Scripts.SaveData
{
    [System.Serializable]
    public class SaveData {
        public float Balance;
        public BusinessSave[] Businesses;
    }

    [System.Serializable]
    public class BusinessSave {
        public int Level;
        public float Progress;
        public bool Upgrade1;
        public bool Upgrade2;
    }
}