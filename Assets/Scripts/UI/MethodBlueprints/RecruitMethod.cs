using System;
using System.Collections.Generic;
using System.Linq;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu (menuName = "UI/RecruitMethod")]
    public class RecruitMethod : CallParameterAction<int>
    {
        [SerializeField] private int[] amounts = new int[3];
        
        private readonly List<Func<int,bool>> enableCheckers = new();

        public void RegisterMethodEnableChecker(Func<int, bool> enableChecker)
        {
            enableCheckers.Add(enableChecker);
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke(parameters[button]));
            
            button.OnDestruction += () => UIUpdater.UIBehaviourModifiedTick -= () =>
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke(parameters[button]));
        }

        private new Func<int, bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }
        
        public List<ExtendedButton> GetButtonForEachOption()
        {
            var buttons = new List<ExtendedButton>();
            
            foreach (var amount in amounts)
            {
                buttons.Add(InstantiateParameterButton(methodInfos.First(x => x.MethodProvider.DoesBelongToPlayer()).MethodProvider, amount, amount.ToString()));
            }

            return buttons;
        }
    }
}