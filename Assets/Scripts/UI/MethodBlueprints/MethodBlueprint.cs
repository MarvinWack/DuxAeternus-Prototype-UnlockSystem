using System;
using System.Collections.Generic;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    public abstract class MethodBlueprint<T> : ScriptableObject, IMethod where T : Delegate
    {
        [Header("Settings")] 
        [SerializeField] private bool visualiseProgress;
        
        public ExtendedButton buttonPrefab;
        
        protected readonly List<T> methodsToCall = new();

        public virtual ExtendedButton InstantiateButton(IMethodProvider methodProvider = null)
        {
            var button = Instantiate(buttonPrefab);
            button.name = name;
            //todo: in base extended button Action<T> -> konkrete buttons nur ein event?

            // EnableStatusChanged += button.SetInteractable;
            
            if(methodProvider != null)
            {
                SubscribeProviderToButtonEvent(methodProvider, button);
                SubscribeButtonToUpdateEvents(methodProvider, button);
            }
            
            // if(visualiseProgress)
            //     receiver.
            
            return button;
        }

        public virtual void SetupButton(IMethodProvider receiver)
        {
            
        }

        protected abstract void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button);
        protected abstract void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button);

        protected T GetDelegate(IMethodProvider receiver)
        {
            if (receiver == null)
            {
                Debug.LogError($"{name}: {nameof(receiver)} is null");
                return null;
            }
        
            return methodsToCall.Find(x => x.Target == receiver);
        }
        
        public abstract void RegisterMethodToCall(T handler);
        
        protected T GetMethod(IMethodProvider methodProvider)
        {
            if (methodProvider == null)
            {
                Debug.LogError($"{name}: {nameof(methodProvider)} is null");
                return null;
            }

            var result = methodsToCall.Find(x => x.Target == methodProvider);
            
            if (result == null)
            {
                Debug.LogError("Receiver not found");
            }
            
            return methodsToCall.Find(x => x.Target == methodProvider);
        }

        public string GetName()
        {
            return name;
        }
    }

    public interface IMethod
    {
        public string GetName();

        public ExtendedButton InstantiateButton(IMethodProvider methodProvider = null);
    }
}