using _Scripts.Components;
using _Scripts.SaveData;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.Systems
{
    public class SaveSystem : IEcsRunSystem {
        private readonly EcsFilter<BalanceComponent> _balanceFilter = null;
        private readonly EcsFilter<BusinessComponent, IncomeComponent, Upgrade1Component, Upgrade2Component> _businessFilter = null;

        private float _saveTimer = 0f;
        private const float SaveInterval = 3f;

        public void Run() {
            _saveTimer += Time.deltaTime;
            if (_saveTimer < SaveInterval) return;
            _saveTimer = 0f;
            Save();
        }

        public void ForceSave(EcsWorld world) {
            // временно создаём фильтры и сохраняем
            Save();
        }

        private void Save() {
            var save = new SaveData.SaveData();
            save.Balance = _balanceFilter.Get1(0).Value;
            save.Businesses = new BusinessSave[_businessFilter.GetEntitiesCount()];

            int idx = 0;
            foreach (var i in _businessFilter) {
                ref var business = ref _businessFilter.Get1(i);
                ref var income = ref _businessFilter.Get2(i);
                ref var upg1 = ref _businessFilter.Get3(i);
                ref var upg2 = ref _businessFilter.Get4(i);

                save.Businesses[idx++] = new BusinessSave {
                    Level = business.Level,
                    Progress = income.Progress,
                    Upgrade1 = upg1.Purchased,
                    Upgrade2 = upg2.Purchased
                };
            }

            PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(save));
            PlayerPrefs.Save();
        }
    }
}