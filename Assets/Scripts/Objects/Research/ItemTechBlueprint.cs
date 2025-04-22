using Production.Items;
using UnityEngine;

namespace Objects.Research
{
    [CreateAssetMenu(menuName = "Tech/ItemTechBlueprint")]
    public class ItemTechBlueprint : TechBlueprint
    {
        public ItemBlueprint Item;
    }
}