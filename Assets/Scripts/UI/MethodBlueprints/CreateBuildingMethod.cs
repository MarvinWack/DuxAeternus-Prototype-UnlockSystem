using System;
using UI.Slot;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/MethodBlueprints/CreateBuildingMethod")]
    public class CreateBuildingMethod : CallFuncMethod<Building>
    {
        public event Action<Building> OnBuildingCreated;

        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            button.OnClickNoParamsTest += () => OnBuildingCreated?.Invoke(GetMethod(methodProvider).Invoke());
        }
    }
}