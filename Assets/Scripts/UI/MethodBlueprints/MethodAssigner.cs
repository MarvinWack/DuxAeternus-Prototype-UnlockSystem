using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Objects.TroopTypes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.MethodBlueprints
{
    [CreateAssetMenu(menuName = "UI/MethodBlueprints/MethodAssigner")]
    public class MethodAssigner : SerializedScriptableObject
    {
        [SerializeField] private TroopTypeCreator troopTypeCreator;
        // [SerializeField] private Dictionary<Type, IMethodBluePrintInput> methods;
        [SerializeField] private List<(Type, ICallActionInput)> callActionMethods;
        
        public void HandleMethodProviderCreated(INEWMethodProvider methodProvider)
        {
            switch (methodProvider)
            {
                case IActionProvider actionProvider:
                    GetBluePrintWarumWirdDasDicNichtOrdentlichSerializedwtf(methodProvider.GetType()).RegisterMethodToCall(actionProvider.GetMethod(), methodProvider);
                    GetBluePrintWarumWirdDasDicNichtOrdentlichSerializedwtf(methodProvider.GetType()).RegisterMethodEnableChecker(actionProvider.GetConditionTester());
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(methodProvider), methodProvider, null);
            }

            
        }

        private ICallActionInput GetBluePrintWarumWirdDasDicNichtOrdentlichSerializedwtf(Type type)
        {
            return callActionMethods.Find(x => x.Item1.IsAssignableFrom(type)).Item2;
        }
    }
}