using System;
using Core;
using Objects;
using UI;
using UnityEngine;

[Serializable]
public class Tech : BaseObject, IProgressSender, IResearchTickReceiver, IUpgradable
{
    [InspectorButton("StartResearch")]
    public bool StartResearchButton;

    [InspectorButton("CompleteResearch")]
    public bool CompleteResearchButton;
    
    public event Action<int> OnUpgrade;
    public event Action<float> OnUpgradeProgress;

    public ushort Level = 0;

    public TechBlueprint TechBlueprint => _objectBluePrint as TechBlueprint;

    private bool _isResearching;

    protected bool _isResearchFinished; //todo: lock interaction when research is at MaxLevel

    private ushort _elapsedResearchTime;

    public bool StartUpgrade()
    {
        if (!_isUnlocked)
        {
            Debug.Log("Tech is not unlocked");
            return false;
        }
        
        if (_isResearchFinished)
        {
            Debug.Log("Research is already finished");
            return false;
        }
        
        if (Level >= TechBlueprint.MaxLevel)
        {
            Debug.Log("Tech is already at max level");
            return false;
        }
        
        _isResearching = true;
        return true;
    }

    private void CompleteResearch()
    {
        Level++;
        _isResearching = false;
        _elapsedResearchTime = 0;
        
        OnUpgrade?.Invoke(Level);
        
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
        Debug.Log($"Research progress: {(float) _elapsedResearchTime / TechBlueprint.UnlockRequirements.UnlockTime}");
        
        if(_elapsedResearchTime >= TechBlueprint.UnlockRequirements.UnlockTime)
            CompleteResearch();
    }
}
