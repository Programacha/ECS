using _Scripts.Components;
using _Scripts.Requests;
using Leopotam.Ecs;

namespace _Scripts.Systems
{
    public class UpgradeButtonSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<Upgrade1Component, Upgrade2Component, BusinessViewComponent> _filter = null;
        
        private readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var i in _filter) 
            {
                var entity = _filter.GetEntity(i);
                ref var view = ref _filter.Get3(i);

                view.Upgrade1Btn.onClick.RemoveAllListeners();
                
                view.Upgrade1Btn.onClick.AddListener(() => 
                {
                    var request = _world.NewEntity();
                    request.Get<Upgrade1Request>().Target = entity;
                });

                view.Upgrade2Btn.onClick.RemoveAllListeners();
                
                view.Upgrade2Btn.onClick.AddListener(() =>
                {
                    var req = _world.NewEntity();
                    req.Get<Upgrade2Request>().Target = entity;
                });
            }
        }
    }
}