using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private TroopType manager;
    [SerializeField] private int amount;
    
    public int Amount => amount;
    
    public void Setup(TroopType troopType, int value = 0)
    {
        manager = troopType;
        amount = value;
    }
    
    public void AddAmount(int value)
    {
        amount += value;
    }
    
    public void RemoveAmount(int value)
    {
        amount -= value;
        
        if(amount < 0)
            amount = 0;
    }
}
