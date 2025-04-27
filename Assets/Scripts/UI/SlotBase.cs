using Objects;
using UnityEngine;

namespace UI
{
    public class SlotBase : MonoBehaviour
    {
        public SlotButton SlotButton => _slotButton;
        
        [SerializeField] private SlotButton _slotButton;
        
        public void Setup(SlotButton slotButton)
        {
            _slotButton = slotButton;
        }
    }
}