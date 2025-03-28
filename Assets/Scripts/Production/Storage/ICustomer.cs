using System;
using System.Collections.Generic;
using Production.Items;

namespace Production.Storage
{
    public interface ICustomer
    {
        public event Action<Dictionary<ResourceType, int>, PurchaseArgs> OnTryPurchase;
    }

    public class PurchaseArgs
    {
        public bool IsValid;
    }
}