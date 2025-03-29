using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private TroopType manager;
    [SerializeField] private int amount;
    
    public int Amount => amount;
    
    public void Setup(TroopType troopType, int amount = 0)
    {
        manager = troopType;
        this.amount = amount;
    }
    
    public void AddAmount(int amount)
    {
        this.amount += amount;
    }
    
    public void RemoveAmount(int amount)
    {
        this.amount -= amount;
    }
}
