using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScripts.Logics.Message;

public class GameManager : SimpleMessageSender
{
    // Start is called before the first frame update
    void Start()
    {
        SendMessageToCortex("This message have been seized");
        foreach(SimpleMessage sme in SimpleMessageCortexDefault.Instance.Data())
        {
            print(sme.info);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
