using System.Collections.Generic;

namespace UI.MethodBlueprints
{
    public interface IMethodProvider
    {
        public List<IMethod> GetMethods();
        public string GetName();

        public bool DoesBelongToPlayer();

        // public bool BelongsToPlayer { get; }
    }
}