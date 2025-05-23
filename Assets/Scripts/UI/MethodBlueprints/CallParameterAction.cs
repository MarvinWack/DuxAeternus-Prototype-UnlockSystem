using System;
using System.Collections.Generic;
using UI.Slot;

namespace UI.MethodBlueprints
{
    public abstract class CallParameterAction<T> : MethodBlueprint<Action<T>>
    {
        protected readonly Dictionary<ExtendedButton, T> parameters = new();
        
        //todo: Selectitem and Recruit both have special implementations of this but method gets called in base class
        protected override void SubscribeButtonToUpdateEvents(IMethodProvider methodProvider, ExtendedButton button) { }

        protected ExtendedButton InstantiateParameterButton(IMethodProvider methodProvider, T parameter, string text = null)
        {
            var button = Instantiate(buttonPrefab);
            
            parameters.Add(button, parameter);
            
            return SetupButton(button, methodProvider, text);
        }

        protected override void SubscribeProviderToButtonEvent(IMethodProvider methodProvider, ExtendedButton button)
        {
            button.OnClickNoParamsTest += () => GetMethod(methodProvider).Invoke(parameters[button]);
        }
    }
}