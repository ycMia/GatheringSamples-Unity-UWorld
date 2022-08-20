using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;
using MyScripts.Interactable.File;
using MyScripts.Logics.Tools;

namespace MyScripts.Interactable.Window
{
    public class Op : FileOn , ICursorDoubleClickable
    {
        public Window targetWindow;

        private void Start()
        {
            if (GetComponent<DoubleClickListener>() == null)
                gameObject.AddComponent<DoubleClickListener>();
        }

        public void OnDoubleClick()
        {
            foreach(GameObject obj in targetWindow.comb.objects)
            {
                obj.SetActive(true);
            }
        }
    }
}