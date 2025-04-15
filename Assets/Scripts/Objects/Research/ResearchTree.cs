// using System;

using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Objects;
using UnityEngine;

/// <summary>
/// Set up the research tree recursively, starting with the last entries in
/// the tree. The tree includes the BuildingManagers as well! Buildings can
/// be unlocked through research, but they may also be required to unlock
/// certain research.
/// </summary>
public class ResearchTree : MonoBehaviour
{
    [SerializeField] private ObjectBluePrint[] lastEntriesInTree;
    
    //make this a list instead?
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

    //ist foreach safe bezogen auf den index?
    public List<BuildingManager> GetBuildingManagersOfType(Type type)
    {
        return _baseObjects.Where(x => x.Value is BuildingManager)// && x.Value.GetType() == type)
            .Select(x => (BuildingManager)x.Value)
            .ToList();
    }
}
