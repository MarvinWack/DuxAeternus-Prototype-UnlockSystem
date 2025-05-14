using System;
using System.Collections.Generic;
using System.Linq;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/MethodBlueprints/CreateBuildingMethod")]
    public class CreateBuildingMethod : MethodBlueprint<Func<Building>>
    {
        public event Action<Building> OnBuildingCreated;
        
        private readonly List<Func<bool>> enableCheckers = new();

        public override void RegisterMethodToCall(Func<Building> handler, IMethodProvider methodProvider)
        {
            methodInfos.Add(new MethodInfo<Func<Building>>
            {
                MethodToCall = handler,
                MethodProvider = methodProvider,
            });
        }

        public void RegisterMethodEnableChecker(Func<bool> checkIfBuildingIsBuildable)
        {
            enableCheckers.Add(checkIfBuildingIsBuildable);
        }

        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            button.OnClickNoParamsTest += () =>
                OnBuildingCreated?.Invoke(GetMethod(methodProvider).Invoke());
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += () => 
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
        }

        // protected override void UnsubscribeButtonFromEvents(ExtendedButton button, IMethodProvider methodProvider)
        // {
        //     UIUpdater.UIBehaviourModifiedTick -= () => 
        //         button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
        // }

        private Func<bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }
    }
}