namespace UI
{
    public interface IParameterButton<T>
    {
        public void SetParameter(T parameter);
        public T GetParameter();
    }
}