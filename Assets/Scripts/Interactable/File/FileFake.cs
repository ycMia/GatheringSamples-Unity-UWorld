using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;
using MyScripts.Interactable.File;
using MyScripts.Logics.Tools;
using MyScripts.Interactable.WindowComposite;

namespace MyScripts.Interactable.File
{
    public class FileFake : FileOn, ICursorDoubleClickable
    {
        public WindowFore ClosedWindow1;
        public WindowFore ClosedWindow2;
        public WindowFore ClosedWindow3;
        public WindowFore ClosedWindow4;
        public WindowFore ForeverHome;
        public GameObject reference1;
        public GameObject reference2;
        public GameObject reference3;
        public GameObject reference4;
        public GameObject reference5;
        public GameObject referencethis;
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
                foreach (GameObject obj in ClosedWindow1.comb.objects)
                {
                    obj.SetActive(false);

                }
                foreach (GameObject obj in ClosedWindow2.comb.objects)
                {
                    obj.SetActive(false);

                }
                foreach (GameObject obj in ClosedWindow3.comb.objects)
                {
                    obj.SetActive(false);

                }
                foreach (GameObject obj in ClosedWindow4.comb.objects)
                {
                    obj.SetActive(false);

                }
                foreach (GameObject obj in ForeverHome.comb.objects)
                {
                    obj.SetActive(true);

                }
                reference1.SetActive(true);
                reference2.SetActive(true);
                reference3.SetActive(true);
                reference4.SetActive(true);
                reference5.SetActive(true);
                srHighlight.color = Color.clear;
            }
        }
    }
}