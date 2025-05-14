using System;
using System.Collections.Generic;
using UI.Slot;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/UpgradeMethod")]
    public class UpgradeMethod : MethodBlueprint<Action>
    {
        private readonly List<Func<bool>> enableCheckers = new();
        public override void RegisterMethodToCall(Action handler, IMethodProvider methodProvider)
        {
            methodInfos.Add(new MethodInfo<Action>
            {
                MethodToCall = handler,
                MethodProvider = methodProvider
            });
        }
        
        public void RegisterEnableChecker(Func<bool> enableChecker)
        {
            enableCheckers.Add(enableChecker); 
        }

        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            button.OnClickNoParamsTest += () => GetMethod(methodProvider).Invoke();
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            void UpdateButtonInteractable()
            {
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
            }

            UIUpdater.UIBehaviourModifiedTick += UpdateButtonInteractable;
            button.OnDestruction += () => UIUpdater.UIBehaviourModifiedTick -= UpdateButtonInteractable;
        }
        
        private Func<bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }
    }
}