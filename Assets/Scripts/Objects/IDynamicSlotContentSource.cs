using System;

namespace Objects
{
    public interface IDynamicSlotContentSource : ISlotContentSource
    {
        public event Action<ISlotContent> SlotContentAdded;
    }
}