using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BusinessWindow
{
    public class BusinessWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _businessNameText;
        [SerializeField] private TextMeshProUGUI _businessLevelText;
        [SerializeField] private TextMeshProUGUI _businessIncomeText;
        [SerializeField] private TextMeshProUGUI _firstBusinessImpName;
        [SerializeField] private TextMeshProUGUI _firstBusinessImpPrice;
        [SerializeField] private TextMeshProUGUI _firstBusinessImpMultiplier;
        [SerializeField] private TextMeshProUGUI _secondBusinessImpName;
        [SerializeField] private TextMeshProUGUI _secondBusinessImpPrice;
        [SerializeField] private TextMeshProUGUI _secondBusinessImpMultiplier;
        [SerializeField] private TextMeshProUGUI _businessLevelUpPriceText;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private Button _businessLevelUpButton;
        [SerializeField] private Button _firstBusinessImpButton;
        [SerializeField] private Button _secondBusinessImpButton;
        
        public TextMeshProUGUI BusinessNameText => _businessNameText;
        public TextMeshProUGUI BusinessLevelText => _businessLevelText;
        public TextMeshProUGUI BusinessIncomeText => _businessIncomeText;
        public TextMeshProUGUI FirstBusinessImpName => _firstBusinessImpName;
        public TextMeshProUGUI FirstBusinessImpPrice => _firstBusinessImpPrice;
        public TextMeshProUGUI SecondBusinessImpName => _secondBusinessImpName;
        public TextMeshProUGUI SecondBusinessImpPrice => _secondBusinessImpPrice;
        public TextMeshProUGUI BusinessLevelUpPriceText => _businessLevelUpPriceText;
        public TextMeshProUGUI FirstBusinessImpMultiplier => _firstBusinessImpMultiplier;
        public TextMeshProUGUI SecondBusinessImpMultiplier => _secondBusinessImpMultiplier;
        public Slider ProgressBar => _progressBar;
        public Button BusinessLevelUpButton => _businessLevelUpButton;
        public Button FirstBusinessImpButton => _firstBusinessImpButton;
        public Button SecondBusinessImpButton => _secondBusinessImpButton;
    }
}
