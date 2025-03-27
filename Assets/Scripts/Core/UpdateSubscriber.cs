using UnityEngine;

namespace Core
{
    public class UpdateSubscriber : MonoBehaviour
    {

        public static void RegisterUpdatableBuilder(ITickReceiverBuilder builder)
        {
            builder.OnUpdatableCreated += HandleUpdatableCreated;
        }

        private static void HandleUpdatableCreated(ITickReceiver updatable)
        {
            if (updatable is IResearchTickReceiver researchTickReceiver)
            {
                Updater.Instance.ResearchTick += researchTickReceiver.ResearchTickHandler;
            }
            if (updatable is IUpgradeTickReceiver upgradeTickReceiver)
            {
                Updater.Instance.UpgradeTick += upgradeTickReceiver.UpgradeTickHandler;
            }
            if (updatable is IProductionTickReceiver productionTickReceiver)
            {
                Updater.Instance.ProductionTick += productionTickReceiver.ProductionTickHandler;
            }
        }
    }
}