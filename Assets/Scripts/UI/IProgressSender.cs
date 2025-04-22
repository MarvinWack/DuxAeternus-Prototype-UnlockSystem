using System;

namespace UI
{
    public interface IProgressSender
    {
        public event Action<float> OnUpgradeProgress;
    }
}