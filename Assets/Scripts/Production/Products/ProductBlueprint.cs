using System;
using UnityEngine;

namespace Production.Items
{
    public enum RequiredProductionAmount
    {
        None = 0,
        Low = 10,
        Medium = 20,
        High = 30,
        VeryHigh = 40
    }
    
    [Serializable]
    public abstract class ProductBlueprint : ScriptableObject
    {
         public RequiredProductionAmount RequiredProductionAmount;
    }
}