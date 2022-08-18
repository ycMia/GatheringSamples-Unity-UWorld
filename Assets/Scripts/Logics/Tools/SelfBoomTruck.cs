using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Logics.Tools;

namespace MyScripts.Logics.Time
{
    public class SelfBoomTruck : MonoBehaviour
    {
        private string _truckHash;
        public string HashInfo
        {
            get => "BoomTruck_" + _truckHash;
        }
        // Mama I don't want to die!
        public float? countdown = null;

        public void ActivateCountdown(float countdown)
        {
            this.countdown = countdown;
            _truckHash = RandomStringGenerator.Instance.GetAString(8);
            TimerHub.Instance.AddClockRent("BoomTruck_"+_truckHash);
        }

        // Update is called once per frame
        void Update()
        {
            if(countdown != null)
            {
                if (TimerHub.Instance.GetAClock("BoomTruck_" + _truckHash) >= countdown)
                {
                    TimerHub.Instance.SweepOutClock("BoomTruck_" + _truckHash);
                    gameObject.SetActive(false);
                    //This is a hard-core slaughter method
                    Destroy(gameObject);
                }
            }
        }
    }
}
