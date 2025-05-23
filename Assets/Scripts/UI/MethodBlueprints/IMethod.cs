using UI.Slot;

namespace UI.MethodBlueprints
{
    public interface IMethod
    {
        public string GetName();
        public ExtendedButton InstantiateButton(IMethodProvider methodProvider, string text = null);
        
        // public ExtendedButton GetButton(IMethodProvider methodProvider);
    }
}