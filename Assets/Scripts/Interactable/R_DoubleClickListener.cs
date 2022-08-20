using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable
{
    public class R_DoubleClickListener : MonoBehaviour
    {
        private IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        [HideInInspector]
        public bool stayState;
        /// <summary>
        /// This member will be add in awake
        /// </summary>
        public UnityEvent R_DoubleClickEvent = new();

        private void Awake()
        {
            IR_CursorDoubleClickable sameLevelScript = gameObject.GetComponent<IR_CursorDoubleClickable>();
            if (sameLevelScript == null) Debug.LogError("Missing Script: IR_CursorDoubleClickable Script");
            R_DoubleClickEvent.AddListener(sameLevelScript.OnR_DoubleClick);
            R_DoubleClickEvent.AddListener(() => { cursorStateMachine.TrySwitchToState(ECursorState.R_DoubleClick); });
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == CursorManager.standardCursorTag)
                stayState = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == CursorManager.standardCursorTag)
                stayState = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (stayState && cursorStateMachine.GetState() == ECursorState.R_DoubleClick_CommandAwait)
            {
                R_DoubleClickEvent.Invoke();
                return;
            }
        }
    }
}