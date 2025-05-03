using System;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/UpgradeMethod")]
    public class UpgradeMethod : MethodBlueprint<Action>
    {
        protected override void SubscribeToButtonEvent(ICallReceiver receiver, ExtendedButton button)
        {
            throw new NotImplementedException();
        }

        public override void RegisterReceiverHandler(Action handler)
        {
            if (handler == null)
            {
                Debug.LogError($"{name}: Registered receiver handler is null");
                return;
            }
            
            delegates.Add(handler);
        }
        
        public override void CallMethod(ICallReceiver receiver)
        {
            if (receiver == null)
            {
                Debug.LogError($"{name}: {nameof(receiver)} is null");
                return;
            }
        
            var result = delegates.Find(x => x.Target == receiver);
            if (result == null)
            {
                Debug.LogError("Receiver not found");
            }
                
            
            delegates.Find(x => x.Target == receiver).Invoke();
        }
    }
}