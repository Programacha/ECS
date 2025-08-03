using _Scripts.Components;
using _Scripts.SaveData;
using Leopotam.Ecs;
using UnityEngine;

public class LoadSystem : IEcsInitSystem 
{
    private readonly EcsFilter<BalanceComponent> _balanceFilter = null;
    private readonly EcsFilter<BusinessComponent, IncomeComponent, Upgrade1Component, Upgrade2Component> _businessFilter = null;

    public void Init() 
    {
        if (!PlayerPrefs.HasKey("SaveData")) 
            return;
        
        var json = PlayerPrefs.GetString("SaveData");
        var save = JsonUtility.FromJson<SaveData>(json);
        
        if (save == null) 
            return;

        ref var balance = ref _balanceFilter.Get1(0);
        
        balance.Value = save.Balance;

        int idx = 0;
        
        foreach (var i in _businessFilter) 
        {
            ref var business = ref _businessFilter.Get1(i);
            ref var income = ref _businessFilter.Get2(i);
            ref var upg1 = ref _businessFilter.Get3(i);
            ref var upg2 = ref _businessFilter.Get4(i);

            var s = save.Businesses[idx++];
            business.Level = s.Level;
            income.Progress = s.Progress;
            upg1.Purchased = s.Upgrade1;
            upg2.Purchased = s.Upgrade2;
            income.Multiplier1 = upg1.Purchased ? upg1.Multiplier : 0f;
            income.Multiplier2 = upg2.Purchased ? upg2.Multiplier : 0f;
        }
    }
}