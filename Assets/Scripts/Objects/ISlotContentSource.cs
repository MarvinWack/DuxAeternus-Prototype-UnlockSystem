using System;
using System.Collections.Generic;

namespace Objects
{
    public interface ISlotContentSource
    {
        public List<ISlotContent> GetSlotItems(Type type);
        // public List<T> GetSlotItems<T>(T type);
    }
}