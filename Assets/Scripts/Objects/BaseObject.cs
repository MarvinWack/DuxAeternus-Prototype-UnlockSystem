using System.Collections.Generic;
using Core;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseObject : SerializedMonoBehaviour, IUnlockable, IRequirement, ITickReceiver, ISlotContent
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;
    
    [SerializeField] protected ObjectBluePrint _objectBluePrint;

    public string Name => _objectBluePrint.name;
    //todo: remove?
    public bool IsAvailable => _isAvailable;
    
    public bool _unlockRequirementsFulfilled;
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

    public string GetName()
    {
        return gameObject.name;
    }

    bool ISlotContent.IsAvailable()
    {
        return _isAvailable;
    }
}
