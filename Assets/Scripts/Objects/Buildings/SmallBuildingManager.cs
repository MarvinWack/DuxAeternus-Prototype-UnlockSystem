public class SmallBuildingManager : BuildingManager
{
    [InspectorButton("TryCreateBuilding")]
    public bool CreateBuildingButton;
    
    private SmallBuildingBlueprint Blueprint => BuildingBlueprint as SmallBuildingBlueprint;
    
    public override Building TryCreateBuilding()
    {
        if (!CheckIfBuildingIsBuildable())
            return null;
    
        var building = CreateBuilding();
        building.SetBlueprint(BuildingBlueprint);
    
        return building;
    }
}

public class Bulding
{
}