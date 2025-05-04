using System;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/UpgradeMethod")]
    public class UpgradeMethod : MethodBlueprint<Action>
    {
        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            button.OnClickNoParamsTest += () => GetMethod(methodProvider);
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            throw new NotImplementedException();
        }

        public override void RegisterMethodToCall(Action handler)
        {
            if (handler == null)
            {
                Debug.LogError($"{name}: Registered receiver handler is null");
                return;
            }
            
            methodsToCall.Add(handler);
        }
        
        // public void RegisterEnableChecker(Func<bool> enableChecker)
        // {
        //     // UIUpdater.UIBehaviourModifiedTick += () => CallEnableStatusChangedEvent(enableChecker()); 
        // }
    }
}