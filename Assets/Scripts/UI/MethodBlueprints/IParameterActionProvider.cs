using System;

namespace UI.MethodBlueprints
{
    public interface IParameterActionProvider<T> : INEWMethodProvider
    {
        public void CallParameterAction(T parameter);
        public bool CheckIfActionCallable(T parameter);

        public new Action<T> GetMethod()
        {
            //todo: funktioniert das auch wenn eine Klasse mehrmals
            //CallParameterAction mit verschiedenen Typen implementiert?
            return CallParameterAction;
        }

        public new Func<T, bool> GetConditionTester()
        {
            return CheckIfActionCallable;
        }
    }
}