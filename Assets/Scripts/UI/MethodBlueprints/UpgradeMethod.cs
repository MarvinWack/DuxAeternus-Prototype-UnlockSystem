using System;
using System.Collections.Generic;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/UpgradeMethod")]
    public class UpgradeMethod : MethodBlueprint<Action>
    {
        
        private readonly List<Func<bool>> enableCheckers = new();
        public override void RegisterMethodToCall(Action handler, IMethodProvider methodProvider)
        {
            if (handler == null)
            {
                Debug.LogError($"{name}: Registered receiver handler is null");
                return;
            }
            
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
            button.OnClickNoParamsTest += () => GetMethod(methodProvider);
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
        }

        private Func<bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }
    }
}