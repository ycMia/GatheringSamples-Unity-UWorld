using MyScripts.Logics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyScripts.Interactable.WindowComposite
{
    public class WindowFore : MonoBehaviour
    {
        public CombinedGameObjects comb = new CombinedGameObjects();

        public void Start()
        {
            AddObjectsInWindowRecursively(comb, gameObject);
        }
        private static void AddObjectsInWindowRecursively(CombinedGameObjects comb, GameObject obj)
        {
            if (obj.transform.childCount == 0) return;
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                AddObjectsInWindowRecursively(comb, obj.transform.GetChild(i).gameObject);
                comb.objects.Add(obj.transform.GetChild(i).gameObject);
                Debug.Log("Add :" + obj.transform.GetChild(i).gameObject.name);
            }
        }

    }
}
