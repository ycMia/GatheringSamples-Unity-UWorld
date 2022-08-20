using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;


namespace MyScripts.Interactable
{
    public class R_SingleClickListener : MonoBehaviour
    {
        private IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        [HideInInspector]
        public bool stayState;
        /// <summary>
        /// This member will be add in awake
        /// </summary>
        public UnityEvent R_SingleClickEvent = new();

        private void Awake()
        {
            IR_CursorSingleClickable sameLevelScript = gameObject.GetComponent<IR_CursorSingleClickable>();
            if (sameLevelScript == null) Debug.LogError("Missing Script: IR_CursorDoubleClickable Script");
            R_SingleClickEvent.AddListener(sameLevelScript.OnR_SingleClick);
            R_SingleClickEvent.AddListener(() => { cursorStateMachine.TrySwitchToState(ECursorState.R_Click); });
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
            if (stayState && cursorStateMachine.GetState() == ECursorState.R_Click_CommandAwait)
            {
                R_SingleClickEvent.Invoke();
                return;
            }
        }
    }
}
