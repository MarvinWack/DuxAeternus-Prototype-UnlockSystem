using System;
using UI.Slot;

namespace UI.MethodBlueprints
{
    public abstract class CallActionMethod : MethodBlueprint<Action>, ICallActionInput
    {
        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            button.OnClickNoParams += () => GetMethod(methodProvider).Invoke();
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
    }
}