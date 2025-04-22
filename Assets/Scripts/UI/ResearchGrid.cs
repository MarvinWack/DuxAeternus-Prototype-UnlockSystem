using System.Threading.Tasks;
using Unity.VisualScripting;

namespace UI
{
    public class ResearchGrid : GridBase
    {
        protected override async void SetupButtonGrid()
        {
            await Task.Delay(500);
            
            var techs = researchTree.GetAllTechs();
            
            for (int i = 0; i < techs.Count; i++)
            {
                InstantiateButton(i, techs[i]);
            }
        }

        private void InstantiateButton(int i, Tech tech)
        {
            var slotButton = Instantiate(slotButtonPrefab, transform);
            var researchSlot = slotButton.transform.GetChild(0).AddComponent<ResearchSlot>();
            researchSlot.Setup(tech, i);
            slotButton.GetComponent<SlotButton>().Setup(i, infoWindow, researchSlot);
            SetLabelText(tech.name, slotButton);
        }
    }
}