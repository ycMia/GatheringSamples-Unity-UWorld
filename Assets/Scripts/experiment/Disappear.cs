using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts.Disappear
{
    public class Disappear : MonoBehaviour
    {
        public GameObject go;
        void Start()
        {
            go.SetActive(false);
        }
    }
}
