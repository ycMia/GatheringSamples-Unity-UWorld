using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Interactable.File;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable.File
{
    public class Drag_ : MonoBehaviour
    {
        //public CursorManager cursorMM;
        bool fl = true;
        public IStateMachine<ECursorState> cursorStateMachine = CursorManager.Instance_StateMachine;
        public Rigidbody2D rb2D;
        Vector2 MousePos;
        Vector2 Distance;
        public bool Stay = false;
        private Vector2 MousePost;
        Vector2 a;
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
            //rb2D = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        private void FixedUpdate()
        {
            if (Stay)
            {
                if (cursorStateMachine.GetState() == ECursorState.Hold)
                {
                    if (fl)
                    {
                        Distance = new Vector2(rb2D.transform.position.x, rb2D.transform.position.y) - MousePos;
                        fl = false;
                    }
                    rb2D.transform.position = Distance+MousePos;
                    rb2D.gravityScale = 0;
                    rb2D.velocity = Vector2.zero;
                }
                else if (cursorStateMachine.GetState() == ECursorState.Normal)
                {
                    rb2D.gravityScale = 0;
                    fl = true;
                }
            }
        }
    }
}
