using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Objects.Research;
using UnityEngine;

/// <summary>
/// Set up the research tree recursively, starting with the last entries in the tree.
/// </summary>
public class ResearchTree : MonoBehaviour
{
    [SerializeField] private ObjectBluePrint[] lastEntriesInTree;
    
    [SerializeField] private SerializedDictionary<string, BaseObject> _baseObjects = new();
    
    // public event IRequirementBuilder.RequirementCreated OnRequirementCreated;
    // public event IUnlockableBuilder.UnlockableCreated OnUnlockableCreated;
    
    private ulong _playerID;
    
    // private UnlockHandler _unlockHandler = new();
    [SerializeField] private BaseObjectFactory _factory;
    
    // private void Awake()
    // {
    //     _unlockHandler.RegisterUnlockableBuilder(this);
    //     _unlockHandler.RegisterRequirementBuilder(this);
    // }

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

    private BaseObject GetNodeFromDictionaryOrCreateNewOne(ObjectBluePrint bluePrint)
    {
        if(_baseObjects.ContainsKey(bluePrint.name))
            return _baseObjects[bluePrint.name];

        var instance = _factory.CreateObject(bluePrint, transform);
        
        _baseObjects.Add(bluePrint.name, instance);
        
        return instance;
    }
}
