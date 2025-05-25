namespace Core
{
    public interface IProductionTickReceiver : ITickReceiver
    {
        public void ProductionTickHandler(); 
    }
}