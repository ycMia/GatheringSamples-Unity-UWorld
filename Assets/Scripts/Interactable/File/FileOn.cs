using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Cursor;

using MyScripts.CursorControl;
using MyScripts.CursorControl.State;

namespace MyScripts.Interactable.File
{
    public class FileOn : MonoBehaviour, ICursorInteractable
    {
        bool flag = false;
        public SpriteRenderer srHighlight;
        private BoxCollider2D _myCollider2D;
        public CursorManager cursorM;

        public bool stayState = false;
        
        void Awake()
        {
            srHighlight.color = new Color(0, 0, 0, 0);
            _myCollider2D = GetComponent<BoxCollider2D>();
        }

        public void OnTriggerEnter2D(Collider2D trigger)
        {
            print("CollisionEnter");
            if(trigger.gameObject == cursorM.cursorGO) stayState = true;
        }

        public void OnTriggerExit2D(Collider2D trigger)
        {
            print("CollisionExit");
            if (trigger.gameObject == cursorM.cursorGO) stayState = false;
        }

        void Update()
        {
            //Debug.Log("askForClickCount:" + cursorM.askForClickCount.ToString());

            if (stayState)
            {
                if (cursorM.stateMachine.GetState() == ECursorState.Click && cursorM.askForClickCount > 0)
                {
                    OnCursorClick();
                    cursorM.askForClickCount--;
                }
            }
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
