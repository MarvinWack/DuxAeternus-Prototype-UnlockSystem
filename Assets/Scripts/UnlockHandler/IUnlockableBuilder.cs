public interface IUnlockableBuilder
{
    delegate void UnlockableCreated(IUnlockable.RequirementFulfilledHandler RequirementsHandler, UnlockRequirements unlockRequirements);
    event UnlockableCreated OnUnlockableCreated;

    // public void CreateUnlockable();
}
