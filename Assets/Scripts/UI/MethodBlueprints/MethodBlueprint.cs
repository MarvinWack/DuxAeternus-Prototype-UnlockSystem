using System;
using System.Collections.Generic;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    public abstract class MethodBlueprint<T> : ScriptableObject, IMethod where T : Delegate
    {
        protected readonly List<T> delegates = new();
        
        public ExtendedButton buttonPrefab;
        
        public virtual ExtendedButton InstantiateButton(ICallReceiver receiver = null)
        {
            var button = Instantiate(buttonPrefab);
            button.name = name;
            //todo: in base extended button Event<T> -> konkrete buttons nur ein event?

            if(receiver != null)
                SubscribeToButtonEvent(receiver, button);
            
            return button;
        }

        protected abstract void SubscribeToButtonEvent(ICallReceiver receiver, ExtendedButton button);

        protected T GetDelegate(ICallReceiver receiver)
        {
            if (receiver == null)
            {
                Debug.LogError($"{name}: {nameof(receiver)} is null");
                return null;
            }
        
            return delegates.Find(x => x.Target == receiver);
        }

        // protected abstract void HandleButtonClick<X>(X args);
        
        // private void HandleButtonClick(T args)

        // {

        //     if (args == null)

        //     {

        //         Debug.LogError($"{name}: {nameof(args)} is null");

        //         return;

        //     }

        //     

        //     

        // }

        public abstract void RegisterReceiverHandler(T handler);
        
        
        public abstract void CallMethod(ICallReceiver receiver);

        public string GetName()
        {
            return name;
        }

        // private void OnEnable()
        // {
        //     //todo:
        //     buttonPrefab = (ExtendedButton)Resources.Load("Prefabs/Visuals/UI", typeof(ExtendedButton));
        // }

        //todo: methoden/params-name?
    }

    public interface IMethod
    {
        public void CallMethod(ICallReceiver receiver);
        public string GetName();

        public ExtendedButton InstantiateButton(ICallReceiver receiver = null);
    }
}