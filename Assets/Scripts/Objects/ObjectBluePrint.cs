using UnityEngine;

public enum ObjectType
{
    None,
    Building,
    Research
}

[CreateAssetMenu]
public abstract class ObjectBluePrint : ScriptableObject
{
    public virtual ObjectType ObjectType => ObjectType.None;
    public UnlockRequirements UnlockRequirements;
}