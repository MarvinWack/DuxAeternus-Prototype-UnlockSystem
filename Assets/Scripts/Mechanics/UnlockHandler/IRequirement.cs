public interface IRequirement
{
    delegate void RequirementValueUpdated(ObjectBluePrint objectType, int value);
    event RequirementValueUpdated OnRequirementValueUpdated;
}
