using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Slot
{
    public class BuildingSlot : MonoBehaviour
    {
        public event Action<Vector3, Building> DropDownCalled;
        public bool IsBuildingSet => _building != null;
        
        [SerializeField] private ExtendedButton callDropdownButton;
        [SerializeField] private Building _building;
        
        private Type _buildingType;
        private List<BuildingManager> _buildingManagers;

        private void Awake()
        {
            callDropdownButton.OnClick += CallDropDown;
        }

        public void SetBuilding(Building building)
        {
            foreach (var method in building.GetMethods())
            {
                // Destroy(callDropdownButton.gameObject);
                // callDropdownButton = method.InstantiateButton(building);
                // callDropdownButton.transform.SetParent(transform.GetChild(1), false);
            }
            callDropdownButton.SetText(building.name);
            _building = building;
        }

        private void CallDropDown(Vector3 position)
        {
            DropDownCalled?.Invoke(position, _building);
        }
        
    }
}