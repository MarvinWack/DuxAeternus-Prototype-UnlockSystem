using UnityEngine;

[CreateAssetMenu(menuName = "Tech/TechBlueprint")]
public class TechBlueprint : ObjectBluePrint
{
    public override ObjectType ObjectType => ObjectType.Research;
    public ushort MaxLevel;
    public string Description;
}