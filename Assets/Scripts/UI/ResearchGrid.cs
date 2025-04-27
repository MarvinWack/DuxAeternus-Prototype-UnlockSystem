using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class ResearchGrid : GridBase
    {
        [SerializeField] private SlotFactory slotFactory;
        
        protected override async void SetupButtonGrid()
        {
            //todo: bootstrapper
            await Task.Delay(500);
            
            slotFactory.CreateGrid(this);
            
            // var techs = islotContentSource.GetSlotItems(typeof(Tech)).Cast<Tech>().ToList();
            
            // for (int i = 0; i < techs.Count; i++)
            // {
            //     // InstantiateButton(i, techs[i]);
            //     
            // }
        }

        // private void InstantiateButton(int i, Tech tech)
        // {
        //     var instance = Instantiate(slotButtonPrefab, transform);
        //     var researchSlot = instance.transform.GetChild(0).AddComponent<ResearchSlot>();
        //     researchSlot.Setup(tech, i);
        //     instance.GetComponent<SlotButton>().Setup(infoWindow, researchSlot);
        //     SetLabelText(tech.name, instance);
        // }
    }
}