using UnityEngine;
using System.Collections.Generic;

namespace MyScripts.Scripts.CursorControl
{
    class CursorFollowing : MonoBehaviour
    {
        [SerializeField] private GameObject cursorPrefab;
        private GameObject cursorGO;

        private Animator animator;

        private void Awake()
        {
            Cursor.visible = false;
            cursorGO = Instantiate(cursorPrefab);
            cursorGO.name = "CursorGameObject";

            animator = cursorGO.GetComponent<Animator>();
        }

        void Update()
        {
            cursorGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10f);

            animator.SetBool("Hold", Input.GetKey(KeyCode.Mouse0));

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

            }
        }
    }
}
