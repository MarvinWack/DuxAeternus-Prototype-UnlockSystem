using Objects.TroopTypes;
using TMPro;
using UI.MethodBlueprints;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class UnitDesignerGrid : SlotGridBase
    {
        [SerializeField] private SelectItemMethod selectWeaponMethod;
        [SerializeField] private SelectItemMethod selectArmorMethod;
        [SerializeField] private UpgradeMethod createTroopTypeMethod;
        
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private ExtendedButton createTroopTypeButton;
        
        [SerializeField] private TroopTypeCreator troopTypeCreator;
        [SerializeField] private ResearchTree researchTree;

        private void Start()
        {
            Setup();
            selectWeaponMethod.Setup(researchTree);
            selectArmorMethod.Setup(researchTree);
        }

        protected override void Setup()
        {
            foreach (var method in troopTypeCreator.GetMethods())
            {
                if (method is not SelectItemMethod selectMethod)
                    return;
                
                var instance = Instantiate(_slotPrefab, transform);

                var slot = instance.GetComponent<SelectItemSlot>();
                slot.Setup(selectMethod, dropDownMenu);
            }

            createTroopTypeMethod.SetupButton(createTroopTypeButton, troopTypeCreator);
            
            nameInputField.onValueChanged.AddListener(troopTypeCreator.SetTypeName);
        }
    }
}