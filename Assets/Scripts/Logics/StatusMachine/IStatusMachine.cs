using System;
using System.Collections.Generic;

namespace MyScripts.Logics.StatusMachine
{
    internal interface IStatusMachine<T>
    {
        void TrySwitchToStatus(T status);
        T GetStatus();
    }
}
