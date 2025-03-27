using System;
using Core;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class Tech : BaseObject, IResearchTickReceiver
{
    [InspectorButton("StartResearch")]
    public bool StartResearchButton;

    [InspectorButton("CompleteResearch")]
    public bool CompleteResearchButton;
    
    public bool IsResearchFinished => _isResearchFinished;
    public ushort Level = 0;

    private TechBlueprint TechBlueprint => _objectBluePrint as TechBlueprint;
    
    private bool _isResearching;
    protected bool _isResearchFinished; //todo: lock interaction when research is at MaxLevel
    private ushort _elapsedResearchTime;

    private void StartResearch()
    {
        Assert.IsTrue(_isUnlocked);
        Assert.IsFalse(_isResearchFinished);
        Assert.IsTrue(Level < TechBlueprint.MaxLevel);
        _isResearching = true;
    }

    private void CompleteResearch()
    {
        Level++;
        _isResearching = false;
        _elapsedResearchTime = 0;
        
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
        
        if(_elapsedResearchTime >= TechBlueprint.UnlockRequirements.UnlockTime)
            CompleteResearch();
    }
}
