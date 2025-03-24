public class Building : BaseObject
{
    public Building(UnlockRequirements unlockRequirements, ObjectBluePrint objectBluePrint) : base(unlockRequirements, objectBluePrint) { }

    private void LevelUp(int level)
    {   
        RaiseOnRequirementValueUpdatedEvent(level);
    }
}
