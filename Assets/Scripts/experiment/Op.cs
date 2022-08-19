using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Cursor;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;
using MyScripts.Interactable.File;
namespace Op
{
    public class Op : FileOn
    {
        public GameObject gameo;
        public GameObject gameo2;
        void Update()
        {
            Debug.Log("State machine :" + cursorStateMachine.GetState().ToString());

            if (stayState)
            {
                if (!flag)
                    OnCursorHover(true);
                if (stayState && cursorStateMachine.GetState() == ECursorState.DoubleClick_CommandAwait)
                {
                    gameo.SetActive(true);
                    gameo2.SetActive(true);
                    cursorStateMachine.TrySwitchToState(ECursorState.DoubleClick);
                }
                if (cursorStateMachine.GetState() == ECursorState.Click_CommandAwait)
                {
                    //Reply to the ClickCommand and restore to unCommanded.
                    OnCursorClick();
                    cursorStateMachine.TrySwitchToState(ECursorState.Click);
                }
            }
            else if (flag == false)
            {
                OnCursorHover(false);
            }
        }
    }
}