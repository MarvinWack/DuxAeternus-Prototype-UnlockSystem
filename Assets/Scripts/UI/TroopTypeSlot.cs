using System;
using Entities.Buildings;
using Objects;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class TroopTypeSlot : MonoBehaviour, IDirectCaller, IProgressVisualiser, IMessageForwarder
    {
        public event Action<string> OnLabelChanged;
        public event Action<float> OnUpgradeProgress;
        public event Action<string> OnMessageForwarded;
        public bool IsTroopTypeSet => _isIsTroopTypeSet;


        [Header("References")]
        
        [SerializeField] private ExtendedButton[] recruitButtons = new ExtendedButton[3];

        [Header("Settings")] 
        [SerializeField] private int[] recruitAmounts = {1, 3, 10};

        [SerializeField] private TroopType _troopType;
        private bool _isIsTroopTypeSet;


        public void Setup(ISlotContent troopType)
        {
            if (_troopType is not null)
                return;
            
            _troopType = troopType as TroopType;
            _isIsTroopTypeSet = true;
            
            _troopType.OnMessageForwarded += OnMessageForwarded;
            
            for(int i = 0; i < recruitAmounts.Length; i++)
            {
                recruitButtons[i].DisplayMessage(recruitAmounts[i].ToString());
                recruitButtons[i].SetIndex(i);
                recruitButtons[i].OnClick += HandleClick;
            }

            if (troopType is IUpgradable upgradable)
                upgradable.OnUpgrade += HandleRecruitment;
            
            OnLabelChanged?.Invoke(_troopType.GetName());
        }

        private void HandleClick(Vector3 arg1, bool arg2, int index)
        {
            _troopType.Recruit(recruitAmounts[index]);
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