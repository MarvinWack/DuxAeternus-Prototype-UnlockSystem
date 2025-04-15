using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private Image resourceIcon;
        [SerializeField] private TextMeshProUGUI amountText;

        public void Setup(Sprite icon, int amount)
        {
            resourceIcon.sprite = icon;
            amountText.text = amount.ToString();
        }
        
        public void UpdateUI(int amount)
        {
            amountText.text = amount.ToString();
        }
    }
}