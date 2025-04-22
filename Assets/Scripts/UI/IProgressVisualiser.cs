using System;

namespace UI
{
    public interface IProgressVisualiser
    {
        //todo: stattdessen Methode die event direkt handled
        public event Action<float> OnUpgradeProgress; 
    }
}