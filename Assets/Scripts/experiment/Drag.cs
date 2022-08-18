using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Interactable.File;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;

using MyScripts.Logics.StateMachine;

namespace MyScripts.Experiment
{
    public class Drag : MonoBehaviour
    {
        //public CursorManager cursorMM;
        public IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        public Rigidbody2D rb2D;
        Vector2 MousePos;
        Vector2 Distance;
        public bool Stay = false;
        public void OnTriggerEnter2D(Collider2D trigger)
        {
            print("CollisionEnter");
            if (trigger.gameObject.tag == CursorManager.standardCursorTag) Stay = true;
        }
        public void OnTriggerExit2D(Collider2D trigger)
        {
            print("CollisionExit");
            if (trigger.gameObject.tag == CursorManager.standardCursorTag) Stay = false;
        }
        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        private void FixedUpdate()
        {
            if (Stay)
            {
                if (cursorStateMachine.GetState() == ECursorState.Click)
                {
                    Distance = new Vector2(transform.position.x, transform.position.y) - MousePos;
                }
                if (cursorStateMachine.GetState() == ECursorState.Hold)
                {
                    transform.position = MousePos + Distance;
                    rb2D.gravityScale = 0;
                    rb2D.velocity = Vector2.zero;
                }
                if (cursorStateMachine.GetState() == ECursorState.Normal)
                {
                    rb2D.gravityScale = 1;
                }
            }
        }
    }
}
