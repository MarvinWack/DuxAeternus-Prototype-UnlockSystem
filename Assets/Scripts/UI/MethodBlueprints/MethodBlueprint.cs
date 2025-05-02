using UnityEngine;

namespace UI.MethodBlueprints
{
    public abstract class MethodBlueprint<T> : ScriptableObject
    {
        public abstract void RegisterReceiverHandler(T handler);

        public abstract void CallMethod(ICallReceiver receiver);
    }
}