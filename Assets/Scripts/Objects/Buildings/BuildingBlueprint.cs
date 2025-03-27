using UnityEngine;

public enum BuildingType
{
    None,
    Core,   //the buildings the village starts with
    Small,  //take one building slot
    Large   //take two building slots, can only be built once
}

public enum ProductionType
{
    None,
    OnDemand,
    Continuous
}
[CreateAssetMenu]
public abstract class BuildingBlueprint : ObjectBluePrint
{
    public override ObjectType ObjectType => ObjectType.Building;
    public virtual BuildingType Type => BuildingType.None;
    public virtual ProductionType ProductionType => ProductionType.None;
    public ushort MaxLevel;
    public ushort UpgradeTime = 3;
}
