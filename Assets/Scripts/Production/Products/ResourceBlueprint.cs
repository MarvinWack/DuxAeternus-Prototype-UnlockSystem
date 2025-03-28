using UnityEngine;

namespace Production.Items
{
    public enum ResourceType
    {
        Wood,
        Gold
    }

[CreateAssetMenu]
    public class ResourceBlueprint : ProductBlueprint
    {
        public ResourceType ResourceType;
    }
}