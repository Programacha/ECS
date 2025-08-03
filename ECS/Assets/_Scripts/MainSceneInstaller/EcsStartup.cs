using _Scripts.BusinessWindow;
using _Scripts.Components;
using _Scripts.Configs;
using _Scripts.Systems;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace _Scripts.MainSceneInstaller {
    public class EcsStartup : MonoBehaviour {
        [SerializeField] private BusinessesConfig _config;
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private BusinessWindowView _viewPrefab;
        [SerializeField] private Transform _content;

        private EcsWorld _ecsWorld;
        private EcsSystems _runtimeSystems;

        private void Start() {
            _ecsWorld = new EcsWorld();
            
            CreateEntities();
            
           // var initSystems = new EcsSystems(_ecsWorld);
          //  initSystems.Add(new LoadSystem());
            //initSystems.Init();
            //initSystems.Destroy();


            _runtimeSystems = new EcsSystems(_ecsWorld);

            _runtimeSystems
                .Add(new BusinessProgressSystem())
                .Add(new LevelUpButtonSystem())
                .Add(new LevelUpApplySystem())
                .Add(new UpgradeButtonSystem())
                .Add(new UpgradeApplySystem())
                .Add(new UIUpdateSystem())
                .Add(new SaveSystem())
                .Init();
        }

        private void CreateEntities() {
            // --- Баланс ---
            var balanceEntity = _ecsWorld.NewEntity();
            ref var balance = ref balanceEntity.Get<BalanceComponent>();
            balance.Value = 150f;

            ref var balanceView = ref balanceEntity.Get<BalanceViewComponent>();
            balanceView.BalanceText = _balanceText;

            // --- Бизнесы ---
            for (int i = 0; i < _config.Businesses.Length; i++) {
                var entity = _ecsWorld.NewEntity();
                var data = _config.Businesses[i];

                ref var business = ref entity.Get<BusinessComponent>();
                business.Id = i;
                business.Level = (i == 0) ? 1 : 0;
                business.BaseCost = data.BaseCost;
                business.BaseIncome = data.BaseIncome;
                business.Delay = data.Delay;

                ref var income = ref entity.Get<IncomeComponent>();
                income.Progress = 0f;

                ref var upg1 = ref entity.Get<Upgrade1Component>();
                upg1.Cost = data.Upgrade1.Cost;
                upg1.Multiplier = data.Upgrade1.Multiplier;

                ref var upg2 = ref entity.Get<Upgrade2Component>();
                upg2.Cost = data.Upgrade2.Cost;
                upg2.Multiplier = data.Upgrade2.Multiplier;

                // --- UI ---
                var ui = Instantiate(_viewPrefab, _content);
                var uiView = ui.GetComponent<BusinessWindowView>();
                ref var view = ref entity.Get<BusinessViewComponent>();

                view.NameText = uiView.BusinessNameText;
                view.LevelText = uiView.BusinessLevelText;
                view.IncomeText = uiView.BusinessIncomeText;
                view.CostText = uiView.BusinessLevelUpPriceText;
                view.ProgressBar = uiView.ProgressBar;
                view.LevelUpBtn = uiView.BusinessLevelUpButton;
                view.Upgrade1Btn = uiView.FirstBusinessImpButton;
                view.Upgrade2Btn = uiView.SecondBusinessImpButton;
                view.Upgrade1NameText = uiView.FirstBusinessImpName;
                view.Upgrade2NameText = uiView.SecondBusinessImpName;
                view.Upgrade1PriceText = uiView.FirstBusinessImpPrice;
                view.Upgrade2PriceText = uiView.SecondBusinessImpPrice;

                // Установка начальных значений из конфига
                view.NameText.text = data.Name;
                view.Upgrade1NameText.text = data.Upgrade1.Name;
                view.Upgrade2NameText.text = data.Upgrade2.Name;
                view.Upgrade1PriceText.text = data.Upgrade1.Cost.ToString();
                view.Upgrade2PriceText.text = data.Upgrade2.Cost.ToString();
            }
        }

        private void Update() => _runtimeSystems?.Run();

        private void OnDestroy() {
            // Финальное сохранение перед выходом
            new SaveSystem().ForceSave(_ecsWorld);

            _runtimeSystems?.Destroy();
            _runtimeSystems = null;
            _ecsWorld?.Destroy();
            _ecsWorld = null;
        }
    }
}
