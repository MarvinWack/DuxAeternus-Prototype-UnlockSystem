using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum ObjectType
{
    Building,
    Research
}

public enum BuildingType
{
    FirstBuilding,
    SecondBuilding
}

[CreateAssetMenu]
[Serializable]
//todo: inherit TechNodeBlueprint and BuildingBlueprint from this class
public class ObjectBluePrint : ScriptableObject
{
    public ObjectType ObjectType;
    public BuildingType BuildingType;
    public UnlockRequirements UnlockRequirements;
}
