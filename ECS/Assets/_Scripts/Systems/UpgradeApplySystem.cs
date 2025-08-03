using _Scripts.Components;
using _Scripts.Requests;
using Leopotam.Ecs;

namespace _Scripts.Systems
{
    public class UpgradeApplySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Upgrade1Request> _upgrade1requests = null;
        private readonly EcsFilter<Upgrade2Request> _upgrade2requests = null;
        private readonly EcsFilter<BalanceComponent> _balanceFilter = null;
        
        public void Run()
        {
            ref var balance = ref _balanceFilter.Get1(0);

            foreach (var i in _upgrade1requests)
            {
                ref var request = ref _upgrade1requests.Get1(i);
                if(!request.Target.IsAlive()) continue;

                ref var upgrade1 = ref request.Target.Get<Upgrade1Component>();
                ref var income = ref request.Target.Get<IncomeComponent>();

                if (! upgrade1.Purchased && balance.Value>= upgrade1.Cost)
                {
                    balance.Value -= upgrade1.Cost;
                    upgrade1.Purchased = true;
                    income.Multiplier1 = upgrade1.Multiplier;
                }
            }
            
            foreach (var i in _upgrade2requests)
            {
                ref var request = ref _upgrade2requests.Get1(i);
                if(!request.Target.IsAlive()) continue;

                ref var upgrade2 = ref request.Target.Get<Upgrade2Component>();
                ref var income = ref request.Target.Get<IncomeComponent>();

                if (! upgrade2.Purchased && balance.Value>= upgrade2.Cost)
                {
                    balance.Value -= upgrade2.Cost;
                    upgrade2.Purchased = true;
                    income.Multiplier1 = upgrade2.Multiplier;
                }
            }
        }
    }
}