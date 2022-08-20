using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyScripts.Logics.Message;
using MyScripts.Interactable;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.StateMachine;
using System;

namespace MyScripts.UI.Button
{
    public class Password : SimpleMessageSender<PasswordResult>
    {
        private string CPassword = "wycnb";
        public Text te;
        public void Click()
        {
            SendMessageToCortex(new PasswordResult(te.text, CPassword == te.text));
        }

    }
    public class PasswordResult{
        public PasswordResult(string key,bool flag)
        {
            this.key = key;
            this.flag = flag;
        }
        public string key;
        public bool flag;
    }
}
