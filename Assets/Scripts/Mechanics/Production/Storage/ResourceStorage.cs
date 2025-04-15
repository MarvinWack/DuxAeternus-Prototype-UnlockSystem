using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    public class ResourceStorage : MonoBehaviour
    {
        [SerializeField] SerializedDictionary<ResourceBlueprint, int> resources = new ();

        public event Action<Dictionary<ResourceBlueprint, int>> OnResourceAmountChanged;

        private void Awake()
        {
            LoadResourceBlueprints();
        }

        private void LoadResourceBlueprints()
        {
            var resourceBlueprints = Resources.LoadAll("ScriptableObjects/Products/Resources", typeof(ResourceBlueprint));

            foreach (var blueprint in resourceBlueprints)
            {
                resources.Add((ResourceBlueprint)blueprint, 0);
            }
        }

        //todo: remove checks once buildings are distuingished by produced product
        public void HandleProductionTick(ProductBlueprint type, int value)
        {
            if(type is ResourceBlueprint resourceType)
            {
                resources[resourceType] += value;
                OnResourceAmountChanged?.Invoke(resources);
            }
            else
            {
                Debug.LogWarning($"Received production tick for {type.name} which is not a ResourceBlueprint");
            }
        }

        public bool CheckIfEnoughResourcesAvailable(ResourceBlueprint resource, int amount)
        {
            return resources[resource] >= amount;
        }

        public void RemoveResources(ResourceBlueprint resource, int amount)
        {
            resources[resource] -= amount;
            OnResourceAmountChanged?.Invoke(resources);
        }

        public List<ResourceBlueprint> GetResourceTypes()
        {
            return resources.Keys.ToList();
        }
    }
}