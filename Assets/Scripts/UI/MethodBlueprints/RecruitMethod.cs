using System;
using System.Collections.Generic;
using System.Linq;
using Objects.Research;
using Production.Items;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu (menuName = "UI/RecruitMethod")]
    public class RecruitMethod : MethodBlueprint<Action<int>>
    {
        [SerializeField] private int amountToRecruit;
        [SerializeField] private int[] amounts = new int[3];
        [SerializeField] private ParameterButton parameterButtonPrefab;
        
        private readonly List<Func<int,bool>> enableCheckers = new();
        
        public override void RegisterMethodToCall(Action<int> handler, IMethodProvider methodProvider)
        {
            methodInfos.Add(new MethodInfo<Action<int>>
            {
                MethodToCall = handler,
                MethodProvider = methodProvider
            });
        }

        public void RegisterMethodEnableChecker(Func<int, bool> enableChecker)
        {
            enableCheckers.Add(enableChecker);
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            if(button is ParameterButton parameterButton)
                UIUpdater.UIBehaviourModifiedTick += () => 
                    button.SetInteractable(GetEnableChecker(methodProvider).Invoke(parameterButton.GetIntValue()));
        }

        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            if(button is ParameterButton parameterButton)
            {
                button.OnClickNoParamsTest += () => GetMethod(methodProvider).Invoke(parameterButton.GetIntValue());
            }
        }

        private Func<int, bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }
        
        public List<ParameterButton> GetButtonForEachOption()
        {
            var buttons = new List<ParameterButton>();
            
            foreach (var amount in amounts)
            {
                buttons.Add(InstantiateParameterButton(methodInfos.First(x => x.MethodProvider.DoesBelongToPlayer()).MethodProvider, amount));
            }

            return buttons;
        }

        private ParameterButton InstantiateParameterButton<T>(IMethodProvider methodProvider, T t)
        {
            var button = Instantiate(parameterButtonPrefab);

            if (t is not int amount)
                return null;
            
            button.SetParameter(amount);    
            
            SetButtonText(button, amount.ToString());
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            
            // UIUpdater.UIBehaviourModifiedTick += () => 
            //     button.SetInteractable(GetEnableChecker(methodProvider));
            
            SubscribeButtonToUpdateEvents(methodProvider, button);
            
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
    }
}