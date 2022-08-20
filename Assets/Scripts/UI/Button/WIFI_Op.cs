using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyScripts.UI.Button
{
    public class WIFI_Op : MonoBehaviour
    {
        public GameObject gob;
        private bool fa = false;
        public void W_O()
        {
            if (!fa)
            {
                gob.SetActive(true);
                fa = true;
            }
            else if (fa)
            {
                gob.SetActive(false);
                fa = false;
            }
        }
    }
}
