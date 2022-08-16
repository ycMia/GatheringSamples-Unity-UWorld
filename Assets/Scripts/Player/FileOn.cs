using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Cursor;
//次脚本可能有bug
namespace File
{
    public class FileOn : MonoBehaviour, ICursorInteractable
    {
        bool flag = false;
        public SpriteRenderer wycnb;
        void Start()
        {
            wycnb = gameObject.GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            OnCursorClick();
        }
        public void OnCursorClick()
        {
            if (!flag/*&&一个bool类型判断点击的函数*/&&Input.GetMouseButton(0))
            {
                wycnb.color = new Color(0, 0, 1, 0.7f);
                flag = true;
            }
            else if (flag && Input.GetMouseButtonDown(1/*补上前面函数后改为0*/)/*!一个bool类型判断点击的函数*/)
            {
                wycnb.color = new Color(0, 0, 0, 0);
                flag = false;
            }
        }
    }
}
