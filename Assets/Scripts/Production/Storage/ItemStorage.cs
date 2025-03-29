using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;
using UnityEngine.Serialization;

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
            Debug.Log($"Received production tick for {itemType.name} + amount: {value}");
            var itemBlueprint = itemType as ItemBlueprint;
            
            if (itemBlueprint != null)
            {
                Items[itemBlueprint] += value;
            }
            else
            {
                Debug.LogWarning($"Received production tick for {itemType.name} which is not an ItemBlueprint");
            }
        }

        public void HandleTryToPurchase(Dictionary<ItemBlueprint, int> cost, PurchaseArgs purchaseArgs)
        {
            foreach (var amount in cost)
            {
                if (Items[amount.Key] < amount.Value)
                    return;
                
                purchaseArgs.IsValid = true;
            }
            
            foreach (var amount in cost) 
                Items[amount.Key] -= amount.Value;
        }
    }
}