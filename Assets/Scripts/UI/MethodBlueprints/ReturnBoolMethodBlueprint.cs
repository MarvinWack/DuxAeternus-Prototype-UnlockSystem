using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MethodBlueprints
{
    public class ReturnBoolMethodBlueprint : ScriptableObject
    {
        public delegate bool ReturnBoolDelegate();
        
        private readonly List<ReturnBoolDelegate> _delegates = new();
        
        public void RegisterReceiverHandler(ReturnBoolDelegate handler)
        {
            if (handler == null)
            {
                Debug.LogError($"{name}: Registered receiver handler is null");
                return;
            }
            
            _delegates.Add(handler);
        }

        public ReturnBoolDelegate RegisterSender(ICallReceiver receiver)
        {
            if (receiver == null)
            {
                Debug.LogError($"{name}: {nameof(receiver)} is null");
                return null;
            }

            return _delegates.Find(x => x.Target == receiver);
        }
    }
}