using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;
using MyScripts.Logics.Tools;
using MyScripts.Interactable.WindowComposite;

namespace MyScripts.Interactable.File
{
    public class OpFile : FileOn, ICursorDoubleClickable
    {
        public WindowFore targetWindow;
        public GameObject referencethis;
        public GameObject targetref;
        private void Start()
        {
            if(referencethis.activeSelf)
            if (GetComponent<DoubleClickListener>() == null)
                gameObject.AddComponent<DoubleClickListener>();
        }

        public void OnDoubleClick()
        {
            if (referencethis.activeSelf)
            {
                foreach (GameObject obj in targetWindow.comb.objects)
                {
                    obj.SetActive(true);
                }
                targetref.SetActive(true);
                srHighlight.color = Color.clear;
                referencethis.SetActive(false);
            }
        }
    }
}