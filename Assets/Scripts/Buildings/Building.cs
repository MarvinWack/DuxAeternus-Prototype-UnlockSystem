using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [InspectorButton("LevelUpDebug")]
    public bool LevelUpButton;

    [Space(20)] 
    
    [SerializeField] private int numberOfLevelsToAdd = 1;
    
    public Action<int> OnLevelUp;

    public int Level => _level;

    public int _level; //todo: make private
    
    private void LevelUp(int amount)
    {   
        _level += amount;
        OnLevelUp?.Invoke(_level);
    }
    
    protected void LevelUpDebug()
    {
        LevelUp(numberOfLevelsToAdd);
    }
    
}
