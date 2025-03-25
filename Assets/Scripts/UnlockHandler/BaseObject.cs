using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseObject : Updatable, IUnlockable, IRequirement
{
    public event IRequirement.RequirementValueUpdated OnRequirementValueUpdated;

    // public event Action ObjectUnlocked;
    // public event Action<int> RequirementValueUpdated;

    [SerializeField] protected ObjectBluePrint _objectBluePrint;

    private BaseObject[] _unlockRequirementInstances;
    public bool IsUnlocked => _isUnlocked;
    public ObjectBluePrint ObjectBluePrint => _objectBluePrint;
    
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
        _unlockRequirementInstances = requirements.ToArray();
        
        if(requirements.Count == 0)
        {
            _isUnlocked = true;
        }
    }

    // private void HandleRequirementValueUpdated(int value)
    // {
    //     if(!CheckIfAllRequirementsAreFulfilled(value))
    //         return;
    //     
    //     _isUnlocked = true;
    //     ObjectUnlocked?.Invoke(); //todo: renove?
    //     
    //     Debug.Log("Object " + gameObject.name + " unlocked");
    // }

    protected void HandleRequirementFulfilled()
    {
        _isUnlocked = true;
    }

    // private bool CheckIfAllRequirementsAreFulfilled(int value)
    // {
    //     foreach (var baseObject in _unlockRequirementInstances)
    //     {
    //         switch (baseObject._objectBluePrint.ObjectType)
    //         {
    //             case ObjectType.Building:
    //                 if (!baseObject.IsUnlocked)
    //                     return false;
    //                 break;
    //             case ObjectType.Research:
    //                 if(!((ResearchNode)baseObject).IsResearchFinished)
    //                     return false;
    //                 break;
    //         }
    //     }
    //
    //     return true;
    // }

    protected void RaiseOnRequirementValueUpdatedEvent(int value)
    {
        OnRequirementValueUpdated?.Invoke(_objectBluePrint, value);
    }
}
