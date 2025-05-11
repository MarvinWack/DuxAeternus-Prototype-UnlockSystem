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

        protected abstract void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button);
        protected abstract void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button);
        
        public abstract void RegisterMethodToCall(T handler, IMethodProvider methodProvider);

        public virtual ExtendedButton InstantiateButton(IMethodProvider methodProvider)
        {
            var button = Instantiate(buttonPrefab);
            button.name = name;
            //todo: in base extended button Action<T> -> konkrete buttons nur ein event?
            
            methodInfos.Find(x => x.MethodProvider == methodProvider).Button = button;
            
            SubscribeProviderToButtonEvent(methodProvider, button);
            SubscribeButtonToUpdateEvents(methodProvider, button);
            
            return button;
        }

        // public void SetupButton(IMethodProvider methodProvider)
        // {
        //     if (methodProvider == null)
        //     {
        //         Debug.LogError("MethodProvider is null");
        //         return;
        //     }
        //
        //     var methodInfo = methodInfos.Find(x => x.IsProviderSet == false);
        //     methodInfo.MethodProvider = methodProvider;
        //     methodInfo.MethodToCall = GetMethod(methodProvider);
        //     
        //     SubscribeProviderToButtonEvent(methodProvider, methodInfo.Button);
        //     SubscribeButtonToUpdateEvents(methodProvider, methodInfo.Button);
        // }

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

        public string GetName()
        {
            return name;
        }
    }

    public interface IMethod
    {
        public string GetName();

        public ExtendedButton InstantiateButton(IMethodProvider methodProvider);
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