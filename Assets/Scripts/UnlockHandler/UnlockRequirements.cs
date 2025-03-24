using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Building,
}
[CreateAssetMenu]
public class UnlockRequirements : ScriptableObject
{
    public Dictionary<ObjectType, int> RequiredLevels;
}
