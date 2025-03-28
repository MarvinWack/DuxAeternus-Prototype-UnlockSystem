using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Objects;
using UnityEngine;

/// <summary>
/// Set up the research tree recursively, starting with the last entries in the tree.
/// </summary>
public class ResearchTree : MonoBehaviour
{
    [SerializeField] private ObjectBluePrint[] lastEntriesInTree;
    
    [SerializeField] private SerializedDictionary<string, BaseObject> _baseObjects = new();
    
    private ulong _playerID;
    
    [SerializeField] private BaseObjectFactory _factory;
    
    private void Start()
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
