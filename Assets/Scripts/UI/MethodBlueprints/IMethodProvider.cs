using System.Collections.Generic;

namespace UI.MethodBlueprints
{
    //useful for later when providers are created in factory?
    public interface IMethodProvider
    {
        public List<IMethod> GetMethods();
    }
}