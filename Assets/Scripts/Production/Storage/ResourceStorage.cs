using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    public class ResourceStorage : MonoBehaviour
    {
        [SerializeField] SerializedDictionary<ResourceType, int> resources = new ();

        private void Awake()
        {
            foreach(var type in Enum.GetValues(typeof(ResourceType)))
                resources.Add((ResourceType)type, 0);
        }

        //todo: remove checks once buildings are distuingished by produced product
        public void HandleProductionTick(ProductBlueprint type, int value)
        {
            var resourceType = type as ResourceBlueprint;
            if (resourceType != null)
            {
                resources[resourceType.ResourceType] += value;
            }
            else
            {
                Debug.LogWarning($"Received production tick for {type.name} which is not a ResourceBlueprint");
            }
        }

        public void HandleTryToPurchase(Dictionary<ResourceType, int> cost, PurchaseArgs purchaseArgs)
        {
            foreach (var amount in cost)
            {
                if (resources[amount.Key] < amount.Value)
                    return;
                
                purchaseArgs.IsValid = true;
            }
            
            foreach (var amount in cost) 
                resources[amount.Key] -= amount.Value;
        }
    }
}