using System;
using Entities.Buildings;
using Objects;
using UnityEngine;

namespace UI
{
    public class TroopTypeSlot : MonoBehaviour, IDirectCaller, IProgressVisualiser
    {
        public event Action<string> OnLabelChanged;
        public event Action<float> OnUpgradeProgress;

        private ISlotContent _troopType;

        public void Setup(ISlotContent troopType)
        {
            _troopType = troopType;

            if (troopType is IUpgradable upgradable)
                upgradable.OnUpgrade += HandleRecruitment;
        }

        private void HandleRecruitment(int amount)
        {
            OnLabelChanged?.Invoke(_troopType.GetName() + " " + amount);
        }

        public void HandleSlotClicked()
        {
            _troopType.CallSlotAction();
        }
    }
}