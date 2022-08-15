using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyScripts.Logics.Message
{
    public interface ISimpleMessageSender<T>
    {
        void SendMessageToCortex(T message);
        void SendMessage(T message, List<SimpleMessage<T>> target);
    }
    public abstract class SimpleMessageSender<T> : MonoBehaviour, ISimpleMessageSender<T>
    {
        public virtual void SendMessageToCortex(T message)
        {
            SendMessage(message,SimpleMessageCortexDefault<T>.Instance.GetMsgReceiver());
        }

        public virtual void SendMessage(T message, List<SimpleMessage<T>> target)
        {
            target.Add(new SimpleMessage<T>(message));
        }
    }
}
