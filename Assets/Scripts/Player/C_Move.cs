using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Interface;

namespace Class
{
    //以下为可移动的物体的类
    public class C_Move : MonoBehaviour, I_K_Move, I_M_Move
    {
        void FixedUpdate()
        {
            K_Movement();
        }
        public Rigidbody2D rb;
        public float speed;
        public float jumpforce;
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
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            }
        }
        public void M_Movement()
        {
            ;
        }
    }
}