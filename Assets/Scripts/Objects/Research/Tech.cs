using System;
using Core;
using UI;
using UnityEngine;

[Serializable]
public class Tech : BaseObject, IResearchTickReceiver, IProgressSender
{
    [InspectorButton("StartResearch")]
    public bool StartResearchButton;

    [InspectorButton("CompleteResearch")]
    public bool CompleteResearchButton;
    
    public event Action<int> OnUpgrade;
    public event Action<float> OnUpgradeProgress;
    
    public bool IsResearchFinished => _isResearchFinished;
    public ushort Level = 0;

    public TechBlueprint TechBlueprint => _objectBluePrint as TechBlueprint;
    
    private bool _isResearching;
    protected bool _isResearchFinished; //todo: lock interaction when research is at MaxLevel
    private ushort _elapsedResearchTime;

    public bool StartResearch()
    {
        if (!_isUnlocked)
        {
            Debug.LogError("Tech is not unlocked");
            return false;
        }
        
        if (_isResearchFinished)
        {
            Debug.LogError("Research is already finished");
            return false;
        }
        
        if (Level >= TechBlueprint.MaxLevel)
        {
            Debug.LogError("Tech is already at max level");
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
