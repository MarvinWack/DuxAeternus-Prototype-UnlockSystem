using System;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu (menuName = "UI/RecruitMethod")]
    public class RecruitMethod : MethodBlueprint<Action<int>>
    {
        [SerializeField] private int amountToRecruit;
        
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
            
            delegates.Find(x => x.Target == receiver).Invoke(amountToRecruit);
        }

        protected override void SubscribeToButtonEvent(ICallReceiver receiver, ExtendedButton button)
        {
            button.OnClickNoParamsTest += () => CallMethod(receiver);
        }

        public override void RegisterReceiverHandler(Action<int> handler)
        {
            if (handler == null)
            {
                Debug.LogError($"{name}: Registered receiver handler is null");
                return;
            }
            
            delegates.Add(handler);
        }
    }
}