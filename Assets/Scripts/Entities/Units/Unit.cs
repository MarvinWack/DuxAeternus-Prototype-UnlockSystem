using UnityEngine;

namespace Entities.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private int amount;
        public int Amount
        {
            get => amount;
            private set 
            {
                amount = value;
                // OnMessageSent?.Invoke(amount.ToString());
            }
        }
    
        public void Setup(int value = 0)
        {
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
