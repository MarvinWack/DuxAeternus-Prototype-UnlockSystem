using UnityEngine;
using UnityEngine.Serialization;

public enum BuildingType
{
    Core,   //the buildings the village starts with
    Small,  //take one building slot
    Large   //take two building slots, can only be built once
}

[CreateAssetMenu]
public class BuildingBlueprint : ObjectBluePrint
{
    public override ObjectType ObjectType => ObjectType.Building;
    public BuildingType Type;
    public ushort MaxLevel;
    public ushort UpgradeTime = 3;
}
