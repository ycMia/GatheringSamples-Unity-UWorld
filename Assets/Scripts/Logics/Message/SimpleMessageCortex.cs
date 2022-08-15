using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyScripts.Logics.Message
{
    public class SimpleMessageCortexDefault<T> : MonoBehaviour, ISimpleMessageReceiver<T>
    {
        private static SimpleMessageCortexDefault<T> _Instance;
        public static SimpleMessageCortexDefault<T> Instance
        {
            get
            {
                if(_Instance == null)
                {
                    GameObject obj = new GameObject("SimpleMessageReceiver_Default");
                    obj.AddComponent<SimpleMessageCortexDefault<T>>();
                    _Instance = obj.GetComponent<SimpleMessageCortexDefault<T>>();
                    DontDestroyOnLoad(obj);
                }
                return _Instance;
            }
        }

        private List<SimpleMessage<T>> data = new();
        public List<SimpleMessage<T>> GetMsgReceiver()
        {
            return data;
        }

        public List<SimpleMessage<T>> MsgData()
        {
            return GetMsgReceiver();
        }
    }

    public abstract class SimpleMessageCortex_MonoBehaviour<T> : MonoBehaviour, ISimpleMessageReceiver<T>
    {
        protected List<SimpleMessage<T>> data = new();
        public List<SimpleMessage<T>> GetMsgReceiver()
        {
            return data;
        }

        public List<SimpleMessage<T>> MsgData()
        {
            return GetMsgReceiver();
        }
    }
}
