using System;
using System.Collections.Generic;
using Production.Items;
using Unity.VisualScripting;

namespace Production.Storage
{
    public interface ICustomer
    {
        public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> OnTryPurchase;
    }

    public class PurchaseArgs
    {
        public bool IsValid;
    }
}