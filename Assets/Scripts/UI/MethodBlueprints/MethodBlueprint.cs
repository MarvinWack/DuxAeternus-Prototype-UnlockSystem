using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UI.Slot;
using UnityEngine;
using Object = UnityEngine.Object;

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
        
        public ExtendedButton buttonPrefab;
        
        protected readonly List<MethodInfo<T>> methodInfos = new();

        public abstract void RegisterMethodToCall(T handler, IMethodProvider methodProvider);
        protected abstract void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button);
        protected abstract void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button);

        public virtual ExtendedButton InstantiateButton(IMethodProvider methodProvider, string buttonText = null)
        {
            var button = Instantiate(buttonPrefab);

            SetButtonText(button, buttonText);
            
            //todo: in base extended button Action<T> -> konkrete buttons nur ein event?
            
            methodInfos.Find(x => x.MethodProvider == methodProvider).Button = button;
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            SubscribeButtonToUpdateEvents(methodProvider, button);
            
            UIUpdater.UIBehaviourModifiedTick?.Invoke();
            
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

        public List<ExtendedButton> GetAllButtons()
        {
            var buttons = new List<ExtendedButton>();

            foreach (var method in methodInfos)
            {
                buttons.Add(InstantiateButton(method.MethodProvider, method.MethodProvider.GetType().Name));
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

        private void SetButtonText(ExtendedButton button, string buttonText)
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

        public ExtendedButton InstantiateButton(IMethodProvider methodProvider, string buttonText = null);
        public ExtendedButton GetButton(IMethodProvider methodProvider);
        // public void SetupButton(IMethodProvider methodProvider);
    }

    public class MethodInfo<T>
    {
        public bool IsProviderSet => MethodProvider is not null;
        public IMethodProvider MethodProvider; //todo: austauschen durch GUID?
        public T MethodToCall;
        public ExtendedButton Button;
    }
}