using System.Collections.Generic;
using Core;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseObject : SerializedMonoBehaviour, IUnlockable, IRequirement, ITickReceiver
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;
    
    [SerializeField] protected ObjectBluePrint _objectBluePrint;
    
    public bool _unlockRequirementsFulfilled;
    protected bool _isAvailable;

    public IUnlockable.RequirementFulfilledHandler GetEventHandler()
    {
        return HandleRequirementFulfilled;
    }

    public abstract UnlockRequirements GetRequirements();

    public void SetData(ObjectBluePrint blueprint)
    {
        _objectBluePrint = blueprint;
    }
    
    public void SetUnlockRequirementInstances(List<BaseObject> requirements)
    {
        if(requirements.Count == 0)
        {
            _unlockRequirementsFulfilled = true;
            _isAvailable = true;
        }
    }

    private void HandleRequirementFulfilled()
    {
        _unlockRequirementsFulfilled = true;
        _isAvailable = true;
    }

    protected void RaiseOnRequirementValueUpdatedEvent(int value)
    {
        OnRequirementValueUpdated?.Invoke(_objectBluePrint, value);
    }
}
