using System.Collections.Generic;
using System.Threading.Tasks;
using Objects;
using UnityEngine;

namespace UI
{
    public class ArmyGrid : GridBase
    {
        [SerializeField] private SlotFactory slotFactory;
        
        private List<TroopTypeSlot> _troopTypeSlots;

        protected async override void SetupButtonGrid()
        { 
            await Task.Delay(500);
            
            slotFactory.CreateGrid(this);
        }

        public void SetSlots(List<TroopTypeSlot> troopTypeSlots)
        {
            _troopTypeSlots = troopTypeSlots;
        }
        
        public void HandleSlotContentChanged(ISlotContent troopType)
        {
            foreach (var slot in _troopTypeSlots)
            {
                if (!slot.IsTroopTypeSet)
                {
                    var troopTypeSlot = slot.GetComponent<TroopTypeSlot>();
                    troopTypeSlot.Setup(troopType);
                    return;
                }
            }
        }
    }
}