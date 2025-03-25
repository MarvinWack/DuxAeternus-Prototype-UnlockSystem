using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

/// <summary>
/// Set up the research tree recursively, starting with the last entries in the tree.
/// </summary>
public class ResearchNodeBuilder : MonoBehaviour, IRequirementBuilder, IUnlockableBuilder
{
    [SerializeField] private ObjectBluePrint[] lastEntriesInTree;
    [SerializeField] private BaseObject researchPrefab;
    [SerializeField] private BaseObject managerPrefab;
    
    [SerializeField] private SerializedDictionary<string, BaseObject> _baseObjects = new();
    
    public event IRequirementBuilder.RequirementCreated OnRequirementCreated;
    public event IUnlockableBuilder.UnlockableCreated OnUnlockableCreated;
    
    private ulong _playerID;
    
    private UnlockHandler _unlockHandler;
    
    private void Awake()
    {
        _unlockHandler = new UnlockHandler();
        _unlockHandler.RegisterUnlockableBuilder(this);
        _unlockHandler.RegisterRequirementBuilder(this);
    }

    private void OnEnable()
    {
        BuildResearchTree(lastEntriesInTree);
    }
    
    private List<BaseObject> BuildResearchTree(ObjectBluePrint[] bluePrints)
    {
        var requiredNodes = new List<BaseObject>();

        if (bluePrints.Length == 0)
            return requiredNodes;
        
        foreach (var bluePrint in bluePrints)
        {
            var node = GetNodeFromDictionaryOrCreateNewOne(bluePrint);
            
            requiredNodes.Add(node);
            
            node.SetUnlockRequirementInstances(BuildResearchTree(bluePrint.UnlockRequirements.GetObjectBluePrints()));
        }

        return requiredNodes;
    }

    private BaseObject Instantiate(ObjectBluePrint bluePrint)
    {
        BaseObject instance;
        
        switch (bluePrint.ObjectType)
        {
            case ObjectType.Building:
                instance = Instantiate(managerPrefab, transform);
                break;
            case ObjectType.Research:
                instance = Instantiate(researchPrefab, transform);
                break;
            default: throw new Exception("Invalid Object Type");
        }
        
        instance.name = bluePrint.name;
        instance.SetData(bluePrint);
        
        OnRequirementCreated?.Invoke(instance);
        OnUnlockableCreated?.Invoke(instance.GetEventHandler(), instance.GetRequirements());
        
        return instance;
    }

    private BaseObject GetNodeFromDictionaryOrCreateNewOne(ObjectBluePrint bluePrint)
    {
        if(_baseObjects.ContainsKey(bluePrint.name))
            return _baseObjects[bluePrint.name];

        var instance = Instantiate(bluePrint);
        _baseObjects.Add(bluePrint.name, instance);
        
        return instance;
    }
}
