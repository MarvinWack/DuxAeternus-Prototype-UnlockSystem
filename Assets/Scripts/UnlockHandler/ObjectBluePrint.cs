using UnityEngine;

public enum ObjectType
{
    Building,
    Research
}

[CreateAssetMenu]
//todo: inherit TechNodeBlueprint and BuildingBlueprint from this class
public class ObjectBluePrint : ScriptableObject
{
    public ObjectType objectType;
    
}
