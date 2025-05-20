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
    
    [SerializeField] private UpgradeMethod _upgradeMethod;

    private bool _isResearching;
    private ushort _elapsedResearchTime;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        _upgradeMethod.RegisterMethodToCall(StartUpgradeNoReturnValue, this);
        _upgradeMethod.RegisterEnableChecker(CheckIfMethodIsEnabled);
    }

    private bool CheckIfMethodIsEnabled()
    {
        return Level > 0 ? CheckIfUpgradeIsPossible() : CheckIfItemIsUnlocked();
    }

    private void StartUpgradeNoReturnValue()
    {
        StartUpgrade();
    }
    
    public bool StartUpgrade()
    {
        _isResearching = CheckIfUpgradeIsPossible();
        return _isResearching;
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

    public override bool CallSlotAction()
    {
        return StartUpgrade();
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
        if (!_isUnlocked)
        {
            // Debug.Log("Tech is not unlocked");
            return false;
        }
        
        if (Level >= TechBlueprint.MaxLevel)
        {
            // Debug.Log("Tech is already at max level");
            return false;
        }

        return true;
    }

    public bool CheckIfItemIsUnlocked()
    {
        return _isUnlocked;
        //return Level > 0;
    }

    public List<IMethod> GetMethods()
    {
        return new List<IMethod> { _upgradeMethod };
    }

    public bool DoesBelongToPlayer()
    {
        return true;
    }
}
