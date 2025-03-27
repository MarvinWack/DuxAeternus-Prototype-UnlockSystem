using UnityEngine;

[CreateAssetMenu]
public class SmallBuildingBlueprint : BuildingBlueprint
{
    public override BuildingType Type => BuildingType.Small;
    public override ProductionType ProductionType => ProductionType.Continuous;
}
