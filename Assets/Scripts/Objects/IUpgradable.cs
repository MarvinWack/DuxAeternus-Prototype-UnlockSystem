using System;

namespace Objects
{
    //todo: wie sicherstellen, dass ITickReceiver ebenfalls implementiert wird?
    //Upgradable-Untergruppe von ITickReceiver als base?
    //evtl allgemeiner -> generelle WerteUpdates (zb Unit-count)
    public interface IUpgradable
    {
        public event Action<int> OnUpgrade;
        public event Action<float> OnUpgradeProgress;
        public event Action<bool> OnUpgradableStatusChanged;
        
        public bool StartUpgrade();
    }
}