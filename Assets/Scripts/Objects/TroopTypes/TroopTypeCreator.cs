using Production.Items;
using UnityEngine;

namespace Objects.TroopTypes
{
    public class TroopTypeCreator : MonoBehaviour
    {
        [InspectorButton("CreateTroopType")]
        public bool _CreateTroopType;
        
        [SerializeField] private TroopType troopTypePrefab;
        [SerializeField] private ItemBlueprint weapon;
        [SerializeField] private ItemBlueprint armour;
        [SerializeField] private string troopTypeName;

        private void CreateTroopType()
        {
            var instance = Instantiate(troopTypePrefab, transform);
            instance.name = troopTypeName + " Type";
           
            instance.Setup(weapon, armour, troopTypeName);
        }
    }
}