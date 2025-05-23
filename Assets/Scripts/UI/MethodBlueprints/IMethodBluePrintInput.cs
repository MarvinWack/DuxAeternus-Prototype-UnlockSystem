using System;

namespace UI.MethodBlueprints
{
    public interface IMethodBluePrintInput<T>
    {
        public void RegisterMethodToCall(T handler, IMethodProvider methodProvider);

        public void RegisterMethodEnableChecker(Func<bool> enableChecker);
    }
}