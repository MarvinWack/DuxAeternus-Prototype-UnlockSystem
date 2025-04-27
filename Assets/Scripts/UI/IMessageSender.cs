using System;

namespace UI
{
    public interface IMessageSender
    {
        public event Action<string> OnMessageSent;
    }
}