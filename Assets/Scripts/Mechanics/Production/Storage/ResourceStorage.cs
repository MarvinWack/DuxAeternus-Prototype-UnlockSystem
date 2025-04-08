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

        public event Action<Dictionary<ResourceType, int>> OnResourceAmountChanged; 
        
        private void Awake()
        {
            foreach(var type in Enum.GetValues(typeof(ResourceType)))
                resources.Add((ResourceType)type, 0);
        }

        //todo: remove checks once buildings are distuingished by produced product
        public void HandleProductionTick(ProductBlueprint type, int value)
        {
            if(type is ResourceBlueprint resourceType)
            {
                resources[resourceType.ResourceType] += value;
                OnResourceAmountChanged?.Invoke(resources);
            }
            else
            {
                Debug.LogWarning($"Received production tick for {type.name} which is not a ResourceBlueprint");
            }
        }

        public bool CheckIfEnoughResourcesAvailable(ResourceBlueprint resource, int amount)
        {
            return resources[resource.ResourceType] >= amount;
        }

        public void RemoveResources(ResourceBlueprint resource, int amount)
        {
            resources[resource.ResourceType] -= amount;
            OnResourceAmountChanged?.Invoke(resources);
        }
    }
}