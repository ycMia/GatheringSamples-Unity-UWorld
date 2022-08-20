using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable.File
{
    public class File_Close : MonoBehaviour
    {
        public GameObject F_Close_2;
        public GameObject F_Close;
        public IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        public bool stayState = false;

        public void OnTriggerEnter2D(Collider2D trigger)
        {
            print("CollisionEnter");
            if (trigger.gameObject.tag == CursorManager.standardCursorTag) stayState = true;
        }

        public void OnTriggerExit2D(Collider2D trigger)
        {
            print("CollisionExit");
            if (trigger.gameObject.tag == CursorManager.standardCursorTag) stayState = false;
        }
        private void Update()
        {
            if (stayState)
            {
                if (cursorStateMachine.GetState() == ECursorState.Click_CommandAwait)
                {
                    F_Close.SetActive(false);
                }
            }
        }
    }
}