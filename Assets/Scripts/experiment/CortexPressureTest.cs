using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Logics.Message;

namespace MyScripts.Experiment
{
    public class DataInfo
    {
        public string coreData;
        public DataInfo(string cd)
        {
            coreData = cd;
        }
    }
    public class CortexPressureTest : MonoBehaviour, ISimpleMessageSender<DataInfo>
    {
        public void SendMessage(DataInfo message, List<SimpleMessage<DataInfo>> target)
        {
            SendMessageToCortex(message);
        }

        public void SendMessageToCortex(DataInfo message)
        {
            SimpleMessageCortexDefault<DataInfo>.Instance.GetMsgReceiver().Add(new SimpleMessage<DataInfo>(message));
        }

        private void Start()
        {
            SendMessageToCortex(new DataInfo("Testing Cortex in 1"));
        }
    }
}