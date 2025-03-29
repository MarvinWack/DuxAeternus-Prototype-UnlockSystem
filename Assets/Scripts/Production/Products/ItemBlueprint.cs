using Production.Storage;
using UnityEngine;

namespace Production.Items
{
    [CreateAssetMenu]
    public class ItemBlueprint : ProductBlueprint
    {
        public RequiredProducts Cost;
        public int Damage;
        public int Defense;
    }
}