using System;
using System.Collections.Generic;
using System.Linq;
using Objects.Research;
using Production.Items;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "MethodBlueprints/SelectItemMethod")]
    public class SelectItemMethod : MethodBlueprint<Action<ItemBlueprint>>
    {
        public event Action<string> OnItemSelected;
        
        [SerializeField] private ItemParameterButton itemButtonPrefab;
        
        private ResearchTree _researchTree;
        private List<Func<bool>> enableCheckers = new();
        
        public override void RegisterMethodToCall(Action<ItemBlueprint> handler, IMethodProvider methodProvider)
        {
            methodInfos.Add(new MethodInfo<Action<ItemBlueprint>>
            {
                MethodToCall = handler,
                MethodProvider = methodProvider
            });
        }

        public void RegisterMethodEnableChecker(Func<bool> checkIfItemIsUnlocked)
        {
            enableCheckers.Add(checkIfItemIsUnlocked);
        }
        
        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            if(button is ItemParameterButton parameterButton)
                parameterButton.OnClickNoParamsTest += () =>
                {
                    GetMethod(methodProvider).Invoke(parameterButton.GetParameter());
                    OnItemSelected?.Invoke(parameterButton.GetParameter().name);
                };
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
        }

        public List<ItemParameterButton> GetButtonForEachOption()
        {
            var buttons = new List<ItemParameterButton>();
            
            foreach (var tech in _researchTree.GetItemTechs())
            {
                buttons.Add(InstantiateParameterButton(methodInfos.First(x => x.MethodProvider.DoesBelongToPlayer()).MethodProvider, tech));
            }

            return buttons;
        }

        public void Setup(ResearchTree researchTree)
        {
            _researchTree = researchTree;
        }
        
        private ItemParameterButton InstantiateParameterButton(IMethodProvider methodProvider, Tech tech)
        {
            var button = Instantiate(itemButtonPrefab);
            
            if(tech.TechBlueprint is ItemTechBlueprint itemTech)
                button.SetParameter(itemTech.Item);    
            else
            {
                Debug.Log("GetButtonForEachOption: TechBlueprint is not an ItemTechBlueprint");
            }
            
            
            SetButtonText(button, tech.name);
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(tech.CheckIfItemIsUnlocked());
            
            // SubscribeButtonToUpdateEvents(methodProvider, button);
            
            try
            {
                UIUpdater.UIBehaviourModifiedTick?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during UIBehaviourModifiedTick invocation! " +
                               $"Check if all required SOs are Set; Check if all enableChecker-Methods work" +
                               $": {ex.Message}\n{ex.StackTrace}");
            }
            
            button.UseFadeOnInteractableChanged(true);
            
            return button;
        }
        
        private Func<bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }
    }
}