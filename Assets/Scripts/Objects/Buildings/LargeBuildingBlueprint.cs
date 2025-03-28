using UnityEngine;

[CreateAssetMenu]
public class LargeBuildingBlueprint : BuildingBlueprint
{
    public override BuildingType Type => BuildingType.Large;
    public override ProductionType ProductionType => ProductionType.OnDemand;
    public override ProductionAmount ProductionAmount => ProductionAmount.High;
}
