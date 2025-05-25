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
        private Blocker _blocker;

        private void Awake()
        {
            callDropdownButton.OnClick += CallDropDown;
            callDropdownButton.SetText("Choose building");
            _blocker = GetComponentInChildren<Blocker>();
        }

        public void Setup(DropDownMenu dropDownMenu)
        {
            _blocker.SetDropDown(dropDownMenu);
        }

        public void SetBuilding(Building building, ExtendedText label, DropDownMenu dropdown)
        {
            callDropdownButton.SetText(building.name);
            label.transform.SetParent(transform.GetChild(0), false);
            _building = building;
        }

        public void AcitvateBlocker()
        {
            _blocker.gameObject.SetActive(true);
        }

        private void CallDropDown(Vector3 position)
        {
            DropDownCalled?.Invoke(position, _building);
        }
    }
}