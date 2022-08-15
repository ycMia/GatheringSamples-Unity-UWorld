using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyScripts.Logics.Message
{
    public interface ISimpleMessageSender
    {
        void SendMessageToCortex(string message);
        void SendMessage(string message, List<SimpleMessage> target);
    }
    public abstract class SimpleMessageSender : MonoBehaviour, ISimpleMessageSender
    {
        public virtual void SendMessageToCortex(string message)
        {
            SendMessage(message,SimpleMessageCortexDefault.Instance.GetReceiver());
        }

        public virtual void SendMessage(string message, List<SimpleMessage> target)
        {
            target.Add(new SimpleMessage(message));
        }
    }
}
