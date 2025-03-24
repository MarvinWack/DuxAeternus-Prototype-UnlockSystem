public interface IRequirement
{
    delegate void RequirementValueUpdated(ObjectType objectType, int value);
    event RequirementValueUpdated OnRequirementValueUpdated;
}
