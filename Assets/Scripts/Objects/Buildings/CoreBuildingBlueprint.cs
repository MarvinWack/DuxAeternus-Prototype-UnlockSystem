using UnityEngine;

[CreateAssetMenu]
public class CoreBuildingBlueprint : BuildingBlueprint
{
    public override BuildingType Type => BuildingType.Core;
    public override ProductionType ProductionType => ProductionType.Continuous;
    public override ProductionAmount ProductionAmount => ProductionAmount.Medium;
}
