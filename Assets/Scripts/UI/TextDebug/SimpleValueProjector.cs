using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MyScripts.Logics.Message;

public class SimpleValueProjector : SimpleMessageCortex_MonoBehaviour<string>
{
    public Text textComponent;
    /// <summary>
    /// This parament controls wether the text is being sweeped out each frame.
    /// </summary>
    public bool textClearFlag;

    // Start is called before the first frame update
    void Start()
    {
        if(textComponent == null)
        {
            gameObject.AddComponent<Text>();
            textComponent = GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = "";
        foreach (SimpleMessage<string> msg in MsgData())
        {
            textComponent.text += msg.info + "\n";
        }
        if(textClearFlag)
            MsgData().Clear();
    }
}
