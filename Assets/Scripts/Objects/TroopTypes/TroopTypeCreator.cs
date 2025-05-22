using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Core;
using Production.Items;
using Production.Storage;
using UI.MethodBlueprints;
using UnityEngine;

namespace Objects.TroopTypes
{
    public class TroopTypeCreator : MonoBehaviour, IDynamicSlotContentSource, IMethodProvider
    {
        [InspectorButton("CreateTroopType")]
        public bool _CreateTroopType;
        
        public List<TroopType> TroopTypes => troopTypes;

        public event Action<ISlotContent> SlotContentAdded;
        
        [SerializeField] protected bool _belongsToPlayer;
        
        [SerializeField] private TroopType troopTypePrefab;
        [SerializeField] private ItemBlueprint weapon;
        [SerializeField] private ItemBlueprint armour;

        [SerializeField] private string troopTypeName;
        [SerializeField] private List<TroopType> troopTypes;
        
        [SerializeField] private StorageAssigner storageAssigner;

        [SerializeField] private SelectItemMethod selectWeaponMethod;
        [SerializeField] private SelectItemMethod selectArmorMethod;
        [SerializeField] private UpgradeMethod createTroopTypeMethod;
        
        private SerializedDictionary<string, ItemBlueprint> itemSlots = new()
        {
            { "weapon", null },
            { "armor", null }
        };

        private void Awake()
        {
            selectWeaponMethod.RegisterMethodToCall(SetWeaponItem, this);
            selectArmorMethod.RegisterMethodToCall(SetArmorItem, this);
            
            createTroopTypeMethod.RegisterMethodToCall(CreateTroopType, this);
            createTroopTypeMethod.RegisterEnableChecker(CheckIfItemsSet);
        }

        public void SetTypeName(string typeName)
        {
            troopTypeName = typeName;
        }

        private bool CheckIfItemsSet()
        {
            if (weapon == null)
                return false;
            
            if (armour == null)
                return false;

            if (troopTypeName.Length == 0)
                return false;
            
            return true;
        }

        private void SetWeaponItem(ItemBlueprint item)
        {
            weapon = item;
        }

        private void SetArmorItem(ItemBlueprint item)
        {
            armour = item;
        }

        private void Start()
        {
            CreateTroopType();
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

        private void CreateTroopType()
        {
            var instance = Instantiate(troopTypePrefab, transform);
            instance.name = troopTypeName + " Type";
           
            instance.Setup(weapon, armour, troopTypeName, _belongsToPlayer);
            troopTypes.Add(instance);
            
            SlotContentAdded?.Invoke(instance);
            storageAssigner.AssignToStorage(instance);
        }

        public List<IMethod> GetMethods()
        {
            return new List<IMethod> { selectWeaponMethod, selectArmorMethod };
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public bool DoesBelongToPlayer()
        {
            return _belongsToPlayer;
        }
    }
}