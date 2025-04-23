using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace UI
{
    public class ResearchGrid : GridBase
    {
        protected override async void SetupButtonGrid()
        {
            //todo: bootstrapper
            await Task.Delay(500);
            
            var techs = IslotContentSource.GetSlotItems(typeof(Tech)).Cast<Tech>().ToList();
            
            for (int i = 0; i < techs.Count; i++)
            {
                InstantiateButton(i, techs[i]);
            }
        }

        private void InstantiateButton(int i, Tech tech)
        {
            var instance = Instantiate(slotButtonPrefab, transform);
            var researchSlot = instance.transform.GetChild(0).AddComponent<ResearchSlot>();
            researchSlot.Setup(tech, i);
            instance.GetComponent<SlotButton>().Setup(i, infoWindow, researchSlot);
            SetLabelText(tech.name, instance);
        }
    }
}