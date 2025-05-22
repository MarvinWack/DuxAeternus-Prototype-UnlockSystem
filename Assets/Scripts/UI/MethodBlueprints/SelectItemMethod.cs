using System;
using System.Collections.Generic;
using System.Linq;
using Objects.Research;
using Production.Items;
using UI.Slot;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "MethodBlueprints/SelectItemMethod")]
    public class SelectItemMethod : MethodBlueprint<Action<ItemBlueprint>>
    {
        public event Action<string> OnItemSelected;
        
        [SerializeField] private ParameterButton parameterButtonPrefab;
        
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
            if(button is ParameterButton parameterButton)
                parameterButton.OnClickNoParamsTest += () =>
                {
                    GetMethod(methodProvider).Invoke(parameterButton.GetItemBlueprint());
                    OnItemSelected?.Invoke(parameterButton.GetItemBlueprint().name);
                };
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
            
            button.OnDestruction += () => UIUpdater.UIBehaviourModifiedTick -= () => button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
        }

        public void Setup(ResearchTree researchTree)
        {
            _researchTree = researchTree;
        }

        public List<ParameterButton> GetButtonForEachOption()
        {
            var buttons = new List<ParameterButton>();
            
            foreach (var tech in _researchTree.GetItemTechs())
            {
                buttons.Add(InstantiateParameterButton(methodInfos.First(x => x.MethodProvider.DoesBelongToPlayer()).MethodProvider, tech));
            }

            return buttons;
        }

        private ParameterButton InstantiateParameterButton<T>(IMethodProvider methodProvider, T t)
        {
            var button = Instantiate(parameterButtonPrefab);

            if (t is not Tech tech)
                return null;
            
            if(tech.TechBlueprint is ItemTechBlueprint itemTech)
                button.SetParameter(itemTech.Item);    
            else
            {
                Debug.Log("GetButtonForEachOption: TechBlueprint is not an ItemTechBlueprint");
            }
            
            
            SetButtonText(button, tech.name);
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(tech.CheckIfAssociatedItemIsUnlocked());

            //funzt nicht weil Tech (enableChecker != MethodProvider)
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