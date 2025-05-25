using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/MethodBlueprints/UpgradeMethod")]
    public class UpgradeMethod : CallActionMethod
    {
        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
        {
            base.SubscribeButtonToUpdateEvents(methodProvider, button);

            if(methodProvider is IUpgradeMethodProvider upgradeMethodProvider)
            {
                upgradeMethodProvider.OnUpgradeProgress += button.SetFillAmount;
                button.OnDestruction += () => upgradeMethodProvider.OnUpgradeProgress -= button.SetFillAmount;
            }
        }
    }
}