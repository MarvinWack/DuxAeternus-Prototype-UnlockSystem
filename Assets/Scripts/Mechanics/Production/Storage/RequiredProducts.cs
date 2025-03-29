using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    /// <summary>
    /// Stores the required products (resources, items etc)
    /// for an item, building, upgrade etc per level.
    /// </summary>
    [CreateAssetMenu]
    public class RequiredProducts : ScriptableObject
    {
        public SerializedDictionary<int, SerializedDictionary<ProductBlueprint, int>> Amount = new();
    }
}