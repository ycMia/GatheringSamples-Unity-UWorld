using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using MyScripts.Game.RecyclerGameLogics;
using MyScripts.Logics.Message;
using MyScripts.Player.Interact;

namespace MyScripts.Interactable.File
{
    public class FileRecyclable : MonoBehaviour, IPlayerEatable
    {
        public void OnBeingAten()
        {
            RecyclerData dat = new RecyclerData(gameObject);
            Recycler.Instance.GetMsgReceiver().Add(new SimpleMessage<RecyclerData>(dat));
            gameObject.SetActive(false);
        }
    }
}
