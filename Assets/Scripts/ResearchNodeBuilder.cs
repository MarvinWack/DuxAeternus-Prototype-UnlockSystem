using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ResearchNodeBuilder : MonoBehaviour
{
    [SerializeField] private ResearchNodeData[] lastEntriesInTree;
    [SerializeField] private GameObject prefab;
    [SerializeField] private SerializedDictionary<string, GameObject> _researchNodes;
    
    private ulong _playerID;

    private void OnEnable()
    {
        _researchNodes = new SerializedDictionary<string, GameObject>();
        BuildResearchTree(lastEntriesInTree);
    }
    
    private List<GameObject> BuildResearchTree(ResearchNodeData[] researchNodeData)
    {
        var requiredNodes = new List<GameObject>();

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

    private GameObject InstantiateResearchNode(ResearchNodeData nodeData)
    {
        var instance = Instantiate(prefab, transform);
        instance.name = nodeData.name;
        instance.GetComponent<ResearchNode>().SetData(nodeData);
        return instance;
    }

    private GameObject GetNodeFromDictionaryOrCreateNewOne(ResearchNodeData nodeData)
    {
        if(_researchNodes.ContainsKey(nodeData.name))
            return _researchNodes[nodeData.name];

        var instance = InstantiateResearchNode(nodeData);
        _researchNodes.Add(nodeData.name, instance);
        
        return instance;
    }
}
