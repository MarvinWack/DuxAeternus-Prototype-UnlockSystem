using System;
using UI;
using UnityEngine;

namespace Entities.Units
{
    public class Unit : MonoBehaviour, IMessageSender
    {
        public event Action<string> OnMessageSent;
    
        [SerializeField] private TroopType manager;

        private int amount;
        public int Amount
        {
            get => amount;
            private set 
            {
                amount = value;
                OnMessageSent?.Invoke(amount.ToString());
            }
        }
    
        public void Setup(TroopType troopType, int value = 0)
        {
            manager = troopType;
            Amount = value;
        }
    
        public void AddAmount(int value)
        {
            Amount += value;
        }
    
        public void RemoveAmount(int value)
        {
            Amount -= value;
        
            if(Amount < 0)
                Amount = 0;
        }
    }
}
