using UnityEngine;
using UnityEngine.Events;

using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable
{
    public class SingleClickListener : MonoBehaviour
    {
        private IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        [HideInInspector]
        public bool stayState;
        /// <summary>
        /// This member will be add in awake
        /// </summary>
        public UnityEvent SingleClickEvent = new();

        private void Awake()
        {
            ICursorSingleClickable sameLevelScript = gameObject.GetComponent<ICursorSingleClickable>();
            if (sameLevelScript == null) Debug.LogError("Missing Script: ICursorDoubleClickable Script");
            SingleClickEvent.AddListener(sameLevelScript.OnSingleClick);
            SingleClickEvent.AddListener(() => { cursorStateMachine.TrySwitchToState(ECursorState.Click); });
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
            if (stayState && cursorStateMachine.GetState() == ECursorState.Click_CommandAwait)
            {
                SingleClickEvent.Invoke();
                return;
            }
        }
    }
}
