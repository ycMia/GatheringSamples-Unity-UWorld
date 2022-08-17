using UnityEngine;
using System.Collections.Generic;
using MyScripts.Logics.Time;
using MyScripts.Logics.StateMachine;

using MyScripts.CursorControl.State;
namespace MyScripts.CursorControl
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private GameObject cursorPrefab;
        public GameObject cursorGO;
        private Animator animator;
        private CursorClickStateMachine _stateMachine = new CursorClickStateMachine();
        internal IStateMachine<ECursorState> stateMachine { get => _stateMachine; }

        //This is a stupid method, I'd rather not to use such counts.
        public int askForClickCount = 0;
        public int askForDBClickCount = 0;

        [SerializeField, Range(0.04f, 1f)] private float _gapOfCursorClick;

        private void Awake()
        {
            //You Don't want to see that WindowsWhiteHead do you (lol)
            Cursor.visible = false;
            cursorGO = Instantiate(cursorPrefab);
            cursorGO.name = "CursorGameObject";

            animator = cursorGO.GetComponent<Animator>();
        }

        void Update()
        {
            //follow
            cursorGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10f);
            //animator.SetBool("Hold", Input.GetKey(KeyCode.Mouse0));

            float timeGap; //will be given a value in each case tag.
            switch (stateMachine.GetState())
            {
                case ECursorState.Normal:
                    timeGap = TimerHub.Instance.GetAClock("CursorClickJudge");
                    if (Input.GetKeyDown(KeyCode.Mouse0) && timeGap<0f)
                    {
                        TimerHub.Instance.AddClockRent("CursorClickJudge");
                    }
                    else if(Input.GetKeyUp(KeyCode.Mouse0) && timeGap>0f && timeGap < _gapOfCursorClick)
                    {
                        stateMachine.TrySwitchToState(ECursorState.Click);
                        print("Clicked");
                        TimerHub.Instance.SweepOutClock("CursorClickJudge");
                        //Yes, the time after you clicked, there's the time for the DoubleClick Judge get started.
                        TimerHub.Instance.AddClockRent("CursorDoubleClickJudge");

                        //Maybe you should Invoke something clickable, and put the code here.
                        //Then the ability to controll the stateMachine is up to them.
                        askForClickCount++;
                    }
                    else if(timeGap > _gapOfCursorClick)
                    {
                        stateMachine.TrySwitchToState(ECursorState.Normal);
                        TimerHub.Instance.SweepOutClock("CursorClickJudge");
                    }
                    break;
                case ECursorState.Click:
                    timeGap = TimerHub.Instance.GetAClock("CursorDoubleClickJudge");
                    //if (Input.GetKeyDown(KeyCode.Mouse0) && timeGap < 0f)
                    //{
                    //}
                    //else if (Input.GetKeyUp(KeyCode.Mouse0) && timeGap > 0 && timeGap < _gapOfCursorClick)
                    if (Input.GetKeyUp(KeyCode.Mouse0) && timeGap > 0 && timeGap <= _gapOfCursorClick)
                    {
                        stateMachine.TrySwitchToState(ECursorState.DoubleClick);
                        print("DoubleClicked");
                        TimerHub.Instance.SweepOutClock("CursorDoubleClickJudge");
                    }
                    else if(timeGap > _gapOfCursorClick)
                    {
                        stateMachine.TrySwitchToState(ECursorState.Normal);
                        TimerHub.Instance.SweepOutClock("CursorDoubleClickJudge");
                        askForClickCount = 0;
                    }
                    break;
                case ECursorState.DoubleClick:
                    //[TODO]:Need to restore State-Normal if there's no clickable stuff existed. -ycMia
                    stateMachine.TrySwitchToState(ECursorState.Normal);
                    break;
            }
            return;//Update
        }
        public class CursorClickStateMachine : IStateMachine<ECursorState>
        {
            public ECursorState Current { get => _currentNode.data; }
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

            public void TrySwitchToState(string state)
            {
                switch (state)
                {
                    case "Click": TrySwitchToState(ECursorState.Click); return;
                    case "DoubleClick": TrySwitchToState(ECursorState.DoubleClick); return;
                    case "Hold": TrySwitchToState(ECursorState.Hold); return;
                    case "Normal": TrySwitchToState(ECursorState.Normal); return;
                    default: break;
                }
            }

            public void TrySwitchToState(ECursorState state)
            {
                if (state == _currentNode.data) return;
                foreach (CursorStateNode node in _currentNode.reachable)
                {
                    if (node.data == state)
                    {
                        _currentNode = node;
                        return;
                    }
                }
                Debug.LogError("This State Is Not Allowed");
            }

            //----------//

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
                    //DoubleClick   ->  Normal <->  Click  ->  DoubleClick
                    //                     ^
                    //                     |
                    //                     V
                    //                    Hold
                }
            }
        }
    }
}

namespace MyScripts.CursorControl.State
{
    public enum ECursorState
    {
        Normal,
        Click,
        DoubleClick,
        Hold,
    }
}
