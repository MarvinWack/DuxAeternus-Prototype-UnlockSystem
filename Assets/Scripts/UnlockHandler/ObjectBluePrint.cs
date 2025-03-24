using UnityEngine;
using UnityEngine.Serialization;

public enum ObjectType
{
    Building,
    Research
}

[CreateAssetMenu]
//todo: inherit TechNodeBlueprint and BuildingBlueprint from this class
public class ObjectBluePrint : ScriptableObject
{
    public ObjectType ObjectType;
    public UnlockRequirements UnlockRequirements;
}
