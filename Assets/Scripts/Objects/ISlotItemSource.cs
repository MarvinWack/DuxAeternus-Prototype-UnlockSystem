using System;
using System.Collections.Generic;

namespace Objects
{
    public interface ISlotItemSource
    {
        public List<ISlotItem> GetSlotItems(Type type);
    }
}