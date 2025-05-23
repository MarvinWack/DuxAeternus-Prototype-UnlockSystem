using System;
using System.Collections.Generic;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    /// <summary>
    /// Class for mapping methods of gameplay-objects to their corresponding buttons.
    /// Controls behaviour of buttons and sets up events for communication between buttons
    /// and method-providers.
    /// </summary>
    public abstract class MethodBlueprint<T> : ScriptableObject, IMethod where T : Delegate
    {
        [SerializeField] protected ExtendedButton buttonPrefab;
        [SerializeField] private string buttonText;
        [SerializeField] private ExtendedText labelPrefab;

        protected readonly List<MethodInfo> methodInfos = new();
        
        private readonly List<Func<bool>> enableCheckers = new();

        public void RegisterMethodToCall(T handler, IMethodProvider methodProvider)
        {
            methodInfos.Add(new MethodInfo
            {
                MethodToCall = handler,
                MethodProvider = methodProvider,
            });
        }

        public void RegisterMethodEnableChecker(Func<bool> enableChecker)
        {
            enableCheckers.Add(enableChecker);
        }

        public virtual ExtendedButton InstantiateButton(IMethodProvider methodProvider, string text = null)
        {
            var button = Instantiate(buttonPrefab);
            
            return SetupButton(button, methodProvider, text);
        }

        public ExtendedButton SetupButton(ExtendedButton button, IMethodProvider methodProvider, string text = null)
        {
            if(text != null)
                SetButtonText(button, text);
            else
                SetButtonText(button, buttonText);
            
            methodInfos.Find(x => x.MethodProvider == methodProvider).Button = button;
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            SubscribeButtonToUpdateEvents(methodProvider, button);
            
            InvokeUIUpdater();
            
            button.UseFadeOnInteractableChanged(true);
            
            return button;
        }

        public ExtendedButton GetButton(IMethodProvider methodProvider)
        {
            var button = methodInfos.Find(x=> x.MethodProvider == methodProvider).Button;
            if(button == null)
                Debug.LogError("GetButton: MethodProvider not in List");
            
            return button;
        }

        public List<ExtendedButton> GetAllButtons(ExtendedButton stylePreset)
        {
            var buttons = new List<ExtendedButton>();

            foreach (var method in methodInfos)
            {
                var button = Instantiate(stylePreset.gameObject).GetComponent<ExtendedButton>();
                buttons.Add(SetupButton(button, method.MethodProvider, method.MethodProvider.GetName()));
            }

            return buttons;
        }

        public List<ExtendedButton> GetAllButtons()
        {
            var buttons = new List<ExtendedButton>();

            foreach (var method in methodInfos)
            {
                buttons.Add(InstantiateButton(method.MethodProvider, method.MethodProvider.GetName()));
            }

            return buttons;
        }

        public List<Tuple<ExtendedButton, ExtendedText>> GetAllButtonsWithLabels()
        {
            var buttons = new List<Tuple<ExtendedButton, ExtendedText>>();

            foreach (var method in methodInfos)
            {
                var button = InstantiateButton(method.MethodProvider, method.MethodProvider.GetName());
                var label = InstantiateLabel(method.MethodProvider, "0");
                
                buttons.Add(new Tuple<ExtendedButton, ExtendedText>(button, label));
            }

            return buttons;
        }

        public string GetName()
        {
            return name;
        }

        protected abstract void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button);
        protected virtual void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            UIUpdater.UIBehaviourModifiedTick += UpdateButtonInteractable;
            button.OnDestruction += () => UIUpdater.UIBehaviourModifiedTick -= UpdateButtonInteractable;

            return;

            void UpdateButtonInteractable()
            {
                button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
            }
        }

        protected void InvokeUIUpdater()
        {
            try
            {
                UIUpdater.UIBehaviourModifiedTick?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error during UIBehaviourModifiedTick invocation! " +
                               "Check if all required SOs are Set; Check if all enableChecker-Methods work. " +
                               "Check if all buttons unsubscribe from UIUpdater correctly. " +
                               $": {ex.Message}\n{ex.StackTrace}");
            }
        }

        protected T GetMethod(IMethodProvider methodProvider)
        {
            if (methodProvider == null)
            {
                Debug.LogError($"{name}: {nameof(methodProvider)} is null");
                return null;
            }

            var result = methodInfos.Find(x => x.MethodToCall.Target == methodProvider);
            
            if (result == null)
            {
                Debug.LogError("Receiver not found");
            }
            
            return methodInfos.Find(x => x.MethodToCall.Target == methodProvider).MethodToCall;
        }

        protected Func<bool> GetEnableChecker(IMethodProvider methodProvider)
        {
            return enableCheckers.Find(x => x.Target == methodProvider);
        }

        protected class MethodInfo
        {
            public IMethodProvider MethodProvider;
            public T MethodToCall;
            public ExtendedButton Button;
        }

        private ExtendedText InstantiateLabel(IMethodProvider methodProvider, string initalText = null)
        {
            var label = Instantiate(labelPrefab);
            
            if(initalText != null)
                label.SetText(initalText);
            
            SubscribeLabelToUpdateEvent(methodProvider, label);
            
            return label;
        }

        private void SubscribeLabelToUpdateEvent(IMethodProvider methodProvider, ExtendedText label)
        {
            if(methodProvider is IUpgradeMethodProvider upgradeMethodProvider)
            {
                upgradeMethodProvider.OnUpgradeFinished += UpdateLabelText;
                label.OnDestruction += () => upgradeMethodProvider.OnUpgradeFinished -= UpdateLabelText;
            }

            return;

            void UpdateLabelText(int value)
            {
                label.SetText(value.ToString());
            }
        }

        private void SetButtonText(ExtendedButton button, string text)
        {
            if(text == null)
            {
                button.SetText(name);
                button.name = name;
            }
            else
            {
                button.SetText(text);
                button.name = text;
            }
        }
    }
}