using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Cursor;
//�νű�������bug
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
            if (!flag/*&&һ��bool�����жϵ���ĺ���*/&&Input.GetMouseButton(0))
            {
                wycnb.color = new Color(0, 0, 1, 0.7f);
                flag = true;
            }
            else if (flag && Input.GetMouseButtonDown(1/*����ǰ�溯�����Ϊ0*/)/*!һ��bool�����жϵ���ĺ���*/)
            {
                wycnb.color = new Color(0, 0, 0, 0);
                flag = false;
            }
        }
    }
}
