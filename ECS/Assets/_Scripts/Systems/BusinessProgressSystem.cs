using _Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.Systems
{
    public class BusinessProgressSystem : IEcsRunSystem {
        private readonly EcsFilter<BusinessComponent, IncomeComponent> _filter = null;
        private readonly EcsFilter<BalanceComponent> _balanceFilter = null;

        public void Run() {
            foreach (var i in _filter) {
                ref var business = ref _filter.Get1(i);
                ref var income = ref _filter.Get2(i);
                ref var balance = ref _balanceFilter.Get1(0);

                if (business.Level <= 0) continue;

                income.Progress += Time.deltaTime;
                if (income.Progress >= business.Delay) {
                    income.Progress = 0f;
                    var value = business.Level * business.BaseIncome * (1 + income.Multiplier1 + income.Multiplier2);
                    balance.Value += value;
                }
            }
        }
    }
}