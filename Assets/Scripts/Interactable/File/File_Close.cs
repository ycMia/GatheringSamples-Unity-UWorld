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
    public class File_Close : MonoBehaviour , ICursorSingleClickable
    {
        public WindowFore targWindow;

        private void Start()
        {
            if(gameObject.GetComponent<SingleClickListener>() == null)
                gameObject.AddComponent<SingleClickListener>();
        }

        public void OnSingleClick()
        {
            foreach (GameObject go in targWindow.comb.objects)
                go.SetActive(false);
        }
    }
} 