using System;
using System.Collections.Generic;
using Objects.TroopTypes;
using TMPro;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    public class SetTextMethod : MethodBlueprint<Action<string>>
    {
        [SerializeField] private GameObject textInputFieldPrefab;
        
        private List<Func<bool>> enableCheckers = new();
        
        public override void RegisterMethodToCall(Action<string> handler, IMethodProvider methodProvider)
        {
            methodInfos.Add(new MethodInfo<Action<string>>
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
            // textInputFieldPrefab.onValueChanged.AddListener(GetMethod(methodProvider).Invoke);
        }

        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += UpdateButtonInteractable;
            button.OnDestruction += () => UIUpdater.UIBehaviourModifiedTick -= UpdateButtonInteractable;

            return;

            void UpdateButtonInteractable()
            {
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
            }
        }
        
        private Func<bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }

        public TMP_InputField InstantiateInputField(TroopTypeCreator troopTypeCreator)
        {
            // var button = Instantiate(buttonPrefab);
            //
            // methodInfos.Find(x => x.MethodProvider == methodProvider).Button = button;
            //
            // SubscribeProviderToButtonEvent(methodProvider, button);
            // SubscribeButtonToUpdateEvents(methodProvider, button);
            //
            // UIUpdater.UIBehaviourModifiedTick?.Invoke();
            //
            // button.UseFadeOnInteractableChanged(true);
            //
            return null;
        }
    }
}