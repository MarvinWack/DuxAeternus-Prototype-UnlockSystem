using System;

namespace UI.MethodBlueprints
{
    public interface INEWUpgradeMethodProvider : IActionProvider
    {
        void CallAction();
        bool CheckIfActionCallable();

        Action GetMethod()
        {
            //todo: funktioniert das auch wenn eine Klasse mehrmals
            //CallParameterAction mit verschiedenen Typen implementiert?
            return CallAction;
        }

        Func<bool> GetConditionTester()
        {
            return CheckIfActionCallable;
        }
    }
}