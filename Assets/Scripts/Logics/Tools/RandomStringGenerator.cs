using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyScripts.Logics.Tools
{
    public class RandomStringGenerator : MonoBehaviour
    {
        private static RandomStringGenerator _Instance;
        public static RandomStringGenerator Instance
        {
            get
            {
                if (_Instance == null)
                {
                    GameObject obj = new GameObject("RandomHashStringGenerator_Obj");
                    _Instance = obj.AddComponent<RandomStringGenerator>();
                    DontDestroyOnLoad(obj);
                }
                return _Instance;
            }
        }

        private List<string> _delivieredHash = new List<string>();

        public string GetAString(int length)
        {
            string hashString = "";
            for (; length > 0; --length)
            {
                int iterR = Random.Range(0, 16);
                hashString += iterR.ToString("X6");
            }

            _delivieredHash.Add(hashString);
            return hashString;
        }
    }
}
