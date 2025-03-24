public interface IUnlockable
{
    public delegate void RequirementFulfilledHandler();

    public RequirementFulfilledHandler GetEventHandler();
    
    public UnlockRequirements GetRequirements();
}
