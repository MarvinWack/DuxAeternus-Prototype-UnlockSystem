using System;
using Entities.Buildings;
using UnityEngine;

namespace UI
{
    public class ResearchSlot : MonoBehaviour, IDirectCaller, IProgressVisualiser, InfoWindowCaller
    {
        private SlotButton _slotButton;
        public event Action<float> OnUpgradeProgress;
        public event Action<string> OnLabelChanged;
        
        private Tech _tech;

        public void Setup(Tech tech, int i, SlotButton slotButton)
        {
            _tech = tech;
            _tech.OnUpgradeProgress += OnUpgradeProgress;
            _tech.OnUpgrade += HandleUpgrade;
            
            _slotButton = slotButton;
        }

        private void HandleUpgrade(int level)
        {
            OnLabelChanged?.Invoke(_tech.name + " " + level);
        }

        public void HandleSlotClicked()
        {
            _tech.StartUpgrade();
        }

        public string GetInfo()
        {
            return _tech.name + ": " + _tech.TechBlueprint.Description;
        }
    }
}