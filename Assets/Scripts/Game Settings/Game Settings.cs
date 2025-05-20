using System;
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

    public int GetNumberOfBuildings(Type buildingType)
    {
        if (buildingType == typeof(SmallBuildingManager))
            return NumberOfSmallBuildings;
        if (buildingType == typeof(LargeBuildingManager))
            return NumberOfLargeBuildings;
        if (buildingType == typeof(CoreBuildingManager))
            return NumberOfCoreBuildings;

        throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null);
    }
}
