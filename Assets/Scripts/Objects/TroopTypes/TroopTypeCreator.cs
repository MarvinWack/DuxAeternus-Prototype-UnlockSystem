using System;
using System.Collections.Generic;
using System.Linq;
using Production.Items;
using UnityEngine;

namespace Objects.TroopTypes
{
    public class TroopTypeCreator : MonoBehaviour
    {
        [InspectorButton("CreateTroopType")]
        public bool _CreateTroopType;
        
        public List<TroopType> TroopTypes => troopTypes;
        
        [SerializeField] private TroopType troopTypePrefab;
        [SerializeField] private ItemBlueprint weapon;
        [SerializeField] private ItemBlueprint armour;
        [SerializeField] private string troopTypeName;
        [SerializeField] private List<TroopType> troopTypes;


        private void Awake()
        {
            CreateTroopType();
        }

        private void CreateTroopType()
        {
            var instance = Instantiate(troopTypePrefab, transform);
            instance.name = troopTypeName + " Type";
           
            instance.Setup(weapon, armour, troopTypeName);
            troopTypes.Add(instance);
        }
        
        public void ApplyTroopLosses(Dictionary<TroopType, int> lossesPerType)
        {
            foreach (var type in lossesPerType)
            {
                troopTypes.Find(t => t == type.Key).SubstractLossesFromFirstUnit(type.Value);
            }
        }

        public bool CheckIfUnitsAvailableToFight()
        {
            return troopTypes.All(type => type.CheckIfUnitsAvailableToFight());
        }
    }
}