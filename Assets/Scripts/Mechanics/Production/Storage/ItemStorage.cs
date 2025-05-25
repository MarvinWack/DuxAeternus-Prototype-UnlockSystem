using AYellowpaper.SerializedCollections;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    public class ItemStorage : MonoBehaviour
    {
        [SerializeField] SerializedDictionary<ItemBlueprint, int> items = new ();

        private void Awake()
        {
            LoadItemBlueprints();
        }

        private void LoadItemBlueprints()
        {
            var itemBlueprints = Resources.LoadAll("ScriptableObjects/Products/Items", typeof(ItemBlueprint));

            foreach (var blueprint in itemBlueprints)
            {
                items.Add((ItemBlueprint)blueprint, 0);
            }
        }
        
        public void HandleProductionTick(ProductBlueprint itemType, int value)
        {
            if (itemType is ItemBlueprint itemBlueprint)
            {
                items[itemBlueprint] += value;
            }
            else
            {
                Debug.LogWarning($"Received production tick for {itemType.name} which is not an ItemBlueprint");
            }
        }

        public bool CheckIfEnoughItemsAvailable(ItemBlueprint item, int amount)
        {
            return items[item] >= amount;
        }

        public void RemoveItems(ItemBlueprint item, int amount)
        {
            items[item] -= amount;
        }
    }
}