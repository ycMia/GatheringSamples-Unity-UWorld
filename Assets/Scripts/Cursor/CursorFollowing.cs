using UnityEngine;
using System.Collections.Generic;
using MyScripts.Logics.Time;
using MyScripts.Logics.StatusMachine;

namespace MyScripts.Scripts.CursorControl
{
    class CursorFollowing : MonoBehaviour
    {
        [SerializeField] private GameObject cursorPrefab;
        private GameObject cursorGO;
        private Animator animator;
        private CursorClickStatusMachine statusMachine = new CursorClickStatusMachine();
        private void Awake()
        {
            Cursor.visible = false;
            cursorGO = Instantiate(cursorPrefab);
            cursorGO.name = "CursorGameObject";

            animator = cursorGO.GetComponent<Animator>();

            print(statusMachine.GetStatus());//
        }

        void Update()
        {
            cursorGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10f);

            animator.SetBool("Hold", Input.GetKey(KeyCode.Mouse0));

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
            }
        }
    }

    class CursorClickStatusMachine : IStatusMachine<CursorClickStatusMachine.ECursorStatus>
    {
        private List<CursorStatusNode> _statusList;
        private CursorStatusNode _currentNode = new CursorStatusNode(ECursorStatus.Normal);
        public CursorClickStatusMachine()
        {
            _statusList = new List<CursorStatusNode>()
            {
                _currentNode,
                new CursorStatusNode(ECursorStatus.Click),
                new CursorStatusNode(ECursorStatus.DoubleClick),
                new CursorStatusNode(ECursorStatus.Hold)
            };

            _statusList.ForEach(delegate (CursorStatusNode cs) { cs.MakeLinkInList(_statusList); });
        }

        public ECursorStatus GetStatus()
        {
            return _currentNode.data;
        }

        public void TrySwitchToStatus(ECursorStatus status)
        {
            foreach(CursorStatusNode node in _currentNode.reachable)
            {
                if (node.data == status)
                {
                    _currentNode = node;
                }
                return;
            }
        }

        //----------//
        public enum ECursorStatus
        {
            Normal,
            Click,
            DoubleClick,
            Hold,
        }

        public class CursorStatusNode
        {
            public CursorStatusNode(ECursorStatus data) => this.data = data;

            //Assemble contains reachable Status
            public List<CursorStatusNode> reachable = new();
            //What am I
            public ECursorStatus data;

            public void MakeLinkInList(List<CursorStatusNode> list)
            {
                if (data == ECursorStatus.Normal)
                    foreach (CursorStatusNode cs in list)
                        if (cs.data == ECursorStatus.Click || cs.data == ECursorStatus.Hold)
                            reachable.Add(cs);
                if (data == ECursorStatus.Click)
                    foreach (CursorStatusNode cs in list)
                        if (cs.data == ECursorStatus.DoubleClick || cs.data == ECursorStatus.Normal)
                            reachable.Add(cs);
                if (data == ECursorStatus.DoubleClick)
                    foreach (CursorStatusNode cs in list)
                        if (cs.data == ECursorStatus.Normal)
                            reachable.Add(cs);
                if (data == ECursorStatus.Hold)
                    foreach (CursorStatusNode cs in list)
                        if (cs.data == ECursorStatus.Normal)
                            reachable.Add(cs);
                //[Info] Damn, the machine is such a thing worth to pay. --ycMia
            }
        }
    }

}
