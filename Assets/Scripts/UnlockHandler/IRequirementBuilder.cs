public interface IRequirementBuilder
{
    delegate void RequirementCreated(IRequirement requirement);
    event RequirementCreated OnRequirementCreated;
}
