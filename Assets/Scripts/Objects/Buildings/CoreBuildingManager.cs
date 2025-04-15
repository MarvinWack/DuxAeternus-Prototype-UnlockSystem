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

    public override Building TryCreateBuilding()
    {
        if (!CheckIfBuildingIsBuildable())
            return null;
        
        var building = CreateBuilding();
        building.SetBlueprint(BuildingBlueprint);
        
        _isAvailable = CheckIfBuildingIsBuildable();

        return building;
    }

    protected override bool CheckIfBuildingIsBuildable()
    {
        if(!base.CheckIfBuildingIsBuildable())
            return false;
        
        if(buildings.Count > 0)
        {
            // Debug.Log("Can't build more than one core building");
            _isAvailable = false;
            
            return false;
        }

        return true;
    }
}