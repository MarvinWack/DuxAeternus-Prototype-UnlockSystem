namespace UI
{
    public interface IParameterButton<T> : IParamTest
    {
        public void SetParameter(T parameter);
        public T GetParameter();
    }

    public interface IParamTest
    {
    }
}