using System;

namespace UI.MethodBlueprints
{
    public interface IUpgradeMethodProvider : IMethodProvider
    {
        public event Action<int> OnUpgradeFinished;
        public event Action<float> OnUpgradeProgress;
        // public event Action<bool> OnUpgradableStatusChanged;
    }
}