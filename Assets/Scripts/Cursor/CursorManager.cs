using UnityEngine;
using System.Collections.Generic;
using MyScripts.Logics.Time;
using MyScripts.Logics.StateMachine;

using MyScripts.CursorControl.State;
using System;

namespace MyScripts.CursorControl
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private GameObject cursorPrefab;
        public GameObject cursorGO;
        private Animator animator;
        //[Info]: Modified 0818, the stateMachine is unique and should be set in static mode.
        private static CursorClickStateMachine _stateMachine = new CursorClickStateMachine();
        public static IStateMachine<ECursorState> Instance_StateMachine
        {
            get => _stateMachine;
        }

        //This is a stupid method, I'd rather not to use such counts.
        //public int askForClickCount = 0;
        //public int askForDoubleClickCount = 0;

        [SerializeField, Range(0.04f, 1f)] private float _gapOfCursorClick;

        private void Awake()
        {
            //You Don't want to see that WindowsWhiteHead do you (lol)
            UnityEngine.Cursor.visible = false;
            cursorGO = Instantiate(cursorPrefab);
            cursorGO.name = "CursorGameObject";
            cursorGO.tag = standardCursorTag;
            animator = cursorGO.GetComponent<Animator>();
        }

        void Update()
        {
            //follow
            cursorGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10f);
            //animator.SetBool("Hold", Input.GetKey(KeyCode.Mouse0));

            float timeGap; //will be given a value in each case tag.
            switch (_stateMachine.GetState())
            {
                case ECursorState.Normal:
                    timeGap = TimerHub.Instance.GetAClock("CursorClickJudge");
                    if (Input.GetKeyDown(KeyCode.Mouse0) && timeGap<0f)
                    {
                        TimerHub.Instance.AddClockRent("CursorClickJudge");
                    }
                    else if(Input.GetKeyUp(KeyCode.Mouse0) && timeGap>0f && timeGap < _gapOfCursorClick)
                    {
                        print("Clicked");
                        TimerHub.Instance.SweepOutClock("CursorClickJudge");
                        //Yes, the time after you clicked, there's the time for the DoubleClick Judge get started.
                        TimerHub.Instance.AddClockRent("CursorDoubleClickJudge");
                        // exp: same as to "askForClickCount++;"
                        _stateMachine.TrySwitchToState(ECursorState.Click_CommandAwait);
                    }
                    else if(timeGap > _gapOfCursorClick)
                    {
                        if(Input.GetKey(KeyCode.Mouse0) == false)
                        {
                            _stateMachine.TrySwitchToState(ECursorState.Normal);
                            TimerHub.Instance.SweepOutClock("CursorClickJudge");
                        }
                        else
                        {
                            _stateMachine.TrySwitchToState(ECursorState.Hold);
                            TimerHub.Instance.SweepOutClock("CursorClickJudge");
                        }
                    }
                    break;
                case ECursorState.Click:
                case ECursorState.Click_CommandAwait:
                    timeGap = TimerHub.Instance.GetAClock("CursorDoubleClickJudge");
                    if (timeGap > Time.maximumDeltaTime) _stateMachine.TrySwitchToState(ECursorState.Click);
                    if (Input.GetKeyUp(KeyCode.Mouse0) && timeGap > 0 && timeGap <= _gapOfCursorClick)
                    {
                        print("DoubleClicked");
                        TimerHub.Instance.SweepOutClock("CursorDoubleClickJudge");
                        _stateMachine.TrySwitchToState(ECursorState.DoubleClick_CommandAwait);
                    }
                    else if(timeGap > _gapOfCursorClick)
                    {
                        _stateMachine.TrySwitchToState(ECursorState.Normal);
                        TimerHub.Instance.SweepOutClock("CursorDoubleClickJudge");
                    }
                    break;
                case ECursorState.DoubleClick:
                case ECursorState.DoubleClick_CommandAwait:
                    timeGap = TimerHub.Instance.GetAClock("CursorDoubleClickJudge");
                    //Thus, I don't know what to do but obey the details that Click have done.
                    //This might be a useless code line.
                    if(timeGap>Time.maximumDeltaTime) _stateMachine.TrySwitchToState(ECursorState.DoubleClick);
                    _stateMachine.TrySwitchToState(ECursorState.DoubleClick);
                    break;
                case ECursorState.Hold:
                    if (Input.GetKey(KeyCode.Mouse0) == false)
                        _stateMachine.TrySwitchToState(ECursorState.Normal);
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
                _stateList = new List<CursorStateNode>(){
                    _currentNode,
                    new CursorStateNode(ECursorState.Click),
                    new CursorStateNode(ECursorState.Click_CommandAwait),
                    new CursorStateNode(ECursorState.DoubleClick),
                    new CursorStateNode(ECursorState.DoubleClick_CommandAwait),
                    new CursorStateNode(ECursorState.Hold)
                };

                _stateList.ForEach(delegate (CursorStateNode cs) { cs.MakeLinkInList(_stateList); });
            }

            public ECursorState GetState()
            {
                return _currentNode.data;
            }

            //Do not use this function too often otherwise some Bug might 発動する.
            public void TrySwitchToState(string state)
            {
                TrySwitchToState((ECursorState)Enum.Parse(typeof(ECursorState), state));
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
                Debug.LogError("This State Is Not Allowed: "+_currentNode.data.ToString() + " to " + state.ToString());
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
                            if (/*cs.data == ECursorState.Click ||*/ cs.data == ECursorState.Click_CommandAwait || cs.data == ECursorState.Hold)
                                reachable.Add(cs);
                    if (data == ECursorState.Click || data == ECursorState.Click_CommandAwait)
                        foreach (CursorStateNode cs in list)
                            if (cs.data == ECursorState.Click || cs.data == ECursorState.DoubleClick_CommandAwait || cs.data == ECursorState.Normal)
                                reachable.Add(cs);
                    if (data == ECursorState.DoubleClick || data == ECursorState.DoubleClick_CommandAwait)
                        foreach (CursorStateNode cs in list)
                            if (cs.data == ECursorState.DoubleClick || cs.data == ECursorState.Normal)
                                reachable.Add(cs);
                    if (data == ECursorState.Hold)
                        foreach (CursorStateNode cs in list)
                            if (cs.data == ECursorState.Normal)
                                reachable.Add(cs);
                    //[Info] Damn, the machine is such a thing worth to pay. --ycMia
                    //DoubleClick(_CommandAwait)   ->  Normal <->  Click(_CommandAwait)  ->  DoubleClick(_CommandAwait)
                    //                                   ^          
                    //                                   |
                    //                                   V
                    //                                  Hold
                }
            }
        }
        //[Info | Warning] look up your project setting
        public static string standardCursorTag = "Cursor";
    }
}

namespace MyScripts.CursorControl.State
{
    public enum ECursorState
    {
        Normal,
        Click,
        Click_CommandAwait,
        DoubleClick,
        DoubleClick_CommandAwait,
        Hold,
    }
}
