using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class Tech : BaseObject
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
        _isResearching = false;
        // _isResearchFinished = true;

        Level++;
        
        RaiseOnRequirementValueUpdatedEvent(Level);

        Debug.Log("Research " + gameObject.name + " is now level " + Level);
    }

    protected override void HandleTick()
    {
        if (!_isResearching) return;
            
        _elapsedResearchTime++;
        
        if(_elapsedResearchTime >= TechBlueprint.UnlockRequirements.UnlockTime)
            CompleteResearch();
    }

    public override UnlockRequirements GetRequirements()
    {
        return TechBlueprint.UnlockRequirements;
    }
}
