using System;
using System.Collections.Generic;

namespace UI
{
    public interface ICallableByUI
    {
        public event Action<Dictionary<Func<bool>, bool>> OnCallableMethodsChanged;
    }
}