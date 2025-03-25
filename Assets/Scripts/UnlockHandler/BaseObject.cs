using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : Updatable, IUnlockable, IRequirement
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;

    [SerializeField] protected ObjectBluePrint _objectBluePrint;

    // private BaseObject[] _unlockRequirementInstances;
    public bool IsUnlocked => _isUnlocked;
    
    public bool _isUnlocked; //todo: make private

    public IUnlockable.RequirementFulfilledHandler GetEventHandler()
    {
        return HandleRequirementFulfilled;
    }

    public UnlockRequirements GetRequirements()
    {
        return _objectBluePrint.UnlockRequirements;
    }
    
    public void SetData(ObjectBluePrint nodeData)
    {
        _objectBluePrint = nodeData;
    }
    
    public void SetUnlockRequirementInstances(List<BaseObject> requirements)
    {
        // _unlockRequirementInstances = requirements.ToArray();
        
        if(requirements.Count == 0)
        {
            _isUnlocked = true;
        }
    }

    protected void HandleRequirementFulfilled()
    {
        _isUnlocked = true;
    }

    protected void RaiseOnRequirementValueUpdatedEvent(int value)
    {
        OnRequirementValueUpdated?.Invoke(_objectBluePrint, value);
    }
}
