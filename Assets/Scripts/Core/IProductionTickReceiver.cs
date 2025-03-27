namespace Core
{
    public interface IProductionTickReceiver : ITickReceiver
    {
        public abstract void ProductionTickHandler(); 
    }
}