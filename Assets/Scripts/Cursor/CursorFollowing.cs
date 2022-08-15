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
                //This commit is set for testing the changings in code... not actually in used by us
            }
        }
    }
}
