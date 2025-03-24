using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

/// <summary>
/// Set up the research tree recursively, starting with the last entreis in the tree.
/// </summary>
public class ResearchNodeBuilder : MonoBehaviour
{
    [SerializeField] private ObjectBluePrint[] lastEntriesInTree;
    [SerializeField] private ResearchNode prefab;
    [SerializeField] private SerializedDictionary<string, ResearchNode> _researchNodes = new();
    
    private ulong _playerID;

    private void OnEnable()
    {
        _researchNodes = new SerializedDictionary<string, ResearchNode>();
        BuildResearchTree(lastEntriesInTree);
    }
    
    private List<ResearchNode> BuildResearchTree(ObjectBluePrint[] bluePrints)
    {
        var requiredNodes = new List<ResearchNode>();

        if (bluePrints.Length == 0)
            return requiredNodes;
        
        foreach (var bluePrint in bluePrints)
        {
            var node = GetNodeFromDictionaryOrCreateNewOne(bluePrint);
            
            requiredNodes.Add(node);
            
            node.SetRequiredResearchNodes(BuildResearchTree(bluePrint.UnlockRequirements.GetObjectBluePrints()));
        }

        return requiredNodes;
    }

    private ResearchNode InstantiateResearchNode(ObjectBluePrint bluePrint)
    {
        var instance = Instantiate(prefab, transform);
        instance.name = bluePrint.name;
        instance.SetData(bluePrint);
        return instance;
    }

    private ResearchNode GetNodeFromDictionaryOrCreateNewOne(ObjectBluePrint bluePrint)
    {
        if(_researchNodes.ContainsKey(bluePrint.name))
            return _researchNodes[bluePrint.name];

        var instance = InstantiateResearchNode(bluePrint);
        _researchNodes.Add(bluePrint.name, instance);
        
        return instance;
    }
}
