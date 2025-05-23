using System;

namespace UI.MethodBlueprints
{
    public interface IActionProvider : INEWMethodProvider
    {
        public void CallParameterAction();
        public bool CheckIfActionCallable();

        public Action GetMethod()
        {
            //todo: funktioniert das auch wenn eine Klasse mehrmals
            //CallParameterAction mit verschiedenen Typen implementiert?
            return CallParameterAction;
        }

        public Func<bool> GetConditionTester()
        {
            return CheckIfActionCallable;
        }
    }
}