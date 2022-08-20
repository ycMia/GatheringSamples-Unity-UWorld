using System.Collections.Generic;
using UnityEngine;

namespace MyScripts.Logics.Tools
{
    public class CombinedGameObjects
    {
        public List<GameObject> objects = new();
        public List<GameObject> AddObjects(GameObject go)
        {
            objects.Add(go);
            return objects;
        }
    }
}
