using Objects;
using UnityEngine;

namespace UI
{
    public abstract class SlotBase : MonoBehaviour
    {
        private ISlotContentSource source;

        public abstract void Setup();
    }
}