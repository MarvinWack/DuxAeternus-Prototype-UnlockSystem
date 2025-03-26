using UnityEngine;

public class LargeBuildingManager : BuildingManager
{
    [InspectorButton("TryCreateBuilding")]
    public bool CreateBuildingButton;

    private LargeBuildingBlueprint Blueprint => BuildingBlueprint as LargeBuildingBlueprint;

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
            Debug.Log("Can't build more than one large building");
            return false;
        }

        return true;
    }
}