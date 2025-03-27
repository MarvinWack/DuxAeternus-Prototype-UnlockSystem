namespace Core
{
    public interface IResearchTickReceiver : ITickReceiver
    {
        public abstract void ResearchTickHandler();
    }
}