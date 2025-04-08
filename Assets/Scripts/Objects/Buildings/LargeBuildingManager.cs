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
    
    //todo: proaktiv wenn gebaut wurde isAvailable in base oder manager setzen
    protected override bool CheckIfBuildingIsBuildable()
    {
        if(!base.CheckIfBuildingIsBuildable())
            return false;
        
        if(buildings.Count > 0)
        {
            Debug.Log("Can't build more than one large building");
            _isAvailable = false;
            
            return false;
        }

        return true;
    }
}