using UnityEngine;

namespace UI.Slot
{
    public class NewResearchSlot : MonoBehaviour
    {
        [SerializeField] private ExtendedButton extendedButton;
        [SerializeField] private ExtendedText extendedText;
        
        private Tech _tech;
        
        public void Setup(Tech tech, int i)
        {
            _tech = tech;
            _tech.OnUpgradeProgress += OnProgress;
            _tech.OnUpgrade += HandleUpgrade;

            extendedButton.OnClick += HandleClick;
        }

        private void HandleClick(Vector3 arg1, bool arg2, int arg3)
        {
            _tech.StartUpgrade();
        }

        private void HandleUpgrade(int level)
        {
            extendedButton.SetText(_tech.name + " " + level);
        }

        private void OnProgress(float progress)
        {
            extendedButton.SetFillAmount(progress);
        }

    }
}