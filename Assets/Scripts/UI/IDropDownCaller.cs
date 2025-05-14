using System;
using System.Collections.Generic;
using UI;
using UI.MethodBlueprints;

namespace Entities.Buildings
{
    public interface IDropdownCaller : IPopUpCaller
    {
        public List<IMethodProvider> GetDropDownOptions(Type type);
    }
}