using System;
using System.Collections.Generic;
using System.Linq;
using Objects.Research;
using Production.Items;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/MethodBlueprints/SelectItemMethod")]
    public class SelectItemMethod : CallParameterAction<ItemBlueprint>
    {
        public event Action<string> OnItemSelected;
        
        private ResearchTree _researchTree;
        
        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            base.SubscribeProviderToButtonEvent(methodProvider, button);
                
            button.OnClickNoParams += () => OnItemSelected?.Invoke(parameters[button].name);
        }

        public void Setup(ResearchTree researchTree)
        {
            _researchTree = researchTree;
        }

        public List<ExtendedButton> GetButtonForEachOption()
        {
            var buttons = new List<ExtendedButton>();
            
            foreach (var tech in _researchTree.GetItemTechs())
            {
                buttons.Add(InstantiateParameterButton(methodInfos.First(x => x.MethodProvider.DoesBelongToPlayer()).MethodProvider, tech, tech.name));
            }
            
            InvokeUIUpdater();

            return buttons;
        }

        //todo: aufräumen sobald methodProvider und enableChecker getrennte interfaces sind
        //(SubscribeButtonToUpdateEvents funzt hier nicht weil Tech nicht MethodProvider ist und 
        //spezielle Methode zum freischalten der Buttons hat)
        private ExtendedButton InstantiateParameterButton(IMethodProvider methodProvider, Tech tech, string text = null)
        {
            if(tech.TechBlueprint is not ItemTechBlueprint itemTech) 
                return null;

            var button = base.InstantiateParameterButton(methodProvider, itemTech.Item, text);
            
            UIUpdater.UIBehaviourModifiedTick += UpdateButtonInteractable;
            button.OnDestruction += () => UIUpdater.UIBehaviourModifiedTick -= UpdateButtonInteractable;
            
            return button;
            
            void UpdateButtonInteractable()
            {
                button.SetInteractable(tech.CheckIfAssociatedItemIsUnlocked());
            }
        }
    }
}