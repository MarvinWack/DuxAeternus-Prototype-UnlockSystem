using UnityEngine;

namespace Mechanics.Traits
{
    [CreateAssetMenu(menuName = "Traits/TickRelatedTrait")]
    public class TickRelatedTrait : Trait
    {
        public float GetModifier(int tickCount)
        {
            return modifier * tickCount;
        }

        [SerializeField] private float modifier;
    }
}