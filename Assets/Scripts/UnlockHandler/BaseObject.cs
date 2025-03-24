using UnityEngine;

public abstract class BaseObject : Updatable, IUnlockable, IRequirement
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;

    public bool IsUnlocked => _isUnlocked;
    
    [SerializeField] private UnlockRequirements _unlockRequirements;
    [SerializeField] private ObjectBluePrint _objectBluePrint;
    
    protected bool _isUnlocked;

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
