using System;
using UnityEngine;

/// <summary>
/// One update method for each game mechanic for easier balancing.
/// </summary>
public class Updater : MonoBehaviour
{
    public event Action ResearchTick;
    public event Action UpgradeTick;
    public event Action ProductionTick;
    public event Action BattleTick;
    
    [SerializeField] private float researchTicksPerSecond = 1;
    [SerializeField] private float upgradeTicksPerSecond = 1;
    [SerializeField] private float productionTicksPerSecond = 1;
    [SerializeField] private float battleTicksPerSecond = 1f;
    
    private static Updater _instance;
    
    private float _researchTimer;
    private float _upgradeTimer;
    private float _productionTimer;
    private float _battleTimer;

    public static Updater Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindObjectOfType<Updater>();
            
            if (_instance == null)
            {
                return null;    
            }

            return _instance;
        }
    }

    private void Update()
    {
        ResearchUpdate();
        UpgradeUpdate();
        ProductionUpdate();
        BattleUpdate();
    }

    private void HandleTickUpdate(ref float timer, float ticksPerSecond, Action tickAction)
    {
        timer += Time.deltaTime;
        
        if(timer >= 1 / ticksPerSecond)
        {
            timer = 0;
            tickAction?.Invoke();
        }
    }

    private void ResearchUpdate() => HandleTickUpdate(ref _researchTimer, researchTicksPerSecond, ResearchTick);
        
    private void UpgradeUpdate() => HandleTickUpdate(ref _upgradeTimer, upgradeTicksPerSecond, UpgradeTick);
    
    private void ProductionUpdate() => HandleTickUpdate(ref _productionTimer, productionTicksPerSecond, ProductionTick);
    private void BattleUpdate() => HandleTickUpdate(ref _battleTimer, battleTicksPerSecond, BattleTick);
}