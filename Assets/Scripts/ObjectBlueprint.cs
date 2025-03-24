using UnityEngine;

public abstract class ObjectBlueprint
{
    [SerializeField] private IData _objectData;
    
    private ulong _playerID;
    private bool _isAvailableToPlayer;
}
