using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Logics.Message;
using MyScripts.CursorControl;
using MyScripts.CursorControl.State;
using MyScripts.Logics.Time;
using MS.F;
using MS.T;

public class GameManager : SimpleMessageSender<string>
{
    //public SimpleValueProjector receiver;
    // Start is called before the first frame update
    void Start()
    {
        //SendMessageToCortex("This message have been seized");
        //foreach(SimpleMessage sme in SimpleMessageCortexDefault.Instance.Data())
        //{
        //    print(sme.info);
        //}
        // //That was succeeded ^

        //string rstr = "R";
        //for(int i =0;i<100;i++)
        //{
        //    TimerHub.Instance.AddClockRent(rstr);
        //    rstr += "R";
        //}
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SimpleMessage<ColorfulFile_uniqueData> data in SimpleMessageCortexDefault<ColorfulFile_uniqueData>.Instance.MsgData())
        {
            print(data.info.result);
        }
        //print("NowStatus="+CursorManager.Instance_StateMachine.GetState().ToString());
        //receiver.GetMsgReceiver().Add(new SimpleMessage<string>("ClickJ:"+TimerHub.Instance.GetAClock("CursorClickJudge").ToString("F5")));
        //receiver.GetMsgReceiver().Add(new SimpleMessage<string>("DoubleJ:"+TimerHub.Instance.GetAClock("CursorDoubleClickJudge").ToString("F5")));
        //for (int i = 0; i < 100; i++)
        //{
        //    string rstr = "R";
        //    for (int j = 0; j < i; j++)
        //        rstr += "R";
        //    receiver.GetMsgReceiver().Add(new SimpleMessage<string>(TimerHub.Instance.TimeDictionary[rstr].ToString("F5")));
        //}
    }
}
