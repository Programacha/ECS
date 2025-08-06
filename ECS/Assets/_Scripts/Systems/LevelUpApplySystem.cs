using _Scripts.Components;
using _Scripts.Requests;
using Leopotam.Ecs;

namespace _Scripts.Systems
{
    public class LevelUpApplySystem : IEcsRunSystem 
    {
        private readonly EcsFilter<LevelUpRequest> _requests = null;
        private readonly EcsFilter<BalanceComponent> _balanceFilter = null;

        public void Run() 
        {
            ref var balance = ref _balanceFilter.Get1(0);

            foreach (var i in _requests) 
            {
                ref var request = ref _requests.Get1(i);
                
                ref var business = ref request.Target.Get<BusinessComponent>();
                
                var cost = (business.Level + 1) * business.BaseCost;

                if (balance.Value >= cost) 
                {
                    balance.Value -= cost;
                    business.Level++;
                }
            }
        }
    }
}