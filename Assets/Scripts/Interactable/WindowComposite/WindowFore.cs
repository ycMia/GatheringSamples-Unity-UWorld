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
            AddObjectsInWindowRecursively(gameObject);
        }
        private void AddObjectsInWindowRecursively(GameObject obj)
        {
            if (obj.transform.childCount == 0) return;
            for (int i = 0; i < transform.childCount; i++)
            {
                AddObjectsInWindowRecursively(obj);
                comb.objects.Add(transform.GetChild(i).gameObject);
                Debug.Log("Add :" + transform.GetChild(i).gameObject.name);
            }
        }
    }
}
