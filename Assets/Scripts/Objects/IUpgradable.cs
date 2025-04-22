using System;

namespace Objects
{
    //todo: wie sicherstellen, dass ITickReceiver ebenfalls implementiert wird?
    //Upgradable-Untergruppe von ITickReceiver als base?
    public interface IUpgradable
    {
        public event Action<int> OnUpgrade;
        public event Action<float> OnUpgradeProgress;
        
        public bool StartUpgrade();
    }
}