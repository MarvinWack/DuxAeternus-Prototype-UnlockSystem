using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Building
}
public class UnlockRequirements : ScriptableObject
{
    public Dictionary<ObjectType, int> RequiredLevels;
}
