using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Objects.TroopTypes
{
    public class TroopTypeCreator : MonoBehaviour, ISlotContentSource
    {
        [InspectorButton("CreateTroopType")]
        public bool _CreateTroopType;
        
        public List<TroopType> TroopTypes => troopTypes;
        
        [SerializeField] private TroopType troopTypePrefab;
        [SerializeField] private ItemBlueprint weapon;
        [SerializeField] private ItemBlueprint armour;
        //todo: slots in game settings definieren
        private SerializedDictionary<string, ItemBlueprint> itemSlots = new()
        {
            { "weapon", null },
            { "armor", null }
        };
        [SerializeField] private string troopTypeName;
        [SerializeField] private List<TroopType> troopTypes;


        private void Awake()
        {
            CreateTroopType();
            // itemSlots.Add("weapon", null);
            // itemSlots.Add("armor", null);
        }

        private void CreateTroopType()
        {
            var instance = Instantiate(troopTypePrefab, transform);
            instance.name = troopTypeName + " Type";
           
            instance.Setup(weapon, armour, troopTypeName);
            troopTypes.Add(instance);
        }

        public List<string> GetItemSlots()
        {
            return itemSlots.Keys.ToList();
        }
        
        public void CreateTroopType(ItemBlueprint firstItem, ItemBlueprint secondItem, string typeName)
        {
            var instance = Instantiate(troopTypePrefab, transform);
            instance.name = typeName + " Type";
           
            instance.Setup(firstItem, secondItem, typeName);
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

        public List<ISlotContent> GetSlotItems(Type type)
        {
            return troopTypes.Cast<ISlotContent>().ToList();
        }
    }
}