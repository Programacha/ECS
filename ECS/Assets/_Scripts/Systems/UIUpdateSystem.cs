using _Scripts.Components;
using Leopotam.Ecs;

namespace _Scripts.Systems
{
    public class UIUpdateSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<BusinessComponent, IncomeComponent, Upgrade1Component, Upgrade2Component, BusinessViewComponent> _filter = null;
        private readonly EcsFilter<BalanceComponent, BalanceViewComponent> _balanceFilter = null;

        public void Run() 
        {

            foreach (var i in _balanceFilter) {
                ref var balance = ref _balanceFilter.Get1(i);
                ref var view = ref _balanceFilter.Get2(i);
                view.BalanceText.text = $"Баланс: {balance.Value:0}$";
            }
        
            foreach (var i in _filter) 
            {
                ref var balance = ref _balanceFilter.Get1(i);
                ref var business = ref _filter.Get1(i);
                ref var income = ref _filter.Get2(i);
                ref var upg1 = ref _filter.Get3(i);
                ref var upg2 = ref _filter.Get4(i);
                ref var view = ref _filter.Get5(i);

                var currentIncome = business.Level * business.BaseIncome * (1 + income.Multiplier1 + income.Multiplier2);

                view.LevelText.text = $"LVL: {business.Level}";
                view.IncomeText.text = $"Доход: {currentIncome:0}$";
                view.CostText.text = $"Цена: {(business.Level + 1) * business.BaseCost:0}$";
                view.ProgressBar.value = income.Progress / business.Delay;

                view.Upgrade1Btn.interactable = !upg1.Purchased && upg1.Cost < balance.Value;
                view.Upgrade1PriceText.text = upg1.Purchased ? "Куплено" : $"{upg1.Cost}$";
                view.Upgrade2Btn.interactable = !upg2.Purchased && upg2.Cost < balance.Value;
                view.Upgrade2PriceText.text = upg2.Purchased ? "Куплено" : $"{upg2.Cost}$";
            }
        }
    }
}