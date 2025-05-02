using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/UpgradeMethod")]
    public class UpgradeMethod : MethodBlueprint<Action>
    {
        // public delegate bool ReturnBoolDelegate();
        
        private readonly List<Action> _delegates = new();
        
        public override void RegisterReceiverHandler(Action handler)
        {
            if (handler == null)
            {
                Debug.LogError($"{name}: Registered receiver handler is null");
                return;
            }
            
            _delegates.Add(handler);
        }

        // public Action RegisterSender(ICallReceiver receiver)
        // {
        //     if (receiver == null)
        //     {
        //         Debug.LogError($"{name}: {nameof(receiver)} is null");
        //         return null;
        //     }
        //
        //     return _delegates.Find(x => x.Target == receiver);
        // }

        public override void CallMethod(ICallReceiver receiver)
        {
            if (receiver == null)
            {
                Debug.LogError($"{name}: {nameof(receiver)} is null");
                return;
            }

            var result = _delegates.Find(x => x.Target == receiver);
            if (result == null)
            {
                Debug.LogError("Receiver not found");
            }
                
            
            _delegates.Find(x => x.Target == receiver).Invoke();
        }
    }
}