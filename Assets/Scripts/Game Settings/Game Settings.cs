using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [Header("Building Settings")]
    public int NumberOfCoreBuildings;
    public int NumberOfSmallBuildings;
    public int NumberOfLargeBuildings;
    
    [Header("Army Settings")]
    public int NumberOfTroopTypes;
}
