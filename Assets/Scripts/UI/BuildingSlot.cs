using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Slot
{
    public class BuildingSlot : TwoBehaviorsSlot
    {
        [SerializeField] private Dictionary<Tuple<Func<bool>, bool>, ExtendedButton> _callableMethods;
        
        //todo: delegate für jew. methodentyp in interface base class definieren,
        //davon dann caller und receiver ableiten
        //davon wiederum interfaces mit namen für jew. methode (StartUpgrade)
        // private delegate bool test();

        // [SerializeField] private Dictionary<Func<bool>, ExtendedButton> delegateButtons = new();

        [SerializeField] private ExtendedButton _buildingButton;
        
        private BuildingType _buildingType;
        // [SerializeField] private Func<bool> _startUpgrade;
        private Building _building;
        private List<BuildingManager> _buildingManagers;
        
        private List<MenuOptionFunc> menuOptionsList = new();
        private delegate bool MenuOptionFunc();

        public void Setup(BuildingType buildingType, DropDownMenu dropdown, ResearchTree researchTree)
        {
            menuOptionsList.Add(CallUpgrade);
            
            _buildingType = buildingType;
            dropdownMenu = dropdown;
            slotContentSource = researchTree;
        }

        protected override Dictionary<string, bool> GetOptionSetMenu()
        {
            var options = new Dictionary<string, bool>();
            
            options.Add("Level " + _building.Level, _building.IsUpgradeable);
            
            return options;
        }

        protected override bool NoOptionSetBehavior(int index)
        {
            _building = _buildingManagers[index].TryCreateBuilding();
            
            if (_building == null)
            {
                Debug.Log("_building could not be built");
                return false;
            }
            
            _building.OnUpgradeProgress += _buildingButton.SetFillAmount;
            _building.OnUpgradableStatusChanged += _buildingButton.SetInteractable;
            
            _buildingButton.SetText(_building.name);
            // OptionSet?.Invoke(_building.name);
            SetSlot();
            return true;
        }

        private bool CallUpgrade()
        {   
            _building.StartUpgrade();
            return false;
        }

        protected override bool OptionSetBehavior(int index)
        {
            if(index == -1)
                Debug.Log("index is -1");
            
            menuOptionsList[index].Invoke();
            return false;
        }

        protected override Dictionary<string, bool> GetOptionNotSetMenu()
        {
            var results = new Dictionary<string, bool>();
            
            switch (_buildingType)
            {
                case BuildingType.Small: _buildingManagers = slotContentSource.GetSlotItems(typeof(SmallBuildingManager)).Cast<BuildingManager>().ToList();
                    break;
                case BuildingType.Core: _buildingManagers = slotContentSource.GetSlotItems(typeof(CoreBuildingManager)).Cast<BuildingManager>().ToList();
                    break;
                case BuildingType.Large: _buildingManagers = slotContentSource.GetSlotItems(typeof(LargeBuildingManager)).Cast<BuildingManager>().ToList();
                    break;
                default: throw new Exception($"Unsupported building type: {_buildingType}");
            }

            foreach (var option in _buildingManagers)
            {
                results.Add(option.GetName(), option.IsAvailable);
            }
            
            return results;
        }

        // protected override void HandleCallableMethodsChanged(Dictionary<Func<bool>, bool> methods)
        // {
        //     foreach (var method in methods)
        //     {
        //         delegateButtons[method.Key].SetInteractable(method.Value);
        //     }
        // }
    }
}