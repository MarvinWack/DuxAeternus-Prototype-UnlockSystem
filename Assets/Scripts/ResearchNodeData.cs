using UnityEngine;

[CreateAssetMenu]
public class ResearchNodeData : IData
{
    public ResearchNodeData[] RequiredResearch;
    public ushort ResearchTime;
}
