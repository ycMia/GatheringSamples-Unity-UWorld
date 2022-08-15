using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyScripts.Logics.Message
{
    public class SimpleMessageCortexDefault : MonoBehaviour, ISimpleMessageReceiver
    {
        private static SimpleMessageCortexDefault _Instance;
        public static SimpleMessageCortexDefault Instance
        {
            get
            {
                if(_Instance == null)
                {
                    GameObject obj = new GameObject("SimpleMessageReceiver_Default");
                    obj.AddComponent<SimpleMessageCortexDefault>();
                    _Instance = obj.GetComponent<SimpleMessageCortexDefault>();
                    DontDestroyOnLoad(obj);
                }
                return _Instance;
            }
        }

        private List<SimpleMessage> data = new();
        public List<SimpleMessage> GetReceiver()
        {
            return data;
        }

        public List<SimpleMessage> Data()
        {
            return GetReceiver();
        }
    }
}
