using System;

namespace UI.MethodBlueprints
{
    public interface IParameterActionProvider<T> : INEWMethodProvider
    {
        public void CallParameterAction(T parameter);
        public bool CheckIfActionCallable(T parameter);

        public Action<T> GetMethod()
        {
            return CallParameterAction;
        }

        public Func<T, bool> GetConditionTester()
        {
            return CheckIfActionCallable;
        }
    }
}