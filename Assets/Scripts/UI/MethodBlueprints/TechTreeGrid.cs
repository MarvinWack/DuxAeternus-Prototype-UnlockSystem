using System.Threading.Tasks;
using UnityEngine;

namespace UI.MethodBlueprints
{
    public class TechTreeGrid : SlotGridBase
    {
        [SerializeField] private UpgradeMethod upgradeMethod;

        private async void Start()
        {
            await Task.Delay(1);
            Setup();
        }

        protected override void Setup()
        {
            foreach (var tuple in upgradeMethod.GetAllButtonsWithLabels())
            {
                var slot = Instantiate(_slotPrefab, transform, false);
                
                slot.GetComponent<TechTreeSlot>().Setup(tuple);
            }
        }
    }
}