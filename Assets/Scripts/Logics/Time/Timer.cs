using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

using MyScripts.Logics.Message;

namespace MyScripts.Logics.Time
{
    internal class Timer: MonoBehaviour, ISimpleMessageReceiver
    {
        public static Timer _Instance;

        public List<SimpleMessage> Data()
        {
            throw new NotImplementedException();
        }

        public List<SimpleMessage> GetReceiver()
        {
            throw new NotImplementedException();
        }
    }
}
