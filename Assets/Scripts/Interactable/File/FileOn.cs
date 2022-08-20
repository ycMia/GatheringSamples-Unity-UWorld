using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Interactable.File
{
    public class FileOn : MonoBehaviour , ICursorSingleClickable , ICursorHoverable
    {
        private bool _flag = false;
        public SpriteRenderer srHighlight;
        public bool stayState = false;
        
        void Awake()
        {
            srHighlight.color = new Color(0, 0, 0, 0);
        }

        public void OnCursorHover()
        {
            srHighlight.color = _flag ? srHighlight.color : new Color(0, 1, 1, 0.1f);
        }
        public void OnCursorNotHover()
        {
            srHighlight.color = _flag ? srHighlight.color : new Color(0, 1, 1, 0f);
        }

        public void OnSingleClick()
        {
            if (!_flag)
            {
                srHighlight.color = new Color(0, 0, 1, 0.7f);
                _flag = true;
            }
            else
            {
                srHighlight.color = new Color(0, 0, 0, 0);
                _flag = false;
            }
        }
    }
}
