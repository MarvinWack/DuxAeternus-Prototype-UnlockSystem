using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ResearchNodeBuilder : MonoBehaviour
{
    [SerializeField] private ResearchNodeData[] lastEntriesInTree;
    [SerializeField] private ResearchNode prefab;
    [SerializeField] private SerializedDictionary<string, ResearchNode> _researchNodes;
    
    private ulong _playerID;

    private void OnEnable()
    {
        _researchNodes = new SerializedDictionary<string, ResearchNode>();
        BuildResearchTree(lastEntriesInTree);
    }
    
    private List<ResearchNode> BuildResearchTree(ResearchNodeData[] researchNodeData)
    {
        var requiredNodes = new List<ResearchNode>();

        if (researchNodeData.Length == 0)
            return requiredNodes;
        
        foreach (var nodeData in researchNodeData)
        {
            var node = GetNodeFromDictionaryOrCreateNewOne(nodeData);
            
            requiredNodes.Add(node);

            var requiredNodeDatas = GetRequiredNodes(nodeData);
            
            node.GetComponent<ResearchNode>().SetRequiredResearch(BuildResearchTree(requiredNodeDatas));
        }

        return requiredNodes;
    }

    private ResearchNodeData[] GetRequiredNodes(ResearchNodeData researchNodeData)
    {
        return researchNodeData.RequiredResearch;
    }

    private ResearchNode InstantiateResearchNode(ResearchNodeData nodeData)
    {
        var instance = Instantiate(prefab, transform);
        instance.name = nodeData.name;
        instance.GetComponent<ResearchNode>().SetData(nodeData);
        return instance;
    }

    private ResearchNode GetNodeFromDictionaryOrCreateNewOne(ResearchNodeData nodeData)
    {
        if(_researchNodes.ContainsKey(nodeData.name))
            return _researchNodes[nodeData.name];

        var instance = InstantiateResearchNode(nodeData);
        _researchNodes.Add(nodeData.name, instance);
        
        return instance;
    }
}
