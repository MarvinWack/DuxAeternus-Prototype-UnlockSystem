using System;

namespace UI
{
    /// <summary>
    /// Triggers UIBehaviourModifiedTick which lets instances check their requirements for different
    /// actions and update the UI accordingly.
    /// </summary>
    public interface IUIBehaviourModifier
    {
        public event Action OnModifierValueUpdated;
    }
}