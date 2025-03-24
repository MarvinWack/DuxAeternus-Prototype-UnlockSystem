using UnityEngine;
public abstract class Updatable : MonoBehaviour
{
    protected abstract void HandleTick();

    private void OnEnable()
    {
        Updater.Instance.Tick += HandleTick;
    }
   
    private void OnDisable()
    {
        Updater.Instance.Tick -= HandleTick;
    }
}