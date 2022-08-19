using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Logics.Message;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;
using MS.F;

namespace MS.T
{
    public class ColorfulFile_T : SimpleMessageSender<ColorfulFile_uniqueData>
    {
        ColorfulFile_uniqueData data = new ColorfulFile_uniqueData(true);
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
                    SendMessageToCortex(data);
                    cursorStateMachine.TrySwitchToState(ECursorState.DoubleClick);
                }
            }

        }
    }

    
}