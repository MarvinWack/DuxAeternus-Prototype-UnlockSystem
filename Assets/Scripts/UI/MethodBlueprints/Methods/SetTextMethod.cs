// using System;
// using System.Collections.Generic;
// using Objects.TroopTypes;
// using TMPro;
// using UI.Slot;
// using UnityEngine;
//
// namespace UI.MethodBlueprints
// {
//     public class SetTextMethod : CallParameterAction<string>
//     {
//         [SerializeField] private GameObject textInputFieldPrefab;
//
//         protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
//         {
//             // textInputFieldPrefab.onValueChanged.AddListener(GetMethod(methodProvider).Invoke);
//         }
//
//         protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button)
//         {
//             UIUpdater.UIBehaviourModifiedTick += UpdateButtonInteractable;
//             button.OnDestruction += () => UIUpdater.UIBehaviourModifiedTick -= UpdateButtonInteractable;
//
//             return;
//
//             void UpdateButtonInteractable()
//             {
//                 button.SetInteractable(GetEnableChecker(methodProvider).Invoke());
//             }
//         }
//     }
// }