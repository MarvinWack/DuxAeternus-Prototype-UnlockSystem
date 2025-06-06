using System.Collections.Generic;
using Core;
using Objects;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour, IUnlockable, IRequirement, ITickReceiver, ISlotContent
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;
    
    [SerializeField] protected ObjectBluePrint _objectBluePrint;

    public string Name => _objectBluePrint.name;
    //todo: remove?
    public bool IsAvailable => _isAvailable;
    
    public bool _isUnlocked;
    public bool _isAvailable;

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
            _isUnlocked = true;
            _isAvailable = true;
        }
    }

    private void HandleRequirementFulfilled()
    {
        _isUnlocked = true;
        _isAvailable = true;
    }

    protected void RaiseOnRequirementValueUpdatedEvent(int value)
    {
        OnRequirementValueUpdated?.Invoke(_objectBluePrint, value);
    }

    public abstract bool CallSlotAction();

    public string GetName()
    {
        return gameObject.name;
    }

    bool ISlotContent.IsAvailable()
    {
        return _isAvailable;
    }
}
