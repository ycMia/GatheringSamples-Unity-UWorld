using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts;

namespace MyScripts.Interactable.File
{
    public class File_ : FileOn
    {
        public GameObject go;
        private void Update()
        {
            OnCursorClick();
            Open();
        }
        public void Open()
        {
            if ( Input.GetMouseButtonDown(0))
            {
                go.SetActive(true);
                print("a");
            }
        }
    }
}
