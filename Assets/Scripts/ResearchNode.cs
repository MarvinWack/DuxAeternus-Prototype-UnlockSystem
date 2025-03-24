using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class ResearchNode : Updatable
{
    [InspectorButton("StartResearch")]
    public bool StartResearchButton;

    [InspectorButton("CompleteResearch")]
    public bool CompleteResearchButton;

    public event Action ResearchComplete;
    
    public bool IsResearchFinished => _isResearchFinished;

    [SerializeField] public ResearchNodeData _data;

    [SerializeField] private ResearchNode[] _requiredResearch;

    [SerializeField] private GameObject[] _requiredResearchObjects;
    
    public bool _isUnlocked;

    private bool _isResearching;
    private bool _isResearchFinished;
    private ushort _elapsedResearchTime;
    
    public void SetData(ResearchNodeData nodeData)
    {
        _data = nodeData;
    }
    
    public void SetRequiredResearch(List<GameObject> requiredResearch)
    {
        _requiredResearchObjects = requiredResearch.ToArray();
        _requiredResearch = new ResearchNode[requiredResearch.Count];
        
        if(requiredResearch.Count == 0)
        {
            _isUnlocked = true;
            return;
        }
        
        for(var i = 0; i < _requiredResearchObjects.Length; i++)
        {
            _requiredResearch[i] = _requiredResearchObjects[i].GetComponent<ResearchNode>();
        }
        
        for(var i = 0; i < _requiredResearchObjects.Length; i++)
        {
            _requiredResearch[i].ResearchComplete -= OnRequiredResearchComplete;
            _requiredResearch[i].ResearchComplete += OnRequiredResearchComplete;
        }
    }

    private void OnRequiredResearchComplete()
    {
        foreach (var research in _requiredResearch)
        {
            if (!research.IsResearchFinished) return;
        }
        
        _isUnlocked = true;
    }

    private void StartResearch()
    {
        Assert.IsTrue(_isUnlocked);
        Assert.IsFalse(_isResearchFinished);
        
        _isResearching = true;
    }

    private void CompleteResearch()
    {
        Assert.IsTrue(_isUnlocked);

        _isResearching = false;
        _isResearchFinished = true;
        ResearchComplete?.Invoke();
        
        Debug.Log("Research " + gameObject.name + " Complete");
    }

    protected override void HandleTick()
    {
        if (!_isResearching) return;
            
        _elapsedResearchTime++;
        
        if(_elapsedResearchTime >= _data.ResearchTime)
            CompleteResearch();
    }
}
