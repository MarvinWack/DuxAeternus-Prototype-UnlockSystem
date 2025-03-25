using System;
using UnityEngine;

public class Updater : MonoBehaviour
{
    public event Action Tick;
    [SerializeField] private float ticksPerSecond = 1;
    [SerializeField] private int CurrentTick;

    private static Updater _instance;
    public static Updater Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindObjectOfType<Updater>();
            
            if (_instance == null)
            {
                return null;    
            }

            return _instance;
        }
    }

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= 1 / ticksPerSecond)
        {
            _timer = 0;
            Tick?.Invoke();

            CurrentTick++;
        }
    }
}