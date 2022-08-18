using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyScripts.Experiment
{
    public class Drag : MonoBehaviour
    {
        public Rigidbody2D rb2D;
        Vector2 MousePos;
        Vector2 Distance;
        private Rigidbody2D Usedrb;
        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            Usedrb = rb2D;
        }
        private void Update()
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        private void OnMouseDown()
        {
            Distance = new Vector2(transform.position.x, transform.position.y) - MousePos;
        }
        private void OnMouseDrag()
        {
            transform.position = MousePos + Distance;
            rb2D.gravityScale = 0;
            rb2D.velocity = Vector2.zero;
        }
        private void OnMouseUpAsButton()
        {
            rb2D = Usedrb;
        }
    }
}
