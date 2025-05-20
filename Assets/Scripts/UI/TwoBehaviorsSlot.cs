using System.Collections.Generic;
using UI.Slot;

namespace UI
{
    public abstract class TwoBehaviorsSlot : BaseSlot
    {
        protected virtual void SetSlot() //interface für obj die in slot reingetüdelt werden können
        {
            _isOptionSet = true;
        }
        // public override bool HandleOptionClicked(int index)
        // {
        //     if (!_isOptionSet)
        //         return NoOptionSetBehavior(index);
        //     else
        //         return OptionSetBehavior(index);
        // }
        //
        // public override Dictionary<string, bool> GetDropDownOptions()
        // {
        //     return _isOptionSet ? GetOptionSetMenu() : GetOptionNotSetMenu();
        // }

        protected abstract Dictionary<string, bool> GetOptionNotSetMenu();

        protected abstract Dictionary<string, bool> GetOptionSetMenu();

        protected abstract bool NoOptionSetBehavior(int index);

        protected abstract bool OptionSetBehavior(int index);
    }
}