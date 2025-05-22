using System.Collections.Generic;
using Mechanics.Traits;
using Production.Storage;
using UnityEngine;

namespace Production.Items
{
    [CreateAssetMenu]
    public class ItemBlueprint : ProductBlueprint
    {
        public RequiredProducts Cost;
        public int MeleeDamage;
        public int RangedDamage;
        public int Defense = 1;
        public List<Trait> Traits;
    }
}