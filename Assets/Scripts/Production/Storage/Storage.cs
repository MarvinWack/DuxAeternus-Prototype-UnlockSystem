using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Production.Storage
{
    public enum ResourceType
    {
        Wood,
        Gold
    }
    public class Storage : MonoBehaviour
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
    }
}