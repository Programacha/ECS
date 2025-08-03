using System;
using UnityEngine;

namespace _Scripts.Configs
{
    [CreateAssetMenu(fileName = "BusinessesConfig", menuName = "Config/BusinessesConfig")]
    public class BusinessesConfig : ScriptableObject
    {
        public BusinessData[] Businesses;
    }

    [System.Serializable]
    public class BusinessData {
        public string Name;
        public float Delay;
        public float BaseCost;
        public float BaseIncome;
        public UpgradeData Upgrade1;
        public UpgradeData Upgrade2;
    }

    [System.Serializable]
    public class UpgradeData {
        public string Name;
        public float Cost;
        public float Multiplier;
    }
}
