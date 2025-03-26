using UnityEngine;

public class CoreBuildingManager : BuildingManager
{
    [InspectorButton("TryCreateBuilding")]
    public bool CreateBuildingButton;

    private CoreBuildingBlueprint Blueprint => BuildingBlueprint as CoreBuildingBlueprint;
    protected override void Setup()
    {
        base.Setup();
        
        TryCreateBuilding();
    }

    private void TryCreateBuilding()
    {
        if (!CheckIfBuildingIsBuildable())
            return;
        
        CreateBuilding()?.SetBlueprint(BuildingBlueprint);
    }

    protected override bool CheckIfBuildingIsBuildable()
    {
        if(!base.CheckIfBuildingIsBuildable())
            return false;
        
        if(buildings.Count > 0)
        {
            Debug.Log("Can't build more than one core building");
            return false;
        }

        return true;
    }
}