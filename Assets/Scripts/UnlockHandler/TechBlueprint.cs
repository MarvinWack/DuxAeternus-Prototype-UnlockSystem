using UnityEngine;

[CreateAssetMenu]
public class TechBlueprint : ObjectBluePrint
{
    public override ObjectType ObjectType => ObjectType.Research;
    public ushort MaxLevel;
}
