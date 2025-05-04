using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Production.Items;
using Production.Storage;
using UnityEngine;

namespace Objects.TroopTypes
{
    public class TroopTypeCreator : MonoBehaviour, IDynamicSlotContentSource
    {
        [InspectorButton("CreateTroopType")]
        public bool _CreateTroopType;
        
        public List<TroopType> TroopTypes => troopTypes;

        public event Action<ISlotContent> SlotContentAdded;
        
        [SerializeField] private TroopType troopTypePrefab;
        [SerializeField] private ItemBlueprint weapon;
        [SerializeField] private ItemBlueprint armour;

        [SerializeField] private string troopTypeName;
        [SerializeField] private List<TroopType> troopTypes;
        
        [SerializeField] private StorageAssigner storageAssigner;
        
        private SerializedDictionary<string, ItemBlueprint> itemSlots = new()
        {
            { "weapon", null },
            { "armor", null }
        };

        private void Start()
        {
            CreateTroopType();
        }

        private void CreateTroopType()
        {
            var instance = Instantiate(troopTypePrefab, transform);
            instance.name = troopTypeName + " Type";
           
            instance.Setup(weapon, armour, troopTypeName);
            troopTypes.Add(instance);
            
            SlotContentAdded?.Invoke(instance);
            storageAssigner.AssignToStorage(instance);
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