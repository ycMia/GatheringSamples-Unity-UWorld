using System;
using System.Collections.Generic;

namespace MyScripts.Logics.StateMachine
{
    public interface IStateMachine<T>
    {
        void TrySwitchToState(T status);
        T GetState();
    }
}
