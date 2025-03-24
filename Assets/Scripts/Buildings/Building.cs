using System;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [InspectorButton("LevelUpDebug")]
    public bool LevelUpButton;

    [Space(20)] 
    
    [SerializeField] private int numberOfLevels = 1;
    
    public Action<int> OnLevelUp;

    public int Level => _level;

    protected int _level;
    
    protected void LevelUp(int amount)
    {   
        _level += amount;
        OnLevelUp?.Invoke(amount);
    }
    
    protected void LevelUpDebug()
    {
        LevelUp(numberOfLevels);
    }
    
}
