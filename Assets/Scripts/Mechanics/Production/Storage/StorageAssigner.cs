using System;
using System.Collections.Generic;
using Production.Items;
using UI;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Production.Storage
{
    public class StorageAssigner : MonoBehaviour
    {
        [SerializeField] private ItemStorage itemStorage;
        [SerializeField] private ResourceStorage resourceStorage;

        public void AssignToStorage(ICustomer customer, ObjectBluePrint bluePrint = null)
        {
            switch (customer)
            {
                case Building building: AssignBuildingToStorage(building, (BuildingBlueprint)bluePrint);
                    break;
                case TroopType troopType: AssignTroopTypeToStorage(troopType);
                    break;
                default: throw new NotImplementedException("Customer type not implemented");
            }
        }

        private void AssignTroopTypeToStorage(TroopType troopType)
        {
            troopType.OnTryPurchase += HandleTryPurchase;
            troopType.CheckIfPurchaseValid += CheckIfPurchaseValid;
        }

        private void AssignBuildingToStorage(Building building, BuildingBlueprint buildingBlueprint)
        {
            switch (buildingBlueprint.ProducedProduct)
            {
                case ItemBlueprint:
                    building.OnProduction += itemStorage.HandleProductionTick;
                    building.OnTryPurchase += HandleTryPurchase;
                    building.CheckIfPurchaseValid += CheckIfPurchaseValid;
                    break;
                case ResourceBlueprint:
                    building.OnProduction += resourceStorage.AddResources;
                    building.OnTryPurchase += HandleTryPurchase;
                    building.CheckIfPurchaseValid += CheckIfPurchaseValid;
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

        private void CheckIfPurchaseValid(Dictionary<ProductBlueprint, int> price, PurchaseArgs args)
        {
            args.IsValid = CheckIfPurchaseValid(price);
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