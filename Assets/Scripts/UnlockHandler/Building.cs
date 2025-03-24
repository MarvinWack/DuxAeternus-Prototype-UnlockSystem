using UnityEngine;

public class Building : BaseObject
{
    [InspectorButton("LevelUpDebug")]
    public bool LevelUpButton;

    [Space(20)] 
    
    [SerializeField] private int numberOfLevels = 1;
    
    private void LevelUp(int level)
    {   
        RaiseOnRequirementValueUpdatedEvent(level);
    }

    protected override void HandleTick()
    {
        
    }

    private void LevelUpDebug()
    {
        LevelUp(numberOfLevels);
    }
    
}
