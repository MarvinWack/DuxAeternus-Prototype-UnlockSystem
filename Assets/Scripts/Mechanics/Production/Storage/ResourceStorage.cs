using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Production.Items;
using UI;
using UnityEngine;

namespace Production.Storage
{
    public class ResourceStorage : MonoBehaviour, IUIBehaviourModifier
    {
        public event Action OnModifierValueUpdated;
        public event Action<Dictionary<ResourceBlueprint, int>> OnResourceAmountChanged;
        
        [SerializeField] SerializedDictionary<ResourceBlueprint, int> resources = new();

        private void Awake()
        {
            LoadResourceBlueprints();
        }

        private void Start()
        {
            OnModifierValueUpdated += UIUpdater.UIBehaviourModifiedTick;
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

        public void AddResources(ProductBlueprint type, int value)
        {
            if(type is ResourceBlueprint resourceType)
            {
                resources[resourceType] += value;
                OnResourceAmountChanged?.Invoke(resources);
                OnModifierValueUpdated?.Invoke();
            }
            // else
            // {
            //     Debug.LogWarning($"Received production tick for {type.name} which is not a ResourceBlueprint");
            // }
        }

        public bool CheckIfEnoughResourcesAvailable(ResourceBlueprint resource, int amount)
        {
            return resources[resource] >= amount;
        }

        public void RemoveResources(ResourceBlueprint resource, int amount)
        {
            resources[resource] -= amount;
            OnResourceAmountChanged?.Invoke(resources);
            OnModifierValueUpdated?.Invoke();
        }

        public List<ResourceBlueprint> GetResourceTypes()
        {
            return resources.Keys.ToList();
        }
    }
}