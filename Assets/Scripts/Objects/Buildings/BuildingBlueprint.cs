using Production.Items;
using Production.Storage;
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

public enum ProductionAmount
{
    None = 0,
    Low = 2,
    Medium = 4,
    High = 6
}
[CreateAssetMenu]
public abstract class BuildingBlueprint : ObjectBluePrint
{
    public override ObjectType ObjectType => ObjectType.Building;
    public virtual BuildingType Type => BuildingType.None;
    public virtual ProductionType ProductionType => ProductionType.None;
    public virtual ProductionAmount ProductionAmount => ProductionAmount.None;
    public ProductBlueprint ProducedProduct;
    public RequiredResources Cost;
    public ushort MaxLevel;
    public ushort UpgradeTime = 3;
}
