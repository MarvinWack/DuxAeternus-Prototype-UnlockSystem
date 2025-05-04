using System;
using System.Collections.Generic;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu (menuName = "UI/RecruitMethod")]
    public class RecruitMethod : MethodBlueprint<Action<int>>
    {
        [SerializeField] private int amountToRecruit;
        
        private readonly List<Func<int,bool>> enableCheckers = new();
        
        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke(amountToRecruit));
        }

        private Func<int, bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }

        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            button.OnClickNoParamsTest += () => 
                GetMethod(methodProvider).Invoke(amountToRecruit);
        }

        public override void RegisterMethodToCall(Action<int> handler)
        {
            methodsToCall.Add(handler);
        }

        public void RegisterMethodEnableChecker(Func<int, bool> enableChecker)
        {
            enableCheckers.Add(enableChecker);
            // UIUpdater.UIBehaviourModifiedTick += () => 
            //     CallEnableStatusChangedEvent(enableChecker(amountToRecruit)); 
        }
    }
}