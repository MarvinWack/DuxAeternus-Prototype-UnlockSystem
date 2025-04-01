using UnityEngine;

namespace Mechanics.Traits
{
    [CreateAssetMenu(menuName = "Traits/AmountRelatedTrait")]
    public class AmountRelatedTrait : Trait
    {
        public float GetModifier(int amountOfUnits)
        {
            return modifier * amountOfUnits;
        }

        [SerializeField] private float modifier;
    }
}