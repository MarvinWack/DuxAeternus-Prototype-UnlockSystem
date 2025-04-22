using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Entities.Buildings;
using Objects.Research;
using Objects.TroopTypes;
using Production.Items;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class TroopCreatorSlot : MonoBehaviour, IDropdownCaller
    {
        public Action<ItemBlueprint, int> ItemSelected;
        public event IDropdownCaller.OptionSetHandler OptionSet;

        [SerializeField] private ItemBlueprint _selectedItem;
        [SerializeField] private List<Tech> _techs = new();

        private ResearchTree _researchTree;
        private TroopTypeCreator _troopTypeCreator;
        private int _index;

        public void HandleSlotClicked()
        {
            throw new NotImplementedException();
        }

        public void Setup(ResearchTree tree, TroopTypeCreator troopTypeCreator, int index)
        {
            _researchTree = tree;
            _troopTypeCreator = troopTypeCreator;
            _index = index;
        }

        public Dictionary<string, bool> GetDropDownOptions()
        {
            _techs = _researchTree.GetItemTechs();

            return _techs.ToDictionary(tech => tech.name, tech => tech.IsAvailable);
        }

        public bool HandleOptionClicked(int index)
        {
            SetSelectedItem(index);
            OptionSet?.Invoke(_selectedItem.name);
            ItemSelected?.Invoke(_selectedItem, _index);
            //todo: check if passendes item für slot
            return true;
        }

        private void SetSelectedItem(int index)
        {
            if(_techs[index].TechBlueprint is ItemTechBlueprint itemTechBlueprint)
            {
                _selectedItem = itemTechBlueprint.Item;
                return;
            }
            
            Debug.Log("Selected Tech was not an Item-Tech");
        }
    }
}