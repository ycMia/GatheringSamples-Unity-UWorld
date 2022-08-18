using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyScripts.Cursor;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable.File
{
    public class FileOn : MonoBehaviour, ICursorInteractable
    {
        private bool flag = false;
        public SpriteRenderer srHighlight;

        public IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine; //CursorStateMahcine MUST be a static one.

        public bool stayState = false;
        
        void Awake()
        {
            srHighlight.color = new Color(0, 0, 0, 0);
        }

        public void OnTriggerEnter2D(Collider2D trigger)
        {
            print("CollisionEnter");
            if(trigger.gameObject.tag == CursorManager.standardCursorTag) stayState = true;
        }

        public void OnTriggerExit2D(Collider2D trigger)
        {
            print("CollisionExit");
            if (trigger.gameObject.tag == CursorManager.standardCursorTag) stayState = false;
        }

        void Update()
        {
            //Debug.Log("State machine :" + cursorStateMachine.GetState().ToString());

            if (stayState)
            {
                if(flag == false)
                    OnCursorHover(true);
                if (cursorStateMachine.GetState() == ECursorState.Click_CommandAwait)
                {
                    //Reply to the ClickCommand and restore to unCommanded.
                    OnCursorClick();
                    cursorStateMachine.TrySwitchToState(ECursorState.Click);
                }
            }
            else if(flag == false)
            {
                OnCursorHover(false);
            }
        }
        
        public void OnCursorHover(bool show)
        {
            srHighlight.color = show ? new Color(0, 1, 1, 0.1f) : new Color(0, 1, 1, 0f);
        }
        
        public void OnCursorPress()
        {

            //[info]You should think if there's any difference between this and 'Click' behaviour. -ycMia
        }

        public void OnCursorClick()
        {
            if (!flag)
            {
                srHighlight.color = new Color(0, 0, 1, 0.7f);
                flag = true;
            }
            else
            {
                srHighlight.color = new Color(0, 0, 0, 0);
                flag = false;
            }
        }
    }
}
