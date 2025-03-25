using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class ResearchNode : BaseObject
{
    [InspectorButton("StartResearch")]
    public bool StartResearchButton;

    [InspectorButton("CompleteResearch")]
    public bool CompleteResearchButton;
    
    public bool IsResearchFinished => _isResearchFinished;

    private bool _isResearching;
    protected bool _isResearchFinished;
    private ushort _elapsedResearchTime;

    private void StartResearch()
    {
        Assert.IsTrue(_isUnlocked);
        Assert.IsFalse(_isResearchFinished);
        
        _isResearching = true;
    }

    private void CompleteResearch()
    {
        _isResearching = false;
        _isResearchFinished = true;

        RaiseOnRequirementValueUpdatedEvent(1);

        Debug.Log("Research" + gameObject.name + " finished");
    }

    protected override void HandleTick()
    {
        if (!_isResearching) return;
            
        _elapsedResearchTime++;
        
        if(_elapsedResearchTime >= _objectBluePrint.UnlockRequirements.UnlockTime)
            CompleteResearch();
    }
}
