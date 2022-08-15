using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;


using MyScripts.Logics.Message;

//welcome to Kiki's magic time shop!
//...maybe they're not Kiki's stuff, but Kikiki's. Btw, make yourselves at home <3. --ycMia
namespace MyScripts.Logics.Time
{
    public class TimerHub: SimpleMessageCortex_MonoBehaviour<string>
    {
        private static TimerHub _Instance;
        public static TimerHub Instance
        {
            get
            {
                if(_Instance == null)
                {
                    GameObject obj = new();
                    obj.AddComponent<TimerHub>();
                    _Instance = obj.GetComponent<TimerHub>();
                    DontDestroyOnLoad(obj);
                }
                return _Instance;
            }
        }

        private Dictionary<string, float> _timeDictionary = new();
        public Dictionary<string, float> TimeDictionary { get => _timeDictionary; }

        public void AddClockRent(string clockName)
        {
            if(_timeDictionary.ContainsKey(clockName))
            {
                clockName += "R";
                //[Question] Why R? Maybe that's the only alpha that may not conflict lol.
            }
            _timeDictionary.Add(clockName, 0f);
        }

        public void SweepOutClock(string clockName)
        {
            _timeDictionary.Remove(clockName);
        }

        public bool RefreshClock(string clockName)
        {
            if(_timeDictionary.ContainsKey(clockName))
            {
                float val = _timeDictionary[clockName];
                _timeDictionary.Remove(clockName);
                _timeDictionary.Add(clockName, val+UnityEngine.Time.deltaTime);
                return true;
            }
            return false;
        }

        public float GetAClock(string clockName)
        {
            //You might want to check this logic twice. I'm serious.
            return _timeDictionary[clockName] + UnityEngine.Time.deltaTime;
        }

        private void Update()
        {
            //can't use foreach, this method contains Remove() method.
            for(int i = 0;i<_timeDictionary.Count;i++)
            {
                RefreshClock(_timeDictionary.Keys.ToArray()[i]);
            }
        }
    }
}
