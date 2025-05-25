using System;
using System.Collections.Generic;
using Core;
using UI.MethodBlueprints;
using UnityEngine;

[Serializable]
public class Tech : BaseObject, IResearchTickReceiver, IEnableChecker, IUpgradeMethodProvider
{
    [InspectorButton("StartResearch")]
    public bool StartResearchButton;

    [InspectorButton("CompleteResearch")]
    public bool CompleteResearchButton;

    public event Action<int> OnUpgradeFinished;
    public event Action<float> OnUpgradeProgress;
    
    public ushort Level = 0;

    public TechBlueprint TechBlueprint => _objectBluePrint as TechBlueprint;
    
    [SerializeField] private UpgradeMethod upgradeMethod;
    [SerializeField] private SelectItemMethod selectWeaponItemMethod;
    [SerializeField] private SelectItemMethod selectArmorItemMethod;

    private bool _isResearching;
    private ushort _elapsedResearchTime;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        upgradeMethod.RegisterMethodToCall(StartUpgrade, this);
        upgradeMethod.RegisterMethodEnableChecker(CheckIfMethodIsEnabled);
        
        // selectWeaponItemMethod.RegisterMethodEnableChecker(CheckIfAssociatedItemIsUnlocked);
        // selectArmorItemMethod.RegisterMethodEnableChecker(CheckIfAssociatedItemIsUnlocked);
    }

    private bool CheckIfMethodIsEnabled()
    {
        return Level > 0 ? CheckIfUpgradeIsPossible() : CheckIfUnlockRequirementsFulfilled();
    }
    
    public void StartUpgrade()
    {
        _isResearching = CheckIfUpgradeIsPossible();
    }

    private void CompleteResearch()
    {
        Level++;
        _isResearching = false;
        _elapsedResearchTime = 0;
        OnUpgradeFinished?.Invoke(Level);
        
        RaiseOnRequirementValueUpdatedEvent(Level);
    }

    public override UnlockRequirements GetRequirements()
    {
        return TechBlueprint.UnlockRequirements;
    }

    public void ResearchTickHandler()
    {
        if (!_isResearching) return;
            
        _elapsedResearchTime++;
        OnUpgradeProgress?.Invoke((float) _elapsedResearchTime / TechBlueprint.UnlockRequirements.UnlockTime);
        
        if(_elapsedResearchTime >= TechBlueprint.UnlockRequirements.UnlockTime)
            CompleteResearch();
    }

    public bool CheckIfUpgradeIsPossible()
    {
        if (!_unlockRequirementsFulfilled) return false;
        
        if (Level >= TechBlueprint.MaxLevel) return false;

        return true;
    }

    public bool CheckIfUnlockRequirementsFulfilled()
    {
        return _unlockRequirementsFulfilled;
    }

    public bool CheckIfAssociatedItemIsUnlocked()
    {
        return Level > 0;
    }

    public List<IMethod> GetMethods()
    {
        return new List<IMethod> { upgradeMethod };
    }

    public string GetName()
    {
        return gameObject.name;
    }

    public bool DoesBelongToPlayer()
    {
        return true;
    }
}
