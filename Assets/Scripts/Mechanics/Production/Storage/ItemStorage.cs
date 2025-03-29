using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    public class ItemStorage : MonoBehaviour
    {
        [SerializeField] SerializedDictionary<ItemBlueprint, int> Items = new ();

        private void Awake()
        {
            LoadItemBlueprints();
        }

        private void LoadItemBlueprints()
        {
            var itemBlueprints = Resources.LoadAll("ScriptableObjects/Products/Items", typeof(ItemBlueprint));

            foreach (var blueprint in itemBlueprints)
            {
                Debug.Log(blueprint.name);
                Items.Add((ItemBlueprint)blueprint, 0);
            }
        }

        //todo: remove checks once buildings are distuingished by produced product
        public void HandleProductionTick(ProductBlueprint itemType, int value)
        {
            if (itemType is ItemBlueprint itemBlueprint)
            {
                Items[itemBlueprint] += value;
            }
            else
            {
                Debug.LogWarning($"Received production tick for {itemType.name} which is not an ItemBlueprint");
            }
        }

        public bool CheckIfEnoughItemsAvailable(ItemBlueprint item, int amount)
        {
            return Items[item] >= amount;
        }

        public void RemoveItems(ItemBlueprint item, int amount)
        {
            Items[item] -= amount;
        }
    }
}