using UnityEngine;

public abstract class BaseObject : Updatable, IUnlockable, IRequirement
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;

    public bool IsUnlocked => _isUnlocked;
    
    // [SerializeField] protected UnlockRequirements _unlockRequirements;
    [SerializeField] protected ObjectBluePrint _objectBluePrint;
    
    protected bool _isUnlocked;

    public IUnlockable.RequirementFulfilledHandler GetEventHandler()
    {
        return HandleRequirementFulfilled;
    }

    public UnlockRequirements GetRequirements()
    {
        return _objectBluePrint.UnlockRequirements;
    }

    protected virtual void HandleRequirementFulfilled()
    {
        _isUnlocked = true;
    }

    protected void RaiseOnRequirementValueUpdatedEvent(int value)
    {
        OnRequirementValueUpdated?.Invoke(_objectBluePrint.ObjectType, value);
    }
}
