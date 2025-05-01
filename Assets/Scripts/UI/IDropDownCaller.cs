using System.Collections.Generic;
using UI;

namespace Entities.Buildings
{
    public interface IDropdownCaller : IPopUpCaller
    {
        public delegate void OptionSetHandler(string option);
        event OptionSetHandler OptionSet;
        
        public Dictionary<string, bool> GetDropDownOptions();
        public bool HandleOptionClicked(int index);
    }
}