using UnityEngine;
using UnityEngine.Events;

using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable
{
    public class CursorHoverListener : MonoBehaviour
    {
        private IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        [HideInInspector]
        public bool stayState;
        /// <summary>
        /// This member will be add in awake
        /// </summary>
        public UnityEvent HoverEvent = new();
        public UnityEvent NotHoverEvent = new();

        private void Awake()
        {
            ICursorHoverable sameLevelScript = gameObject.GetComponent<ICursorHoverable>();
            if (sameLevelScript == null) Debug.LogError("Missing Script: ICursorHoverable Script");
            HoverEvent.AddListener(sameLevelScript.OnCursorHover);
            NotHoverEvent.AddListener(sameLevelScript.OnCursorNotHover);
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
            if (stayState)
            {
                HoverEvent.Invoke();
            }
            else
            {
                NotHoverEvent.Invoke();
            }    
        }
    }
}

