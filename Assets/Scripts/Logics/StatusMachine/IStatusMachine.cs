using System;
using System.Collections.Generic;

namespace MyScripts.Logics.StateMachine
{
    internal interface IStateMachine<T>
    {
        void TrySwitchToState(T status);
        T GetState();
    }
}
