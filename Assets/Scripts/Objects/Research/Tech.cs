using System;
using Core;
using UI.MethodBlueprints;
using UnityEngine;

[Serializable]
public class Tech : BaseObject, IResearchTickReceiver, IEnableChecker
{
    [InspectorButton("StartResearch")]
    public bool StartResearchButton;

    [InspectorButton("CompleteResearch")]
    public bool CompleteResearchButton;

    public ushort Level = 0;

    public TechBlueprint TechBlueprint => _objectBluePrint as TechBlueprint;

    private bool _isResearching;

    protected bool _isResearchFinished; //todo: lock interaction when research is at MaxLevel

    private ushort _elapsedResearchTime;

    public bool StartUpgrade()
    {
        _isResearching = CheckIfMethodIsEnabled();
        return _isResearching;
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

    public override bool CallSlotAction()
    {
        return StartUpgrade();
    }

    public void ResearchTickHandler()
    {
        if (!_isResearching) return;
            
        _elapsedResearchTime++;
        
        if(_elapsedResearchTime >= TechBlueprint.UnlockRequirements.UnlockTime)
            CompleteResearch();
    }

    public bool CheckIfMethodIsEnabled()
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

        return true;
    }
}
