using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnlockRequirements : ScriptableObject
{
    public Dictionary<ObjectBluePrint, int> RequiredLevels;
    public ushort UnlockTime;
}
