using System;

namespace UI
{
    public interface IMessageForwarder
    {
        public event Action<string> OnMessageForwarded;
    }
}