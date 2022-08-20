using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Logics.Message;

namespace MyScripts.UI.Button
{
    public class WI_B : SimpleMessageSender<WIFI>
    {
        public WIFI me;
        public GameObject TP;
        public GameObject re;
        public void Cli()
        {
            if (re.activeInHierarchy)
            {
                me = new WIFI(TP, true);
                SendMessageToCortex(me);
            }
            else
            {
                me = new WIFI(TP, false);
                SendMessageToCortex(me);
            }
        }
    }
    public class WIFI
    {
        public GameObject gameobject;
        public bool Can;
        public WIFI(GameObject x,bool y)
        {
            gameobject = x;
            Can = y;
        }
    }
}
