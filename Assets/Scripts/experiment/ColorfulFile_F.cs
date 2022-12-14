using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Logics.Message;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MS.F
{
    public class ColorfulFile_F : SimpleMessageSender<ColorfulFile_uniqueData>
    {
        ColorfulFile_uniqueData data = new ColorfulFile_uniqueData(false);
        public IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        public bool StayState = false;
        public void OnTriggerEnter2D(Collider2D trigger)
        {
            print("CollisionEnter");
            if (trigger.gameObject.tag == CursorManager.standardCursorTag) StayState = true;
        }
        public void OnTriggerExit2D(Collider2D trigger)
        {
            print("CollisionExit");
            if (trigger.gameObject.tag == CursorManager.standardCursorTag) StayState = false;
        }
        private void Update()
        {
            if (StayState)
            {
                if (cursorStateMachine.GetState() == ECursorState.DoubleClick_CommandAwait)
                {
                    //SimpleMessageCortexDefault<ColorfulFile_F>.Instance.MsgData().Remove(SimpleMessageCortexDefault<ColorfulFile_F>.Instance.MsgData().ToArray()[0]);
                    //SimpleMessageCortexDefault<ColorfulFile_F>.Instance.MsgData().ToArray()[0];
                    //foreach (SimpleMessage<ColorfulFile_F> cfsm in SimpleMessageCortexDefault<ColorfulFile_F>.Instance.MsgData())
                    //{
                    //    Debug.Log(cfsm.info.data);
                    //}
                    SendMessageToCortex(data);
                    cursorStateMachine.TrySwitchToState(ECursorState.DoubleClick);
                }
            }

        }

        
    }
    public class ColorfulFile_uniqueData
    {
        public bool result;
        public ColorfulFile_uniqueData(bool a)
        {
            result = a;
        }
    }

}