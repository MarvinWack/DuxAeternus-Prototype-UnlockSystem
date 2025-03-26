public class SmallBuildingManager : BuildingManager
{
    [InspectorButton("TryCreateBuilding")]
    public bool CreateBuildingButton;
    
    private SmallBuildingBlueprint Blueprint => BuildingBlueprint as SmallBuildingBlueprint;

    private void TryCreateBuilding()
    {
        if (!CheckIfBuildingIsBuildable())
            return;
        
        CreateBuilding()?.SetBlueprint(BuildingBlueprint);
    }
}