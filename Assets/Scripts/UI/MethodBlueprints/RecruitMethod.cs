using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu (menuName = "UI/RecruitMethod")]
    public class RecruitMethod : MethodBlueprint<Action<int>>
    {
        public int AmountToRecruit;
        
        private readonly List<Action<int>> _delegates = new();
        
        public override void RegisterReceiverHandler(Action<int> handler)
        {
            if (handler == null)
            {
                Debug.LogError($"{name}: Registered receiver handler is null");
                return;
            }
            
            _delegates.Add(handler);
        }

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
            
            _delegates.Find(x => x.Target == receiver).Invoke(AmountToRecruit);
        }
    }
}