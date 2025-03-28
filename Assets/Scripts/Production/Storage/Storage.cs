using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    public class Storage : MonoBehaviour //ResourceStorage
    {
        [SerializeField] SerializedDictionary<ResourceType, int> resources = new ();

        private void Awake()
        {
            foreach(var type in Enum.GetValues(typeof(ResourceType)))
                resources.Add((ResourceType)type, 0);
        }

        public void HandleProductionTick(ResourceType type, int value)
        {
            resources[type] += value;
        }

        public bool RemoveResources(Dictionary<ResourceType, int> resources)
        {
            return false;
        }
    }
}