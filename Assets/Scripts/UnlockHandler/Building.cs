public class Building : BaseObject
{
    private void LevelUp(int level)
    {   
        RaiseOnRequirementValueUpdatedEvent(level);
    }

    protected override void HandleTick()
    {
        throw new System.NotImplementedException();
    }
}
