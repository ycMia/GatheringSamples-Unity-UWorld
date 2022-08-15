using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Cursor
{
    class CursorFollowing : MonoBehaviour
    {
        private GameObject mousePrefab;
        public GameObject mouseGameObject;
        private void Awake()
        {
            mouseGameObject = Instantiate(mousePrefab);
        }

        private void SetParaments()
        {

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {

            }
        }
    }
}
