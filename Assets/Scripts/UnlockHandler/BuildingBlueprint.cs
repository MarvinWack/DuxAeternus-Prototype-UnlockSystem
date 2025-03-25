using UnityEngine;

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
    public BuildingType BuildingType;
    public ushort MaxLevel;
}
