using UnityEngine;
using UnityEngine.Events;

using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable
{
    public class DoubleClickListener : MonoBehaviour
    {
        private IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        [HideInInspector]
        public bool stayState;
        /// <summary>
        /// This member will be add in awake
        /// </summary>
        public UnityEvent DoubleClickEvent = new();

        private void Awake()
        {
            ICursorDoubleClickable sameLevelScript = gameObject.GetComponent<ICursorDoubleClickable>();
            if (sameLevelScript == null) Debug.LogError("Missing Script: ICursorDoubleClickable Script");
            DoubleClickEvent.AddListener(sameLevelScript.OnDoubleClick);
            DoubleClickEvent.AddListener(() => { cursorStateMachine.TrySwitchToState(ECursorState.DoubleClick); });
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
            if (stayState && cursorStateMachine.GetState() == ECursorState.DoubleClick_CommandAwait)
            {
                DoubleClickEvent.Invoke();
                return;
            }
        }
    }
}
