using Production.Items;
using UnityEngine;

namespace Mechanics.Traits
{
    [CreateAssetMenu(menuName = "Traits/AttackTrait")]
    public class AttackTrait : Trait
    {
        public ItemBlueprint Target;
        public float Value;
    }
}