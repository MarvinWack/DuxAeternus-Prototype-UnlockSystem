using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private TroopType manager;
    [SerializeField] private int amount;
    
    public void Setup(TroopType troopType, int amount = 0)
    {
        manager = troopType;
        this.amount = amount;
    }
}
