using UnityEngine;

[CreateAssetMenu]
public class ResearchNodeData : ScriptableObject
{
    public ResearchNodeData[] RequiredResearch;
    public ushort ResearchTime;
}
