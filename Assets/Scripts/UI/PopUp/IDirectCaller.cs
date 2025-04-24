using System;
using UI;

namespace Entities.Buildings
{
    public interface IDirectCaller
    {
        public event Action<string> OnLabelChanged;
        public void HandleSlotClicked();
    }
}