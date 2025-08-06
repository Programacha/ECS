using _Scripts.Components;
using _Scripts.Requests;
using Leopotam.Ecs;

namespace _Scripts.Systems
{
    public class LevelUpButtonSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<BusinessComponent, BusinessViewComponent> _filter = null;
        private readonly EcsWorld _world = null;

        public void Run() 
        {
            foreach (var i in _filter) 
            {
                var entity = _filter.GetEntity(i);
                ref var view = ref _filter.Get2(i);

                view.LevelUpBtn.onClick.RemoveAllListeners();
                view.LevelUpBtn.onClick.AddListener(() => 
                {
                    var request = _world.NewEntity();
                    request.Get<LevelUpRequest>().Target = entity;
                });
            }
        }
    }
}