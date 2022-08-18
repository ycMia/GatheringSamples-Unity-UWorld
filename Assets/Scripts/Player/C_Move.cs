using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using MyScripts.Logics.Time;

//以下为可移动的物体的类
namespace Class
{
    public class C_Move : MonoBehaviour, I_K_Move, I_M_Move
    {
        void Update()
        {
            if(Input.GetButtonDown("Jump"))
            f = true;
        }
        void FixedUpdate()
        {
            K_Movement();
        }
        public Rigidbody2D rb;
        public float speed;
        public float jumpforce;
        private bool f = false;
        public void K_Movement()
        {
            float horizontalmove;
            float facedirection;
            facedirection = Input.GetAxisRaw("Horizontal");
            horizontalmove = Input.GetAxis("Horizontal");
            if (horizontalmove != 0)
            {
                rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            }
            if (f)
            {
                TimerHub.Instance.AddClockRent("a");
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                TimerHub.Instance.SweepOutClock("a");
                f = false;
                TimerHub.Instance.SweepOutClock("a");
            }
        }
        public void M_Movement()
        {
            ;
        }

    }
}