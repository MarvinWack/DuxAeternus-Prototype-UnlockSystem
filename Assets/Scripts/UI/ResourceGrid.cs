using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Production.Items;
using Production.Storage;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ResourceGrid : MonoBehaviour
    {
        private readonly SerializedDictionary<ResourceBlueprint, Resource> resourceTypes = new();
        [SerializeField] private ResourceStorage resourceStorage;
        [SerializeField] private GameObject resourcePrefab;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            var types = resourceStorage.GetResourceTypes();

            foreach (var type in types)
            {
                var instance = Instantiate(resourcePrefab, transform);
                instance.name = type.name;
                var resource = instance.GetComponent<Resource>();
                resource.Setup(type.Icon, 0);
                resourceStorage.OnResourceAmountChanged += UpdateUI;
                resourceTypes.Add(type, resource);
            }
        }

        private void UpdateUI(Dictionary<ResourceBlueprint, int> amountPerResource)
        {
            foreach (var amount in amountPerResource)
            {
                resourceTypes[amount.Key].UpdateUI(amount.Value);
            }
        }
    }
}