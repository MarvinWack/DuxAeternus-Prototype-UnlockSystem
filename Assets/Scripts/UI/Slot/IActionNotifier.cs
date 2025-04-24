using System;
using System.Collections.Generic;

namespace UI.Slot
{
    public interface IActionNotifier
    {
        public event Action<int> OnActionCalled;
        // public void SetupActionButtons(int amount);
    }
}