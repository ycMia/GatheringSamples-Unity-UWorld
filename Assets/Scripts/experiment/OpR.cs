using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;
using MyScripts.Interactable.File;
using MyScripts.Logics.Tools;

namespace MyScripts.Interactable.WindowComposite
{
    public class OpR : FileOn , IR_CursorDoubleClickable
    {
        public WindowFore targetWindow;

        private void Start()
        {
            if (GetComponent<R_DoubleClickListener>() == null)
                gameObject.AddComponent<R_DoubleClickListener>();
        }

        public void OnR_DoubleClick()
        {
            foreach(GameObject obj in targetWindow.comb.objects)
            {
                obj.SetActive(true);
            }
            srHighlight.color = Color.clear;
        }
    }
}