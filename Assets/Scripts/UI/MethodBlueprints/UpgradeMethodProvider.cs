namespace UI.MethodBlueprints
{
    public interface UpgradeMethodProvider
    {
        public void StartUpgrade();
        public bool CheckIfUpgradePossible();
    }
}