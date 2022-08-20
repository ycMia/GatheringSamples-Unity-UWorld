using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using MyScripts.Player.Interact;
using MyScripts.Logics.Time;

namespace MyScripts.Player
{
    public class PlayerEatJudge : MonoBehaviour
    {
        private IPlayerEatable target;
        public bool contacted = false;
        private SpriteRenderer tip;

        private void Start()
        {
            tip ??= GetComponent<SpriteRenderer>();
            if(tip!=null)
                tip.color = new Color(tip.color.r, tip.color.g, tip.color.b, 0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            target = collision.gameObject.GetComponent<IPlayerEatable>();
            if (target != null)
            {
                contacted = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(target!=null && target == collision.gameObject.GetComponent<IPlayerEatable>())
            {
                contacted = false;
            }
        }

        public float timeSet_tip = 1f;

        public void Update()
        {
            if(TimerHub.Instance.GetAClock("PlayerEatJudge_tipSR") > timeSet_tip)
            {
                tip.color = new Color(tip.color.r, tip.color.g, tip.color.b, 0f);
                TimerHub.Instance.SweepOutClock("PlayerEatJudge_tipSR");
            }

            if(Input.GetKeyDown(KeyCode.J) && contacted)
            {
                if(TimerHub.Instance.GetAClock("PlayerEatJudge_tipSR") > 0f)
                {
                    TimerHub.Instance.ResetAClock("PlayerEatJudge_tipSR");
                }
                tip.color = new Color(tip.color.r, tip.color.g, tip.color.b, 1f);
                target.OnBeingAten();
                contacted = false;
                target = null;
            }
        }
    }
}

