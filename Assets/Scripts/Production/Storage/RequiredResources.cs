using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    /// <summary>
    /// Stores the required resources for a product per level.
    /// </summary>
    [CreateAssetMenu]
    public class RequiredResources : ScriptableObject
    {
        public SerializedDictionary<int, SerializedDictionary<ResourceType, int>> Amount = new();
    }
}