namespace Core
{
    public interface IUpgradeTickReceiver : ITickReceiver
    {
        public abstract void UpgradeTickHandler();
    }
}