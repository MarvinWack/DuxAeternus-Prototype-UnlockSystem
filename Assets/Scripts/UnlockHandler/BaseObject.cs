using UnityEngine;

public abstract class BaseObject : IUnlockable, IRequirement
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;

    public bool IsUnlocked => _isUnlocked;
    
    private UnlockRequirements _unlockRequirements;
    private ObjectBluePrint _objectBluePrint;
    
    private bool _isUnlocked;

    protected BaseObject(UnlockRequirements unlockRequirements, ObjectBluePrint objectBluePrint)
    {
        _unlockRequirements = unlockRequirements;
        _objectBluePrint = objectBluePrint;
    }

    public IUnlockable.RequirementFulfilledHandler GetEventHandler()
    {
        return HandleRequirementFulfilled;
    }

    public UnlockRequirements GetRequirements()
    {
        return _unlockRequirements;
    }

    protected virtual void HandleRequirementFulfilled()
    {
        _isUnlocked = true;
    }

    protected void RaiseOnRequirementValueUpdatedEvent(int value)
    {
        OnRequirementValueUpdated?.Invoke(_objectBluePrint.objectType, value);
    }
}
