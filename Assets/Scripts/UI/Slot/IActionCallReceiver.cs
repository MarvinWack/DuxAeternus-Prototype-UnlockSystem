using System.Collections.Generic;

namespace UI.Slot
{
    public interface IActionCallReceiver
    {
        public delegate void ActionDelegate();
        public void SetupActions(List<ActionDelegate> actions);
        public void HandleActionCall(int index);
    }
}