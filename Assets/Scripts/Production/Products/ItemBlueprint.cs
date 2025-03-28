using Production.Storage;
using UnityEngine;

namespace Production.Items
{
    [CreateAssetMenu]
    public class ItemBlueprint : ProductBlueprint
    {
        public RequiredResources Cost;
        public int Damage;
        public int Defense;
    }
}