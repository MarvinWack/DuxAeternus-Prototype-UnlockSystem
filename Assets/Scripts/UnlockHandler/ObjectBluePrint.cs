using System;
using UnityEngine;

public enum ObjectType
{
    None,
    Building,
    Research
}

[CreateAssetMenu]
[Serializable]
//todo: inherit TechNodeBlueprint and BuildingBlueprint from this class
public abstract class ObjectBluePrint : ScriptableObject
{
    public virtual ObjectType ObjectType => ObjectType.None;
    public UnlockRequirements UnlockRequirements;
}

//https://discussions.unity.com/t/scriptableobject-dictionary-key-problem/839631/2 -> use GUID