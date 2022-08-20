using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using MyScripts.Logics.Message;
namespace MyScripts.Game.RecyclerGameLogics
{
    public class RecyclerData
    {
        public GameObject recycled;

        public RecyclerData(GameObject recycled)
        {
            this.recycled = recycled;
        }
    }

    public class Recycler : MonoBehaviour, ISimpleMessageReceiver<RecyclerData>
    {
        private static Recycler _Instance;
        public static Recycler Instance
        {
            get
            {
                if (_Instance == null)
                {
                    GameObject recyclerHolder = new GameObject("Recycler holder");
                    _Instance = recyclerHolder.AddComponent<Recycler>();
                    DontDestroyOnLoad(recyclerHolder);
                }
                return _Instance;
            }
        }
        
        public List<SimpleMessage<RecyclerData>> RecData = new();
        public List<RecyclerData> SavedData = new();

        public List<SimpleMessage<RecyclerData>> GetMsgReceiver()
        {
            return RecData;
        }
        public List<SimpleMessage<RecyclerData>> MsgData()
        {
            return GetMsgReceiver();
        }
        public void OnRecycleUpdate()
        {
            foreach(SimpleMessage<RecyclerData> dat in RecData)
            {
                RecData.Remove(dat);
                SavedData.Add(dat.info);
                Debug.Log("Added Recy");
            }
        }

        public void RevertRecycledObject(RecyclerData dat)
        {

        }

        private void Update()
        {
            if(RecData.Count != 0)
            {
                OnRecycleUpdate();
            }
        }
    }
}
