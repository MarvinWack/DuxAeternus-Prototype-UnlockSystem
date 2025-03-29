using System;
using System.Collections.Generic;
using Production.Items;
using UnityEngine;

namespace Production.Storage
{
    public class StorageAssigner : MonoBehaviour
    {
        [SerializeField] private ItemStorage itemStorage;
        [SerializeField] private ResourceStorage resourceStorage;
        
        public void AssignBuildingToStorage(Building building, BuildingBlueprint buildingBlueprint)
        {
            switch (buildingBlueprint.ProducedProduct)
            {
                case ItemBlueprint:
                    building.OnProduction += itemStorage.HandleProductionTick;
                    building.OnTryPurchase += HandleTryPurchase;
                    break;
                case ResourceBlueprint:
                    building.OnProduction += resourceStorage.HandleProductionTick;
                    building.OnTryPurchase += HandleTryPurchase;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleTryPurchase(Dictionary<ProductBlueprint, int> price, PurchaseArgs args)
        {
            if(!CheckIfPurchaseValid(price))
                return;
                
            RemoveResources(price);
            args.IsValid = true;
        }

        private bool CheckIfPurchaseValid(Dictionary<ProductBlueprint, int> price)
        {
            foreach (var productAmount in price)
            {
                bool validPurchase = productAmount.Key switch
                {
                    ItemBlueprint itemBlueprint => itemStorage.CheckIfEnoughItemsAvailable(itemBlueprint, productAmount.Value),
                    ResourceBlueprint resourceBlueprint => resourceStorage.CheckIfEnoughResourcesAvailable(resourceBlueprint, productAmount.Value),
                    _ => throw new Exception("Invalid product type")
                };

                if (!validPurchase) return false;
            }

            return true;
        }

        private void RemoveResources(Dictionary<ProductBlueprint, int> price)
        {
            foreach (var productAmount in price)
            {
                switch (productAmount.Key)
                {
                    case ItemBlueprint itemBlueprint: 
                        itemStorage.RemoveItems(itemBlueprint, productAmount.Value);
                        break;
                    case ResourceBlueprint resourceBlueprint:
                        resourceStorage.RemoveResources(resourceBlueprint, productAmount.Value);
                        break;
                    default: throw new Exception("Invalid product type");
                }
            }
        }
    }
}