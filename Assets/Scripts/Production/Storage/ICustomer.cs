using System;
using System.Collections.Generic;
using Production.Items;
using Unity.VisualScripting;

namespace Production.Storage
{
    public interface ICustomer
    {
        public event Action<Dictionary<ResourceType, int>, PurchaseArgs> OnTryPurchase;
    }

    public class PurchaseArgs
    {
        public PurchaseArgs(int level) => Level = level;

        public bool IsValid;
        public readonly int Level;
    }
}