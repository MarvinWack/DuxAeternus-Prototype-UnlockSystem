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
                    building.OnTryPurchase += resourceStorage.HandleTryToPurchase;
                    break;
                case ResourceBlueprint:
                    building.OnProduction += resourceStorage.HandleProductionTick;
                    building.OnTryPurchase += resourceStorage.HandleTryToPurchase;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }

        }
    }
}