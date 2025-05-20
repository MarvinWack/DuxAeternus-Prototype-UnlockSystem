using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    public abstract class GameplayInstance : SerializedMonoBehaviour
    {
        public bool BelongsToPlayer => _belongsToPlayer;
        
        [SerializeField] protected bool _belongsToPlayer;

        protected virtual void Setup(bool belongsToPlayer)
        {
            _belongsToPlayer = belongsToPlayer;
        }
    }
}