using UnityEngine;
using System.Collections.Generic;
using MyScripts.Logics.Time;
using MyScripts.Logics.StateMachine;

namespace MyScripts.Scripts.CursorControl
{
    class CursorFollowing : MonoBehaviour
    {
        [SerializeField] private GameObject cursorPrefab;
        private GameObject cursorGO;
        private Animator animator;
        private CursorClickStateMachine stateMachine = new CursorClickStateMachine();
        private void Awake()
        {
            Cursor.visible = false;
            cursorGO = Instantiate(cursorPrefab);
            cursorGO.name = "CursorGameObject";

            animator = cursorGO.GetComponent<Animator>();

            print(stateMachine.GetState());//
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

    class CursorClickStateMachine : IStateMachine<CursorClickStateMachine.ECursorState>
    {
        private List<CursorStateNode> _stateList;
        private CursorStateNode _currentNode = new CursorStateNode(ECursorState.Normal);
        public CursorClickStateMachine()
        {
            _stateList = new List<CursorStateNode>()
            {
                _currentNode,
                new CursorStateNode(ECursorState.Click),
                new CursorStateNode(ECursorState.DoubleClick),
                new CursorStateNode(ECursorState.Hold)
            };

            _stateList.ForEach(delegate (CursorStateNode cs) { cs.MakeLinkInList(_stateList); });
        }

        public ECursorState GetState()
        {
            return _currentNode.data;
        }

        public void TrySwitchToState(ECursorState state)
        {
            foreach(CursorStateNode node in _currentNode.reachable)
            {
                if (node.data == state)
                {
                    _currentNode = node;
                }
                return;
            }
        }

        //----------//
        public enum ECursorState
        {
            Normal,
            Click,
            DoubleClick,
            Hold,
        }

        public class CursorStateNode
        {
            public CursorStateNode(ECursorState data) => this.data = data;

            //Assemble contains reachable State
            public List<CursorStateNode> reachable = new();
            //What am I
            public ECursorState data;

            public void MakeLinkInList(List<CursorStateNode> list)
            {
                if (data == ECursorState.Normal)
                    foreach (CursorStateNode cs in list)
                        if (cs.data == ECursorState.Click || cs.data == ECursorState.Hold)
                            reachable.Add(cs);
                if (data == ECursorState.Click)
                    foreach (CursorStateNode cs in list)
                        if (cs.data == ECursorState.DoubleClick || cs.data == ECursorState.Normal)
                            reachable.Add(cs);
                if (data == ECursorState.DoubleClick)
                    foreach (CursorStateNode cs in list)
                        if (cs.data == ECursorState.Normal)
                            reachable.Add(cs);
                if (data == ECursorState.Hold)
                    foreach (CursorStateNode cs in list)
                        if (cs.data == ECursorState.Normal)
                            reachable.Add(cs);
                //[Info] Damn, the machine is such a thing worth to pay. --ycMia
            }
        }
    }

}
