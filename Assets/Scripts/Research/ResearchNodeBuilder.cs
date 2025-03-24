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
    
    private List<ResearchNode> BuildResearchTree(ObjectBluePrint[] researchNodeData)
    {
        var requiredNodes = new List<ResearchNode>();

        if (researchNodeData.Length == 0)
            return requiredNodes;
        
        foreach (var nodeData in researchNodeData)
        {
            var node = GetNodeFromDictionaryOrCreateNewOne(nodeData);
            
            requiredNodes.Add(node);
            
            node.GetComponent<ResearchNode>().SetRequiredResearch(
                BuildResearchTree(nodeData.UnlockRequirements.GetObjectBluePrints()));
        }

        return requiredNodes;
    }

    private ResearchNode InstantiateResearchNode(ObjectBluePrint nodeData)
    {
        var instance = Instantiate(prefab, transform);
        instance.name = nodeData.name;
        instance.SetData(nodeData);
        return instance;
    }

    private ResearchNode GetNodeFromDictionaryOrCreateNewOne(ObjectBluePrint nodeData)
    {
        if(_researchNodes.ContainsKey(nodeData.name))
            return _researchNodes[nodeData.name];

        var instance = InstantiateResearchNode(nodeData);
        _researchNodes.Add(nodeData.name, instance);
        
        return instance;
    }
}
