using System;

namespace Core
{
    public interface ITickReceiverBuilder
    {
        public event Action<ITickReceiver> OnUpdatableCreated;
    }
}