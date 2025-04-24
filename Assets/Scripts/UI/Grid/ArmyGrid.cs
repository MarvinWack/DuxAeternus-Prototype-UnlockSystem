using System.Threading.Tasks;
using Objects;
using Unity.VisualScripting;

namespace UI
{
    public class ArmyGrid : GridBase
    {
        protected async override void SetupButtonGrid()
        { 
            await Task.Delay(500);
            
            var troopTypes = IslotContentSource.GetSlotItems(typeof(TroopType));

            for (int i = 0; i < troopTypes.Count; i++)
            {
                InstantiateButton(i, troopTypes[i]);
            }
        }
        
        private void InstantiateButton(int index, ISlotContent type)
        {
            var instance = Instantiate(slotButtonPrefab, transform);
            var troopTypeSlot = instance.transform.GetChild(0).AddComponent<TroopTypeSlot>();
            troopTypeSlot.Setup(type);
            instance.GetComponentInChildren<SlotButton>().Setup(index, infoWindow, troopTypeSlot);
            SetLabelText(type.GetName(), instance);
        }
    }
}