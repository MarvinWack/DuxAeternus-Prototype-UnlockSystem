using System;
using System.Collections.Generic;
using UI.Slot;
using Unity.VisualScripting;
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
        [Header("Settings")] 
        [SerializeField] private bool visualiseProgress;
        [SerializeField] private string buttonText;
        
        public ExtendedButton buttonPrefab;
        [SerializeField] private ExtendedText labelPrefab;
        
        protected readonly List<MethodInfo<T>> methodInfos = new();

        public abstract void RegisterMethodToCall(T handler, IMethodProvider methodProvider);
        protected abstract void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button);
        protected abstract void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button);

        public ExtendedButton SetupButton(ExtendedButton button, IMethodProvider methodProvider, string text = null)
        {
            if(text != null)
                SetButtonText(button, text);
            else
                SetButtonText(button, buttonText);
            
            methodInfos.Find(x => x.MethodProvider == methodProvider).Button = button;
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            SubscribeButtonToUpdateEvents(methodProvider, button);
            
            UIUpdater.UIBehaviourModifiedTick?.Invoke();
            
            button.UseFadeOnInteractableChanged(true);
            
            return button;
        }
        
        public virtual ExtendedButton InstantiateButton(IMethodProvider methodProvider, string text = null)
        {
            var button = Instantiate(buttonPrefab);

            if(text != null)
                SetButtonText(button, text);
            else
                SetButtonText(button, buttonText);
            
            methodInfos.Find(x => x.MethodProvider == methodProvider).Button = button;
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            SubscribeButtonToUpdateEvents(methodProvider, button);
            
            UIUpdater.UIBehaviourModifiedTick?.Invoke();
            
            button.UseFadeOnInteractableChanged(true);
            
            return button;
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

        protected void SetButtonText(ExtendedButton button, string buttonText)
        {
            if(buttonText == null)
            {
                button.SetText(name);
                button.name = name;
            }
            else
            {
                button.SetText(buttonText);
                button.name = buttonText;
            }
        }
    }

    public interface IMethod
    {
        public string GetName();

        public ExtendedButton InstantiateButton(IMethodProvider methodProvider, string text = null);
        public ExtendedButton GetButton(IMethodProvider methodProvider);
    }

    public class MethodInfo<T>
    {
        public bool IsProviderSet => MethodProvider is not null;
        public IMethodProvider MethodProvider; //todo: austauschen durch GUID?
        public T MethodToCall;
        public ExtendedButton Button;
    }
}