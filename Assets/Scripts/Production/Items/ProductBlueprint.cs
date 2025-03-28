using UnityEngine;

namespace Production.Items
{
    public enum RequiredProductionAmount
    {
        None = 0,
        Low = 2,
        Medium = 4,
        High = 6,
        VeryHigh = 8
    }
    public abstract class ProductBlueprint : ScriptableObject
    {
         public RequiredProductionAmount RequiredProductionAmount;
    }
}